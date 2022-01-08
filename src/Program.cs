using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using Volvo;

const int MapBounds = 32768;

T GetInput<T>(string msg, T dflt)
{
    Console.ForegroundColor = ConsoleColor.White;
    Console.Write($"{msg} [Default: {dflt?.ToString()}]: ");
    Console.ForegroundColor = ConsoleColor.Yellow;
    try
    {
        return (T) TypeDescriptor.GetConverter(typeof(T)).ConvertFromString(Console.ReadLine());
    }
    catch
    {
        return GetInput<T>(msg, dflt);
    }
}

int DisplacementSize = GetInput<int>("Displacement Size", 1024);
int SizeSqr = GetInput<int>($"Size Squared [Min: 1 | Max: {(MapBounds / DisplacementSize) - 1}] ", 5);
float Amplitude = GetInput<float>("Amplitude", 512.0f);
float Scale = GetInput<float>("Scale", 2.0f);
int Octaves = GetInput<int>("Octaves", 6);
int Seed = GetInput<int>("Seed", 0);
const float Persistance = 0.8f;
const float Lacunarity = 2.0f;
Console.WriteLine();

Random rnd = new Random(Seed);
float SeedX = rnd.Next(-100000, 100000);
float SeedY = rnd.Next(-100000, 100000);

int StartTime = DateTime.Now.Millisecond;

Console.ForegroundColor = ConsoleColor.Yellow;

VMFBuilder Builder = new VMFBuilder();
Builder.WorldCreated += CreateObjects;

void CreateObjects()
{
    float TotalSize = SizeSqr * DisplacementSize;
    float StartX = (DisplacementSize * 0.5f) - (TotalSize * 0.5f);
    float StartY = (DisplacementSize * 0.5f) - (TotalSize * 0.5f);

    FastNoiseLite Noise = new FastNoiseLite();
    Noise.SetNoiseType(FastNoiseLite.NoiseType.Perlin);

    Console.WriteLine("Generating terrain...");
    for (int x1 = 0; x1 < SizeSqr; x1++)
    {
        for (int y1 = 0; y1 < SizeSqr; y1++)
        {
            Vector3 CubePos = new Vector3(StartX + (y1 * DisplacementSize), StartY + (x1 * DisplacementSize), 0);

            float[,] Distances = new float[9, 9];
            for (int x = 0; x < 9; x++)
            {
                for (int y = 0; y < 9; y++)
                {
                    float Amp = 1;
                    float Freq = 1;
                    float Height = 0;

                    for (int o = 0; o < Octaves; o++)
                    {
                        float SampleX = SeedX + (x - (x1 * 8)) / Scale * Freq;
                        float SampleY = SeedY + (y - (y1 * 8)) / Scale * Freq;

                        Height += Noise.GetNoise(SampleX, SampleY) * Amp;
                        Amp *= Persistance;
                        Freq *= Lacunarity;
                    }

                    Distances[x, y] = MathF.Round(Height * Amplitude, 3);
                }
            }

            Displacement displ = new Displacement();
            displ.Distances = Distances;
            if (CubePos.x < 0 && CubePos.y <= 0) // Bottom Left Quadrant
            {
                displ.Order = DisplacementOrder.BottomLeft;
            }
            else if (CubePos.x >= 0 && CubePos.y >= 0) // Top Right Quadrant
            {
                displ.Order = DisplacementOrder.TopRight;
            }
            else if (CubePos.x < 0 && CubePos.y >= 0) // Top Left Quadrant
            {
                displ.Order = DisplacementOrder.TopLeft;
            }
            else if (CubePos.x >= 0 && CubePos.y < 0) // Bottom Right Quadrant
            {
                displ.Order = DisplacementOrder.BottomRight;
            }

            Solid Cube = new Solid();
            Cube.Position = CubePos;
            Cube.Texture = "nature/grassfloor002a";
            Cube.Scale = new Vector3(DisplacementSize, DisplacementSize, 64);
            Cube.MakeDisplacement(displ);

            Builder.AddObject(Cube);
        }
    }
}

string data = Builder.Create();

Console.WriteLine("Writing to file...");
File.WriteAllText("output.vmf", data);

Console.ForegroundColor = ConsoleColor.Green;
Console.WriteLine($"Success! Took {(DateTime.Now.Millisecond - StartTime) / 1000.0f} seconds");
Console.ForegroundColor = ConsoleColor.White;
Console.Write("Press any key to exit...");

Console.ReadKey();
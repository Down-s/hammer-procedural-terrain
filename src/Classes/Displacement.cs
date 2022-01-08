using System.Text;

namespace Volvo;
public enum DisplacementOrder
{
    BottomLeft = 0,
    TopLeft,
    TopRight,
    BottomRight,
}

public class Displacement : VMFObject
{
    public float[,] Distances = new float[9, 9];
    public DisplacementOrder Order = 0;

    public override void Create(VMFWriter Writer)
    {
        Writer.OpenScope("dispinfo");
        {
            Writer.Insert<int>("power", 3); // Yes, I'm forcing power 3 displacements
            Writer.Insert<string>("startposition", "[0 0 64]");
            Writer.Insert<int>("flags", 0);
            Writer.Insert<int>("elevation", 0);
            Writer.Insert<int>("subdiv", 0);
            Writer.OpenScope("normals");
            {
                string normals = "0 0 1 0 0 1 0 0 1 0 0 1 0 0 1 0 0 1 0 0 1 0 0 1 0 0 1";
                for (int i = 0; i < 9; i++)
                {
                    Writer.Insert<string>($"row{i}", normals);
                }
            }
            Writer.CloseScope();
            Writer.OpenScope("distances");
            {
                switch (Order)
                {
                    case DisplacementOrder.BottomLeft:
                        for (int x = 0; x < 9; x++)
                        {
                            var builder = new StringBuilder();
                            for (int y = 0; y < 9; y++)
                            {
                                builder.Append($"{Distances[x, y]} ");
                            }
                            Writer.Insert<string>($"row{x}", builder.ToString());
                        }
                        break;
                    case DisplacementOrder.TopLeft:
                        for (int x = 0; x < 9; x++)
                        {
                            var builder = new StringBuilder();
                            for (int y = 0; y < 9; y++)
                            {
                                builder.Append($"{Distances[8 - y, 8 - x]} ");
                            }
                            Writer.Insert<string>($"row{8 - x}", builder.ToString());
                        }
                        break;
                    case DisplacementOrder.TopRight:
                        for (int x = 0; x < 9; x++)
                        {
                            var builder = new StringBuilder();
                            for (int y = 0; y < 9; y++)
                            {
                                builder.Append($"{Distances[8 - x, 8 - y]} ");
                            }
                            Writer.Insert<string>($"row{x}", builder.ToString());
                        }
                        break;
                    case DisplacementOrder.BottomRight:
                        for (int x = 0; x < 9; x++)
                        {
                            var builder = new StringBuilder();
                            for (int y = 0; y < 9; y++)
                            {
                                builder.Append($"{Distances[y, x]} ");
                            }
                            Writer.Insert<string>($"row{8 - x}", builder.ToString());
                        }
                        break;
                }
            }
            Writer.CloseScope();
            Writer.OpenScope("offsets");
            {
                string offsets = "0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0";
                for (int i = 0; i < 9; i++)
                {
                    Writer.Insert<string>($"row{i}", offsets);
                }
            }
            Writer.CloseScope();
            Writer.OpenScope("offset_normals");
            {
                string offset_normals = "0 0 1 0 0 1 0 0 1 0 0 1 0 0 1 0 0 1 0 0 1 0 0 1 0 0 1";
                for (int i = 0; i < 9; i++)
                {
                    Writer.Insert<string>($"row{i}", offset_normals);
                }
            }
            Writer.CloseScope();
            Writer.OpenScope("alphas");
            {
                string alphas = "0 0 0 0 0 0 0 0 0";
                for (int i = 0; i < 9; i++)
                {
                    Writer.Insert<string>($"row{i}", alphas);
                }
            }
            Writer.CloseScope();
            Writer.OpenScope("triangle_tags");
            {
                Writer.Insert<string>("row0", "0 1 0 0 0 0 0 0 0 0 0 0 0 1 0 0");
                Writer.Insert<string>("row1", "0 0 0 0 1 0 0 1 0 0 0 1 0 0 1 9");
                Writer.Insert<string>("row2", "0 0 0 0 0 0 0 1 0 9 0 1 0 0 0 1");
                Writer.Insert<string>("row3", "0 0 0 0 0 0 0 0 0 9 0 0 0 1 0 1");
                Writer.Insert<string>("row4", "1 0 0 0 0 0 0 0 0 0 0 1 0 0 0 0");
                Writer.Insert<string>("row5", "1 0 9 0 0 0 1 0 0 0 0 0 0 9 9 0");
                Writer.Insert<string>("row6", "9 0 0 0 0 0 9 0 0 9 9 1 1 9 9 9");
                Writer.Insert<string>("row7", "0 0 9 9 9 9 9 0 0 0 0 0 0 1 9 9");
            }
            Writer.CloseScope();
            Writer.OpenScope("allowed_verts");
            {
                Writer.Insert<string>("10", "-1 -1 -1 -1 -1 -1 -1 -1 -1 -1");
            }
            Writer.CloseScope();
        }
        Writer.CloseScope();
    }
}
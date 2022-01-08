using System;
using System.Text;

namespace Volvo;
public enum PrimitiveType
{
    Cube = 0,
}

public class Solid : VMFObject
{
    int ID = 1;
    List<Side> sides = new List<Side>();

    public Vector3 Position = new Vector3(100, 200, 300);
    public Vector3 Scale = new Vector3(96, 48, 24);
    public string Texture = "TOOLS/TOOLSNODRAW";

    public Solid()
    {

    }

    public void MakePrimitive(PrimitiveType type)
    {
        sides.Clear();
        switch (type)
        {
            case PrimitiveType.Cube:
                // TODO: For loop instead of this shit

                var side1 = new Side();
                side1.Texture = this.Texture;
                side1.Plane[0] = new Vector3(-Scale.x / 2 + Position.x, Scale.y / 2 + Position.y, Scale.z / 2 + Position.z);
                side1.Plane[1] = new Vector3(Scale.x / 2 + Position.x, Scale.y / 2 + Position.y, Scale.z / 2 + Position.z);
                side1.Plane[2] = new Vector3(Scale.x / 2 + Position.x, -Scale.y / 2 + Position.y, Scale.z / 2 + Position.z);

                var side2 = new Side();
                side2.Texture = this.Texture;
                side2.Plane[0] = new Vector3(-Scale.x / 2 + Position.x, -Scale.y / 2 + Position.y, -Scale.z / 2 + Position.z);
                side2.Plane[1] = new Vector3(Scale.x / 2 + Position.x, -Scale.y / 2 + Position.y, -Scale.z / 2 + Position.z);
                side2.Plane[2] = new Vector3(Scale.x / 2 + Position.x, Scale.y / 2 + Position.y, -Scale.z / 2 + Position.z);

                var side3 = new Side();
                side3.Texture = this.Texture;
                side3.Plane[0] = new Vector3(-Scale.x / 2 + Position.x, Scale.y / 2 + Position.y, Scale.z / 2 + Position.z);
                side3.Plane[1] = new Vector3(-Scale.x / 2 + Position.x, -Scale.y / 2 + Position.y, Scale.z / 2 + Position.z);
                side3.Plane[2] = new Vector3(-Scale.x / 2 + Position.x, -Scale.y / 2 + Position.y, -Scale.z / 2 + Position.z);

                var side4 = new Side();
                side4.Texture = this.Texture;
                side4.Plane[0] = new Vector3(Scale.x / 2 + Position.x, Scale.y / 2 + Position.y, -Scale.z / 2 + Position.z);
                side4.Plane[1] = new Vector3(Scale.x / 2 + Position.x, -Scale.y / 2 + Position.y, -Scale.z / 2 + Position.z);
                side4.Plane[2] = new Vector3(Scale.x / 2 + Position.x, -Scale.y / 2 + Position.y, Scale.z / 2 + Position.z);

                var side5 = new Side();
                side5.Texture = this.Texture;
                side5.Plane[0] = new Vector3(Scale.x / 2 + Position.x, Scale.y / 2 + Position.y, Scale.z / 2 + Position.z);
                side5.Plane[1] = new Vector3(-Scale.x / 2 + Position.x, Scale.y / 2 + Position.y, Scale.z / 2 + Position.z);
                side5.Plane[2] = new Vector3(-Scale.x / 2 + Position.x, Scale.y / 2 + Position.y, -Scale.z / 2 + Position.z);

                var side6 = new Side();
                side6.Texture = this.Texture;
                side6.Plane[0] = new Vector3(Scale.x / 2 + Position.x, -Scale.y / 2 + Position.y, -Scale.z / 2 + Position.z);
                side6.Plane[1] = new Vector3(-Scale.x / 2 + Position.x, -Scale.y / 2 + Position.y, -Scale.z / 2 + Position.z);
                side6.Plane[2] = new Vector3(-Scale.x / 2 + Position.x, -Scale.y / 2 + Position.y, Scale.z / 2 + Position.z);

                sides.Add(side1);
                sides.Add(side2);
                sides.Add(side3);
                sides.Add(side4);
                sides.Add(side5);
                sides.Add(side6);

                break;
            default:
                break;
        }
    }

    public void MakeDisplacement(Displacement displacement)
    {
        MakePrimitive(PrimitiveType.Cube);
        sides[0].Displacement = displacement;
    }

    public override void Create(VMFWriter Writer)
    {
        Writer.OpenScope("solid");
        {
            for (int i = 0; i < sides.Count; i++)
            {
                var side = sides[i];
                side.Create(Writer);
            }
        }
        Writer.CloseScope();
    }
}
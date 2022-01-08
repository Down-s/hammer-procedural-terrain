using System;
using System.Text;

namespace Volvo;

struct SideData
{
    public string uaxis = "[1 0 0 0] 0.25";
    public string vaxis = "[0 -1 0 0] 0.25";
    public float rotation = 0;
    public int lightmapscale = 16;
    public int smoothing_groups = 0;
}

public class Side : VMFObject
{
    static int CurrentID = 0;

    public Displacement? Displacement = null;
    public Vector3[] Plane = new Vector3[3];
    public string Texture = "TOOLS/TOOLSNODRAW";

    private int ID => CurrentID;
    private SideData SideData = new SideData();

    public Side()
    {
        Side.CurrentID++;
    }

    public override void Create(VMFWriter Writer)
    {
        CurrentID++;
        Writer.OpenScope("side");
        {
            Writer.Insert<int>("id", this.ID);
            Writer.Insert<string>("plane", $"{this.Plane[0].ToString()} {this.Plane[1].ToString()} {this.Plane[2].ToString()}");
            Writer.Insert<string>("material", this.Texture);
            Writer.Insert<string>("uaxis", this.SideData.uaxis);
            Writer.Insert<string>("vaxis", this.SideData.vaxis);
            Writer.Insert<float>("rotation", this.SideData.rotation);
            Writer.Insert<int>("lightmapscale", this.SideData.lightmapscale);
            Writer.Insert<int>("smoothing_groups", this.SideData.smoothing_groups);
            if (Displacement != null)
                Displacement.Create(Writer);
        }
        Writer.CloseScope();
    }
}
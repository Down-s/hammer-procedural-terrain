namespace Volvo;
public class VMFBuilder
{
    VMFWriter Writer = new VMFWriter();
    public delegate void _WorldCreated();
    public event _WorldCreated WorldCreated;

    public void AddObject(VMFObject obj)
    {
        obj.Create(Writer);
    }

    public string Create()
    {
        // VMF Bullshit
        Writer.OpenScope("versioninfo");
        {
            Writer.Insert<int>("editorversion", 400);
            Writer.Insert<int>("editorbuild", 9169);
            Writer.Insert<int>("mapversion", 14);
            Writer.Insert<int>("formatversion", 100);
            Writer.Insert<int>("prefab", 0);
        }
        Writer.CloseScope(); // versioninfo
        Writer.OpenScope("visgroups");
        {
            // No visgroups
        }
        Writer.CloseScope();
        Writer.OpenScope("viewsettings");
        {
            Writer.Insert<int>("bSnapToGrid", 1);
            Writer.Insert<int>("bShowGrid", 1);
            Writer.Insert<int>("bShowLogicalGrid", 0);
            Writer.Insert<int>("nGridSpacing", 64);
            Writer.Insert<int>("bShow3DGrid", 0);
        }
        Writer.CloseScope(); // viewsettings
                             // Start of world
        Writer.OpenScope("world");
        {
            Writer.Insert<int>("bSnapToGrid", 1);
            Writer.Insert<int>("bShowGrid", 1);
            Writer.Insert<int>("bShowLogicalGrid", 0);
            Writer.Insert<int>("nGridSpacing", 64);
            Writer.Insert<int>("bShow3DGrid", 0);

            WorldCreated();
        }
        Writer.CloseScope(); // world

        return Writer.GetText();
    }
}
namespace Volvo;
public class Vector3
{
    public float x, y, z;
    public Vector3(float x = 0, float y = 0, float z = 0)
    {
        this.x = x;
        this.y = y;
        this.z = z;
    }

    public string ToString()
    {
        return $"({this.x} {this.y} {this.z})";
    }
}
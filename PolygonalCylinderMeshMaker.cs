using UnityEngine;
public static class PolygonalCylinderMeshMaker
{
    public static MeshData GenerateMeshData(int numSides, float length)
    {
        // return DemoDataUtil.GetDemoData();
        var pc = new PolygonCylinder(numSides, length);
        return pc.meshData;
    }
}

public class PolygonCylinder
{
    public int numSides;
    public float length;

    public MeshData meshData;
    private Polygon polygon;

    public PolygonCylinder(int numSides, float length)
    {
        this.numSides = numSides;
        this.length = length;
        this.GeneratePolygonCylinder();
    }


    private void GeneratePolygonCylinder()
    {
        meshData = new MeshData();
        polygon = new Polygon(numSides);

        BuildMesh();

        // Debug.Log(meshData.TrianglesToString());
        // Debug.Log($"len(meshData.vertices)={meshData.vertices.Length}, len(meshData.triangles)={meshData.triangles.Length}, len(meshData.Triangles)={meshData.Triangles.Length}");
    }

    void StackPolygon(float z1, float z2)
    {
        Vector2[] polyVs = polygon.vertices;
        float[] angularUvs = polygon.angularUvs;

        for (int i1 = 0; i1 < polygon.numSides; i1++)
        {
            int i2 = (i1 + 1) % polyVs.Length;

            float angularUv1 = angularUvs[i1];
            float angularUv2 = angularUvs[i2];

            float zUv1 = z1 / length;
            float zUv2 = z2 / length;

            int idx1 = meshData.AddVertex(new Vector3(polyVs[i1].x, polyVs[i1].y, z1), new Vector2(angularUv1, zUv1));
            int idx2 = meshData.AddVertex(new Vector3(polyVs[i2].x, polyVs[i2].y, z1), new Vector2(angularUv2, zUv1));
            int idx3 = meshData.AddVertex(new Vector3(polyVs[i1].x, polyVs[i1].y, z2), new Vector2(angularUv1, zUv2));
            meshData.AddTriangleIdxs(idx1, idx2, idx3);

            idx1 = meshData.AddVertex(new Vector3(polyVs[i2].x, polyVs[i2].y, z1), new Vector2(angularUv2, zUv1));
            idx2 = meshData.AddVertex(new Vector3(polyVs[i2].x, polyVs[i2].y, z2), new Vector2(angularUv2, zUv2));
            idx3 = meshData.AddVertex(new Vector3(polyVs[i1].x, polyVs[i1].y, z2), new Vector2(angularUv1, zUv2));
            meshData.AddTriangleIdxs(idx1, idx2, idx3);
        }
    }

    void BuildMesh()
    {
        meshData.Reset();
        StackPolygon(0, length);
    }
}

public class Polygon
{
    public int numSides;
    public Vector2[] vertices;
    public float[] angularUvs;

    public Polygon(int numSides)
    {
        this.numSides = numSides;
        SetVertices();
    }

    void SetVertices()
    {
        float angle = 2 * Mathf.PI / numSides;
        vertices = new Vector2[numSides];
        angularUvs = new float[numSides];

        for (int i = 0; i < numSides; i++)
        {
            float x = Mathf.Cos(i * angle);
            float y = Mathf.Sin(i * angle);
            vertices[i] = new Vector2(x, y);
            angularUvs[i] = (float)i / numSides;
        }
    }
}
using System;
using UnityEngine;

public class MeshMaker : MonoBehaviour
{
    public bool autoUpdate;

    [Range(3, 40)]
    public int numSides;

    [Min(0.01f)]
    public float length;

    [Min(0.01f)]
    public float polygonSideLength;

    public void MakeMesh()
    {
        Debug.Log("GenerateMesh invoked...");
        MeshDrawer drawer = FindObjectOfType<MeshDrawer>();

        Debug.Log("Getting texture...");
        var texture = GetTexture();

        Debug.Log("Getting mesh...");
        var meshData = PolygonalCylinderMeshMaker.GenerateMeshData(numSides, length, polygonSideLength);

        Debug.Log("Drawing mesh...");
        drawer.DrawMesh(meshData, texture);

        Debug.Log("Mesh generation complete");
    }

    private static Texture2D GetTexture()
    {
        Texture2D texture = new Texture2D(1, 1);
        return texture;
    }
}
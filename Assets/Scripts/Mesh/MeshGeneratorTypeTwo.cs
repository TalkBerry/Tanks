using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshGeneratorTypeTwo
{

    public static Mesh Triangle(Vector3 vertex0, Vector3 vertex1, Vector3 vertex2)
    {
        var mesh = new Mesh
        {
            vertices = new[] { vertex0, vertex1, vertex2 },
            uv = new[] { new Vector2(0, 0), new Vector2(0, 1), new Vector2(1, 1) },
            triangles = new[] { 0, 1, 2 }
        };
        mesh.RecalculateNormals();
        return mesh;
    }

    public static Mesh Quad(Vector3 origin, Vector3 width, Vector3 length, Vector2 [] uvs)
    {
        var normal = Vector3.Cross(length, width).normalized;
        var mesh = new Mesh
        {
            vertices = new[] { origin, origin + length, origin + length + width, origin + width },
            //uv = new[] { new Vector2(0, 0), new Vector2(0, 1), new Vector2(1, 1), new Vector2(1, 0) },
            uv = uvs,
            triangles = new[] { 0, 1, 2, 0, 2, 3 }
        };
        mesh.RecalculateNormals();
        return mesh;
    }

    public static Mesh Plane(Vector3 origin, Vector3 width, Vector3 length, int widthCount, int lengthCount)
    {
        var combine = new CombineInstance[widthCount * lengthCount];

        var i = 0;
        var uv = new[] { new Vector2(0, 0), new Vector2(0, 1), new Vector2(1, 1), new Vector2(1, 0) };
        for (var x = 0; x < widthCount; x++)
        {
            for (var y = 0; y < lengthCount; y++)
            {
                combine[i].mesh = Quad(origin + width * x + length * y, width, length, uv);
                i++;
            }
        }

        var mesh = new Mesh();
        mesh.CombineMeshes(combine, true, false);
        mesh.RecalculateNormals();
        return mesh;
    }



    public static Mesh Cube(Vector3 width, Vector3 length, Vector3 height)
    {
        var corner0 = -width / 2 - length / 2 - height / 2;
        var corner1 = width / 2 + length / 2 + height / 2;
        var uv = new[] { new Vector2(0, 0), new Vector2(0, 1), new Vector2(1, 1), new Vector2(1, 0) };
        var combine = new CombineInstance[6];
        combine[0].mesh = Quad(corner0, length, width, uv);
        combine[1].mesh = Quad(corner0, width, height,uv);
        combine[2].mesh = Quad(corner0, height, length, uv);
        combine[3].mesh = Quad(corner1, -width, -length, uv);
        combine[4].mesh = Quad(corner1, -height, -width, uv);
        combine[5].mesh = Quad(corner1, -length, -height, uv);

        var mesh = new Mesh();
        mesh.CombineMeshes(combine, true, false);
        return mesh;
    }


}

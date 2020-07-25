using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
public class MeshGenerator : MonoBehaviour
{

    [SerializeField] private int _xSize;
    [SerializeField] private int _zSize;
    [SerializeField] private float _precision = 20f;
    [SerializeField] private Texture2D _heightMap;
    private Mesh _mesh;
    private Vector3[] _verticles;
    private int[] _triangles;
    private float[] _heights;
    void Start()
    {
        // create new mesh and apply it to mesh filter
        //_mesh = new Mesh();
        //GetComponent<MeshFilter>().mesh = _mesh;

        // Method generates plane from texture
        // GetHeightFromTexture(_heightMap);

        //set new mesh params
        //SetShapeParams();
        //UpdateMesh();

        // create mesh by new generator
        //CreateFromGenerator();
    }


    void CreateFromGenerator()
    {
        var uv = new[] { new Vector2(0, 0), new Vector2(0, 1), new Vector2(1, 1), new Vector2(1, 0) };

        // get generated triangle 
        GetComponent<MeshFilter>().mesh = MeshGeneratorTypeTwo.Triangle(new Vector3(0, 0, 0), new Vector3(0, 0, 1), new Vector3(1, 0, 1));

        // get generated quad
        //GetComponent<MeshFilter>().mesh = MeshGeneratorTypeTwo.Quad(new Vector3(0, 0, 0), new Vector3(1, 0, 0), new Vector3(0, 0, 1),uv);

        // get generated cube
        //GetComponent<MeshFilter>().mesh = MeshGeneratorTypeTwo.Cube(new Vector3(1, 0, 0), new Vector3(0, 0, 1), new Vector3(0, 1, 0));

        // get generated plane with size
        //GetComponent<MeshFilter>().mesh = MeshGeneratorTypeTwo.Plane(new Vector3(0, 0, 0), new Vector3(1, 0, 0), new Vector3(0, 0, 1), _xSize, _zSize);

    }


    void SetShapeParams()
    {
        //  Set Mesh Params like verticles and triangles

        // set params for single triangle
        {
          _verticles = new Vector3[]
          {
            new Vector3(0,0,0),
            new Vector3(0,0,1),
            new Vector3(1,0,0)
          };

            _triangles = new int[]
            {
                0,1,2
            };
        }

        // set params for single quad
        /* {
             _verticles = new Vector3[]
           {
             new Vector3(0,0,0),
             new Vector3(0,0,1),
             new Vector3(1,0,0),
             new Vector3(1,0,1),
           };

             _triangles = new int[]
             {
                0,1,2,1,3,2
             };
         }*/

        // set params for single quad in plane with size _xSize and _zSize
        /* {
             _verticles = new Vector3[(_xSize + 1) * (_zSize + 1)];
             for (int index = 0, z = 0; z < _zSize + 1; z++)
             {
                 for (int x = 0; x < _xSize + 1; x++)
                 {
                     //float y = Mathf.PerlinNoise(x*0.5f,z*0.3f)*3f;
                     float y = _heights[index];
                     _verticles[index] = new Vector3(x, y, z);
                     index++;
                 }
             }

             _triangles = new int[6];
             _triangles[0] = 0;
             _triangles[1] = _xSize + 1;
             _triangles[2] = 1;
             _triangles[3] = 1;
             _triangles[4] = _xSize + 1;
             _triangles[5] = _xSize + 2;

         }*/

        //set params for plane with size _xSize and _zSize
       /* {
            _verticles = new Vector3[(_xSize + 1) * (_zSize + 1)];
            for (int index = 0, z = 0; z < _zSize + 1; z++)
            {
                for (int x = 0; x < _xSize + 1; x++)
                {
                    // add random height to verticals
                    float y = Mathf.PerlinNoise(x * 0.5f, z * 0.3f) * 3f;
                    _verticles[index] = new Vector3(x, y, z);
                    index++;
                }
            }

            _triangles = new int[_xSize * _zSize * 6];
            int vert = 0;
            int tris = 0;
            for (int z = 0; z < _zSize; z++)
            {
                for (int x = 0; x < _xSize; x++)
                {
                    _triangles[tris + 0] = vert + 0;
                    _triangles[tris + 1] = vert + _xSize + 1;
                    _triangles[tris + 2] = vert + 1;
                    _triangles[tris + 3] = vert + 1;
                    _triangles[tris + 4] = vert + _xSize + 1;
                    _triangles[tris + 5] = vert + _xSize + 2;
                    vert++;
                    tris += 6;

                }
                vert++;
            }
        }*/
    }

    void UpdateMesh()
    {
        _mesh.Clear();
        _mesh.vertices = _verticles;
        _mesh.triangles = _triangles;
        _mesh.RecalculateNormals();
        // aplly vert and triang to mesh 

    }

    private void OnDrawGizmos()
    {
        if (_verticles == null)
        {
            return;
        }

        foreach (Vector3 vert in _verticles)
        {
            Gizmos.DrawSphere(vert, .1f);
        }
    }

    void GetHeightFromTexture(Texture2D texture)
    {
        Color[] pixels = texture.GetPixels();
        _heights = new float[pixels.Length / 4];
        //_xSize = texture.width-1;
        //_zSize = texture.height-1;
        int y = 0;
        int row = 0;
        for (int i = 0; i < _heights.Length; i++)
        {
            if (row == _heights.Length)
            {
                row = 0;
                y += 4;
            }
            int x = row * 4;
            Color pixel = texture.GetPixel(x, y);
            _heights[i] = pixels[i].grayscale * _precision;
            row++;

        }


        GetComponent<MeshFilter>().mesh = MeshGeneratorTypeTwo.Plane(new Vector3(0, 0, 0), new Vector3(1, 0, 0), new Vector3(0, 0, 1), 128, 128);
        var oldHeights = GetComponent<MeshFilter>().mesh.vertices;
        //var newVerticals = new Vector3[oldHeights.Length];
        List<Vector3> newVerticals = new List<Vector3>();
        for (int i = 0; i < oldHeights.Length; i++)
        {
            //newVerticals[i] = new Vector3(oldHeights[i].x, _heights[i], oldHeights[i].z);
            newVerticals.Add(new Vector3(oldHeights[i].x, _heights[i], oldHeights[i].z));
        }
        GetComponent<MeshFilter>().mesh.SetVertices(newVerticals);


    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
public class MeshGenerator : MonoBehaviour
{

    [SerializeField] private int _xSize = 20;
    [SerializeField] private int _zSize = 20;
    private Mesh _mesh;
    private Vector3[] _verticles;
    private int[] _triangles;
    void Start()
    {
        _mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = _mesh;
        SetShapeParams();
        UpdateMesh();
    }

   
    void SetShapeParams()
    {
        //  Set Mesh Params like verticles and triangles
      
    }

    void UpdateMesh()
    {
   
        // aplly vert and triang to mesh 

    }

    private void OnDrawGizmos()
    {
        if (_verticles == null)
            return;
        
        foreach(Vector3 vert in _verticles)
        {
            Gizmos.DrawSphere(vert, .1f);
        }
    }

}

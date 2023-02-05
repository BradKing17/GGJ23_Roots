using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class MeshGenerator : MonoBehaviour
{
    public Mesh _mesh;
    public MeshFilter _meshfilter;
    public MeshRenderer _meshRenderer;
    // Start is called before the first frame update
    void Start()
    {
        _mesh = new Mesh{name = "root"};
        _meshfilter = new MeshFilter();
        _meshRenderer = new MeshRenderer();
        _meshfilter.sharedMesh = _mesh;
        _mesh.indexFormat = IndexFormat.UInt32;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

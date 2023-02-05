using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
public class MeshGenController : MonoBehaviour
{
    [SerializeField] Transform[] controlPoints;
    Vector3 Getpos(int i) => controlPoints[i].position;
    RootController RC;
    public float pointTimer = 0.2f;
    float curPointTimer = 0.0f;
    int pointCount;
    public List<GameObject> players;
    public List<Vector3> playerPos;

    public GameObject meshWorldRoot;
    public GameObject meshGenerator;
    public List<Vector3> meshGuide;
    
    public List<Transform> rootTangentTransforms;
    public List<Vector3> vertexList;
    public List<int> triList;
    [Header ("Root Options")]
    public float sliceAngle = 180;
    public float stackLength = 0.05F;
    public float stemWidth;
    // Start is called before the first frame update
    public Mesh _mesh;
    public MeshFilter _meshfilter;
    public MeshRenderer _meshRenderer;

    SpawnController SC;
    public GameObject manager;
    // Start is called before the first frame update
 
    void Start()
    {
        _mesh = new Mesh{name = "root"};
        _meshfilter = new MeshFilter();
        _meshRenderer = new MeshRenderer();
        _meshfilter.sharedMesh = _mesh;
        _mesh.indexFormat = IndexFormat.UInt32;

        meshWorldRoot = this.gameObject;
        curPointTimer = pointTimer;
        playerPos = new List<Vector3>();   
        players = new List<GameObject>();
        players = new List<GameObject>(GameObject.FindGameObjectsWithTag("Player"));
       
    }
    void FixedUpdate()
    {
        if (players.Capacity == 0)
        {
            players = new List<GameObject>(GameObject.FindGameObjectsWithTag("Player"));
            var foreachcount = 1;
            foreach (var player in players)
            {
                meshGenerator = new GameObject{
                    name = "Player " + foreachcount + "'s Mesh Handler",
                    transform = {
                        parent = this.transform
                    }
                };
                meshGenerator.AddComponent<MeshFilter>();
                meshGenerator.AddComponent<MeshRenderer>();
                playerPos.Add(player.transform.position);
                foreachcount++;
            }
            print("ding");
        }
        //Point Dropping
            if (curPointTimer >= 0.0f)
            {
                curPointTimer -= Time.deltaTime;
            }
            else
            {
                DropPoint();
            }
    }
    void DropPoint()
    {
        if(manager.GetComponent<SpawnController>().gameRunning)
        {
            var mTP = this.transform.position;
            for (int i = 0; i < players.Capacity; i++)
            {
                curPointTimer = pointTimer;
                Debug.Log(curPointTimer);

                print("ding");

                var clone = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                pointCount++;
                clone.name = "Point " + pointCount.ToString();
                clone.transform.localScale  = new Vector3(0.4f, 0.4f, 0.4f);
                
                clone.transform.parent      = this.transform.GetChild(0);
                clone.transform.position    = new Vector3( playerPos[i].x,playerPos[i].y, playerPos[i].z);
                print("dong");
                Physics.IgnoreCollision(clone.GetComponent<Collider>(), players[i].GetComponent<Collider>());
            }
        }
        
        
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < players.Capacity; i++)
        {
            var currentPlayerPos = players[i].transform.position;
            playerPos[i] = new Vector3(currentPlayerPos.x, currentPlayerPos.y, currentPlayerPos.z);
        }
        
        foreach (var player in players)
        {   
            var meshObj = player.transform.GetChild(0);
            
        }

        
    }
    // PointOrientation GetBezierPoint(float t)
    // {
    //     Vector3 p0 = Getpos(0);
    //     Vector3 p1 = Getpos(1);
    //     Vector3 p2 = Getpos(2);

    //     Vector3 a = Vector3.Lerp(p0,p1,t);
    //     Vector3 b = Vector3.Lerp(p1,p2,t);
        
    //     Vector3 pos =  Vector3.Lerp(a, b, t);
    //     Vector3 tangent = (b - a).normalized;

    //     // if (t >= _stemLengthCalculation)
    //     // {
    //     //     _stemLengthCalculation += _resolutionCalculation;
    //     //     stackCount++;
            
    //     //     StemPointGen(GetBezierPoint(tPoint));
    //     // }
        
    //     return new PointOrientation(pos, tangent);
    // }
    // void StemPointGen(PointOrientation point)
    // {
    //     float pointsInCircumference = 360 / sliceAngle;        
    //     for (int i = 0; i < pointsInCircumference; i++)
    //     {
    //         var tempSliceAngle = sliceAngle * i;
    //         Vector2 v = new Vector2(stemWidth, stemWidth);

    //         Vector3 resultV = Quaternion.Euler(0, 0, tempSliceAngle) * v;
            
    //         var stemPoint = new GameObject
    //         {
    //             transform =
    //             {
    //                 position = point.LocaltoWorld(resultV),
    //                 rotation = point.rot,
    //                 parent = transform.GetChild(3)
    //             },
    //             name = "Point " + rootTangentTransforms.Count
    //         };
    //         rootTangentTransforms.Add(stemPoint.transform);
    //     } 
    //     TriangleGen(pointsInCircumference);
        
    // }
    // private void TriangleGen(float pointsInCircumference)
    // {
    //     if (pointCount <= 1) return;
        
    //     var pIc = (int) pointsInCircumference;
    //     var bPt = rootTangentTransforms;
    //     // List<Vector3> vertexList = new List<Vector3>{};
    //     // List<int> triList = new List<int>{};
    //     triList.Clear();
    //     vertexList.Clear();

    //     for (int i = pIc; i < bPt.Count - 1; i++)
    //     {
    //         int offset = vertexList.Count;

    //         vertexList.AddRange(new []
    //         {
    //             bPt[i].transform.position,
    //             bPt[i+1].transform.position ,
    //             bPt[i-pIc].transform.position,
    //             bPt[i-pIc + 1].transform.position
    //         });

    //         triList.AddRange(new []
    //         {
    //             offset+2,
    //             offset+1,
    //             offset,

    //             offset+3,
    //             offset+1,
    //             offset+2,
    //         });
    //     }
        
    //     _mesh.SetVertices(vertexList);
    //     _mesh.SetTriangles(triList, 0);
    //     _mesh.RecalculateNormals();
    //     _mesh.OptimizeReorderVertexBuffer();
        
    // }

}

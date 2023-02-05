using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshGenController : MonoBehaviour
{
    public float pointTimer = 0.2f;
    float curPointTimer = 0.0f;
    int pointCount;
    public List<GameObject> players;
    public List<Vector3> playerPos;

    public GameObject meshWorldRoot;

    public GameObject meshGenerator;

    public List<Vector3> meshGuide;
    
    // Start is called before the first frame update
    void Start()
    {
        meshWorldRoot = this.gameObject;
        curPointTimer = pointTimer;
        playerPos = new List<Vector3>();   
        players = new List<GameObject>();
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
            playerPos.Add(player.transform.position);
            foreachcount++;
        }
       
        
    }
    void FixedUpdate()
    {
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
        var mTP = this.transform.position;
        for (int i = 0; i < players.Capacity; i++)
        {
            curPointTimer = pointTimer;
            Debug.Log("poop");
            Debug.Log(curPointTimer);

            print("ding");

            var clone = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            pointCount++;
            clone.name = "Point " + pointCount.ToString();
            clone.transform.localScale  = new Vector3(0.4f, 0.4f, 0.4f);
            
            clone.transform.parent      = this.transform.GetChild(0);
            clone.transform.position    = new Vector3( playerPos[i].x,playerPos[i].y, playerPos[i].z);
            print("dong");
            // Physics.IgnoreCollision(clone.GetComponent<Collider>(), GetComponent<Collider>());
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
    }
}

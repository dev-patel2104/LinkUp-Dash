using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class rope : MonoBehaviour
{
    [SerializeField] GameObject partPrefab;
    [SerializeField] GameObject player1;
    [SerializeField] GameObject player2;
    [Range(1, 50)] [SerializeField] int length;
    [SerializeField] int maxDistance = 15;
    [SerializeField] int ropelength = 16;
    [SerializeField] float waitTime = 5f;

    Rigidbody p1rg;
    Rigidbody p2rg;
    HingeJoint p1hg, p2hg;
    JointSpring hs, hs2;
    private float partlength;
    private GameObject lastSpawnned;
    bool ropeCnt;
    int temp = 0;
    public bool ropePresent;
    JoystickMovement jm;
    PlayerMovement[] pm;
    
    List<GameObject> ropePart = new List<GameObject>();

    void Start()
    {
        p1rg = player1.GetComponent<Rigidbody>();
        p2rg = player2.GetComponent<Rigidbody>();
        ropeCnt = false;
        ropePresent = false;
        partlength = partPrefab.transform.localScale.y;
        jm = FindObjectOfType<JoystickMovement>();
        pm = FindObjectsOfType<PlayerMovement>();

        // assigning photon players

        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        Debug.Log(players);
        foreach (GameObject Player in players)
        {
            
            if(PhotonView.Get(Player).IsMine)
            {
                Debug.Log("Found Player");

                if(PhotonPlayer.photonPlayer.myTeam == 1)
                {
                    Debug.Log("Assigned player 1");
                    player1 = Player;
                }
                else if(PhotonPlayer.photonPlayer.myTeam == 2)
                {
                    Debug.Log("Assigned player 2");

                    player2 = Player;
                }
            }
        }
        
        
    }
    
    // Update is called once per frame
    void Update()
    {
        length = (int)(Vector3.Distance(player1.transform.position, player2.transform.position));

        if (!ropeCnt && length < maxDistance)
        { 
            spawnRope();       
        }

        if (length > maxDistance)
        {
            DestroyRope();
            
        }
        
    }


    private void spawnRope()
    {
        if (temp == 0)
        {
            ropePresent = true;
            p1rg.isKinematic = true;
            p2rg.isKinematic = true;
            temp++;  // temp and ropeCnt are the gateway to allow the execution of the function
            ropeCnt = true;
            length = (int)(Vector3.Distance(player1.transform.position, player2.transform.position)); // length between the two player
            int cnt = (int)(ropelength / partlength); // number of rope parts needed
            p1hg =  player1.AddComponent<HingeJoint>();
            p2hg =  player2.AddComponent<HingeJoint>();
            JointSpring hs = p1hg.spring;
            JointSpring hs2 = p2hg.spring;
            playerJointConfig();
            for (int i = 0; i < cnt; i++)
            {
                GameObject part;
                if (i == 0)
                {
                    part = Instantiate(partPrefab, new Vector3(player1.transform.position.x + partlength * (i + 1), player1.transform.position.y, (player1.transform.position.z )), Quaternion.identity);
                    part.transform.localRotation = Quaternion.Euler(0f, 0f, 90f);
                    part.transform.parent = player1.transform;
                    part.GetComponent<Collider>().isTrigger = true;
                    part.GetComponent<HingeJoint>().connectedBody = player1.GetComponent<Rigidbody>();
                    player1.GetComponent<HingeJoint>().connectedBody = part.GetComponent<Rigidbody>();

                }
                else if (i == cnt - 1)
                {
                    part = Instantiate(partPrefab, new Vector3(player1.transform.position.x + partlength * (i + 1), player1.transform.position.y, (player1.transform.position.z)), Quaternion.identity);
                    part.transform.localRotation = Quaternion.Euler(0f, 0f, 90f);
                    part.transform.parent = player2.transform;
                    
                    part.GetComponent<HingeJoint>().connectedBody = lastSpawnned.GetComponent<Rigidbody>();
                    part.transform.localPosition = new Vector3(0f, 0f, 0f);
                }
                else
                {
                    part = Instantiate(partPrefab, new Vector3(player1.transform.position.x + partlength * (i + 1), player1.transform.position.y, (player1.transform.position.z)), Quaternion.identity);
                    part.transform.localRotation = Quaternion.Euler(0f, 0f, 90f);
                    part.GetComponent<HingeJoint>().connectedBody = lastSpawnned.GetComponent<Rigidbody>();
                }

                lastSpawnned = part;
                ropePart.Add(part);
            }
            player2.GetComponent<HingeJoint>().connectedBody = lastSpawnned.GetComponent<Rigidbody>();
        }
        p1rg.isKinematic = false;
        p2rg.isKinematic = false;
    }

    public void DestroyRope()
    {
        ropeCnt = true;
        ropePresent = false;
        foreach(GameObject temp in ropePart)
        {
            Destroy(temp);
        }
        temp--;
        if(temp <0)
        {
            temp = 0;
        }
        Destroy(p1hg);
        Destroy(p2hg);
        StartCoroutine(respawnProcess());
    }

    IEnumerator respawnProcess()
    {
        yield return new WaitForSeconds(waitTime);
        ropeCnt = false;
        
    }

    private void playerJointConfig()
    {
        p1hg.anchor = new Vector3(0, 1, 0);
        p2hg.anchor = new Vector3(0, 1, 0);
        p1hg.axis = p2hg.anchor =new Vector3(1, 0, 0);
        p1hg.autoConfigureConnectedAnchor = p2hg.autoConfigureConnectedAnchor = true;
        p1hg.useSpring = p2hg.useSpring = true;
        hs.spring = hs2.spring = 1500;
        hs.damper = hs2.damper = 40;
        p1hg.enableCollision = true;
        p2hg.enableCollision = true;
        p1hg.enablePreprocessing = false;
        p2hg.enablePreprocessing = false;

    }

   
    
}

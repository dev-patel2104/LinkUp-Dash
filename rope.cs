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

    Rigidbody p1rg;
    Rigidbody p2rg;
    private float partlength;
    private GameObject lastSpawnned;
    bool ropeCnt;
    int temp = 0;
    
    
    List<GameObject> ropePart = new List<GameObject>();

    void Start()
    {
        p1rg = player1.GetComponent<Rigidbody>();
        p2rg = player2.GetComponent<Rigidbody>();
        ropeCnt = false;
        partlength = partPrefab.transform.localScale.y;
        
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
            p1rg.isKinematic = true;
            p2rg.isKinematic = true;
            temp++;  // temp and ropeCnt are the gateway to allow the execution of the function
            ropeCnt = true;
            length = (int)(Vector3.Distance(player1.transform.position, player2.transform.position)); // length between the two player
            int cnt = (int)(ropelength / partlength); // number of rope parts needed
            for (int i = 0; i < cnt; i++)
            {
                GameObject part;
                if (i == 0)
                {
                    part = Instantiate(partPrefab, new Vector3(player1.transform.position.x, player1.transform.position.y, (player1.transform.position.z + partlength * (i + 1))), Quaternion.identity);
                    part.transform.localRotation = Quaternion.Euler(-90f, 0f, 0f);
                    part.transform.parent = player1.transform;
                    part.GetComponent<HingeJoint>().connectedBody = player1.GetComponent<Rigidbody>();

                }
                else if (i == cnt - 1)
                {
                    part = Instantiate(partPrefab, new Vector3(player1.transform.position.x, player1.transform.position.y, (player1.transform.position.z + partlength * (i + 1))), Quaternion.identity);
                    part.transform.localRotation = Quaternion.Euler(-90f, 0f, 0f);
                    part.transform.parent = player2.transform;
                    part.GetComponent<HingeJoint>().connectedBody = lastSpawnned.GetComponent<Rigidbody>();
                    part.transform.localPosition = new Vector3(0f, 0f, 0f);
                }
                else
                {
                    part = Instantiate(partPrefab, new Vector3(player1.transform.position.x, player1.transform.position.y, (player1.transform.position.z + partlength * (i + 1))), Quaternion.identity); 
                    part.transform.localRotation = Quaternion.Euler(-90f, 0f, 0f);
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
        foreach(GameObject temp in ropePart)
        {
            Destroy(temp);
        }
        temp--;
        if(temp <0)
        {
            temp = 0;
        }
        StartCoroutine(respawnProcess());
    }

    IEnumerator respawnProcess()
    {
        yield return new WaitForSeconds(5f);
        ropeCnt = false;
    }
    
}

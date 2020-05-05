using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rope : MonoBehaviour
{
    [SerializeField] GameObject partPrefab;
    [SerializeField] GameObject player1;
    [SerializeField] GameObject player2;
    [Range(1, 50)] [SerializeField] int length;

    private float partlength;
    private GameObject lastSpawnned;
    bool ropeCnt;

    private void Awake()
    {
       
    }
    void Start()
    {
        ropeCnt = false;
        partlength = partPrefab.transform.localScale.y;
    }

    // Update is called once per frame
    void Update()
    {
        if (!ropeCnt)
        {
            spawnRope();
        }
    }

    private void spawnRope()
    {
        length = (int)(Vector3.Distance(player1.transform.position, player2.transform.position)) ;
        int cnt = (int)(length / partlength);
        for(int i=0; i < cnt; i++)
        {
            GameObject part;
            if(i==0)
            {
                part = Instantiate(partPrefab, new Vector3(transform.position.x , transform.position.y, (transform.position.z + partlength * (i + 1))), Quaternion.identity);
                part.transform.localRotation = Quaternion.Euler(-90f, 0f, 0f);
                part.transform.parent = player1.transform;
                part.GetComponent<HingeJoint>().connectedBody = player1.GetComponent<Rigidbody>();

            }
            else if(i == cnt - 1)
            {
                part =  Instantiate(partPrefab, new Vector3(transform.position.x , transform.position.y , (transform.position.z + partlength * (i + 1))), Quaternion.identity);
                part.transform.localRotation = Quaternion.Euler(-90, 0f, 0f);
                part.transform.parent = player2.transform;
                part.GetComponent<HingeJoint>().connectedBody = lastSpawnned.GetComponent<Rigidbody>();
                part.transform.localPosition = new Vector3(0f, 0f, 0f);
            }
            else
            {
                part = Instantiate(partPrefab, new Vector3(transform.position.x , transform.position.y , (transform.position.z + partlength * (i + 1))), Quaternion.identity);
                part.transform.parent = player1.transform;
                part.transform.localRotation = Quaternion.Euler(-90f, 0f, 0f);
                part.GetComponent<HingeJoint>().connectedBody = lastSpawnned.GetComponent<Rigidbody>();
            }
            
            lastSpawnned = part;
           
        }
        player2.GetComponent<HingeJoint>().connectedBody = lastSpawnned.GetComponent<Rigidbody>();
        ropeCnt = true;
     
    }

}

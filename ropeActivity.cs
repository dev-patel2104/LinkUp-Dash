using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ropeActivity : MonoBehaviour
{

    rope r;
    void Start()
    {
        r = FindObjectOfType<rope>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Danger"))
        {
            r.DestroyRope();
        }
    }
}

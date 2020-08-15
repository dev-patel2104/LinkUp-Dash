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

    private IEnumerator OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Danger"))
        {
            yield return new WaitForSeconds(0.5f);
            r.DestroyRope();
        }
    }
}

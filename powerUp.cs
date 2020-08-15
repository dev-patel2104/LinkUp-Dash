using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class powerUp : MonoBehaviour
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

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player") && this.gameObject.CompareTag("Resistor"))
        {
            Debug.Log("we are inside the resistor");
            r.DestroyRope();
            Destroy(this.gameObject);
        }
    }
}

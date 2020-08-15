using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{ 
    void Start()
    {
       
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            Debug.Log("You entered a checkpoint");
            other.GetComponent<PlayerMovement>().lastCheckPoint = transform.position;
            Material mat = GetComponentInChildren<MeshRenderer>().material;
            mat.SetColor("_EmissionColor", Color.blue);
            mat.EnableKeyword("_EMISSION");
        }
    }
}

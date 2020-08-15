using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class magnet : MonoBehaviour
{
    [Range(0f, 20f)] [SerializeField] float upBoost = 12f;
    

    void Start()
    {
            
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(transform.localRotation.x);
    }


    private void OnTriggerEnter(Collider other)
    {
          if (other.gameObject.CompareTag("Player") && transform.rotation.x == 1)
          {
              Debug.Log("we are going up");
              other.gameObject.GetComponent<Rigidbody>().AddForce(0f, upBoost, 0f, ForceMode.Impulse);
          }

          else if(other.gameObject.CompareTag("Player") && transform.rotation.x == 0)
          {
              Debug.Log("We are going down");
              other.gameObject.GetComponent<Rigidbody>().AddForce(0f, -upBoost + (upBoost / 2), 0f, ForceMode.Impulse);
          } 
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            Destroy(other.gameObject);
        }
    }
}

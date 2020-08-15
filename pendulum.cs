using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pendulum : MonoBehaviour
{
    [SerializeField] Quaternion startAngle;
    [SerializeField] Quaternion endAngle;
    [SerializeField] float speed = 10f;
    [SerializeField] float startTime;
    [SerializeField] float period;

    Quaternion temp;

    void Start()
    {
        
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (startAngle.x != 0f && endAngle.x != 0)
        {
            //Debug.Log("Inside if");
            transform.rotation = Quaternion.Lerp(startAngle, endAngle, speed * startTime * Mathf.Sin((2 * Mathf.PI) * (Time.frameCount / period)));
        }

        else if(Time.time >= startTime)
        {
            //Debug.Log("Inside else");
            transform.Rotate(Vector3.right * Time.deltaTime * speed * speed);
        }
    }

    

    
   

  
    
    
    
}

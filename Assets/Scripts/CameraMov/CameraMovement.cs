using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CameraMovement : Singleton<CameraMovement>
{
    public Vector3 target;
    void Start()
    {
        target = gameObject.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if(target != gameObject.transform.position)
        {
            transform.position = Vector3.Lerp(transform.position, target, 5* Time.deltaTime);
            //transform.position = Vector3.MoveTowards(transform.position, target, 10f * Time.deltaTime);
        }
        
    }

   
}

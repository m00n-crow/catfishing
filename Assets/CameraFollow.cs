using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.Find("haken").transform;
    }
    public Transform target;

    public Vector3 offset = new Vector3(0, 2, -2);
   
    // Update is called once per frame
    private void LateUpdate() 
 
    {
        transform.position = target.position + offset;
    }
}

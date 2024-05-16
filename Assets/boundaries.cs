using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//sollten eigentlich die Grenzen des Spielfeldes sein, funktioniert aber nicht?? : border eigentlich eingetragen. evtl. falsch verstanden... 
public class StayInside : MonoBehaviour
{
    // Start is called before the first frame update
    void Update()
    {
       transform.position = new Vector3(Mathf.Clamp(transform.position.x, -1f, 5760f), 
        Mathf.Clamp(transform.position.y, -2760f, 1500f), transform.position.z);
        
    }

}

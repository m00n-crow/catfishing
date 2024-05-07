using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wurf : MonoBehaviour
{
   Vector3 throwVector;
   Rigidbody2D _rb;
   LineRenderer _lr;
   void Awake()
    {
         _rb = GetComponent<Rigidbody2D>();
        _lr = GetComponent<LineRenderer>();
     }
    //on Mouse event possible  tahnks to monobehaviour + collider2d

    void OnMouseDown()
    {
    CalculateThrowVector();
    SetArrow();
    }

    void CalculateThrowVector()
    
    {

        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //doing vector2 math to ignore z axis in our distance.
        Vector2 distance = mousePos - this.transform.position;
        //dont normalize the distance, we want the thorw strenght to vary
        throwVector = -distance.normalized*100;
    }
    void SetArrow()
    {
        _lr.positionCount = 2;
        _lr.SetPosition(0, Vector3.zero);
        _lr.SetPosition(1, throwVector.normalized/2);
        _lr.enabled = true;
    }
    void OnMouseUp()
    {
        RemoveArrow();
        Throw();
    }
    void RemoveArrow()
    {
        _lr.enabled = false;
    }
    public void Throw()
    {
        _rb.AddForce(throwVector);
    }

}

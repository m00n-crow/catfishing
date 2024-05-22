using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Throwable : MonoBehaviour
{
    Vector3 throwVector;
    Rigidbody2D _rb;
    LineRenderer _lr;
    bool isDragging = false;
    bool hasThrown = false; // überprüft ob der Hacken schon geworfen wurde 
    
    //public bool wasdSteuerung = false;



    // Bestimmt wie schnell der Pfeil ausgemalt werden soll 
    public const int multiplier = 100; // MUSS ein int sein, warum auch immer, kann wegen const noch nicht geändert werden! ToDo
    
    // Bestimmt wie schnell der Pfeil maximal geworfen werden soll  
    public const float maxThrowDistance = 2; // Muss wieder ein const sein, sonst wird es überschrieben!

    // Bestimmt die Länge des Pfeiles -> Höherer Wert = kleinerer Pfeil 
    public const int arrowLength = 10; // Dies kann auch ein const bleiben



    void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _lr = GetComponent<LineRenderer>();
    }

    void OnMouseDown()
    {
        if (hasThrown) return; // Wenn der Hacken schon geworfen wurde, dann wird nichts gemacht
        isDragging = true;
        CalculateThrowVector();
        SetArrow();
    }

    void OnMouseUp()
    {
        if (hasThrown) return; 
        RemoveArrow();
        Throw();
    }

    void Update()
    {
        if (isDragging && !hasThrown)
        {
            CalculateThrowVector();
            SetArrow();
        }

        if (hasThrown && transform.position.y < -2) 
        {
            // wasdSteuerung = true;
            DisableGravity();
        }
    }

    void DisableGravity()
    {   
        _rb.gravityScale = 0.5f;
        _rb.velocity = new Vector2(_rb.velocity.y, 0f); // Setze die y-Komponente der Geschwindigkeit auf 0
        
    }

    // bestimmt die Weite 
    void CalculateThrowVector()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0; // Ensure z value is zero for 2D calculations
        Vector2 distance = mousePos - transform.position;
        
        Debug.Log("maxThrowDistance value: " + maxThrowDistance);
        if (distance.magnitude > maxThrowDistance)
        {
                distance = distance.normalized * maxThrowDistance;
        }

        throwVector = -distance * multiplier; 
        
        Debug.Log("Multiplier value: " + multiplier);
        //throwVector = -distance * multiplier; // Increase factor for stronger throws
    }

    // bestimmt die länge des Pfeiles 
    void SetArrow()
    {
        _lr.positionCount = 2;
        _lr.SetPosition(0, transform.position); // Start of the arrow at the object's position
        _lr.SetPosition(1, transform.position + throwVector / arrowLength); // Ende des Vektors, + vektor um entgegengesetzt
        _lr.enabled = true;
    }

    void RemoveArrow()
    {
        _lr.enabled = false;
    }

    public void Throw()
    {
        if (hasThrown) return; // Prevent further interactions
        _rb.AddForce(throwVector);
        _rb.gravityScale = 0.1f; // Enable gravity 

        hasThrown = true; // Mark als geworfen 
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//die kamera sollte dem objekt folgen, an das sie angehängt ist (haken)

public class CameraFollow : MonoBehaviour
{
    // Das Ziel, dem die Kamera folgen soll
    public Transform target;
    // Offset der Kamera relativ zum Ziel
    public Vector3 offset = new Vector3(0, 2, -2);
    // Grenzen der Welt
    public Vector2 minPosition;
    public Vector2 maxPosition;

    // Start wird aufgerufen, bevor das erste Frame-Update erfolgt
    void Start()
    {
        if (target == null)
        {
            // Finde das Ziel, falls es nicht im Inspektor gesetzt ist
            target = GameObject.Find("haken").transform;
        }
    }

    // LateUpdate wird nach allen anderen Updates aufgerufen
    void LateUpdate()
    {
        // Berechne die gewünschte Position der Kamera
        Vector3 desiredPosition = target.position + offset;

        // Begrenze die Kamera innerhalb der Weltgrenzen
        desiredPosition.x = Mathf.Clamp(desiredPosition.x, minPosition.x, maxPosition.x);
        desiredPosition.y = Mathf.Clamp(desiredPosition.y, minPosition.y, maxPosition.y);

        // Setze die Kamera-Position
        transform.position = desiredPosition;
    }
}
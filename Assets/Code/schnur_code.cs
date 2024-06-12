using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// für visuelle Darstellung der Schnur
public class Schnur : MonoBehaviour
{
    public Transform Haken;

    private LineRenderer LineRenderer;
    // public Color lineColor = Color.white;

    void Start()
    {
        LineRenderer = GetComponent<LineRenderer>();
        if (LineRenderer == null)
        {
            Debug.LogError("LineRenderer nicht gefunden!");
            return;
        }

        if (Haken == null)
        {
            Debug.LogError("Haken Transform ist nicht zugewiesen!");
            return;
        }

        LineRenderer.positionCount = 2; // start und end- punkt
    }

    void Update()
    {
        if (Haken == null)
        {
            Debug.LogError("Haken Transform ist nicht zugewiesen!");
            return;
        }

        Vector3 startPosition = new Vector3(-3.1f, -0.1f, 0f);
        Vector3 endPosition = new Vector3(Haken.position.x, Haken.position.y, 0);

        LineRenderer.SetPosition(0, startPosition);
        LineRenderer.SetPosition(1, endPosition);
    }
}
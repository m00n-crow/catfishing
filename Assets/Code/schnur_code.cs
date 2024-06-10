using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//für visuelle Darstellung der schnur

public class Schnur : MonoBehaviour
{
    public Transform Angel;
    public Transform Haken;

    private LineRenderer LineRenderer;
   //  public Color lineColor = Color.white;

    void Start()
    {
        LineRenderer = GetComponent<LineRenderer>();
        LineRenderer.positionCount = 2; //start und end- punkt
    }

    void Update()
    {
        Vector3 startPosition = new Vector3(-3.1f, -0.1f, 0f);
        Vector3 endPosition = new Vector3(Haken.position.x, Haken.position.y, 0);

        LineRenderer.SetPosition(0, startPosition);
        LineRenderer.SetPosition(1, endPosition);

    }
}

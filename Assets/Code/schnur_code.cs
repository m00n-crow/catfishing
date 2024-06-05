using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//für visuelle Darstellung der schnur

public class Schnur : MonoBehaviour
{
    public Transform Angel;
    public Transform Haken;

    private LineRenderer LineRenderer;

    void Start()
    {
        LineRenderer = GetComponent<LineRenderer>();
        LineRenderer.positionCount = 2; //start und end- punkt
    }

    void Update()
    {
        LineRenderer.SetPosition(0, Angel.position);
        LineRenderer.SetPosition(1, Haken.position);
    }
}

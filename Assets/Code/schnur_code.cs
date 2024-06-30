using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// für visuelle Darstellung der Schnur
public class Schnur : MonoBehaviour
{
    private Transform _haken;
    private LineRenderer _lineRendererSchnur;
    private bool _isHakenNull;
    public Color lineColor;

    void Awake()
    {
        _haken = GameObject.Find("haken").GetComponent<Transform>();
        _isHakenNull = _haken == null;
        _lineRendererSchnur = GetComponent<LineRenderer>();
    }
    
    void Start()
    {
        if (_lineRendererSchnur != null)
        {
            _lineRendererSchnur.positionCount = 2; // start und end- punkt
            _lineRendererSchnur.material.color = lineColor;
            Debug.Log("Schnur wurden angebracht!");
        }

        if (_isHakenNull)
        {
            Debug.LogError("Haken wurde nicht gefunden.");
        }
    }

    void Update()
    {
        Vector3 startPosition = new Vector3(-3.1f, -0.1f, 0f);
        Vector3 endPosition = new Vector3(_haken.position.x, _haken.position.y, 0);

        _lineRendererSchnur.SetPosition(0, startPosition);
        _lineRendererSchnur.SetPosition(1, endPosition);
    }
}
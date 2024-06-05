using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//um realistische bewegungen der schnur zu erzeugen
public class Spring_joint_skript : MonoBehaviour

{
	public Transform Angel;
    public Transform Haken;
private LineRenderer LineRenderer;

void Start()
{
    LineRenderer = GetComponent<LineRenderer>();
    LineRenderer.positionCount = 2; //start und end punkt
    
}
	
	void Update()
	{ 
		LineRenderer.SetPosition(0, Angel.position);
		LineRenderer.SetPosition(1, Haken.position);
	}
}



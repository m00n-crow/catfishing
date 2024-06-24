using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BiteAlertController : MonoBehaviour
{
    public GameObject biteAlert;

    void Start()
    {
        // der Alert ist erst nicht sichtbar 
        biteAlert.SetActive(false);
    }

    public void ShowAlert()
    {
        biteAlert.SetActive(true);
    
    }

    public void HideAlert()
    {
        biteAlert.SetActive(false);
        
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BiteAlertController : MonoBehaviour
{
    public GameObject biteAlert;

    void Start()
    {
        // Sicherstellen, dass der Alert zunächst nicht sichtbar ist
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
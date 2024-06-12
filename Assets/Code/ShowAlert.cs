using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Beispielscript, das erkennt, wenn ein Fisch anbeißt
public class FishBiteDetector : MonoBehaviour
{
    public BiteAlertController biteAlertController;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Fish"))
        {
            biteAlertController.ShowAlert();
            // Hier kannst du auch den Zustand setzen, dass ein Fisch angebissen hat
            hasFishBite = true;
        }
    }

    private bool hasFishBite = false;

    void Update()
    {
        // Wenn der Spieler linksklickt und ein Fisch angebissen hat
        if (Input.GetMouseButtonDown(0) && hasFishBite)
        {
            // Fisch einholen und Alert verstecken
            CatchFish();
            biteAlertController.HideAlert();
            hasFishBite = false;
        }
    }

    void CatchFish()
    {
        // Hier kannst du die Logik zum Einholen des Fisches hinzufügen
        Debug.Log("Fisch gefangen!");
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BISSPROBABILITY : MonoBehaviour
{
   [Range(0f, 1f)]
    public float catchProbability = 0.5f; // 50% Wahrscheinlichkeit standardmäßig

    void OnTriggerEnter2D(Collider2D other)
    {
        // Überprüfen, ob das andere Objekt der Haken ist
        if (other.CompareTag("Hook"))
        {
            // Generiere eine Zufallszahl zwischen 0 und 1
            float randomValue = Random.Range(0f, 1f);

            // Überprüfe, ob die Zufallszahl kleiner als die Fangwahrscheinlichkeit ist
            if (randomValue <= catchProbability)
            {
                // Logik, was passiert, wenn der Fisch den Haken berührt und gefangen wird
                Debug.Log("Fish caught!");
                // Hier kannst du weitere Aktionen hinzufügen, z.B. den Fisch an den Haken binden,
                // den Fang zählen, die Fischbewegung stoppen, etc.
            }
            else
            {
                // Logik, was passiert, wenn der Fisch nicht gefangen wird
                Debug.Log("Fish escaped!");
            }
        }
    }
}
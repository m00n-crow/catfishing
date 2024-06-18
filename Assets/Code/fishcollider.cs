using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Fish : MonoBehaviour
{
    public Transform hook;  // Referenz zum Haken
    public float sightRadius = 10f;  // Sichtbereich des Fisches
    public float biteRadius = 1f;  // Radius, innerhalb dessen der Fisch anbeißt

    private NavMeshAgent agent;  // Für die Bewegung des Fisches

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        float distanceToHook = Vector3.Distance(transform.position, hook.position);

        if (distanceToHook < sightRadius)
        {
            // Fisch sieht den Haken und bewegt sich darauf zu
            agent.SetDestination(hook.position);
        }

        if (distanceToHook < biteRadius)
        {
            // Fisch beißt an
            Bite();
        }
    }

    void Bite()
    {
        Debug.Log("Fisch hat angebissen!");
        // Hier kannst du weitere Logik hinzufügen, z.B. Punkte vergeben oder den Fisch verschwinden lassen
    }

    private void OnDrawGizmosSelected()
    {
        // Zeichnet den Sichtbereich und den Anbeißradius im Editor
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sightRadius);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, biteRadius);
    }
}
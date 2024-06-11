using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishMovement : MonoBehaviour
{
    
    public float speed = 2f; // Geschwindigkeit der Fische

    private Vector2 direction; // Bewegungsrichtung
    private Bounds bounds; // Begrenzung des Bereichs

    void Start()
    {
        // Get the spawnFishies component and initialize the movement bounds
        var spawnFishiesScript = fishSpawnerObject.GetComponent<spawnFishies>();
        bounds = spawnFishiesScript.GetSpawnBounds();

        // Zufällige Anfangsrichtung
        direction = GetRandomDirection();
    }

    void Update()
    {
        // Bewegung des Fisches
        MoveFish();

        // Überprüfen, ob der Fisch den Rand erreicht hat
        CheckBounds();
    }

    private void MoveFish()
    {
        // Bewegung basierend auf Richtung und Geschwindigkeit
        transform.Translate(direction * speed * Time.deltaTime);
    }

    private void CheckBounds()
    {
        Vector2 position = transform.position;

        // Wenn der Fisch den Rand des Bereichs erreicht, Richtung ändern
        if (position.x < bounds.min.x || position.x > bounds.max.x)
        {
            direction.x = -direction.x;
        }
        if (position.y < bounds.min.y || position.y > bounds.max.y)
        {
            direction.y = -direction.y;
        }

        // Sicherstellen, dass der Fisch innerhalb der Begrenzung bleibt
        position.x = Mathf.Clamp(position.x, bounds.min.x, bounds.max.x);
        position.y = Mathf.Clamp(position.y, bounds.min.y, bounds.max.y);
        transform.position = position;
    }

    private Vector2 GetRandomDirection()
    {
        // Zufällige Bewegungsrichtung erzeugen
        float angle = Random.Range(0f, 2f * Mathf.PI);
        return new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishMovement : MonoBehaviour
{
    public float speed = 2f; // Geschwindigkeit der Fische

    // Spezifische Höhenbereiche für jeden Fisch
    public float borderMinY = -100f;
    public float borderMaxY = -2f;

    // Horizontale Grenzen (falls diese auch variieren sollen, können sie ebenfalls individuell gemacht werden)
    public float borderMinX = 0;
    public float borderMaxX = 100f;

    private Vector2 direction; // Bewegungsrichtung

    void Start()
    {
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
        if (position.x < borderMinX || position.x > borderMaxX)
        {
            direction.x = -direction.x;
        }
        if (position.y < borderMinY || position.y > borderMaxY)
        {
            direction.y = -direction.y;
        }

        // Sicherstellen, dass der Fisch innerhalb der Begrenzung bleibt
        position.x = Mathf.Clamp(position.x, borderMinX, borderMaxX);
        position.y = Mathf.Clamp(position.y, borderMinY, borderMaxY);
        transform.position = position;
    }

    private Vector2 GetRandomDirection()
    {
        // Zufällige Bewegungsrichtung erzeugen
        float angle = Random.Range(0f, 2f * Mathf.PI);
        return new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));
    }
}

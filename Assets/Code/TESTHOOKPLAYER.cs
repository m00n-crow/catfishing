using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class HookPlayerScript : MonoBehaviour
{
    public float speed = 2.0f;
    public bool isFishNear = false;
    public GameObject nearestFish;

    void Update()
    {
        // Haken bewegen
        MoveHook();

        // Fisch angeln
        if (isFishNear && Input.GetMouseButtonDown(0))
        {
            CatchFish();
        }
    }

    void MoveHook()
    {
        // Bewegungscode hier hinzufügen (zum Beispiel mit Pfeiltasten)
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector2 movement = new Vector2(moveHorizontal, moveVertical);
        transform.Translate(movement * speed * Time.deltaTime);
    }

    void CatchFish()
    {
        // Ziehe den Fisch hoch (hier kannst du deine Logik einfügen, z.B. den Fisch entfernen und Punkte hinzufügen)
        if (nearestFish != null)
        {
            Destroy(nearestFish);
            isFishNear = false;
            nearestFish = null;
            Debug.Log("Fisch geangelt!");
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Fish"))
        {
            Debug.Log("Fisch in der Nähe");
            isFishNear = true;
            nearestFish = other.gameObject;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Fish"))
        {
            Debug.Log("Fisch weg");
            isFishNear = false;
            nearestFish = null;
        }
    }
}
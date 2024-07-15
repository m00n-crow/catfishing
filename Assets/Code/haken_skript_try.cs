using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class haken_skript_try : MonoBehaviour
{
    public bool isFishNear = false;
    public GameObject nearestFish;

    // Define the minimum y-position limit for the hook
    public float minYPosition = -47f;

    void Start()
    {
        minYPosition = -47f;
    }

    void FixedUpdate()
    {
        // Restrict the movement of the hook to the minYPosition limit
        if (transform.position.y < minYPosition)
        {
            // If the hook's y-position is below the limit, set it to the limit
            transform.position = new Vector3(transform.position.x, minYPosition, transform.position.z);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Fish"))
        {
            isFishNear = true;
            nearestFish = other.gameObject;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Fish"))
        {
            isFishNear = false;
            nearestFish = null;
        }
    }
}
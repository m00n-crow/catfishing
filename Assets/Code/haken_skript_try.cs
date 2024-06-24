using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class haken_skript_try : MonoBehaviour

{
    public bool isFishNear = false;
    public GameObject nearestFish;

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
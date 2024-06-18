using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishScript : MonoBehaviour
{
    public float speed = 2.0f;
    private HookScript hookScript;

    void Update()
    {
        if (hookScript != null && hookScript.isFishNear)
        {
            Vector3 direction = (hookScript.transform.position - transform.position).normalized;
            transform.position += direction * speed * Time.deltaTime;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Hook"))
        {
            hookScript = other.GetComponent<HookScript>();
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Hook"))
        {
            hookScript = null;
        }
    }
}
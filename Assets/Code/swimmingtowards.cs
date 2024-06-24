using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class swimmingtowards : MonoBehaviour
{
    public float speed = 2.0f;
    private haken_skript_try haken_skript_try;

    void Update()
    {
        if (haken_skript_try != null && haken_skript_try.isFishNear)
        {
            Vector3 direction = (haken_skript_try.transform.position - transform.position).normalized;
            transform.position += direction * speed * Time.deltaTime;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Hook"))
        {
            haken_skript_try = other.GetComponent<haken_skript_try>();
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Hook"))
        {
            haken_skript_try = null;
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionController : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.name == "cat") {
            Debug.Log("Playyyyer entered " + gameObject.name);
            Rigidbody2D rb = GetComponent<Rigidbody2D>();
            if(rb != null) {
                rb.velocity = Vector2.zero;
            }
        }
    }
}

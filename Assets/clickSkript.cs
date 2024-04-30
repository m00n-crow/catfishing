using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    public Vector3 mousePos;
    public Camera mainCamera;
    public Vector3 mousePosWorld;
    public Vector2 mousePosWorld2D;
    RaycastHit2D hit;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // wurde maus geklickt?

        if (Input.GetMouseButtonDown(0))
        {

        //  erkannt, dass die Maus geklickt wurde
        print("Maus wurde geklickt");
        //maus position auslesen
        mousePos = Input.mousePosition;
        //Mausposition auf Konsole ausgeben
        print("Screen Space: " + mousePos);

        //koordinaten von screen space to world space umrechnen
        mousePosWorld = mainCamera.ScreenToWorldPoint(mousePos);
        //world space koordinaten auf unity konsole ausgeben
        print("World Space: " + mousePosWorld);
        //Ummwandlung von Vector 3 in Vector 2 
        mousePosWorld2D = new Vector2(mousePosWorld.x, mousePosWorld.y);

        //Raycast 2D = Hit abspeichern
        hit = Physics2D.Raycast(mousePosWorld2D, Vector2.zero);
        }

        //Überprüfe, ob hit einen collider beinhaltet
        if (hit.collider != null)
        {
            print("objekt mit collider wurde getroffen!");
            //Ausgabe des gettroffenen Game Objekts (name)
            print("Name: " + hit.collider.gameObject.name);
        }
        else
        {
            print("Kein Hit");
        }
    }
}

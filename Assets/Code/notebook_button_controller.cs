using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopController : MonoBehaviour
{
    public GameObject shopCanvas;
    bool opened = false;

    // Funktion zum Öffnen des Shops
    public void OpenShop()
    {
        if (!opened)
        {
            shopCanvas.SetActive(true);
            opened = true; 
        }
        else
        {
            shopCanvas.SetActive(false);
            opened = false;

        }    
    }

    // Funktion zum Schließen des Shops
    public void CloseShop()
    {
        shopCanvas.SetActive(false);
    }
}

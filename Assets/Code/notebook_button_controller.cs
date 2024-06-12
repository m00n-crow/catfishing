using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopController : MonoBehaviour
{
    public GameObject shopCanvas;

    // Funktion zum Öffnen des Shops
    public void OpenShop()
    {
        shopCanvas.SetActive(true);
    }

    // Funktion zum Schließen des Shops
    public void CloseShop()
    {
        shopCanvas.SetActive(false);
    }
}

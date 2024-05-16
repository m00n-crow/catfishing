using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


//gibt text für Preis und Menge aus bzw macht deren buttons sichtbar/clickbar
public class ButtonInfo : MonoBehaviour
{
 
 public int ItemID;
    public Text PriceTxt;
    public Text QuantityTxt;
    public GameObject ShopManager;

    void Update()
    {
        PriceTxt.text = "Price: CC" + ShopManager.GetComponent<ShopManagerScript>().shopItems[2, ItemID].ToString();
        QuantityTxt.text = ShopManager.GetComponent<ShopManagerScript>().shopItems[3, ItemID].ToString();
    }
}

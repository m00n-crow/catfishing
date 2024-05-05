using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ShopManagerScript : MonoBehaviour
{
   public int[,] shopItems = new int[5, 5];
   public float coins;
   public Text CoinsTXT;
    void Start()
    {

        CoinsTXT.text = "Coins: " + coins.ToString();

        shopItems[1,1] = 1;
        shopItems[1,2] = 2;
        shopItems[1,3] = 3;
        shopItems[1,4] = 4;

        //price

        shopItems[1,1] = 10;
        shopItems[1,2] = 15;
        shopItems[1,3] = 20;
        shopItems[1,4] = 25;

        //quantity

        shopItems[3,1] = 1;
        shopItems[3,2] = 2;
        shopItems[3,3] = 3;
        shopItems[3,4] = 4;

    }

   
    public void Buy()
    {
        GameObject ButtonRef = GameObject.FindGameObjectWithTag("Event").GetComponent<EventSystem>().currentSelectedGameObject;

        if (coins >= shopItems[2, ButtonRef.GetComponent<ButtonInfo>().ItemID])
        {
            coins -= shopItems[2, ButtonRef.GetComponent<ButtonInfo>().ItemID];
            shopItems[3, ButtonRef.GetComponent<ButtonInfo>().ItemID]++;
            CoinsTXT.text = "Coins: " + coins.ToString();
            ButtonRef.GetComponent<ButtonInfo>().QuantityTxt.text = shopItems[3, ButtonRef.GetComponent<ButtonInfo>().ItemID].ToString();

        }
    }
}

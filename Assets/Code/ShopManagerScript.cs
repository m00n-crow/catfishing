using UnityEngine;
using UnityEngine.UI; // Für normale UI-Text
using TMPro; // Für TextMeshPro
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class ShopManagerScript : MonoBehaviour
{
    public int[,] shopItems = new int[5, 5];
    public float coins;
    public Text CoinsTXT;

    public Image[] fishImages; // Array der Fisch-Icons für das Inventar
    public TextMeshProUGUI[] fishCounters; // Array der Textfelder für die Counter
    public GameObject fishDetailsPanel; // Panel für Details
    public Text fishDescription; // Textfeld für die Beschreibung
    public Image hiddenFishSlot; // Slot für den versteckten Fisch

    private Dictionary<int, int> fishInventory = new Dictionary<int, int>(); // Inventar

    void Start()
    {
        CoinsTXT.text = "Coins: " + coins.ToString();

        shopItems[1, 1] = 1;
        shopItems[1, 2] = 2;
        shopItems[1, 3] = 3;
        shopItems[1, 4] = 4;

        // Preise
        shopItems[2, 1] = 10;
        shopItems[2, 2] = 15;
        shopItems[2, 3] = 20;
        shopItems[2, 4] = 25;

        // Menge
        shopItems[3, 1] = 1;
        shopItems[3, 2] = 2;
        shopItems[3, 3] = 3;
        shopItems[3, 4] = 4;

        // Set hidden fish slot inactive initially
        hiddenFishSlot.gameObject.SetActive(false);

        // Initialize fish inventory counters
        for (int i = 0; i < fishImages.Length; i++)
        {
            fishInventory[i] = 0;
            fishImages[i].gameObject.SetActive(false); // Alle Fisch-Images initial deaktivieren
        }
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

            // Update inventory
            int fishID = ButtonRef.GetComponent<ButtonInfo>().ItemID;
            UpdateInventory(fishID);
        }
    }

    private void UpdateInventory(int fishID)
    {
        fishInventory[fishID]++;
        fishImages[fishID].gameObject.SetActive(true); // Fisch-Image aktivieren
        fishCounters[fishID].text = fishInventory[fishID].ToString();
    }

    public void ShowFishDetails(int fishID)
    {
        fishDetailsPanel.SetActive(true);
        fishDescription.text = "Description for fish " + fishID;
    }

    public void HideFishDetails()
    {
        fishDetailsPanel.SetActive(false);
    }

    public void CatchSpecialFish(int fishID)
    {
        hiddenFishSlot.gameObject.SetActive(true);
        UpdateInventory(fishID);
    }
}

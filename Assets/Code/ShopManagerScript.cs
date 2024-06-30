using UnityEngine;
using UnityEngine.UI; // Für normale UI-Text
using TMPro; // Für TextMeshPro
using System.Collections.Generic;

public class ShopManagerScript : MonoBehaviour
{
    public int[,] shopItems = new int[5, 5];
    // public float coins;
    // public Text CoinsTXT;

    public Image[] fishImages; // Array der Fisch-Icons für das Inventar
    public GameObject[] greyFishSprites; // Array des grauen Fisch Sprite GameObjects damit wir SetActive nutzen können
    public Sprite[] discoveredFishSprites; // Array der entdeckten Fisch-Sprites
    public TextMeshProUGUI[] fishCounters; // Array der Textfelder für die Counter
    public GameObject fishDetailsPanel; // Panel für Details
    public Text fishDescription; // Textfeld für die Beschreibung
    public Image hiddenFishSlot; // Slot für den versteckten Fisch

    public Dictionary<int, int> fishInventory = new Dictionary<int, int>(); // Inventar
    private string[] fishDescriptions = new string[] // Beispielbeschreibungen
    {
        "Description for fish 1",
        "Description for fish 2",
        "Description for fish 3",
        "Description for fish 4"
    };

    private HandleCatchInventory _HandleCatchInventoryScript;

    void Start()
    {
        // //CoinsTXT.text = "Coins: " + coins.ToString();
        // shopItems[1, 1] = 1;
        // shopItems[1, 2] = 2;
        // shopItems[1, 3] = 3;
        // shopItems[1, 4] = 4;
        //
        // // Preise
        // shopItems[2, 1] = 10;
        // shopItems[2, 2] = 15;
        // shopItems[2, 3] = 20;
        // shopItems[2, 4] = 25;
        //
        // // Menge
        // shopItems[3, 1] = 1;
        // shopItems[3, 2] = 2;
        // shopItems[3, 3] = 3;
        // shopItems[3, 4] = 4;

        // Set hidden fish slot inactive initially
        hiddenFishSlot.gameObject.SetActive(false);

        _HandleCatchInventoryScript =
            GameObject.Find("GameObjectForHandleCatchInventory").GetComponent<HandleCatchInventory>();

        SetupFishInventory();
    }

    // public void Buy()
    // {
    //     // GameObject ButtonRef = GameObject.FindGameObjectWithTag("Event").GetComponent<EventSystem>().currentSelectedGameObject;
    //
    //     // if (coins >= shopItems[2, ButtonRef.GetComponent<ButtonInfo>().ItemID])
    //     // {
    //     //     coins -= shopItems[2, ButtonRef.GetComponent<ButtonInfo>().ItemID];
    //     //     shopItems[3, ButtonRef.GetComponent<ButtonInfo>().ItemID]++;
    //     //     CoinsTXT.text = "Coins: " + coins.ToString();
    //     //     ButtonRef.GetComponent<ButtonInfo>().QuantityTxt.text = shopItems[3, ButtonRef.GetComponent<ButtonInfo>().ItemID].ToString();
    //     //
    //     //     // Update inventory
    //     //     int fishID = ButtonRef.GetComponent<ButtonInfo>().ItemID;
    //     //     UpdateInventory(fishID);
    //     // }
    // }
    
    
    // Auskommentiert weil das Update hier nicht benutzt wird sondern über das HandleCatchInventory ausgeführt wird.
    // private void UpdateInventory(int fishID)
    // {
    //     fishInventory[fishID]++;
    //     fishImages[fishID].sprite = discoveredFishSprites[fishID]; // Set the discovered fish sprite
    //     fishCounters[fishID].text = fishInventory[fishID].ToString();
    //     greyFishSprites[fishID].SetActive(false);
    //     Debug.Log($"Fish ID {fishID} updated: Count = {fishInventory[fishID]}");
    // }

    public void ShowFishDetails(int fishID)
    {
        fishDetailsPanel.SetActive(true);
        fishDescription.text = fishDescriptions[fishID];
    }

    public void HideFishDetails()
    {
        fishDetailsPanel.SetActive(false);
    }

    public void CatchSpecialFish(int fishID)
    {
        hiddenFishSlot.gameObject.SetActive(true);
        _HandleCatchInventoryScript.UpdateCatchInventory(hiddenFishSlot.name);
    }

    public void SetupFishInventory()
    {
        // Initialize fish inventory counters and set grey images
        for (int i = 0; i < fishImages.Length; i++)
        {
            fishInventory[i] = 0;
            fishImages[i].sprite = discoveredFishSprites[i]; // Set sprites
            fishImages[i].gameObject.SetActive(true); // Make sure the image is active
            greyFishSprites[i].SetActive(true); // Cover up with the grey clones
            Debug.Log($"Fish Image {i} initialized with {discoveredFishSprites[i].name}.");
        }
    }
}

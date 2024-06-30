using System.Collections;
using UnityEngine;

public class HandleCatchInventory : MonoBehaviour
{
    public ShopManagerScript shopManager;
    private string _fishNameCleaned;
    private int _confirmedFishID;
    private int _discoveredFishSpritesListLength;

    void Start()
    {
        _discoveredFishSpritesListLength = shopManager.discoveredFishSprites.Length;
    }

    // Find out what fish is on the hook
    public void UpdateCatchInventory(string fishName)
    {
        _fishNameCleaned = RemoveCloneName(fishName);
        Debug.Log($"Trimmed name was {fishName} and is now {_fishNameCleaned}");
        
        _confirmedFishID = ConfirmFishIndexID(_fishNameCleaned);
        Debug.Log($"FishID for catch is {_confirmedFishID}");
        
        UpdateInventory(_confirmedFishID, _fishNameCleaned);
    }
    
    
    string RemoveCloneName(string str)
    {
        return str.Replace("(Clone)", "");
    }

    int ConfirmFishIndexID(string fishNameCleaned)
    {
        int i = 0;
        int fishID = -1;
        for (; i < _discoveredFishSpritesListLength; i++)
        {
            if (shopManager.discoveredFishSprites[i].name == fishNameCleaned)
            {
                fishID = i;
                break;
            }
        }

        return fishID;
    }
    
    
    // Update the Inventory UI to reflect your catch
    public void UpdateInventory(int fishID, string fishName)
    {
        // Wenn der Fisch gefunden wurde
        if (fishID != -1)
        {
            // Aktualisiere das Inventar
            shopManager.fishInventory[fishID]++;
            shopManager.fishImages[fishID].sprite = shopManager.discoveredFishSprites[fishID]; // Setze den entdeckten Fisch-Sprite
            shopManager.fishCounters[fishID].text = shopManager.fishInventory[fishID].ToString();

            if (shopManager.fishInventory[fishID] == 1)
            {
                shopManager.greyFishSprites[fishID].SetActive(false); // Disable den grauen Fish Sprite beim ersten Count Up
            }
            
            Debug.Log($"Fisch {fishName} (ID: {fishID}) gefangen. Neuer Zähler: {shopManager.fishInventory[fishID]}");
        }
        else
        {
            Debug.Log($"{fishName}");
        }
    }
}

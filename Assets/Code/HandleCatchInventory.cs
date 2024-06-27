using UnityEngine;
using TMPro; // Für TextMeshPro

public class HandleCatchInventory : MonoBehaviour
{
    public ShopManagerScript shopManager;
   
    public void UpdateCatchInventory(string fishName)
    {
        // Entferne "(Clone)" aus dem Fisch-Namen
        if (fishName.Contains("(Clone)"))
        {
            fishName = fishName.Replace("(Clone)", "").Trim();
        }

        // Finde den Fisch-Index basierend auf dem Namen
        int fishID = -1;
        for (int i = 0; i < shopManager.fishImages.Length; i++)
        {
            if (shopManager.fishImages[i].gameObject.name == fishName)
            {
                fishID = i;
                break;
            }
        }

        // Wenn der Fisch gefunden wurde
        if (fishID != -1)
        {
            // Aktualisiere das Inventar
            shopManager.fishInventory[fishID]++;
            shopManager.fishImages[fishID].sprite = shopManager.discoveredFishSprites[fishID]; // Setze den entdeckten Fisch-Sprite
            shopManager.fishCounters[fishID].text = shopManager.fishInventory[fishID].ToString();

            // Debug Log
            Debug.Log($"Fisch {fishName} (ID: {fishID}) gefangen. Neuer Zähler: {shopManager.fishInventory[fishID]}");
        }
        else
        {
            Debug.LogError($"Fisch {fishName} nicht im Shop-Manager gefunden.");
        }
    }
}

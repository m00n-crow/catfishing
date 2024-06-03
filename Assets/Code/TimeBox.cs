using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // Vergiss nicht, dies zu importieren, um UI-Komponenten zu verwenden

public class TimeBox : MonoBehaviour
{
    public Image timeBoxImage; // Das Image-Component der Box
    private int hour;
    // Farben definieren
    private Color dawnColor;
    private Color dayColor;
    private Color duskColor;
    private Color nightColor;

    void Start()
    {
        // Hex-Farbcodes zuweisen
        ColorUtility.TryParseHtmlString("#FFD700", out dawnColor); // PureYellow
        ColorUtility.TryParseHtmlString("#00BFFF", out dayColor); // PureBlue
        ColorUtility.TryParseHtmlString("#FF4500", out duskColor); // PureOrange
        ColorUtility.TryParseHtmlString("#0029b3", out nightColor); // StrongBlue

        // Abonniere die Zeitänderungsereignisse
        TimeManager.OnHourChanged += UpdateBoxColor;
        TimeManager.OnMinuteChanged += UpdateBoxColor;
        
        // Initialisiere die Farbe basierend auf der aktuellen Zeit
        UpdateBoxColor();

        hour = TimeManager.Hour;

    }

    void OnDestroy()
    {
        // Stelle sicher, dass die Ereignisse abgemeldet werden, wenn das Objekt zerstört wird
        TimeManager.OnHourChanged -= UpdateBoxColor;
        TimeManager.OnMinuteChanged -= UpdateBoxColor;
    }

    void UpdateBoxColor()
    {
        // Hier wird die Farbe basierend auf der In-Game-Zeit gesetzt
        hour = TimeManager.Hour;

        if (hour >= 4 && hour < 9)
        { Debug.Log("Wird gelesen");
            timeBoxImage.color = dawnColor;
        }
        else if (hour >= 9 && hour < 18)
        { Debug.Log("Wird gelesen");
            timeBoxImage.color = dayColor;
        }
        else if (hour >= 18 && hour < 22)
        { Debug.Log("Wird gelesen");
            timeBoxImage.color = duskColor;
        }
        else
        { Debug.Log("Wird gelesen");
            timeBoxImage.color = nightColor;
        }
    }
}
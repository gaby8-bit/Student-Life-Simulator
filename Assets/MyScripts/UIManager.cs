using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    private GameManager gameManager;

    [Header("Bari de Statistici")]
    public Slider foameSlider;

    [Header("Text Informații")]
    public TextMeshProUGUI foameText;
    public TextMeshProUGUI energieText; 
    public TextMeshProUGUI invatatText; // Folosim un singur nume clar

    [Header("Ceas")]
    public TextMeshProUGUI timeText;

    void Awake()
    {
        gameManager = Object.FindAnyObjectByType<GameManager>();
        if (foameSlider != null) foameSlider.maxValue = 100f;
        UpdateStatsUI();
    }

    public void UpdateStatsUI() 
    {
        // Folosim direct instanta Singleton pentru a fi siguri de date
        if (GameManager.instance == null) return;

        if (foameSlider != null) foameSlider.value = GameManager.instance.foame;
        if (foameText != null) foameText.text = "Foame: " + Mathf.Floor(GameManager.instance.foame);
        if (energieText != null) energieText.text = "Energie: " + Mathf.Floor(GameManager.instance.energie) + "%";

        // Verificăm dacă variabila invatatText este trasă în Inspector
        
         if (invatatText != null) invatatText.text = "Nivel: " + Mathf.Floor(GameManager.instance.nivelInvatare);
        
    }

    public void UpdateTimeUI(int hours, int minutes)
    {
        if (timeText != null)
            timeText.text = string.Format("{0:00}:{1:00}", hours, minutes);
    }
}
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("Statistici")]
    public float foame = 100f;
    public float energie = 100f;
    public float nivelInvatare = 0f;

    [SerializeField] private float foameDecayAmount = 5f;
    [SerializeField] private float foameDecayInterval = 5f;
    [SerializeField] private float energieDecayAmount = 3f;

    private UIManager uiManager;
    private float hungerTimer;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // Persistenta intre scene
        }
        else
        {
            Destroy(gameObject);
            return;
        }
        InitializeStats();
    }

    // Aceasta este functia pe care o cauta FoodObject.cs
    public void IncreaseFoame(float amount) 
    {
        foame = Mathf.Clamp(foame + amount, 0f, 100f);
        if (uiManager != null) uiManager.UpdateStatsUI();
    }

    public void IncreaseEnergie(float amount)
    {
        energie = Mathf.Clamp(energie + amount, 0f, 100f);
        if (uiManager != null) uiManager.UpdateStatsUI();
    }

    public void Study(float costEnergie, float gainInvatare)
    {
        if (energie >= costEnergie)
        {
            energie -= costEnergie;
            nivelInvatare += gainInvatare;
            if (uiManager != null) uiManager.UpdateStatsUI();
        }
    }

    void Update()
    {
        // Chiar daca UI-ul nu e gasit, stats ar trebui sa scada
        hungerTimer -= Time.deltaTime;
        if (hungerTimer <= 0)
        {
            hungerTimer = foameDecayInterval;
            DecayFoame();
            DecayEnergie();
        }
    }

    void DecayFoame()
    {
        foame = Mathf.Clamp(foame - foameDecayAmount, 0f, 100f);
        // Cautam UI-ul din nou daca am pierdut referinta
        if (uiManager == null) uiManager = FindAnyObjectByType<UIManager>();
        if (uiManager != null) uiManager.UpdateStatsUI();
    }

    void DecayEnergie()
    {
        // Folosim variabila energieDecayAmount care apărea ca fiind nefolosită
        energie = Mathf.Clamp(energie - energieDecayAmount, 0f, 100f);
        if (uiManager != null) uiManager.UpdateStatsUI();
    }

    // --- Restul logicii de Update si OnSceneLoaded din scriptul tau anterior ---
    void InitializeStats()
    {
        uiManager = Object.FindAnyObjectByType<UIManager>();
        if (uiManager != null) uiManager.UpdateStatsUI();
    }
}
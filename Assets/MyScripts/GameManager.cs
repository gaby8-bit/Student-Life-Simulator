using TMPro.EditorUtilities;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Statistici Foame")]
    public float foame = 100f;
    [SerializeField] private float foameDecayAmount = 5f;//Cat scade la fiecare unitate de timp
    [SerializeField] private float foameDecayInterval = 5f;//La cat timp scade
    [SerializeField] private float energieDecayInterval = 10f;//La cat timp scade energia
    [SerializeField] private float energieDecayAmount = 3f;//Cat scade la fiecare unitate de timp
    [SerializeField] private int hoursToSleep = 8;
    [SerializeField] private float energyRestoreAmount = 100f;
    [Header("Statistici Energie")]
    public float energie = 100f;
   

    private UIManager uiManager;
    [Header("Cronometru Foame")]
    private float hungerTimer; // Cronometrul care numără până la 5 secunde
    private bool isInitialized = false; // Flag de verificare

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        // Awake se executa inaintea Start si asigura ca UIManager este gasit rapid
        uiManager = FindAnyObjectByType<UIManager>();
        if (uiManager != null)
        {
            // Apelam prima actualizare o data
            uiManager.UpdateStatsUI();
            isInitialized = true;
        }
        hungerTimer = foameDecayInterval; // Initializam cronometrul cu 5 secunde
    }
    void Start()
    {
        

    }

       void DecayFoame()
    {
        Debug.Log("Scădere Foame declanșată!"); // Linia de debug
        foame -= foameDecayAmount;
         foame = Mathf.Clamp(foame, 0f, 100f);
        if (uiManager != null)
        {
            uiManager.UpdateStatsUI();
        }
    }
    void DecayEnergie()
    {
        Debug.Log("Scădere Energie declanșată!"); // Linia de debug
        energie -= energieDecayAmount;
        energie = Mathf.Clamp(energie, 0f, 100f);
        if (uiManager != null)
        {
            uiManager.UpdateStatsUI();
        }
    }

    public void IncreaseFoame(float amount)
    {
        foame += amount;
        foame = Mathf.Clamp(foame, 0f, 100f);
        if (uiManager != null)
        {
            uiManager.UpdateStatsUI();
        }
    }
    public void IncreaseEnergie(float amount)
    {
        energie += amount;
        energie = Mathf.Clamp(energie, 0f, 100f);
        if (uiManager != null) uiManager.UpdateStatsUI();
    }
    // Update is called once per frame
    void Update()
    {
        if (!isInitialized) return; // Iese daca Managerul UI nu a fost gasit

        // 1. Scade timpul din cronometru
        hungerTimer -= Time.deltaTime;

        // 2. Verifica daca cronometrul a ajuns la zero
        if (hungerTimer <= 0)
        {
            // Resetam cronometrul
            hungerTimer = foameDecayInterval;

            // Apelam functia de scadere a foamei
            DecayFoame();
            DecayEnergie();
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // 1. Refacem Energia prin GameManager
            GameManager gm = Object.FindAnyObjectByType<GameManager>();
            if (gm != null)
            {
                gm.IncreaseEnergie(energyRestoreAmount);
            }

            // 2. Înaintăm Timpul prin TimeManager
            TimeManager tm = Object.FindAnyObjectByType<TimeManager>();
            if (tm != null)
            {
                tm.AddHours(hoursToSleep);
            }

            Debug.Log("Te-ai trezit după 8 ore de somn!");
        }
    }
}


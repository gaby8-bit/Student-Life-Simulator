using UnityEngine;

public class TimeManager : MonoBehaviour
{
    [Header("Setări Timp")]
    public float timeMultiplier = 60f; // 1 secundă reală = 1 minut în joc
    public int startHour = 8;

    private float elapsedSeconds;
    public int currentMinutes;
    public int currentHours;

    private UIManager uiManager;

    void Start()
    {
        uiManager = FindAnyObjectByType<UIManager>();
        currentHours = startHour;
    }
    public void AddHours(int hoursToAdd)
    {
        currentHours += hoursToAdd;

        
        if (currentHours >= 24)
        {
            currentHours -= 24;
        }

        if (uiManager != null)
        {
            uiManager.UpdateTimeUI(currentHours, currentMinutes);
        }

        Debug.Log("Timpul a sărit cu 8 ore. Ora actuală: " + currentHours);
    }

    void Update()
    {
        
        elapsedSeconds += Time.deltaTime * timeMultiplier;

        if (elapsedSeconds >= 60f)
        {
            elapsedSeconds = 0;
            currentMinutes++;

            if (currentMinutes >= 60)
            {
                currentMinutes = 0;
                currentHours++;

                if (currentHours >= 24)
                {
                    currentHours = 0;
                }
            }

            if (uiManager != null)
            {
                uiManager.UpdateTimeUI(currentHours, currentMinutes);
            }
        }
    }
}

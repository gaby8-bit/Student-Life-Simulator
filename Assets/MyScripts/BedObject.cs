using UnityEngine;

public class BedObject : MonoBehaviour
{
    [SerializeField] private float energyRestoreAmount = 100f; // Câtă energie primești

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Verificăm dacă cel care a atins patul este Jucătorul
        if (other.CompareTag("Player"))
        {
            // Căutăm GameManager-ul în scenă
            GameManager gm = Object.FindAnyObjectByType<GameManager>();
            if (gm.energie > 80)
            {
                Debug.Log("Nu ești destul de obosit ca să dormi!");
                return; // Iese din funcție și nu se întâmplă nimic
            }
            else
            { 
            if (gm != null)
            {
                // Folosim funcția nouă de IncreaseEnergie din GameManager
                gm.IncreaseEnergie(energyRestoreAmount);
                Debug.Log("Te-ai odihnit! Energia a fost refăcută.");
                    TimeManager tm = FindAnyObjectByType<TimeManager>();
                    if (tm != null)
                    {
                        tm.AddHours(8); // Aici apelăm funcția de mai sus
                    }
                }
        }

           
        }
    }
}
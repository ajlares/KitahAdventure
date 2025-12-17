using System.Net.Mime;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStatsManager : MonoBehaviour
{
    public static PlayerStatsManager instance;
    
    [Header("Health")]
    public float maxHealth = 100;
    [Header("Stamina")]
    public float maxStamina = 100;
    public float currentStamina;
    public Image staminaBar;
    
    [SerializeField] float staminaRegenRate;
    [SerializeField] float sprintDrainRate;
    [SerializeField] float regenDelay;

    [SerializeField] float regenTimer;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        
        currentStamina = maxStamina;
    }

    private void Update()
    {
        staminaBar.fillAmount = currentStamina / maxStamina;
        RegenerateStamina();
    }

    public bool HasEnoughStamina()
    {
        return currentStamina > 0;
    }

    public void ConsumeSprintStamina()
    {
        currentStamina -= sprintDrainRate * Time.deltaTime;
        currentStamina = Mathf.Max(0, currentStamina);
        regenTimer = 0f;
    }

    private void RegenerateStamina()
    {
        regenTimer += Time.deltaTime;

        if (regenTimer >= regenDelay)
        {
            currentStamina += staminaRegenRate * Time.deltaTime;
            // Stops stamina from going over max, max Stamina can be upgraded
            currentStamina = Mathf.Min(maxStamina, currentStamina);
        }
    }
}

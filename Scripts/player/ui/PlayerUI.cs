using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private PlayerController playerController;
    [SerializeField] private PlayerLevelSystem playerLevelSystem;
    [SerializeField] private Slider healthSlider;
    [SerializeField] private Slider xpSlider;
    [SerializeField] private TextMeshProUGUI levelText;
    [SerializeField] private TextMeshProUGUI healthText;



    public void Start()
    {

        if (playerController == null)
            playerController = FindAnyObjectByType<PlayerController>();

        if (playerLevelSystem == null)
            playerLevelSystem = FindAnyObjectByType<PlayerLevelSystem>();

        playerController.HPAction += UpdateHealthUI;
        playerLevelSystem.XPAction += UpdateXpUI;
        if (playerLevelSystem != null)
        {
            playerLevelSystem.OnLevelUp.AddListener(UpdateLevelUI);
        }
        UpdateAllUI();
    }

    private void UpdateAllUI()
    {
        UpdateHealthUI(playerController.CurrentHealth, playerController.MaxHealth);
        UpdateXpUI(playerLevelSystem.CurrentXP, playerLevelSystem.XPToNextLevel);
        UpdateLevelUI(playerLevelSystem.CurrentLevel);
    }

    private void UpdateXpUI(float currentXp, float xpToNextLevel)
    {
        if (xpSlider != null)
        {
            xpSlider.value = currentXp / xpToNextLevel;
        }
    }

    private void UpdateHealthUI(float currentHealth, float maxHealth)
    {
        if (healthSlider != null && healthText != null)
        {
            healthSlider.value = currentHealth / maxHealth;
            healthText.text = $"{currentHealth}/{maxHealth}";
        }
    }

    private void UpdateLevelUI(int arg0)
    {
        if (levelText != null)
        {
            levelText.text = $"level {arg0}";
        }
    }
    
    private void OnDestroy()
    {
        if (playerController != null)
            playerController.HPAction -= UpdateHealthUI;
        
        if (playerLevelSystem != null)
        {
            playerLevelSystem.XPAction -= UpdateXpUI;
            playerLevelSystem.OnLevelUp.RemoveListener(UpdateLevelUI);
        }
    }
}
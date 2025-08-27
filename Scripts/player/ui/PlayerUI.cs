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

        if (playerLevelSystem != null)
        {
            playerLevelSystem.OnLevelUp.AddListener(UpdateLevelUI);
        }
        UpdateAllUI();
    }

    private void UpdateAllUI()
    {
        UpdateLevelUI(playerLevelSystem.CurrentLevel);
        UpdateHealthUI();
        UpdateXpUI();
   }

    private void Update()
    {
        UpdateHealthUI();
        UpdateXpUI();
    }

    private void UpdateXpUI()
    {
        if (xpSlider != null && playerLevelSystem != null)
        {
            xpSlider.value = playerLevelSystem.XPRatio;
        }
    }

    private void UpdateHealthUI()
    {
        if (healthSlider != null && healthText != null)
        {
            healthText.text = $"HP {playerController.CurrentHealth}";
            healthSlider.value = playerController.CurrentHealth / playerController.MaxHealth;
        }
    }

    private void UpdateLevelUI(int arg0)
    {
        if (levelText != null)
        {
            levelText.text = $"level {arg0}";
        }
    }
}
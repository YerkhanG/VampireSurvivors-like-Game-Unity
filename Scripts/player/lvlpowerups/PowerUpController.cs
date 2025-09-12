using System;
using System.Collections.Generic;
using UnityEngine;
public class PowerUpController : MonoBehaviour
{
    public PlayerLevelSystem playerLevelSystem;
    public PowerUpUI uiManager;
    public PowerUpsDatabase powerUpsDatabase;
    public PowerUpContext context;
    void Start()
    {
        uiManager.OnPowerCardSelected += ApplyPowerUp;
    }

    private void ApplyPowerUp(PowerUp up)
    {
        up.OnSelected(context);
    }

    private void OnEnable()
    {
        playerLevelSystem.LevelUpPowerUpAction += HandleLevelUp;
    }
    private void OnDisable()
    {
        playerLevelSystem.LevelUpPowerUpAction -= HandleLevelUp;
    }
    private void OnDestroy()
    {
        // Unsubscribe to prevent memory leaks
        if (uiManager != null)
        {
            uiManager.OnPowerCardSelected -= ApplyPowerUp;
        }
    }

    private void HandleLevelUp()
    {
        List<PowerUp> p = powerUpsDatabase.GetThreeRandomPowerUps();
        uiManager.PowerUpLevelUI(p);
    }   
}
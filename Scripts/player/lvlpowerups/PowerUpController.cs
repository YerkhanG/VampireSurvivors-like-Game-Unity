using UnityEngine;
public class PowerUpController : MonoBehaviour
{
    private PlayerLevelSystem playerLevelSystem;
    private PowerUpUI uiManager;
    private PowerUpsDatabase powerUpsDatabase;
    void Start()
    {
        playerLevelSystem = FindAnyObjectByType<PlayerLevelSystem>();
        powerUpsDatabase = FindAnyObjectByType<PowerUpsDatabase>();
        uiManager = FindAnyObjectByType<PowerUpUI>();
    }
    private void OnEnable()
    {
        playerLevelSystem.LevelUpPowerUpAction += HandleLevelUp;
    }
    private void OnDisable()
    {
        playerLevelSystem.LevelUpPowerUpAction -= HandleLevelUp;
    }

    private void HandleLevelUp()
    {   

    }   
}
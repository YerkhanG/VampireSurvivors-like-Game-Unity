using UnityEngine;

[CreateAssetMenu(fileName = "NewHealthPowerUp", menuName = "PowerUps/HealthPowerUp")]
public class HealthPowerUp : PowerUp
{
    public float healthIncreaseAmount = 25f;
    public override void OnSelected(PowerUpContext context)
    {
        context.Health.IncreaseMaxHealth(healthIncreaseAmount);
        Debug.Log($"Max health increased by {healthIncreaseAmount}!");
    }
}
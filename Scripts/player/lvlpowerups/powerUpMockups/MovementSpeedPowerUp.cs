using UnityEngine;
[CreateAssetMenu(fileName = "NewMovementSpeedPowerUp", menuName = "PowerUps/SpeedPowerUp")]
public class MovementSpeedPowerUp : PowerUp
{
    public float movementSpeedIncrease = 1.2f;
    public override void OnSelected(PowerUpContext context)
    {
        context.PlayerMovement.speed *= 1.2f;
        Debug.Log($"Speed increased by {movementSpeedIncrease}!");
    }
}
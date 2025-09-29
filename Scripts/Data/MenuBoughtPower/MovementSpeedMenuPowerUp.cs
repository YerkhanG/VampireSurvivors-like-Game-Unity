using Data.Context;
using UnityEngine;
namespace Data.MenuBoughtPower
{
    [CreateAssetMenu(fileName = "MovementSpeedNewMenuPowerUp", menuName = "MenuPowerUps/MovementSpeedMenuPowerUp")] 
    public class MovementSpeedMenuPowerUp: MenuPowerUp
    {
        public float speedIncreasePercentage;

        public override void ApplyEffect(BasePowerUpContext context, int level)
        {
            context.PlayerMovement.speed *= (speedIncreasePercentage * level);
        }
    }
}
using Data.Context;
using UnityEngine;

namespace Data.MenuBoughtPower
{
    [CreateAssetMenu(fileName = "NewHealthMenuPowerUp", menuName = "MenuPowerUps/HealthMenuPowerUp")] 
    public class HealthMenuPowerUp : MenuPowerUp
    {
        public float healthIncrease;
        public override void ApplyEffect(BasePowerUpContext context, int level)
        {
            context.Health.IncreaseMaxHealth(healthIncrease * level);
        }
    }
}
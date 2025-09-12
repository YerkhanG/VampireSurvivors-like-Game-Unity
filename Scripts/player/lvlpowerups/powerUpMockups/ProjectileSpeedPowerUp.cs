using UnityEngine;

[CreateAssetMenu(fileName = "NewProjectileSpeedPowerUp", menuName = "PowerUps/ProjectileSpeedPowerUp")]
public class ProjectileSpeedPowerUp : PowerUp
{
    public float projectileSpeedModifier = 1.15f;
    public override void OnSelected(PowerUpContext context)
    {
        foreach (DamageSpell d in context.AllDamageSpells)
        {
            d.projectileSpeed *= projectileSpeedModifier;
            Debug.Log($"Projectile Speed increased by {projectileSpeedModifier}!");
        }
    }
}

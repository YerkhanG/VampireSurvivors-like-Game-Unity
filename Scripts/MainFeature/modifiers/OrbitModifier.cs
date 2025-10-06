using UnityEngine;

public class OrbitModifier : SpellMods
{
    [SerializeField] private float orbitRadius = 3f;
    [SerializeField] private float orbitSpeed = 2f;
    [SerializeField] private float orbitDuration = 6f;
    public override bool CanModify(BaseSpell spell)
    {
        return spell is DamageSpell;
    }

    public override void ModifyProjectile(GameObject projectile, SpellCastContext context)
    {
        var orbit = projectile.AddComponent<OrbitMovement>();
        
        orbit.center = context.caster;
        orbit.radius = orbitRadius;
        orbit.duration = orbitDuration;
        orbit.speed = orbitSpeed *= context.spellQueue.ProjectileSpeedModifier;
    }

    public override void ModifySpell(BaseSpell spell, SpellCastContext context)
    {
        
    }
}
using UnityEngine;

public class ForkingModifier : SpellMods
{
    [SerializeField] private int forkingCount = 3;
    [SerializeField] private float forkingRadius = 20f;
    [SerializeField] private LayerMask enemyLayer;
    public override bool CanModify(BaseSpell spell)
    {
        return spell is DamageSpell;
    }

    public override void ModifyProjectile(GameObject projectile, SpellCastContext context, int totalProjectileCount)
    {
        var forking = projectile.AddComponent<ForkingProjectile>();
        forking.forkingCount = forkingCount;
        forking.forkingRadius = forkingRadius;
        forking.enemyLayer = enemyLayer;
    }

    public override void ModifySpell(BaseSpell spell, SpellCastContext context)
    {
        
    }
}
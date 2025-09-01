using UnityEngine;

public class StatModSpell : SpellMods
{
    [SerializeField] private float damageMod = 1f;
    [SerializeField] private float pierceMod = 1f;
    [SerializeField] private float projSpeedMod = 1f;
    [SerializeField] private float projCountMod = 1f;
    [SerializeField] private float cooldownMod = 1f;
    public override void ModifySpell(BaseSpell spell, SpellCastContext context)
    {
        spell.spellCooldown *= cooldownMod;
    }

    public override void ModifyProjectile(GameObject projectile, SpellCastContext context)
    {
         
    }

    public override bool CanModify(BaseSpell spell)
    {
        throw new System.NotImplementedException();
    }
}
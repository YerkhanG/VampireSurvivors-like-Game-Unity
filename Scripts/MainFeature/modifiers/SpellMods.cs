using UnityEngine;

public abstract class SpellMods : MonoBehaviour
{
    public abstract void ModifySpell(BaseSpell spell, SpellCastContext context);
    public abstract void ModifyProjectile(GameObject projectile, SpellCastContext context);
    public abstract bool CanModify(BaseSpell spell);
}
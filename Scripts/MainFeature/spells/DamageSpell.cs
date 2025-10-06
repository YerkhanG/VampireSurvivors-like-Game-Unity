using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewDamageSpell", menuName = "Spells/DamageSpells")]
public class DamageSpell : BaseSpell
{
    public float damage = 100f;

    public float projectileSpeed = 5f;
    public int projectileCount = 1;
    public float spreadAngle = 15f;
    public float pierce = 0f;
    public float timeToLive = 2f;
    public GameObject projectilePrefab;
    
    
    public void CastWithModifiers(SpellCastContext context, List<SpellMods> modifiers)
    {
        PlayCastEffect(context.caster.position, context.direction);
        if (!projectilePrefab) return;
    
        Vector3 direction = context.direction.normalized;
    
        if (projectileCount == 1)
        {
            LaunchProjectile(context.caster.position, direction, context, modifiers);
        }
        else if (projectileCount % 2 == 1)
        {
            LaunchProjectile(context.caster.position, direction, context, modifiers);
        
            for (int i = 1; i <= projectileCount / 2; i++)
            {
                float angle = spreadAngle * i;
                LaunchProjectile(context.caster.position, Quaternion.Euler(0, 0, angle) * direction, context, modifiers);
                LaunchProjectile(context.caster.position, Quaternion.Euler(0, 0, -angle) * direction, context, modifiers);
            }
        }
        else
        {
            float offset = spreadAngle / 2f;
        
            for (int i = 0; i < projectileCount / 2; i++)
            {
                float angle = offset + (spreadAngle * i);
                LaunchProjectile(context.caster.position, Quaternion.Euler(0, 0, angle) * direction, context, modifiers);
                LaunchProjectile(context.caster.position, Quaternion.Euler(0, 0, -angle) * direction, context, modifiers);
            }
        }
    }
    public override void Cast(SpellCastContext context)
    {   
        CastWithModifiers(context, null);
    }

    public void LaunchProjectile(Vector3 position, Vector3 direction, SpellCastContext context, List<SpellMods> modifiers = null)
    {
        GameObject projectile = Instantiate(
            projectilePrefab,
            position,
            Quaternion.identity
        );

        Projectile proj = projectile.GetComponent<Projectile>();
        proj.SetSpell(this);
        if (proj != null)
        {
            proj.Damage = damage;
            proj.Speed = projectileSpeed;
            proj.Direction = direction;
            proj.Pierce = pierce;
            proj.TTL = timeToLive;
        }
        else
        {
            Debug.Log("Projectile script missing on projectilePrefab!");
        }
        if (modifiers != null && modifiers.Count > 0)
        {
            foreach (var modifier in modifiers)
            {
                modifier.ModifyProjectile(projectile, context, projectileCount);
            }
        }
    }
}
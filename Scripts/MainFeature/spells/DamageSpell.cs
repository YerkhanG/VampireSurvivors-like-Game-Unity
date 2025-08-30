using System;
using System.Security.Cryptography;
using UnityEngine;
[CreateAssetMenu(fileName = "NewDamageSpell", menuName = "Spells/DamageSpells")]
public class DamageSpell : BaseSpell
{
    public float damage = 100f;

    public float projectileSpeed = 5f;
    public float pierce = 0f;
    public GameObject projectilePrefab;
    public Vector2 castDirection = Vector2.right;
    public Vector2 mousePosition;

    public NewActions mouseAction;

    public override void Cast(SpellCastContext context)
    {

        if (projectilePrefab == null) return;
        GameObject projectile = Instantiate(
            projectilePrefab,
            context.caster.position,
            Quaternion.identity
        );
        Projectile proj = projectile.GetComponent<Projectile>();
        if (proj != null)
        {
            proj.Damage = damage;
            proj.Speed = projectileSpeed;
            proj.Pierce = pierce;
            proj.Direction = context.direction.normalized;
        }
        else
        {
            Debug.LogError("Projectile script missing on effectPrefab!");
        }
    }
}
using System;
using System.Collections.Generic;
using UnityEngine;

public class SpellQueue : MonoBehaviour
{
    public List<BaseSpell> queue = new List<BaseSpell>(7); 
    private float _currentCooldown;
    private int _currentIndex = 0;
    private NewActions mouseActions;
    private SpellQueueUIManager uiManager;

    public List<GameObject> spellMods = new();
    void Awake()
    {
        for (int i = queue.Count; i < 7; i++)
        {
            queue.Add(null);
        }
        mouseActions = new NewActions();
        uiManager = FindAnyObjectByType<SpellQueueUIManager>();
    }
    void OnEnable()
    {
        mouseActions.Player.Cast.Enable();
    }
    void OnDisable()
    {
        mouseActions.Player.Cast.Disable();
    }
    public void Update()
    {
        if (queue.Count == 0) return; // Don't try to cast if queue is empty
        if (_currentCooldown > 0)
        {
            _currentCooldown -= Time.deltaTime;
        }
        else
        {
            CastSpellInqueue();
        }
    }

    private void CastSpellInqueue()
    {
        if (_currentIndex >= queue.Count || queue[_currentIndex] == null)
        {
            _currentIndex = (_currentIndex + 1) % queue.Count;
            return;
        }
        Vector2 castDirection;
        try 
        {
            castDirection = mouseActions.Player.Cast.ReadValue<Vector2>();
        }
        catch (System.Exception e)
        {
            Debug.LogError("Failed to read cast input: " + e.Message);
            return;
        }

        BaseSpell spell = queue[_currentIndex];
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(new Vector3(castDirection.x, castDirection.y, Camera.main.nearClipPlane));
        Vector2 direction = (mouseWorldPos - transform.position).normalized;
        var context = new SpellCastContext(this,_currentIndex, transform,direction);
        if (spellMods.Count > 0 && spell is not ModifySpell)
        {
            ApplyModifiersAndCast(spell, context);
            spellMods.Clear();
        }
        else
        {
            spell.Cast(context);
        }
        _currentCooldown = spell.spellCooldown;
        _currentIndex = (_currentIndex + 1) % queue.Count;
    }

    private void ApplyModifiersAndCast(BaseSpell spell, SpellCastContext context)
    {
        BaseSpell spellCopy = Instantiate(spell);
        List<SpellMods> modifiers = new List<SpellMods>();
        
        foreach (var prefab in spellMods)
        {
            var modifierComponents = prefab.GetComponents<SpellMods>();
            foreach (var modifier in modifierComponents)
            {
                if (modifier.CanModify(spellCopy))
                {
                    modifiers.Add(modifier);
                    modifier.ModifySpell(spellCopy, context);
                }
            }
        }
        
        // Custom cast logic that applies projectile modifiers
        if (spellCopy is DamageSpell damageSpell && modifiers.Count > 0)
        {
            CastModifiedDamageSpell(damageSpell, context, modifiers);
        }
        else
        {
            spellCopy.Cast(context);
        }
    }

    private void CastModifiedDamageSpell(DamageSpell spell, SpellCastContext context, List<SpellMods> modifiers)
    {
        if (spell.projectilePrefab == null) return;
        
        GameObject projectile = Instantiate(
            spell.projectilePrefab,
            context.caster.position,
            Quaternion.identity
        );
        
        Projectile proj = projectile.GetComponent<Projectile>();
        if (proj != null)
        {
            proj.Damage = spell.damage;
            proj.Speed = spell.projectileSpeed;
            proj.Pierce = spell.pierce;
            proj.Direction = context.direction.normalized;
        }
        
        // Apply all projectile modifiers
        foreach (var modifier in modifiers)
        {
            modifier.ModifyProjectile(projectile, context);
        }
    }

    public void SwapActiveSpells(int index1, int index2)
    {
        if (index1 < 0 || index2 < 0 || index1 >= queue.Count || index2 >= queue.Count) return;
        BaseSpell temp = queue[index1];
        Debug.Log("Trying to change spells in backend: " + queue[index1].spellName + " " + queue[index2].spellName);
        queue[index1] = queue[index2];
        queue[index2] = temp;
        Debug.Log("Changed spells in backend: " + index1 + " " + index2);
    }
    public void RemoveSpell(int index) => queue.RemoveAt(index);

    internal void AddModifierPrefab(GameObject spellModifier)
    {
        spellMods.Add(spellModifier);
    }   
}
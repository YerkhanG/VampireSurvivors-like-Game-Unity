using System;
using UnityEngine;
using UnityEngine.Rendering;
[CreateAssetMenu(fileName = "NewSpell", menuName = "Spells/Spell")] 
public abstract class BaseSpell : ScriptableObject
{
    public String spellName;
    public Sprite spellIcon;
    public float spellCooldown;
    public GameObject EffectPrefab;

    public abstract void Cast(SpellCastContext context);
} 
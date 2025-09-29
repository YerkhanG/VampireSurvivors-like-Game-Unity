using System;
using UnityEngine;
using UnityEngine.Rendering;
[CreateAssetMenu(fileName = "NewSpell", menuName = "Spells/Spell")]
public abstract class BaseSpell : ScriptableObject
{
    [Header("Basic settings")]
    public String spellName;
    public Sprite spellIcon;
    public float spellCooldown;
    [Header("Visuals")]
    public GameObject hitEffectPrefab;
    public GameObject missEffectPrefab;
    public GameObject castEffectPrefab;
    [Header("Audio")]
    public AudioClip castSound;
    public AudioClip hitSound;

    public abstract void Cast(SpellCastContext context);
    public virtual void PlayCastEffect(Vector3 position, Vector3 direction)
    {
        if (castEffectPrefab != null)
        {
            GameObject effect = Instantiate(castEffectPrefab, position, Quaternion.LookRotation(direction));
            SpellEffect effectComponent = effect.GetComponent<SpellEffect>();
            if (effectComponent != null)
            {
                Destroy(effect, effectComponent.duration);
            }
            else
            {
                Destroy(effect, 5f); // Fallback destruction time
            }
        }
    }
    public virtual void PlayHitEffect(Vector3 position , Vector3 normal = default )
    {
        if (hitEffectPrefab != null)
        {
            Quaternion rotation = normal != Vector3.zero ? Quaternion.LookRotation(-normal) : Quaternion.identity;
            GameObject effect = Instantiate(hitEffectPrefab, position, rotation);
            
            SpellEffect effectComponent = effect.GetComponent<SpellEffect>();
            if (effectComponent != null)
            {
                Destroy(effect, effectComponent.duration);
            }
            else
            {
                Destroy(effect, 3f);
            }
        }
    }
} 
using UnityEngine;
[CreateAssetMenu(fileName = "NewModifySpell", menuName = "Spells/ModifySpells")]
public class ModifySpell : BaseSpell
{
    [SerializeField]public GameObject modPrefab;
    public override void Cast(SpellCastContext context)
    {
        if (modPrefab != null)
        {
            context.spellQueue.AddModifierPrefab(modPrefab);
        }
        else
        {
            Debug.LogWarning("ModifierPrefab is null on " + name);
        }
    }
}
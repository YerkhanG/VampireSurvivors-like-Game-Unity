using UnityEngine;
public struct SpellCastContext
{
    public SpellQueue spellQueue;
    public int currentIndex;
    public Transform caster;
    public Vector2 direction;
    public SpellCastContext(SpellQueue spellQueue, int currentIndex, Transform caster, Vector2 direction)
    {
        this.caster = caster;
        this.spellQueue = spellQueue;
        this.direction = direction;
        this.currentIndex = currentIndex;
    }
}
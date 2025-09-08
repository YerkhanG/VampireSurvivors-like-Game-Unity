using UnityEngine;

public class PowerUpContext : MonoBehaviour
{
    public PlayerController Health { get; private set; }
    public PlayerLevelSystem LevelSystem { get; private set; }
    public PlayerMovement PlayerMovement { get; private set; }
    public SpellQueue SpellQueue { get; private set; }
    public DamageSpell DamageSpell{ get; private set; }
    private void Awake()
    {
        Health = FindAnyObjectByType<PlayerController>();
        LevelSystem = FindAnyObjectByType<PlayerLevelSystem>();
        PlayerMovement = FindAnyObjectByType<PlayerMovement>();
        SpellQueue = FindAnyObjectByType<SpellQueue>();
        DamageSpell = FindAnyObjectByType<DamageSpell>();
    }
}
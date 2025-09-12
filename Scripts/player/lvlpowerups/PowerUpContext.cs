using System.Collections.Generic;
using UnityEngine;

public class PowerUpContext : MonoBehaviour
{
    public PlayerController Health { get; private set; }
    public PlayerLevelSystem LevelSystem { get; private set; }
    public PlayerMovement PlayerMovement { get; private set; }
    public SpellQueue SpellQueue { get; private set; }
    public DamageSpell[] AllDamageSpells{ get; private set; }
    private void Awake()
    {
        Health = GetComponent<PlayerController>();
        LevelSystem = GetComponent<PlayerLevelSystem>();
        PlayerMovement = GetComponent<PlayerMovement>();
        SpellQueue = GetComponent<SpellQueue>();
        AllDamageSpells = FindObjectsByType<DamageSpell>(FindObjectsInactive.Include, FindObjectsSortMode.None);
    }
}
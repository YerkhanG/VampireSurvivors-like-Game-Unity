using System.Collections.Generic;
using UnityEngine;

public class PowerUpContext : MonoBehaviour
{
    public PlayerController Health { get; private set; }
    public PlayerLevelSystem LevelSystem { get; private set; }
    public PlayerMovement PlayerMovement { get; private set; }
    public SpellQueue SpellQueue { get; private set; }
    public DamageSpell[] AllDamageSpells { get; private set; }

    public static PowerUpContext Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;


        Health = GetComponent<PlayerController>();
        LevelSystem = GetComponent<PlayerLevelSystem>();
        PlayerMovement = GetComponent<PlayerMovement>();
        SpellQueue = GetComponent<SpellQueue>();
        AllDamageSpells = FindObjectsByType<DamageSpell>(FindObjectsInactive.Include, FindObjectsSortMode.None);


        if (Health == null) Debug.LogWarning("PowerUpContext: Health (PlayerController) is null!");
        if (LevelSystem == null) Debug.LogWarning("PowerUpContext: LevelSystem is null!");
        if (PlayerMovement == null) Debug.LogWarning("PowerUpContext: PlayerMovement is null!");
        if (SpellQueue == null) Debug.LogWarning("PowerUpContext: SpellQueue is null!");
        if (AllDamageSpells == null || AllDamageSpells.Length == 0) Debug.LogWarning("PowerUpContext: No DamageSpells found!");

    }
    private void OnDestroy() {
        if (Instance == this)
        {
            Instance = null;
        }

    }
}
using UnityEngine;
using UnityEngine.Events;


[System.Serializable]
public class LevelUpEvent : UnityEvent<int> { }
public class PlayerLevelSystem : MonoBehaviour
{
    [Header("Level Settings")]
    [SerializeField] private int currentLevel = 1;
    [SerializeField] private float currentXP = 0f;
    [SerializeField] private float baseXpForLeveling = 100f;
    [SerializeField] private float exponentialFactorLvl = 1.5f;
    [SerializeField] private float linearFactorLvl = 100f;

    [Header("Pickup Settings")]
    [SerializeField] private float pickupRadius = 2f;
    [SerializeField] private LayerMask xpGemLayer;

    public int CurrentLevel => currentLevel;
    public float CurrentXP => currentXP;
    public float XPToNextLevel => CalculateXPForLevel(currentLevel);
    public float XPRatio => currentXP / XPToNextLevel;
    public LevelUpEvent OnLevelUp;

    public System.Action<float, float> XPAction;
    private float CalculateXPForLevel(int level)
    {
        // Exponential formula: baseXP * (exponentialFactor ^ (level-1)) + linearFactor * (level-1)
        return baseXpForLeveling * Mathf.Pow(exponentialFactorLvl, level - 1) + linearFactorLvl * (level - 1);
    }
    public void Update()
    {
        CheckForXPGems();
    }
    public void GainXP(float xpValue)
    {
        currentXP += xpValue;
        XPAction?.Invoke(currentXP , XPToNextLevel);
        while (currentXP >= XPToNextLevel)
        {
            LevelUp();
        }
    }

    private void LevelUp()
    {
        currentXP -= XPToNextLevel;
        currentLevel++;
        XPAction?.Invoke(currentXP, XPToNextLevel);
        OnLevelUp?.Invoke(currentLevel);
        
        Debug.Log($"Level Up! Now level {currentLevel}");
    }
    private void CheckForXPGems()
    {
        Collider2D[] gems = Physics2D.OverlapCircleAll(transform.position, pickupRadius, xpGemLayer);
        foreach (Collider2D gem in gems)
        {
            XpGem xpGem = gem.GetComponent<XpGem>();
            if (xpGem != null)
            {
                xpGem.SetMagnetRadius(pickupRadius * 1.5f); // Increase magnet radius when close
            }
        }
    }
}
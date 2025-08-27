using UnityEngine;
using UnityEngine.Events;


[System.Serializable]
public class LevelUpEvent : UnityEvent<int> { }
public class PlayerLevelSystem : MonoBehaviour
{
    [Header("Level Settings")]
    [SerializeField] private int currentLevel;
    [SerializeField] private float currentXP = 0f;
    [SerializeField] private float xpToNextLevel = 50f;
    [SerializeField] private AnimationCurve xpCurve = AnimationCurve.Linear(1, 100, 50, 1000);

    [Header("Pickup Settings")]
    [SerializeField] private float pickupRadius = 2f;
    [SerializeField] private LayerMask xpGemLayer;

    public int CurrentLevel => currentLevel;
    public float CurrentXP => currentXP;
    public float XPToNextLevel => xpToNextLevel;
    public float XPRatio => currentXP / xpToNextLevel;
    public LevelUpEvent OnLevelUp;

    public void Update()
    {
        CheckForXPGems();
    }
    public void GainXP(float xpValue)
    {
        currentXP += xpValue;
        while (currentXP >= xpToNextLevel)
        {
            LevelUp();
        }
    }

    private void LevelUp()
    {
        currentXP -= xpToNextLevel;
        currentLevel++;

        xpToNextLevel = xpCurve.Evaluate(currentLevel);

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
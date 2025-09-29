using Data.Context;
using UnityEngine;
[CreateAssetMenu(fileName = "NewMenuPowerUp", menuName = "MenuPowerUps/MenuPowerUp")] 
public abstract class MenuPowerUp : ScriptableObject
{
    public string ID;
    public int cost;
    public int maxLevel;
    public int currentLevel = 1;
    public string description;
    public string shopItemName;
    public Sprite menuPowerSprite;

    public abstract void ApplyEffect(BasePowerUpContext context, int level);
    public virtual int GetCostForLevel()
    {
        return cost * (currentLevel + 1);
    }

    void OnEnable()
    {
        if (string.IsNullOrEmpty(ID))
            ID = System.Guid.NewGuid().ToString();
    }
}
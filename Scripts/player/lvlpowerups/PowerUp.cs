using UnityEngine;
using UnityEngine.UI;
[CreateAssetMenu(fileName = "NewPowerUp", menuName = "PowerUps/PowerUp")] 
public abstract class PowerUp : ScriptableObject
{
    public string powerUpName;
    public string description;
    public Rarity rarity;
    public Sprite sprite;
    public abstract void OnSelected(PowerUpContext context);

    // public PowerUp(string name, string description,string rarity)
    // {
    //     this.name = name;
    //     this.rarity = rarity;
    //     this.description = description;
    // }
}

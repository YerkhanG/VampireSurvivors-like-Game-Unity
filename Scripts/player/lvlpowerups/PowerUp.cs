using UnityEngine;

public class PowerUp
{
    private string name;
    private string description;
    private string rarity;
    private Sprite sprite;
    private System.Action onSelect;

    public PowerUp(string name, string description,string rarity,  System.Action onSelect)
    {
        this.name = name;
        this.rarity = rarity;
        this.description = description;
        this.onSelect = onSelect;
    }
}

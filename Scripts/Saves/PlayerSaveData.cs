using System.Collections.Generic;

[System.Serializable]
public class PlayerSaveData
{
    public int currentCurrency;
    public List<PowerUpProgressionData> PowerUpProgression = new List<PowerUpProgressionData>();

    public PlayerSaveData()
    {
        currentCurrency = 0;
    }
}
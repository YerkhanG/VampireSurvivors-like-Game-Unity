using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PowerUpsDatabase : MonoBehaviour
{
    public List<PowerUp> allPowerUps = new List<PowerUp>();
    public List<RarityWeight> rarityWeights = new List<RarityWeight>();
    public Rarity GetRandomRarityByWeight()
    {
        float totalWeight = 0;
        foreach (RarityWeight rw in rarityWeights)
        {
            totalWeight += rw.weight;
        }
        float randomValue = Random.Range(0, totalWeight);
        float currentWeightTotal = 0;
        foreach (RarityWeight rw in rarityWeights)
        {
            currentWeightTotal += rw.weight;
            if (currentWeightTotal > randomValue)
            {
                return rw.rarity;
            }
        }
        return Rarity.Common;
    }
    public List<PowerUp> GetPowerUpsByRarity(Rarity targetRarity)
    {
        return allPowerUps.Where(p => p.rarity == targetRarity).ToList(); 
    }
    public List<PowerUp> GetThreeRandomPowerUps()
    {
        Rarity selectedRarity = GetRandomRarityByWeight();
        List<PowerUp> selectedPowerUps = GetPowerUpsByRarity(selectedRarity);
        return ShufflePowerUps(selectedPowerUps, 3);
    }

    public List<PowerUp> ShufflePowerUps(List<PowerUp> originalList, int takeCount)
    {
        List<PowerUp> shuffledList = new List<PowerUp>(originalList);
        for (int i = shuffledList.Count - 1; i > 0; i--)
        {
            int j = Random.Range(0, i + 1);
            PowerUp temp = shuffledList[i];
            shuffledList[i] = shuffledList[j];
            shuffledList[j] = temp;
        }
        return shuffledList.GetRange(0,takeCount);
    }

    
}
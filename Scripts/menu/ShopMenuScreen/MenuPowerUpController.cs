using System.Collections.Generic;
using UnityEngine;

namespace menu.ShopMenuScreen
{
    public class MenuPowerUpController : MonoBehaviour
    {
        public static MenuPowerUpController instance;
        public SaveLoadManager saveLoadManager;
        private PlayerSaveData playerSaveData;
        public static int? CurrencyData => instance?.playerSaveData?.currentCurrency;
        public static List<PowerUpProgressionData> PowerUps => instance?.playerSaveData?.PowerUpProgression;

        void Awake()
        {
            if (instance == null)
            {
                instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }

        void Start()
        {
            if (SaveLoadManager.LoadData() != null)
            {
                Debug.Log("game loaded");
                playerSaveData = SaveLoadManager.LoadData();
            }
            else
            {
                Debug.Log("created new save 35");
                playerSaveData = new PlayerSaveData();
                SaveLoadManager.SaveData(playerSaveData);
            }
        }

    public void PurchasePowerUp(string id, int cost)
        {
            if (instance.playerSaveData.currentCurrency < cost)
            {
                return;
            }

            instance.playerSaveData.currentCurrency -= cost;
            var existingData = instance.playerSaveData.PowerUpProgression.Find(p => p.menuPowerUpID == id);
            if (existingData != null)
            {
                existingData.level++;
            }
            else
            {
                instance.playerSaveData.PowerUpProgression.Add(
                    new PowerUpProgressionData(id, 1)
                );
                
            }
            SaveLoadManager.SaveData(playerSaveData);
            Debug.Log("saved the game meta prog  63");
        }
    }
}
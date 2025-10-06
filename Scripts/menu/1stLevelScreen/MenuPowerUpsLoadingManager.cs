using System.Collections.Generic;
using Data.Context;
using menu.ShopMenuScreen;
using UnityEngine;

namespace menu._1stLevelScreen
{
    public class MenuPowerUpsLoadingManager : MonoBehaviour
    {
        public List<MenuPowerUp>  menuPowerUps;
        public BasePowerUpContext context;
        public GameObject Player;
        void Awake()
        {
            context = new MetaProgressionContext(Player);
            LoadPowerUps();
        }

        private void LoadPowerUps()
        {
            if (MenuPowerUpController.PowerUps == null || 
                MenuPowerUpController.PowerUps.Count == 0)
            {
                Debug.Log("Nothing to load - PowerUps is null or empty");
                return;
            }

            foreach (var m in MenuPowerUpController.PowerUps)
            {
                if (m == null) continue; // Skip null entries
        
                var foundPowerUp = menuPowerUps.Find(p => p.ID == m.menuPowerUpID);
                if (foundPowerUp != null)
                {
                    foundPowerUp.ApplyEffect(context, m.level);
                }
            }
        }
    }
}
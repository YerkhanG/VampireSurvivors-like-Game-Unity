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
            if (MenuPowerUpController.PowerUps != null)
            {
                foreach (var m in MenuPowerUpController.PowerUps)
                {
                    var foundPowerUp = menuPowerUps.Find(p => p.ID == m.menuPowerUpID);
                    if (foundPowerUp != null)
                    {
                        foundPowerUp.ApplyEffect(context,m.level);
                    }
                }   
            }
            else
            {
                Debug.Log("Nothing to load");
            }
        }
    }
}
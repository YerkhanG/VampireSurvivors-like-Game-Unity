using UnityEngine;

namespace Data.Context
{
    public class MetaProgressionContext : BasePowerUpContext
    {
        public SpellQueue SpellQueue;
    
        public MetaProgressionContext(GameObject player)
        {
            Health = player.GetComponent<PlayerController>();
            LevelSystem = player.GetComponent<PlayerLevelSystem>();
            PlayerMovement = player.GetComponent<PlayerMovement>();
            SpellQueue = player.GetComponent<SpellQueue>();
        }
    }
}
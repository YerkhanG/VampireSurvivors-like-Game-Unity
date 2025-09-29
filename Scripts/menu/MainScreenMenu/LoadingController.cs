using UnityEngine;

namespace menu.MainScreenMenu
{
    public class LoadingController : MonoBehaviour
    {
        void Awake()
        {
            DontDestroyOnLoad(gameObject);
            SaveLoadManager.LoadData(); 
        }
    }
}
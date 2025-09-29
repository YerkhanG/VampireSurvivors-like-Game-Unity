using UnityEngine;
using UnityEngine.SceneManagement;

namespace menu.ShopMenuScreen
{
    public class BackButton : MonoBehaviour
    {
        public void Back()
        {
            SceneManager.LoadSceneAsync(0);
        }
    }
}
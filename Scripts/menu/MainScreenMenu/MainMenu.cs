namespace menu.MainScreenMenu
{
    using UnityEngine;
    using UnityEngine.SceneManagement;

    public class MainMenu : MonoBehaviour
    {
        public void PlayGame()
        {
            SceneManager.LoadSceneAsync(2);
        }

        public void OpenStore()
        {
            SceneManager.LoadSceneAsync(1);
        }
    }
}
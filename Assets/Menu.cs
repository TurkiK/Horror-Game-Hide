using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public void playBtn()
    {
        SceneManager.LoadScene("Game");
    }
    public void quitBtn()
    {
        Application.Quit();
    }
}

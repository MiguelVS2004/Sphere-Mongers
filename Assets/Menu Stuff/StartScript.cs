using UnityEngine;
using UnityEngine.SceneManagement;

public class StartScript : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene("BernasScene");
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}

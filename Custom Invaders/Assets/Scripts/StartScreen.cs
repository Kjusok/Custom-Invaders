using UnityEngine;
using UnityEngine.SceneManagement;

public class StartScreen : MonoBehaviour
{
    public void PressButtonNewGame()
    {
        SceneManager.LoadScene("MainScene");
    }
}

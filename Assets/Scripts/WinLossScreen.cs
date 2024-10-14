using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class WinLossScreen : MonoBehaviour
{
    public Text resultText;

    void Start()
    {
        resultText.text = PlayerPrefs.GetString("GameResult", "No Result");
    }

    public void PlayAgain()
    {
        SceneManager.LoadScene("GameScene"); 
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu"); 
    }
}

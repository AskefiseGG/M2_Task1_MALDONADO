using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class TicTacToe : MonoBehaviour
{
    public Button[] gridButtons;
    private string currentPlayer = "X";
    private string currentPlayerName = "You";
    private bool isGameOver = false;

    void Start()
    {
        ResetGame();
    }

    public void PlayerMove(int index)
    {
        if (gridButtons[index] == null)
        {
            Debug.LogError("Button at index " + index + " is not assigned in the Inspector!");
            return;
        }

        TextMeshProUGUI buttonText = gridButtons[index].GetComponentInChildren<TextMeshProUGUI>();
        if (buttonText == null)
        {
            Debug.LogError("Button at index " + index + " is missing a TextMeshProUGUI component!");
            return;
        }

        if (!isGameOver && buttonText.text == "")
        {
            buttonText.text = currentPlayer;
            if (CheckWin())
            {
                EndGame(currentPlayerName + " Win!");
            }
            else if (IsDraw())
            {
                EndGame("A Draw!");
            }
            else
            {
                SwitchPlayer();
                if (currentPlayer == "O")
                {
                    AITurn();
                }
            }
        }
    }

    void AITurn()
    {
        for (int i = 0; i < gridButtons.Length; i++)
        {
            if (gridButtons[i] != null && gridButtons[i].GetComponentInChildren<TextMeshProUGUI>().text == "")
            {
                gridButtons[i].GetComponentInChildren<TextMeshProUGUI>().text = "O";
                if (CheckWin())
                {
                    EndGame(currentPlayerName + " Wins!");
                }
                else if (IsDraw())
                {
                    EndGame("A Draw!");
                }
                SwitchPlayer();
                break;
            }
        }
    }

    void SwitchPlayer()
    {
        if (currentPlayer == "X")
        {
            currentPlayer = "O";
            currentPlayerName = "AI";
        }
        else
        {
            currentPlayer = "X";
            currentPlayerName = "You";
        }
    }

    bool CheckWin()
    {
        if (Match(0, 1, 2) || Match(3, 4, 5) || Match(6, 7, 8))
            return true;

        if (Match(0, 3, 6) || Match(1, 4, 7) || Match(2, 5, 8))
            return true;

        if (Match(0, 4, 8) || Match(2, 4, 6))
            return true;

        return false;
    }

    bool Match(int i, int j, int k)
    {
        TextMeshProUGUI b1 = gridButtons[i].GetComponentInChildren<TextMeshProUGUI>();
        TextMeshProUGUI b2 = gridButtons[j].GetComponentInChildren<TextMeshProUGUI>();
        TextMeshProUGUI b3 = gridButtons[k].GetComponentInChildren<TextMeshProUGUI>();

        if (b1 != null && b2 != null && b3 != null)
        {
            return b1.text == currentPlayer && b2.text == currentPlayer && b3.text == currentPlayer;
        }

        return false;
    }

    bool IsDraw()
    {
        foreach (Button button in gridButtons)
        {
            if (button != null && button.GetComponentInChildren<TextMeshProUGUI>().text == "")
            {
                return false;
            }
        }
        return true;
    }

    void EndGame(string result)
    {
        isGameOver = true;
        PlayerPrefs.SetString("GameResult", result);
        SceneManager.LoadScene("WinLossScene");
    }

    public void ResetGame()
    {
        isGameOver = false;
        currentPlayer = "X";
        currentPlayerName = "You";
        foreach (Button button in gridButtons)
        {
            if (button != null)
            {
                TextMeshProUGUI buttonText = button.GetComponentInChildren<TextMeshProUGUI>();
                if (buttonText != null)
                {
                    buttonText.text = "";
                }
                else
                {
                    Debug.LogError("Button is missing a TextMeshProUGUI component!");
                }
            }
            else
            {
                Debug.LogError("A button is not assigned in the Inspector!");
            }
        }
    }
}

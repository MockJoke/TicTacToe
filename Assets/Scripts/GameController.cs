﻿using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    [Header("Start Menu")] 
    [SerializeField] private GameObject StartMenu;
    [SerializeField] private Button xSideSelector;
    [SerializeField] private Button oSideSelector;
    
    [Header("Grid")]
    [SerializeField] private Button[] gridButtons;                  //playable buttons to input in game
    [SerializeField] private CanvasGroup gridButtonsCanvas;          
    [SerializeField] private GameObject[] winningLines;             //lines to indicate winning pattern
    
    [Header("GameOver Panel")]
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private TextMeshProUGUI gameResultText;
    
    [Header("Buttons")]
    [SerializeField] private Button restartBtn;
    [SerializeField] private Button rematchBtn;
    [SerializeField] private Button quitBtn;
    
    [Header("Score Card")]
    [SerializeField] private GameObject ScoreCard;
    [SerializeField] private TextMeshProUGUI xScoreText;
    [SerializeField] private TextMeshProUGUI oScoreText;
    [SerializeField] private GameObject xTurnIndicator;
    [SerializeField] private GameObject oTurnIndicator;
    
    
    private int[] markedGrids;       //markings on grid to check player side
    private int whoseTurn;           //0: O & 1: X
    private int firstTurn;           //to decide which side will take the first turn
    private int turnCount = 0;       //counts the no of turns played

    private int xScore = 0;
    private int oScore = 0;

    private bool isNewGame = true;

    private void Awake()
    {
        isNewGame = true;
        
        OnGameStart();
    }

    private void OnGameStart()
    {
        StartMenu.SetActive(true);
    }

    public void OnSideSelection(int no)
    {
        firstTurn = no;
        
        StartMenu.SetActive(false);

        if (isNewGame)
        {
            Setup();
        }
        else
        {
            ResetTurn();
        }
        
        isNewGame = false;
    }
    
    private void Setup()
    {
        if (isNewGame)
        {
            ResetTurn();
        }
        gameOverPanel.SetActive(false);
        ResetGrid();
    }

    private void ResetTurn()
    {
        whoseTurn = firstTurn;

        if (whoseTurn == 0)
        {
            oTurnIndicator.SetActive(true);
            xTurnIndicator.SetActive(false);
        }
        else
        {
            xTurnIndicator.SetActive(true);
            oTurnIndicator.SetActive(false);
        }
        
        turnCount = 0;
    }

    private void ResetGrid()
    {
        gridButtonsCanvas.interactable = true;
        
        for (int i = 0; i < gridButtons.Length; i++)
        {
            gridButtons[i].interactable = true;
            gridButtons[i].GetComponentInChildren<TextMeshProUGUI>().text = "";
        }

        markedGrids = new int[gridButtons.Length];

        for (int i = 0; i < markedGrids.Length; i++)
        {
            markedGrids[i] = -100;
        }

        for (int i = 0; i < winningLines.Length; i++)
        {
            winningLines[i].SetActive(false);
        }
    }

    public void OnClickBtn(int no)
    {
        gridButtons[no].GetComponentInChildren<TextMeshProUGUI>().text = whoseTurn == 0 ? "O" : "X";
        gridButtons[no].interactable = false;

        markedGrids[no] = whoseTurn;
        
        turnCount++;

        if (turnCount > 4)
        {
            CheckWin();
        }

        if (turnCount >= 9)
        {
            if (!CheckWin())
            {
                Draw();
            }
        }
        
        ChangeTurn();
    }

    private void ChangeTurn()
    {
        if (whoseTurn == 0)
        {
            whoseTurn = 1;
            xTurnIndicator.SetActive(true);
            oTurnIndicator.SetActive(false);
        }
        else if (whoseTurn == 1)
        {
            whoseTurn = 0;
            xTurnIndicator.SetActive(false);
            oTurnIndicator.SetActive(true);
        }
    }

    private bool CheckWin()
    {
        int s1 = markedGrids[0] + markedGrids[3] + markedGrids[6];
        int s2 = markedGrids[1] + markedGrids[4] + markedGrids[7];
        int s3 = markedGrids[2] + markedGrids[5] + markedGrids[8];
        int s4 = markedGrids[0] + markedGrids[1] + markedGrids[2];
        int s5 = markedGrids[3] + markedGrids[4] + markedGrids[5];
        int s6 = markedGrids[6] + markedGrids[7] + markedGrids[8];
        int s7 = markedGrids[0] + markedGrids[4] + markedGrids[8];
        int s8 = markedGrids[2] + markedGrids[4] + markedGrids[6];

        var solutions = new int[] { s1, s2, s3, s4, s5, s6, s7, s8 };

        for (int i = 0; i < solutions.Length; i++)
        {
            if (solutions[i] == 3 * whoseTurn)
            {
                GameOver();
                DisplayWinner(i);
                DisplayScore();
                return true;
            }
        }

        return false;
    }

    private void Draw()
    {
        GameOver();
        
        SetGameDrawText();
    }

    private void GameOver()
    {
        gameOverPanel.SetActive(true);
        gridButtonsCanvas.interactable = false;
    }

    private void DisplayWinner(int lineIndex)
    {
        winningLines[lineIndex].SetActive(true);
        
        SetGameWinnerText();
    }

    private void DisplayScore()
    {
        xScoreText.text = $"{xScore}";
        oScoreText.text = $"{oScore}";
    }
    
    private void SetGameWinnerText()
    {
        string value;

        if (whoseTurn == 0)
        {
            oScore++;
            value = "O";
        }
        else
        {
            xScore++;
            value = "X";
        }
        
        gameResultText.text = $"Player {value} wins!";
    }

    private void SetGameDrawText()
    {
        gameResultText.text = $"Game Draws!";
    }

    public void Restart()
    {
        ResetTurn();
        Setup();
    }

    public void Rematch()
    {
        Setup();
        OnGameStart();
    }
    
    public void Quit()
    {
        Application.Quit();
    }
}

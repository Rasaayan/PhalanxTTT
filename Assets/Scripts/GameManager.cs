using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Text infoPanel;
    public Text currentTurnDisplay;
    public Text unitsDisplay;
    public Text resultsDisplay;
    public Text conditionDisplay;

    public GameObject gameOverDisplay;

    public int rng;
    public int phalanxCounter = 0;    
    public int spartanUnits = 15;
    public int spartanPhalanxes = 0;
    public int persianUnits = 15;
    public int persianPhalanxes = 0; 
    public int vacantTiles = 25;

    public bool spartanTurn = false;
    public bool phalanxCreated = false;

    public string[] phalanxTiles = new string[25];

    public char[,] board = 
    {
        { '.', '.', '.', '.', '.'},
        { '.', '.', '.', '.', '.'},
        { '.', '.', '.', '.', '.'},
        { '.', '.', '.', '.', '.'},
        { '.', '.', '.', '.', '.'}
    };

    void Start()
    {
        FirstTurnDecider();
        DisplayBoard();
        DisplayUnits();
    }

    public void FirstTurnDecider()
    {
        if (Random.Range(0, 2) == 0)
        {
            spartanTurn = true;
            currentTurnDisplay.text = "Current Turn: Spartans";
        }
        else
        {
            spartanTurn = false;
            currentTurnDisplay.text = "Current Turn: Persians";
        }
    }

    public void DisplayBoard()
    {
        string line = "";
        infoPanel.text = "";
        for (int i = 0; i < 5; i++)
        {
            for (int j = 0; j < 5; j++)
            {
                line = line + board[i, j];
            }
            infoPanel.text += line + "\n";
            line = "";
        }
    }
    
    public void DisplayUnits()
    {
        string line = "Spartans: " + "\n";

        for (int i = 0; i < spartanUnits; i++)
        {
            line += "X, ";
        }

        line += "\n" + "Persians: " + "\n";

        for (int i = 0; i < persianUnits; i++)
        {
            line += "O, ";
        }

        unitsDisplay.text = line;
    }

    public void CheckMatchStatus()
    {
        // Draw Condition: There are no vacant tiles and both players have the same amount of Phalanxes
        if (vacantTiles == 0 && (spartanPhalanxes == persianPhalanxes))
        {
            gameOverDisplay.SetActive(true);
            resultsDisplay.text = "Draw";
            conditionDisplay.text = "There are no Vacant Tiles and both Players have the same number of Phalanxes";
            return;
        }

        // Draw Condition: There are no vacant tiles and both players have the same number of units on the board
        if (vacantTiles == 0 && (persianUnits == spartanUnits))
        {
            gameOverDisplay.SetActive(true);
            resultsDisplay.text = "Draw";
            conditionDisplay.text = "There are no Vacant Tiles and both Players have the same number of Units on the Board";
            return;
        }

        // Win Condition: A player creates 4 Phalanxes 
        if (persianPhalanxes == 4 || spartanPhalanxes == 4)
        {
            gameOverDisplay.SetActive(true);
            if (persianPhalanxes == 4)
            {
                resultsDisplay.text = "Win";
                conditionDisplay.text = "Persians have 4 Phalanxes";
            }
            else
            {
                resultsDisplay.text = "Win";
                conditionDisplay.text = "Spartans have 4 Phalanxes";

            }
            return;            
        }

        // Win Condition: There are no vacant tiles left, the player with the most Phalanxes wins
        if (vacantTiles == 0)
        {
            gameOverDisplay.SetActive(true);
            if (spartanPhalanxes > persianPhalanxes )
            {
                resultsDisplay.text = "Win";
                conditionDisplay.text = "Spartans have more Phalanxes";
                return;
            }
            else
            {
                resultsDisplay.text = "Win";
                conditionDisplay.text = "Persians have more Phalanxes";
                return;
            }            
        }
    }
}

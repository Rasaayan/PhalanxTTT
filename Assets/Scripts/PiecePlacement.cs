using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PiecePlacement : MonoBehaviour
{
    public GameManager gameManagerObj;
    public PieceSpawning pieceSpawningObj;
    public TileManager tileManagerObj;

    public int row;
    public int column;

    public string rowAsChar;
    public string columnAsChar;
    public string tileID;    

    public void OnMouseDown()
    {
        tileID = gameObject.name;

        IDconverter(tileID);

        if (gameManagerObj.phalanxCreated == false)
        {
            if (CheckIfVacant(row, column) == true)
            {
                gameManagerObj.vacantTiles--;
                NextTurn();
            }
        }
        else if (gameManagerObj.phalanxCreated == true)
        {
            if (TileInPhalanx() == false)
            {
                gameManagerObj.board[row, column] = 'D';
                tileManagerObj.RemoveTile(tileID);
                gameManagerObj.phalanxCreated = false;
            }            
        }
        gameManagerObj.DisplayBoard();
        gameManagerObj.DisplayUnits();
        gameManagerObj.CheckMatchStatus();
    }

    public void IDconverter(string tileID)
    {
        rowAsChar = tileID[0] + "";
        columnAsChar = tileID[1] + "";

        row = int.Parse(rowAsChar);
        column = int.Parse(columnAsChar);
    }

    public bool CheckIfVacant(int row, int column)
    {
        if (gameManagerObj.board[row, column] == '.')
        {
            return true;
        }
        return false;
    }

    public void NextTurn()
    {
        if (gameManagerObj.spartanTurn == true)
        {
            gameManagerObj.currentTurnDisplay.text = "Current Turn: Persians" ;
            TakeTurn('x', 0);
            gameManagerObj.spartanTurn = false;

        }
        else if (gameManagerObj.spartanTurn == false)
        {
            gameManagerObj.currentTurnDisplay.text = "Current Turn: Spartans" ;
            TakeTurn('o', 1);
            gameManagerObj.spartanTurn = true;
        }
    }

    public void TakeTurn(char factionSymbol, int spawnID)
    {
        gameManagerObj.board[row, column] = factionSymbol;
        pieceSpawningObj.SpawnPiece(spawnID, tileID);

        if (spawnID == 0)
        {
            gameManagerObj.spartanUnits--;
        }
        else
        {
            gameManagerObj.persianUnits--;
        }

        //Check for rows or columns
        if (EvaluateBoard(gameManagerObj.board) == true)
        {
            gameManagerObj.phalanxCreated = true;
            if (spawnID == 0)
            {
                gameManagerObj.spartanPhalanxes++;
            }
            else
            {
                gameManagerObj.persianPhalanxes++;
            }
            Debug.Log("Phalanx Created");
        }
    }

    public bool EvaluateBoard(char[,] board)
    {
        // Checks all the Rows for Phalanxes 
        for (int i = 0; i < 5; i++)
        {
            // If there are 3 in a row that are the same                      they arent periods       
            if ((board[i, 0] == board[i, 1] && board[i, 1] == board[i, 2]) && board[i, 0] != '.')
            {
                string phalanxPiece1 = i + "" + 0;
                string phalanxPiece2 = i + "" + 1;
                string phalanxPiece3 = i + "" + 2;

                if (CheckArrayForPhalanxes(phalanxPiece1, phalanxPiece2, phalanxPiece3) == true)
                {
                    return true;
                }
            }
            if ((board[i, 1] == board[i, 2] && board[i, 2] == board[i, 3]) & board[i, 1] != '.')
            {
                string phalanxPiece1 = i + "" + 1;
                string phalanxPiece2 = i + "" + 2;
                string phalanxPiece3 = i + "" + 3;

                if (CheckArrayForPhalanxes(phalanxPiece1, phalanxPiece2, phalanxPiece3) == true)
                {
                    return true;
                }
            }
            if ((board[i, 2] == board[i, 3] && board[i, 3] == board[i, 4]) & board[i, 2] != '.') 
            {
                string phalanxPiece1 = i + "" + 2;
                string phalanxPiece2 = i + "" + 3;
                string phalanxPiece3 = i + "" + 4;

                if (CheckArrayForPhalanxes(phalanxPiece1, phalanxPiece2, phalanxPiece3) == true)
                {
                    return true;
                }
            }
        }
        // Checks all the Rows for Phalanxes 
        for (int i = 0; i < 5; i++)
        {
            if ((board[0, i] == board[1, i] && board[1, i] == board[2, i]) & board[0, i] != '.')
            {
                string phalanxPiece1 = 0 + "" + i;
                string phalanxPiece2 = 1 + "" + i;
                string phalanxPiece3 = 2 + "" + i;

                if (CheckArrayForPhalanxes(phalanxPiece1, phalanxPiece2, phalanxPiece3) == true)
                {
                    return true;
                }
            }
            if ((board[1, i] == board[2, i] && board[2, i] == board[3, i]) & board[1, i] != '.') 
            {
                string phalanxPiece1 = 1 + "" + i;
                string phalanxPiece2 = 2 + "" + i;
                string phalanxPiece3 = 3 + "" + i;

                if (CheckArrayForPhalanxes(phalanxPiece1, phalanxPiece2, phalanxPiece3) == true)
                {
                    return true;
                }
            }
            if ((board[2, i] == board[3, i] && board[3, i] == board[4, i]) & board[2, i] != '.')
            {
                string phalanxPiece1 = 2 + "" + i;
                string phalanxPiece2 = 3 + "" + i;
                string phalanxPiece3 = 4 + "" + i;

                if (CheckArrayForPhalanxes(phalanxPiece1, phalanxPiece2, phalanxPiece3) == true)
                {
                    return true;
                }
            }
        }
        return false;
    }

    public bool CheckArrayForPhalanxes(string tile1Name, string tile2Name, string tile3Name)
    {
        for (int i = 0; i < gameManagerObj.phalanxTiles.Length; i++)
        {
            // If any of these return true then it means that a piece in the would-be phalanx is already in another phalanx
            if (tile1Name == gameManagerObj.phalanxTiles[i])
            {
                return false;
            }
            if (tile2Name == gameManagerObj.phalanxTiles[i])
            {
                return false;
            }
            if (tile3Name == gameManagerObj.phalanxTiles[i])
            {
                return false;
            }
        }

        // Adds the pieces in the newly created phalanx to the array 
        gameManagerObj.phalanxTiles[gameManagerObj.phalanxCounter] = tile1Name;
        gameManagerObj.phalanxCounter++;
        gameManagerObj.phalanxTiles[gameManagerObj.phalanxCounter] = tile2Name;
        gameManagerObj.phalanxCounter++;
        gameManagerObj.phalanxTiles[gameManagerObj.phalanxCounter] = tile3Name;
        gameManagerObj.phalanxCounter++;

        return true;
    }

    public bool TileInPhalanx()
    {
        for (int i = 0; i < gameManagerObj.phalanxTiles.Length; i++)
        {
            if( tileID == gameManagerObj.phalanxTiles[i])
            {
                return true;
            }
        }
        return false;
    }
}

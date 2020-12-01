using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileManager : MonoBehaviour
{
    public GameManager gameManagerObj;

    public void RemoveTile(string tileID)
    {
        // Checks if there is a Piece on the tile and if so, removes it aswell 
        if (GameObject.Find(tileID + "Piece") != null)
        {
            gameManagerObj.vacantTiles++;
            GameObject piece = GameObject.Find(tileID + "Piece");
            piece.SetActive(false);
        }

        gameManagerObj.vacantTiles--;
        GameObject tile = GameObject.Find(tileID);
        tile.SetActive(false);
    }
}

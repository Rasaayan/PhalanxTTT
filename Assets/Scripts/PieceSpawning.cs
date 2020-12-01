using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieceSpawning : MonoBehaviour
{
    public GameObject[] pieceTemplates;

    public void SpawnPiece(int pieceID, string tileID)
    {
        // Tile at which piece must be spawned
        GameObject currentTile = GameObject.Find(tileID);

        float xValueTile = xValueTile = currentTile.transform.position.x;
        float yValueTile = yValueTile = currentTile.transform.position.y;
        float zValueTile = zValueTile = currentTile.transform.position.z;     
        
        GameObject pieceToSpawn = Instantiate(pieceTemplates[pieceID]);

        //Instantiate piece
        pieceTemplates[pieceID].transform.position = new Vector3(xValueTile, yValueTile + 2f, zValueTile);
        pieceToSpawn.transform.position = pieceTemplates[pieceID].transform.position;
        pieceToSpawn.SetActive(true);
        pieceToSpawn.name = tileID + "Piece";
    }
}

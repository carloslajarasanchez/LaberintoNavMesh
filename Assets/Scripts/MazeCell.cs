using System;
using UnityEngine;

public class MazeCell: MonoBehaviour
{
    // Variables
    public bool isVisited = false;
    [SerializeField] private GameObject cellObject;
    public MazeCell(int x, int y, GameObject obj) // Constructor 
    { 
        cellObject = obj;
    } 

    public void RemoveWall(string wallName)
    {
        Transform wall = cellObject.transform.Find(wallName);
        if (wall != null)
        {
            wall.gameObject.SetActive(false); // Se desactiva
        }
    }
}
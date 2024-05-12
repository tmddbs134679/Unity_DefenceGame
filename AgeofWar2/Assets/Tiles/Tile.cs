using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField] bool isPlaceable;
    [SerializeField] Tower towerPrefab;


    GridManager gridManager;
    Pathfinder pathfinder;
    Vector2Int coordinates = new Vector2Int();
    public bool IsPlaceable
    {
        get
        {
            return isPlaceable;
        }
    }


    void Awake()
    {
        gridManager = FindObjectOfType<GridManager>();
        pathfinder = FindObjectOfType<Pathfinder>();
    }

    void Start()
    {
        if(gridManager != null)
        {
            coordinates = gridManager.GetCoordinatesFromPosition(transform.position);

            if(!isPlaceable)
            {
                gridManager.BlockNode(coordinates);

            }
        }
    }


    public bool GetIsPlaceable()
    { 
        return isPlaceable; 
    }



    void OnMouseDown()
    {
        if(gridManager.GetNode(coordinates).isWalkable && !pathfinder.WillBlockPath(coordinates))
        {
            bool isSuccussful = towerPrefab.CreateTower(towerPrefab, transform.position);
          
            if(isSuccussful)
            {
                gridManager.BlockNode(coordinates);

                //BroadCast
                pathfinder.NotifyReceivers();

            }
        }
    }

    public void OnRightMouse()
    {
      
    }
    

  

}

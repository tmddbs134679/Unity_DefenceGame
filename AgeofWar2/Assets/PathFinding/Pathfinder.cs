using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Pathfinder : MonoBehaviour
{
    [SerializeField] Vector2Int startCoordinates;

    public Vector2Int StartCoordinates
    {
        get
        {
            return startCoordinates;
        }
    }


    [SerializeField] Vector2Int destinationCoordinates;

    public Vector2Int DestinationCoordinates
    {
        get
        {
            return destinationCoordinates;
        }
    }

    Node startNode;
    Node destinationNode;
    Node currenSearchNode;

    Queue<Node> frontier = new Queue<Node>();

    Dictionary<Vector2Int, Node> reached = new Dictionary<Vector2Int, Node>();



    Vector2Int[] directions = { Vector2Int.right, Vector2Int.left, Vector2Int.up, Vector2Int.down };
    GridManager gridManager;

    //Node가 이미 탐험되었는지 확인용
    Dictionary<Vector2Int, Node> grid = new Dictionary<Vector2Int, Node>();


    void Awake()
    {
        gridManager = FindAnyObjectByType<GridManager>();

        if(gridManager != null )
        {
            grid = gridManager.Grid;
            startNode = grid[startCoordinates];
            destinationNode = grid[destinationCoordinates];
        
        }
    }
    void Start()
    {
        GetNewPath();
    }

    public List<Node> GetNewPath()
    {
        return GetNewPath(StartCoordinates);
    }

    public List<Node> GetNewPath(Vector2Int coordinates)
    {
        gridManager.ResetNodes();
        BreadthFirstSearch(coordinates);
        return BuildPath();
    }


    void ExploreNeighbers()
    {
        List<Node> neighnors = new List<Node>();

        foreach(Vector2Int direction in directions)
        {
            Vector2Int neighberCoords = currenSearchNode.coordinates + direction;

            if(grid.ContainsKey(neighberCoords))
            {
                neighnors.Add(grid[neighberCoords]);

                ////TODO: Remove after testing
                //grid[neighberCoords].isExplored = true;
                //grid[currenSearchNode.coordinates].isPath = true;
            }
        }

        foreach(Node neighber in neighnors)
        {
            if(!reached.ContainsKey(neighber.coordinates) && neighber.isWalkable)
            {
                neighber.connectedTo = currenSearchNode;
                reached.Add(neighber.coordinates, neighber);
                frontier.Enqueue(neighber);
            }
        }
    }


    void BreadthFirstSearch(Vector2Int coordinates)
    {
        startNode.isWalkable = true;
        destinationNode.isWalkable = true;

        frontier.Clear();
        reached.Clear();


        bool isRunning = true;

        frontier.Enqueue(grid[coordinates]);
        reached.Add(coordinates, grid[coordinates]);


        while (frontier.Count > 0 && isRunning)
        {
            currenSearchNode = frontier.Dequeue();
            currenSearchNode.isExplored = true;
            ExploreNeighbers();

            if(currenSearchNode.coordinates == destinationCoordinates)
            {
                isRunning = false;
            }
        }
    }

    List<Node> BuildPath()
    {
        List<Node> path = new List<Node>();
        Node currentNode = destinationNode;

        path.Add(currentNode);
        currentNode.isPath = true; 

        while(currentNode.connectedTo != null)
        {
            currentNode = currentNode.connectedTo;
            path.Add(currentNode);
            currentNode.isPath = true;
        }

        //경로를 다 돌고 역순으로 만듬
        path.Reverse();

        return path;

    }

    public bool WillBlockPath(Vector2Int coordinates)
    {
        if(grid.ContainsKey(coordinates))
        {

            bool previusState = grid[coordinates].isWalkable;

            grid[coordinates].isWalkable = false;
            List<Node> newPath = GetNewPath();

            grid[coordinates].isWalkable = previusState;


            if(newPath.Count <= 1)
            {
                GetNewPath();
                return true;
            }

          
        }

        return false;
    }


    public void NotifyReceivers()
    {
        BroadcastMessage("RecalculatePath", false, SendMessageOptions.DontRequireReceiver);
    }

}

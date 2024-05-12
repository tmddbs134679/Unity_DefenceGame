using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;


[RequireComponent(typeof(Enemy))]
public class EnermyMover : MonoBehaviour
{
    [SerializeField][Range(0f, 5f)] float speed = 1f;


    [SerializeField] List<Node> path = new List<Node>();
    
    [SerializeField] int AttackDamage = 10;

    Enemy enemy;
    GridManager gridManager;
    Pathfinder pathfinder;

    HP hp;

    // Start is called before the first frame update
    void OnEnable()
    {
        ReturnToStart();
        RecalculatePath(true);
        
    }

     void Awake()
    {
        enemy = GetComponent<Enemy>();
        hp = FindAnyObjectByType<HP>();
        gridManager = FindObjectOfType<GridManager>();
        pathfinder = FindObjectOfType<Pathfinder>();
    }

    void RecalculatePath(bool resetPath)
    {
        Vector2Int coordinates = new Vector2Int();

        if(resetPath)
        {
            coordinates = pathfinder.StartCoordinates;
        }
        else
        {
            coordinates = gridManager.GetCoordinatesFromPosition(transform.position);
        }

        StopAllCoroutines();
        
        path.Clear();
        path = pathfinder.GetNewPath(coordinates);
        StartCoroutine(FollowPath());
    }

    void ReturnToStart()
    {
        transform.position = gridManager.GetPositionFromCoordinates(pathfinder.StartCoordinates);
    }


    void FinishPath()
    {
        hp.Damage(AttackDamage);

       // enemy.StealGold();
        gameObject.SetActive(false);
    }

    IEnumerator FollowPath()
    {
        for(int i = 1 ; i<path.Count; ++i)
        {
            Vector3 startPosition = transform.position;

            Vector3 endpoofPosition = gridManager.GetPositionFromCoordinates(path[i].coordinates);
            float travelPercent = 0f;

           
            transform.LookAt(endpoofPosition);

            while (travelPercent < 1f)
            {
                travelPercent += Time.deltaTime * speed; 
                transform.position = Vector3.Lerp(startPosition, endpoofPosition, travelPercent);
                yield return new WaitForEndOfFrame();
            }
            

        }


        FinishPath();

    }
}

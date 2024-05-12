using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Unity.VisualScripting.Member;


public class Tower : MonoBehaviour
{
    [SerializeField] int cost = 75;
    [SerializeField] float buildDelay = 0.3f;
    [SerializeField] AudioClip ac;
    [SerializeField] AudioSource acSource;



    Vector2Int coordinates = new Vector2Int();
    GridManager gridManager;
    Tile tile;
    Bank bank;

    public int Cost
    {
        get
        {
            return cost;
        }
    }

    void Start()
    {
       
        StartCoroutine(Build());
        acSource = GetComponent<AudioSource>();
        bank = FindObjectOfType<Bank>();
        tile = FindObjectOfType<Tile>();
        gridManager = FindObjectOfType<GridManager>();
    }

    void Update()
    {
        Resell();
    }


    public bool CreateTower(Tower tower, Vector3 position)
    {
        Bank bank = FindObjectOfType<Bank>();
       // AS.


        if(bank == null)
        {
            return false;
        }
        
        if(bank.CurrenrBalance >= cost)
        {
            Instantiate(tower.gameObject, position, Quaternion.identity);
            bank.Withdraw(cost);
            return true;
        }

        return false;
    }

     void Resell()
    {
        if (Input.GetMouseButton(1))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.gameObject == gameObject)
                {

                

                    coordinates = gridManager.GetCoordinatesFromPosition(transform.position);

                    if (!(gridManager.GetNode(coordinates).isWalkable))
                    {
                        gridManager.GetNode(coordinates).isWalkable = true;
                        bank.ReturnMoney(this);

                        acSource.clip = ac;
                        acSource.Play();

                        Invoke("RecellObject", ac.length / 2);
                       
                    }


                }
            }
        }
    }

    //Recell하면서 소리난 후 제거
    void RecellObject()
    {
        Tower.Destroy(gameObject);
    }



    IEnumerator Build()
    {
        foreach(Transform child in transform)
        {
            child.gameObject.SetActive(false);

            foreach(Transform grandchild in child)
            {
                grandchild.gameObject.SetActive(false);
            }
        }



        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(true);
            yield return new WaitForSeconds(buildDelay);


            foreach (Transform grandchild in child)
            {
                grandchild.gameObject.SetActive(true);
            }
        }

    }



    
}

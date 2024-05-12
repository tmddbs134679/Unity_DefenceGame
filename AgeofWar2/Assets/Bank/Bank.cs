using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;




public class Bank : MonoBehaviour
{

    [SerializeField] int startingBalance = 150;
    [SerializeField] int CurrentBalance;
    [SerializeField] TextMeshProUGUI displayBalance;


    public int CurrenrBalance
    {
        get
        {
            return CurrentBalance;
        }
    }

    void Awake()
    {
        CurrentBalance = startingBalance;
        UpdateDisplay();
    }
    public void Deposit(int amount)
    {
        CurrentBalance += Mathf.Abs(amount);
        UpdateDisplay();

    }

    public void Withdraw(int amount)
    {
        CurrentBalance -= Mathf.Abs(amount);
        UpdateDisplay();

        if (CurrenrBalance < 0)
        {
            //lose
            ReloadScene();

        }
    }

    public void ReturnMoney(Tower tower)
    {
        int cost = tower.Cost;
        CurrentBalance += cost;
        UpdateDisplay();

        //땅 다시 설치 가능하게 만들어야함 
    }

    void UpdateDisplay()
    {
        displayBalance.text = "Gold : " + CurrentBalance;
    }


    void ReloadScene()
    {
        Scene currenscene = SceneManager.GetActiveScene();

        SceneManager.LoadScene(currenscene.buildIndex);

    }
    

}

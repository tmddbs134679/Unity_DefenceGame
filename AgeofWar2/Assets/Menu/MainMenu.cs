using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{

   public Button startBtn, EndBtn, EditBtn;


    void Awake()
    {
        startBtn.onClick.AddListener(OnClickGameStart);
        EndBtn.onClick.AddListener(OnClickGameEnd);
        EditBtn.onClick.AddListener(OnClickEdit);
    }
    public void OnClickGameStart()
    {

        Debug.Log("game Start");
        SceneManager.LoadScene("MiniGame");

    }

    public void OnClickGameEnd()
    {
        Debug.Log("game End");
        Application.Quit();

    }

    public void OnClickEdit()
    {
        Debug.Log("Edit");
    }

}

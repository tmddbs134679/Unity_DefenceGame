using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Round : MonoBehaviour
{
    [SerializeField] int RoundCount = 10;
    [SerializeField] TextMeshProUGUI GameLeveltxt;
    int GameLevel = 1;

    Dictionary<int, int> Level = new Dictionary<int, int>()
    {
        {1,10},
        {2,15},
        {3,20}
    };


    void Start()
    {
        GameLevelUpdate(GameLevel);
    }

    // Update is called once per frame
    void Update()
    {
        if(RoundCount <= 0)
        {


            NextLevel();
        }
    }

    void NextLevel()
    {
        GameLevel++;
        RoundCount = GetLevelCount(GameLevel);
        GameLevelUpdate(GameLevel);
    }

    void GameLevelUpdate(int level)
    {
        GameLeveltxt.text = "Lv." + level;
    }

    public void DisCount()
    {
        RoundCount--;
    }

    int GetLevelCount(int level)
    {
        // Level 딕셔너리에서 해당 레벨의 Count 값을 가져옵니다.
        int count;
        Level.TryGetValue(level, out count);
        return count;
    }

}

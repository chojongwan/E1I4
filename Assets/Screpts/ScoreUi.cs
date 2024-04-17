using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class ScoreUi : MonoBehaviour
{
    public static ScoreUi instance;

    public Text highScoreTxt; //최고기록 텍스트



    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public void ShowHS(int stg)
    {
        //각 스테이지 최대점수를 string변수에 할당
        string hScoreKey = "HighScore" + stg.ToString();
        //최고 기록을 출력
        highScoreTxt.text = ("최고 기록 : " + PlayerPrefs.GetInt(hScoreKey));
    }

    public void UpdateHighScore(int stg, int score)
    {
        //각 스테이지 최대점수를 string변수에 할당
        string hScoreKey = "HighScore" + stg.ToString();

        //현재 스테이지에 최대 점수가 있는지 확인
        if(PlayerPrefs.HasKey(hScoreKey))
        {
            //저장된 최대점수가 있다면 위에 string으로 할당된 키값(현재 최대점수)을 임시로 int값에 할당
            int hScore = PlayerPrefs.GetInt(hScoreKey);

            //현재 점수가 최대 점수보다 높으면 저장
            if (score > hScore)
            {
                PlayerPrefs.SetInt(hScoreKey, score);
            }
        }
        else
        {
            //최대점수가 존재하지 않으면 현재 점수를 저장
            PlayerPrefs.SetInt(hScoreKey, score);
        }
    }
}

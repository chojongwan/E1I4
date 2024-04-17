using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class ScoreUi : MonoBehaviour
{
    public static ScoreUi instance;

    public Text highScoreTxt; //�ְ��� �ؽ�Ʈ



    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public void ShowHS(int stg)
    {
        //�� �������� �ִ������� string������ �Ҵ�
        string hScoreKey = "HighScore" + stg.ToString();
        //�ְ� ����� ���
        highScoreTxt.text = ("�ְ� ��� : " + PlayerPrefs.GetInt(hScoreKey));
    }

    public void UpdateHighScore(int stg, int score)
    {
        //�� �������� �ִ������� string������ �Ҵ�
        string hScoreKey = "HighScore" + stg.ToString();

        //���� ���������� �ִ� ������ �ִ��� Ȯ��
        if(PlayerPrefs.HasKey(hScoreKey))
        {
            //����� �ִ������� �ִٸ� ���� string���� �Ҵ�� Ű��(���� �ִ�����)�� �ӽ÷� int���� �Ҵ�
            int hScore = PlayerPrefs.GetInt(hScoreKey);

            //���� ������ �ִ� �������� ������ ����
            if (score > hScore)
            {
                PlayerPrefs.SetInt(hScoreKey, score);
            }
        }
        else
        {
            //�ִ������� �������� ������ ���� ������ ����
            PlayerPrefs.SetInt(hScoreKey, score);
        }
    }
}

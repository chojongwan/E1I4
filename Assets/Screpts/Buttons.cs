using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Buttons : MonoBehaviour
{
    public GameObject StageImg;

    public void StageSelect()
    {
        StageImg.SetActive(true);
    }

    public void StageScene()
    {
        AudioManager.instance.ChangeMusic(0);
        StageSelect();
    }

    // ���� �����, ������ �� �� �̵�
    public void GameStart()
    {
        SceneManager.LoadScene("MainScene");
    }

    // 1��������
    public void Stage1()
    {
        // StageCard�� int�� ������ 6�� ����
        PlayerPrefs.SetInt("StageCard", 6);
        GameStart();
    }

    // 2��������
    public void Stage2()
    {
        PlayerPrefs.SetInt("StageCard", 8);
        GameStart();
    }

    // 3��������
    public void Stage3()
    {
        PlayerPrefs.SetInt("StageCard", 10);
        GameStart();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class Buttons : MonoBehaviour
{
    public GameObject StageImg;

    // �������� �������� Ȱ��ȭ ��Ű�� ���
    public void StageSelect()
    {
        StageImg.SetActive(true);
    }

    // ���� �����, ������ �� �� �̵�
    public void GameStart(UnityAction<Scene, LoadSceneMode> callback)
    {
        SceneManager.sceneLoaded += callback;
        SceneManager.LoadScene("MainScene");
    }

    // Start������ ���� ���۹�ư ���
    public void GameReStart()
    {
        SceneManager.LoadScene("MainScene");
        AudioManager.instance.ChangeMusic(0);
    }

    // ó�� ���� ������ �̵��ϴ� ���
    public void Lobby()
    {
        SceneManager.LoadScene("StartScene");
        AudioManager.instance.ChangeMusic(0);
        Gamemanager.instance.time = 0;
    }

    // 1��������
    public void Stage1()
    {
        // StageCard�� int�� ������ 6�� ����
        PlayerPrefs.SetInt("StageCard", 6);
        GameStart((scece, mode) =>
        {
            Gamemanager.instance.stage = 1;
        });
       
    }

    // 2��������
    public void Stage2()
    {
        PlayerPrefs.SetInt("StageCard", 8);
        GameStart((scece, mode) =>
        {
            Gamemanager.instance.stage = 2;
        });
    }

    // 3��������
    public void Stage3()
    {
        PlayerPrefs.SetInt("StageCard", 10);
        GameStart((scece, mode) =>
        {
            Gamemanager.instance.stage = 3;
        });
    }

    public void Next() // ���� ���������� ���� ��ư
    {
        if (PlayerPrefs.GetInt("StageCard") == 6)
        {
            Stage2();
        }
        else if (PlayerPrefs.GetInt("StageCard") == 8)
        {
            Stage3();
        }
        AudioManager.instance.ChangeMusic(0);
        gameObject.SetActive(false);
    }
}

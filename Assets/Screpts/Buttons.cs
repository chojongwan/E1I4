using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Buttons : MonoBehaviour
{
    public GameObject StageImg;
    public GameObject RuleImg;

    public GameObject unLock;

    // �������� �������� Ȱ��ȭ ��Ű�� ���
    public void StageSelect()
    {
        StageImg.SetActive(true);
        PlayerPrefs.DeleteAll();
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

    public void RuleButton()
    {
        RuleImg.SetActive(true);
    }

    // ó�� ���� ������ �̵��ϴ� ���
    public void Lobby()
    {
        SceneManager.LoadScene("StartScene");
        AudioManager.instance.ChangeMusic(0);
        Gamemanager.instance.time = 0;
        Time.timeScale = 1f;
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

        Gamemanager.stage1Clear = true;
    }

    // 2��������
    public void Stage2()
    {
        if (Gamemanager.stage1Clear)
        {
            
            PlayerPrefs.SetInt("StageCard", 8);
            GameStart((scece, mode) =>
            {
                Gamemanager.instance.stage = 2;
            });
        }
        else
        {
            unLock.SetActive(true);
            Invoke("InvokeUnLock", 0.5f);
        }
    }

    // 3��������
    public void Stage3()
    {
        if (Gamemanager.stage2Clear)
        {
            PlayerPrefs.SetInt("StageCard", 10);
            GameStart((scece, mode) =>
            {
                Gamemanager.instance.stage = 3;
            });
        }
        else
        {
            unLock.SetActive(true);
            Invoke("InvokeUnLock", 0.5f);
        }
    }

    public void InvokeUnLock()
    {
        unLock.SetActive(false);
    }

    public void Next() // ���� ���������� ���� ��ư
    {
        if (Gamemanager.instance.stage == 1)
        {
            Stage2();
        }
        else if (Gamemanager.instance.stage == 2)
        {
            Stage3();
        }
        
        AudioManager.instance.ChangeMusic(0);
        gameObject.SetActive(false);
    }
}

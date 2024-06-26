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
    public GameObject Resultimg;

    public GameObject GoLobby;
    public GameObject unLock;

    // 스테이지 선택지를 활성화 시키는 기능
    public void StageSelect()
    {
        StageImg.SetActive(true);
    }

    // 로비로 여부 확인하는 버튼
    public void GoToLobby()
    {
        GoLobby.SetActive(true);
    }

    public void LobbyCancel()
    {
        GoLobby.SetActive(false);
    }

    public void ResultEnd()
    {
        Resultimg.SetActive(false);
    }

    // 게임 재시작, 시작할 때 씬 이동
    public void GameStart(UnityAction<Scene, LoadSceneMode> callback)
    {
        SceneManager.sceneLoaded += callback;
        SceneManager.LoadScene("MainScene");
    }

    // Start씬에서 게임 시작버튼 기능
    public void GameReStart()
    {
        SceneManager.LoadScene("MainScene");
        AudioManager.instance.ChangeMusic(0);
    }

    public void RuleButton()
    {
        RuleImg.SetActive(true);
    }

    // 처음 게임 씬으로 이동하는 기능
    public void Lobby()
    {
        SceneManager.LoadScene("StartScene");
        AudioManager.instance.ChangeMusic(0);
        Gamemanager.instance.time = 0;
        Time.timeScale = 1f;
    }

    // 1스테이지
    public void Stage1()
    {
        // StageCard의 int형 변수에 6을 저장
        PlayerPrefs.SetInt("StageCard", 6);
        GameStart((scece, mode) =>
        {
            Gamemanager.instance.stage = 1;
        });
    }

    // 2스테이지
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

    // 3스테이지
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

    public void Next() // 다음 스테이지로 가는 버튼
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

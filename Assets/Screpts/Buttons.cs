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

    // 게임 재시작, 시작할 때 씬 이동
    public void GameStart()
    {
        SceneManager.LoadScene("MainScene");
    }

    // 1스테이지
    public void Stage1()
    {
        // StageCard의 int형 변수에 6을 저장
        PlayerPrefs.SetInt("StageCard", 6);
        GameStart();
    }

    // 2스테이지
    public void Stage2()
    {
        PlayerPrefs.SetInt("StageCard", 8);
        GameStart();
    }

    // 3스테이지
    public void Stage3()
    {
        PlayerPrefs.SetInt("StageCard", 10);
        GameStart();
    }
}

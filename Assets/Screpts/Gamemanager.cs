using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Gamemanager : MonoBehaviour
{

    public static Gamemanager instance;
    public Card fristCard;
    public Card secondCard;

    AudioSource audioSource;
    public AudioClip clip;
    public AudioClip failClip; //실패 효과음
    public Text TimeTxt;
    public GameObject ResultImg;
    public Text scoreText; // 점수
    public Text timeText;  // 남은 시간
    public Text ClearText; // 클리어 텍스트
    public GameObject TeamNameTxt; // 팀원 이름 텍스트
    public GameObject FailureTxt; // 실패 텍스트

    public void PlayFailSound()
    {
        audioSource.PlayOneShot(failClip);
    }


    public int CardCount=0;
    public float time = 0.00f;                     //-8초(임시)

    public Text matchTxt; //매치 시도 횟수 ui텍스트
    int matchCount = 0;   //매치 시도 횟수 변수

    public Slider limitBar;    //제한시간 ui
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    void Start()
    {
        // 시작 시 코루틴 시작
        StartCoroutine("TextChange");
        Debug.Log("매니져");
        Time.timeScale = 1f;
        audioSource = GetComponent<AudioSource>();
        //Board.instance.ReceiveValueToModify(time);
    }

    void Update()
    {
        time += Time.deltaTime;
        TimeTxt.text = time.ToString("N2");
        if (time > 30.0f)
        {
            Time.timeScale = 0.0f;
            ResultText(1);
        }
        matchTxt.text = ("매치 : " + matchCount);  //매치 시도 횟수 표시

        if (fristCard != null)
        {
            limitBar.gameObject.SetActive(true);  //첫번째 카드가 뒤집어 졌을 때 시간제한 ui표시
            limitBar.value -= Time.deltaTime;     //시간 제한 ui가 줄어듦
            if (limitBar.value <= 0.0f)
            {
                fristCard.CloseCardInvoke();            //시간제한 게이지가 전부 줄어들 시 첫번째 카드를 되돌려놓음
                fristCard = null;
            }
        }
        else
        {
            limitBar.gameObject.SetActive(false); //첫번째 카드를 뒤집지 않았거나 다시 원래대로 돌려놓을 경우 시간제한 ui 숨김
            limitBar.value = 5f;                  //시간제한 초기화
        }
    }

    public void ResultText(int number)
    {
        ResultImg.SetActive(true);
        if (number == 0)
        {
            ClearText.text = "Game Clear!!!";
            float TT = 30 - time;
            timeText.text = TT.ToString("N0");
        }
        else
        {
            ClearText.text = "Game Over...";
            timeText.text = "X";
        }

        int ST = 1000 - matchCount * 2;
        scoreText.text = ST.ToString();
    }

    public void DestroyTxt() // 성공, 실패 텍스트 삭제용
    {
        TeamNameTxt.SetActive(false);
        FailureTxt.SetActive(false);
    }

    public void Matched()
    {
        if (fristCard.idx == secondCard.idx)
        {
            audioSource.PlayOneShot(clip);
            fristCard.DestoryCard();
            secondCard.DestoryCard();
            time -= 2.0f;
            matchCount++;   	//매치 시도 시 횟수 증가
            CardCount -= 2;

            int idx = -1;  // 초기값 설정
            string name = "";  // 이름 변수 추가

            switch (fristCard.idx)
            {
                case 1:
                case 6:
                    idx = 1;
                    name = "고지후";
                    break;
                case 2:
                case 9:
                    idx = 2;
                    name = "하영빈";
                    break;
                case 3:
                case 10:
                    idx = 3;
                    name = "이동훈";
                    break;
                case 4:
                case 7:
                    idx = 4;
                    name = "이재형";
                    break;
                case 5:
                case 8:
                    idx = 5;
                    name = "조종완";
                    break;
            }

            if (fristCard.frontImage.sprite != null)
            {
                TeamNameTxt.GetComponent<Text>().text = name;  // 이름을 텍스트로 설정
                TeamNameTxt.SetActive(true);
                Invoke("DestroyTxt", 0.5f); // 팀이름 텍스트 삭제
            }
            else
            {
                TeamNameTxt.GetComponent<Text>().text = "스프라이트를 찾을 수 없습니다.";
            }

            if (CardCount == 0)
            {
                ResultText(0);
                Time.timeScale = 0f;
                DestroyTxt();
            }
        }
        else
        {
            fristCard.CloseCard();
            secondCard.CloseCard();
            matchCount++;       //매치 시도 시 횟수 증가
            time += 1.0f;

            FailureTxt.SetActive(true); // 실패 텍스트 생성
            PlayFailSound();  // 실패 효과음 재생
            Invoke("DestroyTxt", 0.5f); // 실패 텍스트 삭제
        }
        fristCard = null;
        secondCard = null;
    }

    IEnumerator TextChange()
    {
        // 글씨(하양 => 빨강 / 빨강 => 하양)으로 계속 변경
        while (true)
        {
            // 15초가 지나면 텍스트 반복 변경 
            if (time > 15)
            {
                TimeTxt.color = new Color(255, 0, 0);
                yield return new WaitForSeconds(0.5f);
                TimeTxt.color = new Color(255, 255, 255);
                yield return new WaitForSeconds(0.5f);
            }
            // 지나지 않으면 변경 X
            else
            {
                yield return 0;
            }
        }
    }
}

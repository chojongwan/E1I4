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
    public AudioSource failAudioSource; //실패 오디오 추가
    public AudioClip failClip; //실패 오디오 추가

    public Text TimeTxt;
    public Text memberNameText;
    public GameObject EndTxt;

    public GameObject SuccessTxt; //성공 텍스트 추가
    public GameObject FailureTxt; //실패 텍스트 추가

    public int CardCount=0;
    float time = 0.00f;                     //-8초(임시)

    public Text matchTxt; //매치 시도 횟수 ui텍스트
    int matchCount = 0;   //매치 시도 횟수 변수
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
            EndTxt.SetActive(true);

        }
        matchTxt.text = ("매치 횟수 : " + matchCount);  //매치 시도 횟수 표시
    }

    public void DestroyTxt()
    {
        SuccessTxt.SetActive(false);
        FailureTxt.SetActive(false);
    }

    public void PlayFailSound()
    {
        failAudioSource.PlayOneShot(failClip);
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

            if (CardCount == 0)
            {
                EndTxt.SetActive(true);
                Time.timeScale = 0f;
            }
        }
        else
        {
            fristCard.CloseCard();
            secondCard.CloseCard();
            matchCount++;   	//매치 시도 시 횟수 증가
            time += 1.0f;

            FailureTxt.SetActive(true);
            Invoke("DestroyTxt", 0.5f); //실패 효과

            PlayFailSound(); //실패 사운드
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

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
    public Text TimeTxt;
    public GameObject EndTxt;

    public int CardCount=0;
    float time = 0.00f;                     //-8초(임시)

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
            EndTxt.SetActive(true);

        }
        matchTxt.text = ("매치 횟수 : " + matchCount);  //매치 시도 횟수 표시

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
            if(CardCount == 0)
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

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
        Debug.Log("ddddd");
        time += Time.deltaTime;
        TimeTxt.text = time.ToString("N2");
        if (time > 30.0f)
        {
            Time.timeScale = 0.0f;
            EndTxt.SetActive(true);

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

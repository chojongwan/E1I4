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
    float time = 0.00f;                     //-8��(�ӽ�)

    public Text matchTxt; //��ġ �õ� Ƚ�� ui�ؽ�Ʈ
    int matchCount = 0;   //��ġ �õ� Ƚ�� ����

    public Slider limitBar;    //���ѽð� ui
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    void Start()
    {
        // ���� �� �ڷ�ƾ ����
        StartCoroutine("TextChange");
        Debug.Log("�Ŵ���");
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
        matchTxt.text = ("��ġ Ƚ�� : " + matchCount);  //��ġ �õ� Ƚ�� ǥ��

        if (fristCard != null)
        {
            limitBar.gameObject.SetActive(true);  //ù��° ī�尡 ������ ���� �� �ð����� uiǥ��
            limitBar.value -= Time.deltaTime;     //�ð� ���� ui�� �پ��
            if (limitBar.value <= 0.0f)
            {
                fristCard.CloseCardInvoke();            //�ð����� �������� ���� �پ�� �� ù��° ī�带 �ǵ�������
                fristCard = null;
            }
        }
        else
        {
            limitBar.gameObject.SetActive(false); //ù��° ī�带 ������ �ʾҰų� �ٽ� ������� �������� ��� �ð����� ui ����
            limitBar.value = 5f;                  //�ð����� �ʱ�ȭ
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
            matchCount++;   	//��ġ �õ� �� Ƚ�� ����
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
            matchCount++;   	//��ġ �õ� �� Ƚ�� ����
            time += 1.0f;
        }
        fristCard = null;
        secondCard = null;
        
    }



    IEnumerator TextChange()
    {
        // �۾�(�Ͼ� => ���� / ���� => �Ͼ�)���� ��� ����
        while (true)
        {
            // 15�ʰ� ������ �ؽ�Ʈ �ݺ� ���� 
            if (time > 15)
            {
                TimeTxt.color = new Color(255, 0, 0);
                yield return new WaitForSeconds(0.5f);
                TimeTxt.color = new Color(255, 255, 255);
                yield return new WaitForSeconds(0.5f);
            }
            // ������ ������ ���� X
            else
            {
                yield return 0;
            }
        }
    }
}

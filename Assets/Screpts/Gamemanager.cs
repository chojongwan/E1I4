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
    public AudioClip failClip; //���� ȿ����
    public Text TimeTxt;
    public GameObject ResultImg;
    public Text scoreText; // ����
    public Text timeText;  // ���� �ð�
    public Text ClearText; // Ŭ���� �ؽ�Ʈ
    public GameObject TeamNameTxt; // ���� �̸� �ؽ�Ʈ
    public GameObject FailureTxt; // ���� �ؽ�Ʈ

    public void PlayFailSound()
    {
        audioSource.PlayOneShot(failClip);
    }


    public int CardCount=0;
    public float time = 0.00f;                     //-8��(�ӽ�)

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
            ResultText(1);
        }
        matchTxt.text = ("��ġ : " + matchCount);  //��ġ �õ� Ƚ�� ǥ��

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

    public void DestroyTxt() // ����, ���� �ؽ�Ʈ ������
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
            matchCount++;   	//��ġ �õ� �� Ƚ�� ����
            CardCount -= 2;

            int idx = -1;  // �ʱⰪ ����
            string name = "";  // �̸� ���� �߰�

            switch (fristCard.idx)
            {
                case 1:
                case 6:
                    idx = 1;
                    name = "������";
                    break;
                case 2:
                case 9:
                    idx = 2;
                    name = "�Ͽ���";
                    break;
                case 3:
                case 10:
                    idx = 3;
                    name = "�̵���";
                    break;
                case 4:
                case 7:
                    idx = 4;
                    name = "������";
                    break;
                case 5:
                case 8:
                    idx = 5;
                    name = "������";
                    break;
            }

            if (fristCard.frontImage.sprite != null)
            {
                TeamNameTxt.GetComponent<Text>().text = name;  // �̸��� �ؽ�Ʈ�� ����
                TeamNameTxt.SetActive(true);
                Invoke("DestroyTxt", 0.5f); // ���̸� �ؽ�Ʈ ����
            }
            else
            {
                TeamNameTxt.GetComponent<Text>().text = "��������Ʈ�� ã�� �� �����ϴ�.";
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
            matchCount++;       //��ġ �õ� �� Ƚ�� ����
            time += 1.0f;

            FailureTxt.SetActive(true); // ���� �ؽ�Ʈ ����
            PlayFailSound();  // ���� ȿ���� ���
            Invoke("DestroyTxt", 0.5f); // ���� �ؽ�Ʈ ����
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

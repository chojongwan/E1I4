using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
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
    public GameObject ResultImg; // ������
    public Text scoreText; // ����
    public Text timeText;  // ���� �ð�
    public Text ClearText; // Ŭ���� �ؽ�Ʈ
    public Text highScoreTxt; //�ְ��� �ؽ�Ʈ
    public GameObject next; // ���� ��������
    public GameObject TeamNameTxt; // ���� �̸� �ؽ�Ʈ
    public GameObject FailureTxt; // ���� �ؽ�Ʈ
    bool GameEnd = true; // ���� �������� ����

    public int stage; //��������
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
        
        // 30�� �� ���ӽð��� ���� ���ٸ�
        if (time > 30.0f && GameEnd)
        {
            Time.timeScale = 0.0f;
            GameEnd = false;
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
        if (stage == 1)
        {
            highScoreTxt.text = ("�ְ� ��� : " + PlayerPrefs.GetInt("HighScore1"));
        }
        else if (stage == 2)
        {
            highScoreTxt.text = ("�ְ� ��� : " + PlayerPrefs.GetInt("HighScore2"));
        }
        else if (stage == 3)
        {
            highScoreTxt.text = ("�ְ� ��� : " + PlayerPrefs.GetInt("HighScore3"));
        }

    }

    // ���� ���� �� ������
    public void ResultText(int number)
    {
        // ������ Ȱ��ȭ
        ResultImg.SetActive(true);
        // ���â �뷡 ���
        AudioManager.instance.ChangeMusic(2);
        // number == 0 �� ���� Ŭ�������� ���
        if (number == 0)
        {
            ClearText.text = "Game Clear!!!";
            float TT = 30 - time;
            timeText.text = TT.ToString("N0");
            if (stage < 3)
            {
                next.SetActive(true);
            }
           
        }
        // �� �ܴ� ���� ����
        else
        {
            ClearText.text = "Game Over...";
            timeText.text = "X";
            next.SetActive(false);
            DestroyTxt();
        }
        // ������ �⺻ 1000�� ��Ī Ƚ���� 2�迡 ���� �ð� �� 100���� ����
        int ST = 1000 - matchCount * 30 + (30 - (int)time) * 100;
        scoreText.text = ST.ToString();

        if (stage == 1)          //�������� 1�϶�
        {
            if (PlayerPrefs.HasKey("HighScore1"))               //�������� 1�� ���̽��ھ �ִٸ�
            {
                int hScore1 = PlayerPrefs.GetInt("HighScore1");     //���̽��ھ �ҷ����� ���� ������ ���Ͽ�
                if (hScore1 < ST)
                {
                    PlayerPrefs.SetInt("HighScore1", ST);       //����
                }
            }
            else                                                //�ƴ϶��
            {
                PlayerPrefs.SetInt("HighScore1", ST);           //�׳� ����
            }
        }
        if (stage == 2)          //�������� 2�϶�
        {
            if (PlayerPrefs.HasKey("HighScore2"))               //�������� 2�� ���̽��ھ �ִٸ�
            {
                int hScore2 = PlayerPrefs.GetInt("HighScore2");     //���̽��ھ �ҷ����� ���� ������ ���Ͽ�
                if (hScore2 < ST)
                {
                    PlayerPrefs.SetInt("HighScore2", ST);       //����
                }
            }
            else                                                //�ƴ϶��
            {
                PlayerPrefs.SetInt("HighScore2", ST);           //�׳� ����
            }
        }
        if (stage == 3)          //�������� 3�϶�
        {
            if (PlayerPrefs.HasKey("HighScore3"))               //�������� 3�� ���̽��ھ �ִٸ�
            {
                int hScore3 = PlayerPrefs.GetInt("HighScore3");     //���̽��ھ �ҷ����� ���� ������ ���Ͽ�
                if (hScore3 < ST)
                {
                    PlayerPrefs.SetInt("HighScore3", ST);       //����
                }
            }
            else                                                //�ƴ϶��
            {
                PlayerPrefs.SetInt("HighScore3", ST);           //�׳� ����
            }
        }
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
            if(time < 0)
            {
                time = 0;
            }
            matchCount++;   	//��ġ �õ� �� Ƚ�� ����
            CardCount -= 2;

            string name = "";  // �̸� ���� �߰�

            switch (fristCard.idx)
            {
                case 1:
                case 6:
                    name = "������";
                    break;
                case 2:
                case 9:
                    name = "�Ͽ���";
                    break;
                case 3:
                case 10:
                    name = "�̵���";
                    break;
                case 4:
                case 7:
                    name = "������";
                    break;
                case 5:
                case 8:
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
                Time.timeScale = 0f;
                ResultText(0);
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

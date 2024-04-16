using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour
{
    public int idx = 0;
    public SpriteRenderer frontImage;

    public GameObject front;
    public GameObject back;
    public GameObject back_Un;      //�߰�


    public Animator anim;
    AudioSource audioSource;
    public AudioClip clip;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        
    }
    public void Setting(int number)
    {
        idx = number;
        frontImage.sprite = Resources.Load<Sprite>($"rtan{idx}");        //idx���� ī���� ��ȣ��
        

    }
    public void OpenCard()
    {
        audioSource.PlayOneShot(clip);      //�ٸ� ����� �ҽ��� ��ġ�� �ʰ� �ѹ��� ����
        anim.SetBool("IsOpen",true);
        front.SetActive(true);
        back.SetActive(false);
        back_Un.SetActive(false);           //�߰�
        transform.rotation = Quaternion.identity;
        

        if (Gamemanager.instance.fristCard == null)
        {
            Gamemanager.instance.fristCard = this;
        }
        else
        {
            Gamemanager.instance.secondCard = this;
            Gamemanager.instance.Matched();
        }
    }
    public void DestoryCard()
    {
        Invoke("DestroyCardInvoke",1f);
    }
    void DestroyCardInvoke()
    {
        Destroy(gameObject);

    }

    public void CloseCard()
    {
        Invoke("CloseCardInvoke", 1f);
    }
    void CloseCardInvoke()
    {
        anim.SetBool("IsOpen",false);
        front.SetActive(false);
        back_Un.SetActive(true);            //�߰�
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour
{
    public int idx = 0;
    public SpriteRenderer frontImage;

    public GameObject front;
    public GameObject back;
    public GameObject back_Un;      //추가


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
        frontImage.sprite = Resources.Load<Sprite>($"rtan{idx}");        //idx값이 카드의 번호값
        

    }
    public void OpenCard()
    {
        audioSource.PlayOneShot(clip);      //다른 오디오 소스와 곂치지 않고 한번만 실행
        anim.SetBool("IsOpen",true);
        front.SetActive(true);
        back.SetActive(false);
        back_Un.SetActive(false);           //추가
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
        back_Un.SetActive(true);            //추가
    }
}

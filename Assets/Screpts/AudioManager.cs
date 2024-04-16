using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;    

    AudioSource audioSource;
    public AudioClip[] clip;
    private bool soundCheck = true;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);          //���� �̵��Ҷ� �ش� ������Ʈ�� �ı����� �ʰ� �״�� ���� �Ѿ�� �ڵ�
        }
        else
        {
            Destroy(gameObject);                    //�̱����� 2���� �����Ҷ� instance�� �̹� �����Ƿ� �������� �ı��Ͽ� �ϳ��� �������ϴ� �ڵ�
        }
    }
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = clip[0];
        audioSource.Play();
    }

    private void Update()
    {
        ChangeMusic(1);
    }

    public void ChangeMusic(int number)
    {
        if (number==0)
        {
            audioSource.clip = clip[0];
            audioSource.Play();
        }

        if(Gamemanager.instance?.time > 15.0f && soundCheck && number==1)
        {
            audioSource.clip = clip[1];
            audioSource.Play();
            soundCheck = false;
        }
    }
}

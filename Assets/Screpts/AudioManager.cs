using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;    

    AudioSource audioSource;
    public AudioClip[] clip;
    bool soundCheck = true;

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
        instance.soundCheck = true;
    }

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = clip[0];
        audioSource.Play();
    }

    private void Update()
    {
        // �뷡 ����
        ChangeMusic(1);
    }

    // �뷡 ����
    public void ChangeMusic(int number)
    {
        // ���� �뷡
        if (number==0)
        {
            audioSource.clip = clip[0];
            audioSource.Play();
        }
        else if(number == 2)
        {
            audioSource.clip = clip[2];
            audioSource.Play();
        }

        // ���� �뷡
        if(Gamemanager.instance?.time > 15.0f && soundCheck && number==1)
        {
            audioSource.clip = clip[1];
            audioSource.Play();
            soundCheck = false;
        }
    }
}

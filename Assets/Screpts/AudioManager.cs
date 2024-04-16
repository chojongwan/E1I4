using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;    

    AudioSource audioSource;
    public AudioClip[] clip;
    private bool ChangeCheck = true;

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

    public void ChangeMusic(int Number)
    {
        if (Number == 0)
        {
            instance.audioSource.clip = clip[Number];
            instance.audioSource.Play();
        }
        if (Number == 1 && ChangeCheck && Gamemanager.instance?.time > 15.0f)
        {
            instance.audioSource.clip = clip[Number];
            instance.audioSource.Play();
            ChangeCheck = false;
        }
    }
}

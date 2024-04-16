using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;    

    AudioSource audioSource;
    public AudioClip clip;
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
        audioSource.clip = clip;
        audioSource.Play();
    }
}

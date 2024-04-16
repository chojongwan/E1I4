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
            DontDestroyOnLoad(gameObject);          //씬을 이동할때 해당 오브젝트를 파괴하지 않고 그대로 씬을 넘어가는 코드
        }
        else
        {
            Destroy(gameObject);                    //싱글톤이 2개가 존재할때 instance가 이미 있으므로 나머지를 파괴하여 하나만 존재케하는 코드
        }
    }
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = clip;
        audioSource.Play();
    }
}

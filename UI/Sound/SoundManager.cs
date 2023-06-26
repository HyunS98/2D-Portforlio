using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class SoundManager : MonoBehaviour
{
    public AudioMixer audioMixer;   // 볼륨을 조절하는 오디오믹서

    public AudioSource clickSound;  // 버튼 클릭마다 나오는 소리

    // ■■■■■■ 마스터, BGM, 버튼클릭 사운드 조절 ■■■■■■
    public Slider masterSlider;     
    public Slider bgmSlider;
    public Slider buttonSlider;

    GameObject SoundBox;            // 사운드 박스(설정창)

    void Awake()
    {
        var obj = FindObjectsOfType<SoundManager>();
        if (obj.Length == 1)
        {
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Update()
    {
        // ■■■■■■ 씬 이동간의 SoundBox 찾기 ■■■■■■
        SoundBox = GameObject.Find("SoundBox");
    }

    // ■■■■■■ 마스터 볼륨 조절 ■■■■■■
    public void SetMasterVolme()
    {
        audioMixer.SetFloat("Master", masterSlider.value);
    }

    // ■■■■■■ BGM 볼륨 조절 ■■■■■■
    public void SetBgmVolme()
    {
        audioMixer.SetFloat("BGM", bgmSlider.value);
    }

    // ■■■■■■ 버튼 볼륨 조절 ■■■■■■
    public void SetButtonVolme()
    {
        audioMixer.SetFloat("Click", buttonSlider.value);
    }

    // ■■■■■■ 버튼 소리 활성화 ■■■■■■
    public void ClickSound()
    {
        clickSound.Play();
    }

    // ■■■■■■ 사운드 옵션창 활성화 ■■■■■■
    public void SoundCheckBox()
    {
        SoundBox.transform.position = new Vector2(960, 540);
    }

    // ■■■■■■ 사운드 옵션창 비활성화 ■■■■■■
    public void ExitSoundCheckBox()
    {
        SoundBox.transform.position = new Vector2(0, 1500);
    }

}


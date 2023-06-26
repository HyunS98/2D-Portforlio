using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class SoundManager : MonoBehaviour
{
    public AudioMixer audioMixer;   // ������ �����ϴ� ������ͼ�

    public AudioSource clickSound;  // ��ư Ŭ������ ������ �Ҹ�

    // ������� ������, BGM, ��ưŬ�� ���� ���� �������
    public Slider masterSlider;     
    public Slider bgmSlider;
    public Slider buttonSlider;

    GameObject SoundBox;            // ���� �ڽ�(����â)

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
        // ������� �� �̵����� SoundBox ã�� �������
        SoundBox = GameObject.Find("SoundBox");
    }

    // ������� ������ ���� ���� �������
    public void SetMasterVolme()
    {
        audioMixer.SetFloat("Master", masterSlider.value);
    }

    // ������� BGM ���� ���� �������
    public void SetBgmVolme()
    {
        audioMixer.SetFloat("BGM", bgmSlider.value);
    }

    // ������� ��ư ���� ���� �������
    public void SetButtonVolme()
    {
        audioMixer.SetFloat("Click", buttonSlider.value);
    }

    // ������� ��ư �Ҹ� Ȱ��ȭ �������
    public void ClickSound()
    {
        clickSound.Play();
    }

    // ������� ���� �ɼ�â Ȱ��ȭ �������
    public void SoundCheckBox()
    {
        SoundBox.transform.position = new Vector2(960, 540);
    }

    // ������� ���� �ɼ�â ��Ȱ��ȭ �������
    public void ExitSoundCheckBox()
    {
        SoundBox.transform.position = new Vector2(0, 1500);
    }

}


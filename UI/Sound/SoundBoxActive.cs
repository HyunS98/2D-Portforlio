using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SoundBoxActive : MonoBehaviour
{
    // ������� �ɼ� ���� �������� �������
    public GameObject button;

    void Update()
    {
        // ������� ����ȣ�� ���� �ɼ� ��ư�� Ȱ��ȭ or ��Ȱ��ȭ �������
        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            button.SetActive(true);
        }
        else
        {
            button.SetActive(false);
        }
    }
}

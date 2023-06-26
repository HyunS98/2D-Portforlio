using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SoundBoxActive : MonoBehaviour
{
    // ■■■■■■ 옵션 변수 가져오기 ■■■■■■
    public GameObject button;

    void Update()
    {
        // ■■■■■■ 씬번호를 통해 옵션 버튼을 활성화 or 비활성화 ■■■■■■
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

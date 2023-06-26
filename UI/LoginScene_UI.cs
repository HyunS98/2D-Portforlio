using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoginScene_UI : MonoBehaviour
{
    public GameObject ExitCheckBox;
    public GameObject helpBox;

    // ■■■■■■ 게임 시작 ■■■■■■
    public void GameStart()
    {
        SceneManager.LoadSceneAsync(2);
    }

    // ■■■■■■ 게임 종료 ■■■■■■
    public void GameExit()
    {
        Application.Quit();
    }

    // ■■■■■■ 게임 종료 박스 활성 ■■■■■■
    public void CheckBox()
    {
        ExitCheckBox.SetActive(true);
    }

    // ■■■■■■ 게임 종료 취소 ■■■■■■
    public void ExitCancle()
    {
        ExitCheckBox.SetActive(false);
    }

    // ■■■■■■ 도움말 열기 ■■■■■■
    public void OpenHelpBox()
    {
        helpBox.SetActive(true);
    }

    // ■■■■■■ 도움말 닫기 ■■■■■■
    public void CloseHelpBox()
    {
        helpBox.SetActive(false);
    }
}

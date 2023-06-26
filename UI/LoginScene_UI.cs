using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoginScene_UI : MonoBehaviour
{
    public GameObject ExitCheckBox;
    public GameObject helpBox;

    // ������� ���� ���� �������
    public void GameStart()
    {
        SceneManager.LoadSceneAsync(2);
    }

    // ������� ���� ���� �������
    public void GameExit()
    {
        Application.Quit();
    }

    // ������� ���� ���� �ڽ� Ȱ�� �������
    public void CheckBox()
    {
        ExitCheckBox.SetActive(true);
    }

    // ������� ���� ���� ��� �������
    public void ExitCancle()
    {
        ExitCheckBox.SetActive(false);
    }

    // ������� ���� ���� �������
    public void OpenHelpBox()
    {
        helpBox.SetActive(true);
    }

    // ������� ���� �ݱ� �������
    public void CloseHelpBox()
    {
        helpBox.SetActive(false);
    }
}

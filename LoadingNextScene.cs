using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingNextScene : MonoBehaviour
{
    public int sceneNumber;

    void Start()
    {
        //StartCoroutine(NextScene(sceneNumber));
        NextScene(sceneNumber);
    }

    void NextScene(int num)
    {
        // �񵿱� �������� �ε��Ѵ�.
        AsyncOperation ao = SceneManager.LoadSceneAsync(num);

        // �ε�� ���� ����� ȭ�鿡 ������ �ʰ� �Ѵ�.
        ao.allowSceneActivation = false;

        // ���� ���� �غ� �ȉ�ٸ�
        while (!ao.isDone)
        {
            // 90% �� ����� ���¸�
            if (ao.progress > 0.9f)
            {
                // �ε� ���� �����ش�.
                ao.allowSceneActivation = true;
            }
        }
    }

    //IEnumerator NextScene(int num)
    //{
    //    // �񵿱� �������� �ε��Ѵ�.
    //    AsyncOperation ao = SceneManager.LoadSceneAsync(num);

    //    // �ε�� ���� ����� ȭ�鿡 ������ �ʰ� �Ѵ�.
    //    ao.allowSceneActivation = false;

    //    // ���� ���� �غ� �ȉ�ٸ�
    //    while (!ao.isDone)
    //    {
    //        // 90% �� ����� ���¸�
    //        if (ao.progress > 0.9f)
    //        {
    //            // �ε� ���� �����ش�.
    //            ao.allowSceneActivation = true;
    //        }
    //    }

    //    yield return null;
    //}
}

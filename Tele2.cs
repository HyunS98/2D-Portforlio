using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Tele2 : MonoBehaviour
{
    public int sceneNumber;
    bool inPortal = false;

    public static Vector2 test;

    void Update()
    {
        if (Input.GetButtonDown("Vertical") && inPortal)
        {
            StartCoroutine(NextScene(sceneNumber));

            test = transform.position;
            Debug.Log(test);
        }
    }

    IEnumerator NextScene(int num)
    {
        // ������ ���� �񵿱� �������� �ε��Ѵ�.
        AsyncOperation ao = SceneManager.LoadSceneAsync(num);

        // �ε�Ǵ� ���� ����� ȭ�鿡 ������ �ʰ� �Ѵ�.
        ao.allowSceneActivation = false;

        // �ε��� �Ϸ�� ������ �ݺ��ؼ� ���� ��ҵ��� �ε��ϰ� ���� ������ ȭ�鿡 ǥ���Ѵ�.
        while (!ao.isDone) // isDone�� �Ϸ��� �̹��ε� 
        {
            // ����, �� �ε� ������� 90%�� �Ѿ��
            if (ao.progress >= 0.9f)
            {
                //�ε�� ���� ȭ�鿡 ���̰��Ѵ�.
                ao.allowSceneActivation = true;
            }

            // ���� �������� �� ������ ��ٸ���.
            yield return null;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        inPortal = true;
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        inPortal = false;
    }
}

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
        // 지정된 씬을 비동기 형식으로 로드한다.
        AsyncOperation ao = SceneManager.LoadSceneAsync(num);

        // 로드되는 씬의 모습이 화면에 보이지 않게 한다.
        ao.allowSceneActivation = false;

        // 로딩이 완료될 때까지 반복해서 씬의 요소들을 로드하고 진행 과정을 화면에 표시한다.
        while (!ao.isDone) // isDone은 완료의 이미인듯 
        {
            // 만일, 씬 로드 진행률이 90%를 넘어가면
            if (ao.progress >= 0.9f)
            {
                //로드된 씬을 화면에 보이게한다.
                ao.allowSceneActivation = true;
            }

            // 다음 프레임이 될 때까지 기다린다.
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

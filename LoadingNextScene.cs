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
        // 비동기 형식으로 로드한다.
        AsyncOperation ao = SceneManager.LoadSceneAsync(num);

        // 로드된 씬은 모습이 화면에 보이지 않게 한다.
        ao.allowSceneActivation = false;

        // 다음 씬이 준비가 안됬다면
        while (!ao.isDone)
        {
            // 90% 라도 진행된 상태면
            if (ao.progress > 0.9f)
            {
                // 로딩 씬을 보여준다.
                ao.allowSceneActivation = true;
            }
        }
    }

    //IEnumerator NextScene(int num)
    //{
    //    // 비동기 형식으로 로드한다.
    //    AsyncOperation ao = SceneManager.LoadSceneAsync(num);

    //    // 로드된 씬은 모습이 화면에 보이지 않게 한다.
    //    ao.allowSceneActivation = false;

    //    // 다음 씬이 준비가 안됬다면
    //    while (!ao.isDone)
    //    {
    //        // 90% 라도 진행된 상태면
    //        if (ao.progress > 0.9f)
    //        {
    //            // 로딩 씬을 보여준다.
    //            ao.allowSceneActivation = true;
    //        }
    //    }

    //    yield return null;
    //}
}

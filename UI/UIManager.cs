using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    // ■■■■■■ 변수 ■■■■■■
    public GameObject OptionBox;            // 옵션창 
    public GameObject[] objectDestroy;      // 매니저 오브젝트 중복제거를 위한 배열변수
    GameObject sound;                       // 사운드박스 변수
    public AudioSource clickSound;          // 클릭시 재생하는 사운드
    public GameObject DeathBox;

    void Awake()
    {
        // ■■■■■■ 중복 오브젝트 제거 ■■■■■■
        var obj = FindObjectsOfType<UIManager>();
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
        // ■■■■■■ 씬 이동간 박스 찾기 ■■■■■■
        sound = GameObject.Find("SoundBox");
       
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            OptionBox.SetActive(true);

            Time.timeScale = 0;     // 게임 진행 멈추기
        }

        OpenDeathBox();
    }

    // ■■■■■■ 첫씬(로그인씬)으로 이동 ■■■■■■
    public void MoveLoginScene()
    {
        SceneManager.LoadScene(0);
        Time.timeScale = 1;

        for (int i=0; i<objectDestroy.Length; i++)
        {
            Destroy(objectDestroy[i]);
        }
    }

    // ■■■■■■ esc를 이용해 설정창 활성화 ■■■■■■
    public void ExitOptionBox()
    {
        OptionBox.SetActive(false);

        Time.timeScale = 1;     // 게임 진행 재활성화
    }

    // ■■■■■■ 사운드 박스 열기 ■■■■■■
    public void OpenSoundBox()
    {
        sound.transform.position = new Vector2(960, 540);
    }

    // ■■■■■■ 사운드 박스 닫기 ■■■■■■
    public void CloseSoundBox()
    {
        sound.transform.position = new Vector2(0, 0);
    }

    // ■■■■■■ 버튼 클릭 소리 재생 ■■■■■■
    public void ClickSound()
    {
        clickSound.Play();
    }

    // ■■■■■■ 플레이어 체력0에 죽음박스 열림 ■■■■■■
    public void OpenDeathBox()
    {
        if(PlayerAll.instance.hp <= 0)
        {
            PlayerAll.All_PlayerMove = false;
            DeathBox.SetActive(true);
            
        }
    }

    // ■■■■■■ DeathBox 게임재시작 ■■■■■■
    public void GameReStart()
    {
        SceneManager.LoadScene(2);
        PlayerAll.All_PlayerMove = true;
        QuestManager.isQuest = false;
        ScrollViewManager.point = 0;
        for (int i = 0; i < objectDestroy.Length; i++)
        {
            Destroy(objectDestroy[i]);
        }
    }

    // ■■■■■■ DeathBox 게임종료 ■■■■■■
    public void GameExit()
    {
        Application.Quit();
    }

}

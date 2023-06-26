using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    // ������� ���� �������
    public GameObject OptionBox;            // �ɼ�â 
    public GameObject[] objectDestroy;      // �Ŵ��� ������Ʈ �ߺ����Ÿ� ���� �迭����
    GameObject sound;                       // ����ڽ� ����
    public AudioSource clickSound;          // Ŭ���� ����ϴ� ����
    public GameObject DeathBox;

    void Awake()
    {
        // ������� �ߺ� ������Ʈ ���� �������
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
        // ������� �� �̵��� �ڽ� ã�� �������
        sound = GameObject.Find("SoundBox");
       
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            OptionBox.SetActive(true);

            Time.timeScale = 0;     // ���� ���� ���߱�
        }

        OpenDeathBox();
    }

    // ������� ù��(�α��ξ�)���� �̵� �������
    public void MoveLoginScene()
    {
        SceneManager.LoadScene(0);
        Time.timeScale = 1;

        for (int i=0; i<objectDestroy.Length; i++)
        {
            Destroy(objectDestroy[i]);
        }
    }

    // ������� esc�� �̿��� ����â Ȱ��ȭ �������
    public void ExitOptionBox()
    {
        OptionBox.SetActive(false);

        Time.timeScale = 1;     // ���� ���� ��Ȱ��ȭ
    }

    // ������� ���� �ڽ� ���� �������
    public void OpenSoundBox()
    {
        sound.transform.position = new Vector2(960, 540);
    }

    // ������� ���� �ڽ� �ݱ� �������
    public void CloseSoundBox()
    {
        sound.transform.position = new Vector2(0, 0);
    }

    // ������� ��ư Ŭ�� �Ҹ� ��� �������
    public void ClickSound()
    {
        clickSound.Play();
    }

    // ������� �÷��̾� ü��0�� �����ڽ� ���� �������
    public void OpenDeathBox()
    {
        if(PlayerAll.instance.hp <= 0)
        {
            PlayerAll.All_PlayerMove = false;
            DeathBox.SetActive(true);
            
        }
    }

    // ������� DeathBox ��������� �������
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

    // ������� DeathBox �������� �������
    public void GameExit()
    {
        Application.Quit();
    }

}

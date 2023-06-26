using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestManager : MonoBehaviour
{
    // ■■■■■■■ 변수 ■■■■■■■
    // 퀘스트창, 퀘스트경고창 그룹
    public static CanvasGroup QuestBox;
    public static CanvasGroup QuestDouble;

    // 퀘스트창, 퀘스트경고창 그룹 위치 조정
    public static RectTransform QuestBoxPos;
    public static RectTransform QuestDoublePos;

    public static bool QuestConfirm = false; // 현재 퀘스트창을 열려 있는 중인지
    public static bool isQuest;    // 퀘스트가 진행중인지 체크 변수

    public static int killcnt = 0;
    public static int Childcnt = 0;

    // ■■■■■■■ 싱글톤 ■■■■■■■
    public static QuestManager instance;

    void Awake()
    {
        if(instance != null)
        {
            Destroy(gameObject);
        }
        instance = this;

        // 씬 이동간 중복된 오브젝트를 삭제
        var obj = FindObjectsOfType<QuestManager>();

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
        // EventSystem을 가져올수가 없어 씬 이동간 지속적으로 초기화를 해줌
        QuestBox = GameObject.Find("QuestBox").GetComponent<CanvasGroup>();
        QuestDouble = GameObject.Find("QuestDouble").GetComponent<CanvasGroup>();
        QuestBoxPos = GameObject.Find("QuestBox").GetComponent<RectTransform>();
        QuestDoublePos = GameObject.Find("QuestDouble").GetComponent<RectTransform>();
    }

    // ■■■■■■■ 퀘스트 창 띄우기 ■■■■■■■
    public void OnQuest(int id)
    {
        if (id >= 100)
        {
            // 진행중인 퀘스트가 없다면
            if (isQuest == false)
            {
                QuestBox.alpha = 1;
                QuestBoxPos.anchoredPosition = new Vector2(0, 0);
                QuestConfirm = true;
            }
            // 진행중인 퀘스트가 있다면
            else if (isQuest == true)
            {
                QuestDouble.alpha = 1;
                QuestDoublePos.anchoredPosition = new Vector2(0, 0);
            }
        }
    }


    // ■■■■■■■ 미션에 따른 달성여부 확인 ■■■■■■■
    public bool con(int id)
    {
        // 첫번째 퀘스트 NPC
        if ((id == 100) && (Childcnt == 3))
        {
            isQuest = false;
            ScrollViewManager.point += 1f;
            Main_Man.QuestSuccess = true;
            return true;
        }
        // 두번째 퀘스트 NPC
        else if ((id == 101) && (killcnt == 3))
        {
            isQuest = false;
            ScrollViewManager.point += 1f;
            Main_OldMan.QuestSuccess = true;
            return true;
        }
        // 일치하는 항목이 없을 경우..
        else
        {
            QuestDouble.alpha = 1;
            QuestDoublePos.anchoredPosition = new Vector2(0, 0);
        }

        return false;
    }
}

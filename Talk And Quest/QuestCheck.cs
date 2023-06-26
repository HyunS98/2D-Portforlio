using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ■■■■ 퀘스트UI 창 버튼 스크립트 ■■■■

public class QuestCheck : MonoBehaviour
{
    // ■■■■■■■ 싱글톤 ■■■■■■■
    public static QuestCheck instance;

    void Start()
    {
        instance = this;
    }

    // ■■■■■■■ 퀘스트 수락 ■■■■■■■
    public void OnClickYes()
    {
        // 퀘스트매니져에서 퀘스트를 진행중으로 변경
        QuestManager.isQuest = true;

        // 포인트를 넘겨줘서 point 별 퀘스트리스트를 불러온다
        //ScrollViewManager.point += 0.5f;

        // 퀘스트창을 닫고 UI위치를 옮김
        QuestManager.QuestBox.alpha = 0;
        QuestManager.QuestConfirm = false;
        QuestManager.QuestBoxPos.anchoredPosition = new Vector2(0, -1000);

        // 플레이어 움직임 시작
        PlayerAll.All_PlayerMove = true;
        //Debug.Log("퀘스트 수락 포인트 : " + ScrollViewManager.point);
    }

    // ■■■■■■■ 퀘스트 거절 ■■■■■■■
    public void OnClickNo()
    {
        // 퀘스트창을 닫고 UI위치를 옮김
        QuestManager.QuestBox.alpha = 0;
        QuestManager.QuestConfirm = false;
        QuestManager.QuestBoxPos.anchoredPosition = new Vector2(0, -1000);

        PlayerAll.All_PlayerMove = true;
    }

    // ■■■■■■■ 퀘스트 경고창 확인버튼 클릭시 경고창 닫음 ■■■■■■■
    public void OnClickOK()
    {
        // 퀘스트 경고창을 닫고 UI위치를 옮김
        QuestManager.QuestDouble.alpha = 0;
        QuestManager.QuestDoublePos.anchoredPosition = new Vector2(0, -1000);

        PlayerAll.All_PlayerMove = true;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class ScrollViewManager : MonoBehaviour
{
    // ■■■■■■■ 변수 ■■■■■■■
    public static TextMeshProUGUI ScrollViewList; // 스크롤뷰 텍스트 가져오기
    public static float point = 0;                  // 퀘스트 포인트 (스크롤뷰에 포인트별로 Text가 보임)

    // ■■■■■■■ 포인트별 퀘스트 항목 ■■■■■■■
    void Update()
    {
        ScrollViewList = GetComponentInChildren<TextMeshProUGUI>();

        // 퀘스트 진행중.. 포인트별 출력 리스트
        if(QuestManager.isQuest == true)
        {
            if (point == 0f)
            {
                ScrollViewList.text = "찾은 아이들 : " + QuestManager.Childcnt + " / 3";
            }
            else if (point == 1f)
            {
                ScrollViewList.text = "처지 몬스터 : " + QuestManager.killcnt + " / 3";
            }
        }
        else
        {
            ScrollViewList.text = "진행중인 퀘스트가 없습니다";
        }
    }
}

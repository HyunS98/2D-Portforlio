using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Main_OldMan : MonoBehaviour
{
    // ■■■■■■■ 변수 ■■■■■■■
    public int NPC_id;             // NPC id 변수
    public string[] talk;          // 퀘스트 전
    public string[] quest_Clear;   // 퀘스트 수행중
    public string[] quest_End;     // 퀘스트 끝난 후
    
    public SpriteRenderer star;
    public SpriteRenderer changeSprite;

    // ■■■■■■■ 씬 이동간 변수 유지 ■■■■■■■
    public static bool QuestSuccess;

    void Update()
    {
        // ■■■■■■■ 퀘스트 성공시 별색 변경 ■■■■■■■
        if (QuestSuccess == true)
        {
            star.sprite = changeSprite.sprite;
        }
    }

    // ■■■■■■■ 마우스 클릭 ■■■■■■■
    void OnMouseDown()
    {
        Debug.Log("클릭한 NPC 넘버 : " + NPC_id);

        // UI가 클릭되면 true를 반환하기에 대화창이 겹치지 않음
        if (!(EventSystem.current.IsPointerOverGameObject()))
        {
            // 퀘스트 창이 띄워져 있는지 확인
            if (QuestManager.QuestConfirm == false)
            {
                PlayerAll.All_PlayerMove = false;

                // 퀘스트 진행중
                if (QuestManager.isQuest == true && NPC_id >= 100)
                {
                    if (QuestManager.instance.con(NPC_id))
                    {
                        TalkManager.instance.OnTalk(quest_Clear, 0);
                    }
                }
                //퀘스트 끝
                else if (QuestSuccess == true)
                {
                    TalkManager.instance.OnTalk(quest_End, 0);
                }
                // 퀘스트 시작 (퀘스트 성공한적X, 다른 퀘스트 진행중X)
                else
                {
                    TalkManager.instance.OnTalk(talk, NPC_id);
                }
            }
        }


    }
}

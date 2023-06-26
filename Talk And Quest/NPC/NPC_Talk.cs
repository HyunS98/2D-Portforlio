using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class NPC_Talk : MonoBehaviour
{
    public int NPC_id;             // NPC id 변수
    public string[] talk;          // 퀘스트 전

    // ■■■■■■■ 마우스 클릭 ■■■■■■■
    void OnMouseDown()
    {
        //Debug.Log("클릭한 NPC 넘버 : " + NPC_id);

        // UI가 클릭되면 true를 반환하기에 대화창이 겹치지 않음
        if (!(EventSystem.current.IsPointerOverGameObject()))
        {
            PlayerAll.All_PlayerMove = false;

            TalkManager.instance.OnTalk(talk, NPC_id);
        }
    }
}

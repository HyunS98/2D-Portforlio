using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class NPC_Talk : MonoBehaviour
{
    public int NPC_id;             // NPC id ����
    public string[] talk;          // ����Ʈ ��

    // �������� ���콺 Ŭ�� ��������
    void OnMouseDown()
    {
        //Debug.Log("Ŭ���� NPC �ѹ� : " + NPC_id);

        // UI�� Ŭ���Ǹ� true�� ��ȯ�ϱ⿡ ��ȭâ�� ��ġ�� ����
        if (!(EventSystem.current.IsPointerOverGameObject()))
        {
            PlayerAll.All_PlayerMove = false;

            TalkManager.instance.OnTalk(talk, NPC_id);
        }
    }
}

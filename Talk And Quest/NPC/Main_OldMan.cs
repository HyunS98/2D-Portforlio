using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Main_OldMan : MonoBehaviour
{
    // �������� ���� ��������
    public int NPC_id;             // NPC id ����
    public string[] talk;          // ����Ʈ ��
    public string[] quest_Clear;   // ����Ʈ ������
    public string[] quest_End;     // ����Ʈ ���� ��
    
    public SpriteRenderer star;
    public SpriteRenderer changeSprite;

    // �������� �� �̵��� ���� ���� ��������
    public static bool QuestSuccess;

    void Update()
    {
        // �������� ����Ʈ ������ ���� ���� ��������
        if (QuestSuccess == true)
        {
            star.sprite = changeSprite.sprite;
        }
    }

    // �������� ���콺 Ŭ�� ��������
    void OnMouseDown()
    {
        Debug.Log("Ŭ���� NPC �ѹ� : " + NPC_id);

        // UI�� Ŭ���Ǹ� true�� ��ȯ�ϱ⿡ ��ȭâ�� ��ġ�� ����
        if (!(EventSystem.current.IsPointerOverGameObject()))
        {
            // ����Ʈ â�� ����� �ִ��� Ȯ��
            if (QuestManager.QuestConfirm == false)
            {
                PlayerAll.All_PlayerMove = false;

                // ����Ʈ ������
                if (QuestManager.isQuest == true && NPC_id >= 100)
                {
                    if (QuestManager.instance.con(NPC_id))
                    {
                        TalkManager.instance.OnTalk(quest_Clear, 0);
                    }
                }
                //����Ʈ ��
                else if (QuestSuccess == true)
                {
                    TalkManager.instance.OnTalk(quest_End, 0);
                }
                // ����Ʈ ���� (����Ʈ ��������X, �ٸ� ����Ʈ ������X)
                else
                {
                    TalkManager.instance.OnTalk(talk, NPC_id);
                }
            }
        }


    }
}

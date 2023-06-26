using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Sub_Warrior : MonoBehaviour
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

                //����Ʈ �� (3)
                if (QuestSuccess == true)
                {
                    TalkManager.instance.OnTalk(quest_End, 0);
                    return;
                }

                if (ScrollViewManager.point == 0)
                {
                    // ����Ʈ ������ (2)
                    if (QuestManager.isQuest == true && NPC_id >= 100)
                    {
                        if (QuestManager.instance.con(NPC_id))
                        {
                            TalkManager.instance.OnTalk(quest_Clear, 0);
                        }
                    }
                    // ����Ʈ ���� (����Ʈ ��������X, �ٸ� ����Ʈ ������X) (1)
                    else
                    {
                        TalkManager.instance.OnTalk(talk, NPC_id);
                    }
                }
            }
        }
    }
}

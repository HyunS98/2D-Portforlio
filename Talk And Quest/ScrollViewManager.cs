using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class ScrollViewManager : MonoBehaviour
{
    // �������� ���� ��������
    public static TextMeshProUGUI ScrollViewList; // ��ũ�Ѻ� �ؽ�Ʈ ��������
    public static float point = 0;                  // ����Ʈ ����Ʈ (��ũ�Ѻ信 ����Ʈ���� Text�� ����)

    // �������� ����Ʈ�� ����Ʈ �׸� ��������
    void Update()
    {
        ScrollViewList = GetComponentInChildren<TextMeshProUGUI>();

        // ����Ʈ ������.. ����Ʈ�� ��� ����Ʈ
        if(QuestManager.isQuest == true)
        {
            if (point == 0f)
            {
                ScrollViewList.text = "ã�� ���̵� : " + QuestManager.Childcnt + " / 3";
            }
            else if (point == 1f)
            {
                ScrollViewList.text = "ó�� ���� : " + QuestManager.killcnt + " / 3";
            }
        }
        else
        {
            ScrollViewList.text = "�������� ����Ʈ�� �����ϴ�";
        }
    }
}

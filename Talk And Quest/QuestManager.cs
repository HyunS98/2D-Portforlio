using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestManager : MonoBehaviour
{
    // �������� ���� ��������
    // ����Ʈâ, ����Ʈ���â �׷�
    public static CanvasGroup QuestBox;
    public static CanvasGroup QuestDouble;

    // ����Ʈâ, ����Ʈ���â �׷� ��ġ ����
    public static RectTransform QuestBoxPos;
    public static RectTransform QuestDoublePos;

    public static bool QuestConfirm = false; // ���� ����Ʈâ�� ���� �ִ� ������
    public static bool isQuest;    // ����Ʈ�� ���������� üũ ����

    public static int killcnt = 0;
    public static int Childcnt = 0;

    // �������� �̱��� ��������
    public static QuestManager instance;

    void Awake()
    {
        if(instance != null)
        {
            Destroy(gameObject);
        }
        instance = this;

        // �� �̵��� �ߺ��� ������Ʈ�� ����
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
        // EventSystem�� �����ü��� ���� �� �̵��� ���������� �ʱ�ȭ�� ����
        QuestBox = GameObject.Find("QuestBox").GetComponent<CanvasGroup>();
        QuestDouble = GameObject.Find("QuestDouble").GetComponent<CanvasGroup>();
        QuestBoxPos = GameObject.Find("QuestBox").GetComponent<RectTransform>();
        QuestDoublePos = GameObject.Find("QuestDouble").GetComponent<RectTransform>();
    }

    // �������� ����Ʈ â ���� ��������
    public void OnQuest(int id)
    {
        if (id >= 100)
        {
            // �������� ����Ʈ�� ���ٸ�
            if (isQuest == false)
            {
                QuestBox.alpha = 1;
                QuestBoxPos.anchoredPosition = new Vector2(0, 0);
                QuestConfirm = true;
            }
            // �������� ����Ʈ�� �ִٸ�
            else if (isQuest == true)
            {
                QuestDouble.alpha = 1;
                QuestDoublePos.anchoredPosition = new Vector2(0, 0);
            }
        }
    }


    // �������� �̼ǿ� ���� �޼����� Ȯ�� ��������
    public bool con(int id)
    {
        // ù��° ����Ʈ NPC
        if ((id == 100) && (Childcnt == 3))
        {
            isQuest = false;
            ScrollViewManager.point += 1f;
            Main_Man.QuestSuccess = true;
            return true;
        }
        // �ι�° ����Ʈ NPC
        else if ((id == 101) && (killcnt == 3))
        {
            isQuest = false;
            ScrollViewManager.point += 1f;
            Main_OldMan.QuestSuccess = true;
            return true;
        }
        // ��ġ�ϴ� �׸��� ���� ���..
        else
        {
            QuestDouble.alpha = 1;
            QuestDoublePos.anchoredPosition = new Vector2(0, 0);
        }

        return false;
    }
}

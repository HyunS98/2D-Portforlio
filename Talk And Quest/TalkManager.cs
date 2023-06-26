using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems; // ���콺 Ŭ���̳� ��ġ�� �����ϴ� �������̽� ����

/************* ���� **************

 IPointerDownHandler >> ���콺�� Ŭ�� or ��ġ �ٿ��� ����

**********************************/

public class TalkManager : MonoBehaviour, IPointerDownHandler
{
    // �������� ť ��������
    public Queue<string> NPC_Contents;

    // �������� ���� ��������
    public TextMeshProUGUI textUI;
    public CanvasGroup textGroup;   // UI �ؽ�Ʈâ�� �׷�
    public bool istyping;           // Ÿ���� ������ Ȯ��

    string currentContents;         // ť�ȿ� �ִ� ���ڿ� �ľ�
    int NPC_id;                     // �޾ƿ� NPC ID�� ������ ����

    // �������� �̱��� ��������
    public static TalkManager instance;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    // �������� ť �ʱ�ȭ ��������
    void Start()
    {
        NPC_Contents = new Queue<string>();
    }

    // �������� ť ������ üũ ��������
    void Update()
    {
        // ���� ť�� ���ڿ� ���� or ��ȭ ��
        if(textUI.text.Equals(currentContents))
        {
            istyping = false;
        }
    }

    // �������� NPC_Talk Ŭ���� ��ȭ �������� ��������
    public void OnTalk(string[] write, int id)
    {
        // ť ������ ����
        NPC_Contents.Clear();

        NPC_id = id;

        // �޾ƿ� ���ڿ��� ť�ȿ� �ִ´�
        foreach (string text in write)
        {
            NPC_Contents.Enqueue(text); 
        }
       
        textGroup.alpha = 1;
        textGroup.blocksRaycasts = true;  // blocksRaycats >> ray�� UIâ�� ��������(üũ) �ȸ�������(��üũ)

        NextContents();
    }

    // �������� ���� ��ȭ ��� ��������
    public void NextContents()
    {
        // ���� Ÿ���� ���� �ƴ϶��..
        if (!istyping)
        {
            // (ť ���� != 0)
            if (NPC_Contents.Count != 0) 
            {
                currentContents = NPC_Contents.Dequeue(); // ���� ���� ������ ��ȯ
                istyping = true;
                StartCoroutine(typing(currentContents));
            }
            else
            {
                textGroup.alpha = 0;
                textGroup.blocksRaycasts = false;

                // ����Ʈ â�� ���� ���� NPC id ���� �����ش�.
                QuestManager.instance.OnQuest(NPC_id);

                PlayerAll.All_PlayerMove = true;
            }
        }
    }

    IEnumerator typing(string write)
    {
        textUI.text = "";
        foreach(char cnt in write.ToCharArray()) // ToCharArray�� ���ڿ��� char�� �迭�� ��ȯ
        {
            textUI.text += cnt;
            yield return new WaitForSeconds(0.05f);
        }
    }

    // IPointerDownHandler�� ���� ����Ǵ� �޼ҵ��, ��ġ�� ������ ȣ���
    public void OnPointerDown(PointerEventData eventData)
    {
        if(!istyping)
        {
            NextContents();
        }
    }
}
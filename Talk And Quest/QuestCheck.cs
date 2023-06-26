using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ����� ����ƮUI â ��ư ��ũ��Ʈ �����

public class QuestCheck : MonoBehaviour
{
    // �������� �̱��� ��������
    public static QuestCheck instance;

    void Start()
    {
        instance = this;
    }

    // �������� ����Ʈ ���� ��������
    public void OnClickYes()
    {
        // ����Ʈ�Ŵ������� ����Ʈ�� ���������� ����
        QuestManager.isQuest = true;

        // ����Ʈ�� �Ѱ��༭ point �� ����Ʈ����Ʈ�� �ҷ��´�
        //ScrollViewManager.point += 0.5f;

        // ����Ʈâ�� �ݰ� UI��ġ�� �ű�
        QuestManager.QuestBox.alpha = 0;
        QuestManager.QuestConfirm = false;
        QuestManager.QuestBoxPos.anchoredPosition = new Vector2(0, -1000);

        // �÷��̾� ������ ����
        PlayerAll.All_PlayerMove = true;
        //Debug.Log("����Ʈ ���� ����Ʈ : " + ScrollViewManager.point);
    }

    // �������� ����Ʈ ���� ��������
    public void OnClickNo()
    {
        // ����Ʈâ�� �ݰ� UI��ġ�� �ű�
        QuestManager.QuestBox.alpha = 0;
        QuestManager.QuestConfirm = false;
        QuestManager.QuestBoxPos.anchoredPosition = new Vector2(0, -1000);

        PlayerAll.All_PlayerMove = true;
    }

    // �������� ����Ʈ ���â Ȯ�ι�ư Ŭ���� ���â ���� ��������
    public void OnClickOK()
    {
        // ����Ʈ ���â�� �ݰ� UI��ġ�� �ű�
        QuestManager.QuestDouble.alpha = 0;
        QuestManager.QuestDoublePos.anchoredPosition = new Vector2(0, -1000);

        PlayerAll.All_PlayerMove = true;
    }
}

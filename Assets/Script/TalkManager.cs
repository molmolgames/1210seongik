using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalkManager : MonoBehaviour
{
    Dictionary<int, string[]> talkData;
    Dictionary<int, Sprite> portraitData;

    public Sprite[] portraitArr;
    private void Awake()
    {
        talkData = new Dictionary<int, string[]>();
        portraitData = new Dictionary<int, Sprite>();
        GenerateData();
    }

    void GenerateData()
    {
        talkData.Add(11, new string[] { "...zzZ:0", "...zzZ?:0" });
        talkData.Add(12, new string[] { "Ah...?:1", "Ahchoo!!:0" });
        talkData.Add(14, new string[] { "����?:0", "���ϴ� ������ ó�� ����?:0", "������ ���� ����?:0" });
        talkData.Add(101, new string[] { "Warning / Dungeon Trap" });
        talkData.Add(602, new string[] { "Scene2 ���� ȹ��!" });
        talkData.Add(603, new string[] { "Scene3 ���� ȹ��!" });
        talkData.Add(604, new string[] { "Scene4 ���� ȹ��!" });
        talkData.Add(702, new string[] { "ù��° ���� ���ȴ�!" });
        talkData.Add(703, new string[] { "�ι�° ���� ���ȴ�!" });
        talkData.Add(704, new string[] { "������ ���� ���ȴ�!" });
        talkData.Add(705, new string[] { "�´� ���谡 �����ϴ�." });
        talkData.Add(401, new string[] { "탈출구를 찾아보자"});
        talkData.Add(402, new string[] { "앞을 조심해!"});
        talkData.Add(403, new string[] { "우측으로 좀 더 가보자!"});
        talkData.Add(404, new string[] { "좌측으로 좀 더 가보자!"});
        talkData.Add(405, new string[] { "빨강,파랑,초록, 하나로는 부족할걸?"});
        talkData.Add(406, new string[] { "돌아가"});
        talkData.Add(407, new string[] { "문을 열려면 주문서가 필요해"});
        talkData.Add(408, new string[] { "돌무더기를 잘 살펴보자"});







        portraitData.Add(11 + 0, portraitArr[0]);
        portraitData.Add(12 + 0, portraitArr[1]);
        portraitData.Add(12 + 1, portraitArr[2]);
        portraitData.Add(14 + 0, portraitArr[3]);

    }
    public string GetTalk(int id, int talkIndex)
    {
        if (talkIndex == talkData[id].Length)
        {
            return null;
        }
        else
        {
            return talkData[id][talkIndex];
        }
    }

    public Sprite GetPortrait(int id, int portraitIndex)
    {
        return portraitData[id + portraitIndex];
    }
}

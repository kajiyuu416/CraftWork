using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShowText : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI text1;
    [SerializeField] TextMeshProUGUI text2;
    [SerializeField] TextMeshProUGUI text3;
    [SerializeField] TextMeshProUGUI text4;
    [SerializeField] Physics2DExtentsion PE;
    [SerializeField] PlayerController PC;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(PE.holdFlag &&!PC.NowHoldItem)
        {
            text1.text = "�A�C�e���������グ�� �E�E�E B �{�^��";
        }
        else if(PC.NowHoldItem)
        {
            text1.text = "�A�C�e����u���@�E�E�E B �{�^��";
            text3.text = "�A�C�e�������ɓ�����@�E�E�E A �{�^��";
            text4.text = "�A�C�e������ɓ�����@�E�E�E Y �{�^��";
        }
        else 
        {
            text1.text = "";
        }

        if(PC.NowHoldItem && !PC.originSR.flipX)
        {
            text2.text = "�A�C�e�����E�ɓ�����@�E�E�E X �{�^��";
        }
        else if(PC.NowHoldItem && PC.originSR.flipX)
        {
            text2.text = "�A�C�e�������ɓ�����@�E�E�E X �{�^��";
        }

        if(!PC.NowHoldItem)
        {
            text2.text = "";
            text3.text = "";
            text4.text = "";
        }



    }
}
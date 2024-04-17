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
    [SerializeField] TextMeshProUGUI text5;
    [SerializeField] TextMeshProUGUI text6;
    [SerializeField] TextMeshProUGUI text7;
    [SerializeField] Physics2DExtentsion PE;
    [SerializeField] PlayerController PC;

    private bool callOne;
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

        if(PE.Pickaxe_Hold_Flag)
        {
            pickaxeSC picSC = pickaxeSC.Instance;
            text6.text = picSC.Use_Pickaxe_Count.ToString();
            text5.text = "�s�b�P���̑ϋv�l�E�E�E";
        }
        else if(!PE.Pickaxe_Hold_Flag)
        {
            text5.text = "";
            text6.text = "";
        }
        if(PE.Torch_Hold_Flag)
        {
            TorchSC torchSC = TorchSC.Instance;
            if(torchSC.TorchLight.range < 10.0f)
            {
                text7.text = "�g�[�`�̉΂�����������";
            }

            if(torchSC.TorchLight.range == 0)
            {
                text7.text = "";
            }

            if(torchSC.burnFlag)
            {
                text7.text = "";
            }


        }
        else if(!PE.Torch_Hold_Flag)
        {
            text7.text = "";
        }

    }
}

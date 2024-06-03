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
    [SerializeField] TextMeshProUGUI text8;
    [SerializeField] TextMeshProUGUI text9;
    private Physics2DExtentsion PE;
    [SerializeField] PlayerController PC;

    //�t���O�ɉ����ăe�L�X�g�\��
    private void Update()
    {
        Expression_Text();
    }
    private void Awake()
    {
        PE = FindObjectOfType<Physics2DExtentsion>();
    }
    private void Expression_Text()
    {
        bool isholdFlag = PE.Duplicate_Hold_Flag;
        bool ispickaxe = PE.Duplicate_Pickaxe_Hold_Flag;
        bool istorch = PE.Duplicate_Torch_Hold_Flag;
        bool isbow = PE.Duplicate_Bow_Hold_Flag;

        if(isholdFlag && !PC.NowHoldItem)
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

        if(ispickaxe)
        {
            pickaxeSC picSC = pickaxeSC.Instance;
            text6.text = picSC.Use_Pickaxe_Count.ToString();
            text5.text = "�s�b�P���̑ϋv�l�E�E�E";
        }
        else if(!ispickaxe)
        {
            text5.text = "";
            text6.text = "";
        }
        if(istorch)
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
        else if(!istorch)
        {
            text7.text = "";
        }

        if(isbow)
        {
            text8.text = "�c���Q�[�W";
            text9.text = "�ˌ��@�E�E�E R1 �{�^��";
        }
        else
        {
            text8.text = "";
            text9.text = "";
        }

    }
}

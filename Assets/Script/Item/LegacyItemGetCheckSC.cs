using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

//�Q�[���J�n���A���X�g�ɓ���̃I�u�W�F�N�g��o�^�A�A�C�e���擾����getLIFlagList���̃t���O��Ԃ�
public class LegacyItemGetCheckSC : MonoBehaviour
{
    public static List<LegacyItemSC> legacyItems = new List<LegacyItemSC>();
    public static List<bool> getLIFlagList = new List<bool>();
    public  static void GetItemCheck()
    {
        int i = 0;
        foreach(var li in legacyItems)
        {
            if(li.ItemGetFlag)
            {
                getLIFlagList[i] = true;
                li.gameObject.SetActive(false);
            }
            i++;
        }
    }

}

using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
//�C���X�y�N�^�[�Ŏw�肵���I�u�W�F�N�g�ɂ��Ă���X�N���v�g�̃t���O���`�F�b�N
public class LegacyItemGetCheckSC : MonoBehaviour
{
    public static List<LegacyItemSC> legacyItems = new List<LegacyItemSC>();
    public static List<bool> getLIFlagList = new List<bool>();

    private void Awake()
    {
        
    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            int i = 0;
            foreach(var li in legacyItems)
            {

                //Debug.Log(i +"�I�u�W�F�N�g��"+li);
                //Debug.Log(i + "�t���O" + getLIFlagList[i]);
                if(getLIFlagList[i])
                {
                    li.gameObject.SetActive(false);
                }
                i++;
            }
        }
    }
    public  static void GetItemCheck()
    {
        int i = 0;
        foreach(var li in legacyItems)
        {

            if(li.ItemGetFlag)
            {
                getLIFlagList[i] = true;
                li.gameObject.GetComponent<SpriteRenderer>().enabled = false;
            }
            i++;
        }
    }

}

using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
//インスペクターで指定したオブジェクトについているスクリプトのフラグをチェック
public class LegacyItemGetCheckSC : MonoBehaviour
{
    public static List<LegacyItemSC> legacyItems = new List<LegacyItemSC>();
    public static List<bool> getLIFlagList = new List<bool>();
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            int i = 0;
            foreach(var li in legacyItems)
            {
                Debug.Log(li + "フラグ"+ getLIFlagList[i]);
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
                li.gameObject.SetActive(false);
            }
            i++;
        }
    }

}

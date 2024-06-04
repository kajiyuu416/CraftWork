using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
//インスペクターで指定したオブジェクトについているスクリプトのフラグをチェック
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

                //Debug.Log(i +"オブジェクト名"+li);
                //Debug.Log(i + "フラグ" + getLIFlagList[i]);
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

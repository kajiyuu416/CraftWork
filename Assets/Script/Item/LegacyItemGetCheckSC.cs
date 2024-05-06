using UnityEngine;
public class LegacyItemGetCheckSC : MonoBehaviour
{
    [SerializeField] LegacyItemSC gold_ring;
    [SerializeField] LegacyItemSC gold_coins;
    [SerializeField] LegacyItemSC gem_diamond;
    [SerializeField] LegacyItemSC gold_crown;
    [SerializeField] LegacyItemSC gold_bars;
    public static bool GetLI1;
    public static bool GetLI2;
    public static bool GetLI3;
    public static bool GetLI4;
    public static bool GetLI5;
    private void Update()
    {
        GetItemCheck();
    }
    //インスペクターで指定したオブジェクトについているスクリプトのフラグをチェック
    private void GetItemCheck()
    {
        if(gold_ring.ItemGetFlag || GetLI1)
        {
            GetLI1 = true;
            gold_ring.LegacyItem.SetActive(false);
        }
        if(gold_coins.ItemGetFlag || GetLI2)
        {
            GetLI2 = true;
            gold_coins.LegacyItem.SetActive(false);
        }
        if(gem_diamond.ItemGetFlag || GetLI3)
        {
            GetLI3 = true;
            gem_diamond.LegacyItem.SetActive(false);
        }
        if(gold_crown.ItemGetFlag || GetLI4)
        {
            GetLI4 = true;
            gold_crown.LegacyItem.SetActive(false);
        }
        if(gold_bars.ItemGetFlag || GetLI5)
        {
            GetLI5 = true;
            gold_bars.LegacyItem.SetActive(false);
        }
    }

}

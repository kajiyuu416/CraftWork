using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LegacyItemSC : MonoBehaviour
{
    [SerializeField] GameObject generationEfect;
    public bool ItemGetFlag;

    private void Awake()
    {
        LegacyItemGetCheckSC.legacyItems.Add(this);
        LegacyItemGetCheckSC.getLIFlagList.Add(false);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        var transPos = transform.position + new Vector3(0, 0, -0.3f);

        if(collision.CompareTag("Player"))
        {
            GameObject geneEfe = Instantiate(generationEfect, transPos, Quaternion.Euler(-90f, 0f, 0f));
            SoundManager SM = SoundManager.Instance;
            SM.SettingPlaySE23();
            ItemGetFlag = true;
            LegacyItemGetCheckSC.GetItemCheck();
        }
    }
}

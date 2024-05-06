using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LegacyItemSC : MonoBehaviour
{
    [SerializeField] GameObject generationEfect;
    public GameObject LegacyItem; 
    public bool ItemGetFlag;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            GameObject geneEfe = Instantiate(generationEfect, transform.position, Quaternion.Euler(-90f, 0f, 0f));
            SoundManager SM = SoundManager.Instance;
            SM.SettingPlaySE23();
            ItemGetFlag = true;
        }
    }
}

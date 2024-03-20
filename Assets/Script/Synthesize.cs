using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Synthesize : MonoBehaviour
{
    [SerializeField] PlayerController PC;
    [SerializeField] HitEfect HE;
    [SerializeField] GameObject OrijinObj;
    [SerializeField] GameObject TargetObj;
    [SerializeField] GameObject generationObj;
    [SerializeField] GameObject generationEfect;
    [SerializeField] Sprite targetObjSprite;
    [SerializeField] Sprite hitObjSprite;
    public bool SynthesizeFlag = false;
    private SpriteRenderer hitObjSpriteRenderer;
   
    void Update()
    {
        if(targetObjSprite == hitObjSprite && !SynthesizeFlag)
        {
            SynthesizeFlag = true;
            GameObject geneObj = Instantiate(generationObj,transform.position, Quaternion.Euler(0f, 0f, 0f));
            GameObject geneEfe = Instantiate(generationEfect, HE.generationPosition, Quaternion.Euler(0f, 0f, 0f));
            Destroy(OrijinObj);
            Destroy(TargetObj);
            PC.NowHoldobj = null;
            PC.NowHoldItem = false;
            SoundManager SM = SoundManager.Instance;
            SM.SettingPlaySE2();
            Debug.Log("対象のアイテムと当たりました　+　合成します");
        }
        else if(targetObjSprite != hitObjSprite)
        {
            return;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Item"))
        {
            hitObjSpriteRenderer = collision.gameObject.GetComponent<SpriteRenderer>();
            hitObjSprite = hitObjSpriteRenderer.sprite;
        }

    }
}

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
    //接触したアイテムのスプライトを取り、ターゲットと同じであれば設定したアイテムを場に生成する処理
    private void Update()
    {
        Generate();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Item"))
        {
            hitObjSpriteRenderer = collision.gameObject.GetComponent<SpriteRenderer>();
            hitObjSprite = hitObjSpriteRenderer.sprite;
        }
    }
    private void Generate()
    {
        if(targetObjSprite == hitObjSprite && !SynthesizeFlag)
        {
            SynthesizeFlag = true;
            GameObject geneObj = Instantiate(generationObj, transform.position, Quaternion.Euler(0f, 0f, 0f));
            GameObject geneEfe = Instantiate(generationEfect, HE.generationPosition, Quaternion.Euler(0f, 0f, 0f));
            Destroy(OrijinObj);
            Destroy(TargetObj);
            PlayerController pc = PlayerController.Instance;
            pc.ItemLost();
            SoundManager SM = SoundManager.Instance;
            SM.SettingPlaySE2();
        }
        else if(targetObjSprite != hitObjSprite)
        {
            return;
        }
    }
}

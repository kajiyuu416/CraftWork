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
    //�ڐG�����A�C�e���̃X�v���C�g�����A�^�[�Q�b�g�Ɠ����ł���ΐݒ肵���A�C�e������ɐ������鏈��
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

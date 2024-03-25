using UnityEngine;

public class Crumbling_rocks : MonoBehaviour
{
    [SerializeField] PlayerController PC;
    [SerializeField] HitEfect HE;
    [SerializeField] pickaxeSC pickaxeSC;
    [SerializeField] Sprite targetObjSprite;
    [SerializeField] Sprite hitObjSprite;
    [SerializeField] GameObject generationEfect;
    private SpriteRenderer hitObjSpriteRenderer;
    private bool HitFlag = false;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Ice_axe"))
        {
            hitObjSpriteRenderer = collision.gameObject.GetComponent<SpriteRenderer>();
            hitObjSprite = hitObjSpriteRenderer.sprite;

            if(targetObjSprite == hitObjSprite && !HitFlag)
            {
                GameObject geneEfe = Instantiate(generationEfect, HE.generationPosition, Quaternion.Euler(0f, 0f, 0f));
                //Destroy(collision.gameObject);
                //pickaxeSC.Use_Pickaxe_Count++;
                this.gameObject.SetActive(false);
                HitFlag = true;
                PC.ItemLost();
                SoundManager SM = SoundManager.Instance;
                SM.SoundPause();
                SM.SettingPlaySE11();
                SM.SettingPlaySE7();
                Invoke("SoundUnpause", 4.0f);
            }
        }
    }
    public void SoundUnpause()
    {
        SoundManager SM = SoundManager.Instance;
        SM.SoundUnPause();
    }
}

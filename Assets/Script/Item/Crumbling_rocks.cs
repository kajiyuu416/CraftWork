using UnityEngine;

public class Crumbling_rocks : Charade
{
    //特定のアイテムと接触したら非表示になる
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Ice_axe"))
        {
            hitObjSpriteRenderer = collision.gameObject.GetComponent<SpriteRenderer>();
            hitObjSprite = hitObjSpriteRenderer.sprite;

            if(targetObjSprite == hitObjSprite && !HitFlag)
            {
                GameObject geneEfe = Instantiate(generationEfect, HE.generationPosition, Quaternion.Euler(0f, 0f, 0f));
                this.gameObject.SetActive(false);
                HitFlag = true;
                pickaxeSC pickaxe = pickaxeSC.Instance;
                pickaxe.addCount();
                SoundManager SM = SoundManager.Instance;
                SM.SettingPlaySE11();
            }
        }
    }
}

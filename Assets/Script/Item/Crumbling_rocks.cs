using UnityEngine;

public class Crumbling_rocks : Charade
{
    [SerializeField] MeshRenderer meshRenderer;
    [SerializeField] BoxCollider2D boxCollider2D;
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
                meshRenderer.enabled = false;
                boxCollider2D.enabled = false;
                HitFlag = true;
                pickaxeSC pickaxe = pickaxeSC.Instance;
                pickaxe.addCount();
                SoundManager SM = SoundManager.Instance;
                SM.SettingPlaySE11();
            }
        }
    }
    private void Update()
    {
        if(PlayerController.SelectReSet && HitFlag)
        {
            meshRenderer.enabled = true;
            boxCollider2D.enabled = true;
            HitFlag = false;
        }
    }


}

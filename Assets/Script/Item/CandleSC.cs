using UnityEngine;

public class CandleSC : Charade
{
    [SerializeField] GameObject targetObject;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Item"))
        {
            hitObjSpriteRenderer = collision.gameObject.GetComponent<SpriteRenderer>();
            hitObjSprite = hitObjSpriteRenderer.sprite;

            if(targetObjSprite == hitObjSprite && !HitFlag)
            {
                GameObject geneEfe = Instantiate(generationEfect, HE.generationPosition, Quaternion.Euler(-90f, 0f, 0f));
                HitFlag = true;
                SetTag("supplyArea");
            }
        }
    }
    public void SetTag(string newTag)
    {
        targetObject.tag = newTag;
    }
}

using UnityEngine;

public class CandleSC : Charade
{
    bool collOne;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Item"))
        {
            hitObjSpriteRenderer = collision.gameObject.GetComponent<SpriteRenderer>();
            hitObjSprite = hitObjSpriteRenderer.sprite;

            if(targetObjSprite == hitObjSprite && !HitFlag)
            {
                HitFlag = true;
                collOne = true;
                GameObject geneEfe = Instantiate(generationEfect, HE.generationPosition, Quaternion.Euler(-90f, 0f, 0f));
                SetTag("supplyArea");
            }
        }
    }
    public void SetTag(string newTag)
    {
        this.tag = newTag;
    }
    private void Update()
    {
        if(HitFlag&&!collOne)
        {
            collOne = true;
            GameObject geneEfe = Instantiate(generationEfect, HE.generationPosition, Quaternion.Euler(-90f, 0f, 0f));
            SetTag("supplyArea");
        }
    }

}

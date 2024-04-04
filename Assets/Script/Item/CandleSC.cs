using UnityEngine;

public class CandleSC : Charade
{
    [SerializeField] GameObject targetObject;
    private void Awake()
    {
        Debug.Log("bbb");
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Item"))
        {
            hitObjSpriteRenderer = collision.gameObject.GetComponent<SpriteRenderer>();
            hitObjSprite = hitObjSpriteRenderer.sprite;

            if(targetObjSprite == hitObjSprite && !HitFlag)
            {
                HitFlag = true;
                GameObject geneEfe = Instantiate(generationEfect, HE.generationPosition, Quaternion.Euler(-90f, 0f, 0f));
                SetTag("supplyArea");
            }
        }
    }
    public void SetTag(string newTag)
    {
        targetObject.tag = newTag;
    }
    private void Update()
    {

    }
}
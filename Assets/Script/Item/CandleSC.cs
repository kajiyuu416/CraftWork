using Unity.VisualScripting;
using UnityEngine;

public class CandleSC : Charade
{
    [SerializeField] clearCheck4 clearCheck4;
    private GameObject generationefect;
    public bool answer;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Item"))
        {
            hitObjSpriteRenderer = collision.gameObject.GetComponent<SpriteRenderer>();
            hitObjSprite = hitObjSpriteRenderer.sprite;

            if(targetObjSprite == hitObjSprite && !HitFlag)
            {
                HitFlag = true;
                generationefect = Instantiate(generationEfect, HE.generationPosition, Quaternion.Euler(-90f, 0f, 0f));
                SetTag("supplyArea");
                if(clearCheck4 != null&&answer==true)
                {
                    clearCheck4.ClearCheck();

                }
            }
        }
    }
    public void SetTag(string newTag)
    {
        this.tag = newTag;
    }
    private void Update()
    {
        if(PlayerController.SelectReSet && HitFlag)
        {
            HitFlag = false;
            Destroy(generationefect);
            SetTag("Untagged");
        }
    }

}

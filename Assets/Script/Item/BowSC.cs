using UnityEngine;
using UnityEngine.UI;

public class BowSC : MonoBehaviour
{
    [SerializeField] GameObject arrow;
    [SerializeField] Physics2DExtentsion PE;
    public Image image;
    public int aroow_Remaining;

    private SpriteRenderer origin_Sprite;
    public static BowSC Instance
    {
        get; private set;
    }
    private void Awake()
    {
        aroow_Remaining = 30;
        origin_Sprite = GetComponent<SpriteRenderer>();
    }
    void Update()
    {
        rotationChange();
        if(aroow_Remaining < 0)
        {
            aroow_Remaining = 0;
        }
        if(PE.Bow_Hold_Flag)
        image.fillAmount = aroow_Remaining / 30.0f;
    }

    private void rotationChange()
    {
        Transform origin_transform = this.transform;
        if(origin_Sprite.flipX)
        {
            Vector3 localAngle = origin_transform.localEulerAngles;
            localAngle.z = 40.0f;
            origin_transform.localEulerAngles = localAngle;
        }
        else
        {
            Vector3 localAngle = origin_transform.localEulerAngles;
            localAngle.z = -40.0f;
            origin_transform.localEulerAngles = localAngle;
        }
    }
    public void RighitShot()
    {
        if(aroow_Remaining > 0)
        {
            GameObject geneObj = Instantiate(arrow, transform.position, Quaternion.Euler(0f, 0f, -45f));
            Rigidbody2D geneObjRB = geneObj.GetComponent<Rigidbody2D>();
            Vector2 force = new Vector2(15.0f, 0);
            geneObjRB.AddForce(force, ForceMode2D.Impulse);
            aroow_Remaining--;
        }
        else if(aroow_Remaining == 0)
        {
            Debug.Log("残数がないため、発射できません");
        }
    }
    public void LeftShot()
    {
        if(aroow_Remaining > 0)
        {
            GameObject geneObj = Instantiate(arrow, transform.position, Quaternion.Euler(0f, 0f, 140.0f));
            Rigidbody2D geneObjRB = geneObj.GetComponent<Rigidbody2D>();
            Vector2 force = new Vector2(15.0f, 0);
            geneObjRB.AddForce(-force, ForceMode2D.Impulse);
            aroow_Remaining--;
        }
        else if(aroow_Remaining == 0)
        {
            Debug.Log("残数がないため、発射できません");
        }

    }

}

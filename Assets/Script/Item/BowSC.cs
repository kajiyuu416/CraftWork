using UnityEngine;
using UnityEngine.UI;

public class BowSC : MonoBehaviour
{
    [SerializeField] GameObject arrow;
    [SerializeField] Physics2DExtentsion PE;
    private Image arrow_Remaining_image;
    private Image arrow_bar_image;
    public int aroow_Remaining;

    private SpriteRenderer origin_Sprite;
    public static BowSC Instance
    {
        get; private set;
    }
    private void Awake()
    {
        aroow_Remaining = 30;
        arrow_Remaining_image = GameObject.Find("arrow_Remaining").GetComponent<Image>();
        arrow_bar_image = GameObject.Find("arrow_bar").GetComponent<Image>();
        origin_Sprite = GetComponent<SpriteRenderer>();
    }
    void Update()
    {
        rotationChange();
        aroow_Remaining_Check();

        if(PE.Bow_Hold_Flag)
        {
            arrow_Remaining_image.fillAmount = aroow_Remaining / 30.0f;
            arrow_Remaining_image.enabled = true;
            arrow_bar_image.enabled = true;
        }
        else
        {
           arrow_Remaining_image.enabled = false;
           arrow_bar_image.enabled = false;
        }


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
    private void aroow_Remaining_Check()
    {
        if(aroow_Remaining > 30)
        {
            aroow_Remaining = 30;
        }

        if(aroow_Remaining < 0)
        {
            aroow_Remaining = 0;
        }

        if(aroow_Remaining == 0)
        {
            Debug.Log("Žc”‚ª‚È‚¢‚½‚ßA”­ŽË‚Å‚«‚Ü‚¹‚ñ");
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
            SoundManager SM = SoundManager.Instance;
            SM.SettingPlaySE16();
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
            SoundManager SM = SoundManager.Instance;
            SM.SettingPlaySE16();
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("quiver"))
        {
            Destroy(collision.gameObject);
            aroow_Remaining = aroow_Remaining +10;
            SoundManager SM = SoundManager.Instance;
            SM.SettingPlaySE2();
        }

    }
    
}

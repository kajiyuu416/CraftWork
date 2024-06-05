using UnityEngine;
using UnityEngine.UI;
public class BowSC : MonoBehaviour
{
    [SerializeField] GameObject arrow;
    public int aroow_Remaining;
    public Physics2DExtentsion PE;
    private Image arrow_Remaining_image;
    private Image arrow_bar_image;
    private SpriteRenderer origin_Sprite;
    private GameObject[] BowObj;
    private const int maxa_roow = 30;

    private BoxCollider2D[] cols;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        aroow_Remaining = maxa_roow;
        origin_Sprite = GetComponent<SpriteRenderer>();
        PE = FindObjectOfType<Physics2DExtentsion>();
    }
    private void Update()
    {
        rotationChange();
        aroow_Remaining_Check();

        if(GameManager.SelectReSet)
        {
            transform.position = PlayerController.CP;
            aroow_Remaining = maxa_roow;
        }
        Check("Bow");
    }
    //Playeの向きに応じて弓の角度、向きを変更
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
    //弓の残数管理の処理
    private void aroow_Remaining_Check()
    {
        bool isbow = PE.Duplicate_Bow_Hold_Flag;

        if(aroow_Remaining > maxa_roow)
        {
            aroow_Remaining = maxa_roow;
        }

        if(aroow_Remaining < 0)
        {
            aroow_Remaining = 0;
        }

        if(PE == null)
        {
            PE = FindObjectOfType<Physics2DExtentsion>();
        }

        if(PE!= null)
        {
            arrow_Remaining_image = GameObject.Find("arrow_Remaining").GetComponent<Image>();
            arrow_bar_image = GameObject.Find("arrow_bar").GetComponent<Image>();
        }


        if(isbow)
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
        if(GameManager.GameClearFlag)
        {
            Destroy(gameObject);
        }
    }
    //発射処理
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
    //フロアに弓矢の数が10本以上になると最初の弓矢を削除する処理(処理負荷軽減のため)
    private void Check(string tagname)
    {
        BowObj = GameObject.FindGameObjectsWithTag(tagname);
        if(BowObj.Length > 1)
        {
            Destroy(BowObj[0]);
        }
    }
    //弓矢補充処理
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("quiver") && aroow_Remaining < maxa_roow)
        {
            collision.gameObject.GetComponent<SpriteRenderer>().enabled = false;
            cols = collision.gameObject.GetComponents<BoxCollider2D>();
            cols[0].enabled = false;
            cols[1].enabled = false;
            aroow_Remaining = aroow_Remaining +10;
            SoundManager SM = SoundManager.Instance;
            SM.SettingPlaySE2();
        }

    }
    
}

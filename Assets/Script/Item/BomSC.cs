using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BomSC : MonoBehaviour
{
    [SerializeField] GameObject explosionEffect;
    [SerializeField] GameObject ignitionEffect;
    [SerializeField] BoxCollider2D boxCol1;
    [SerializeField] BoxCollider2D boxCol2;
    [SerializeField] CapsuleCollider2D capsuleCol;
    private SpriteRenderer spriteRenderer;
    private Color defaultColor = new Color(255f, 255f, 255f, 255f);
    private Color ResetColor = new Color(1f, 1f, 1f, 1f);
    private float countdownSecond = 5;
    private float Maxcount = 5;
    public bool ignitionFlag;
    private bool First_ignitionFlag;
    private bool burnFlag;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    private void Update()
    {
        Ignition();

        if(countdownSecond <= 4.8f)
        {
            gameObject.layer = 3;
        }
        if(countdownSecond < 0.5f)
        {
            gameObject.layer = 0;
        }
        if(countdownSecond <= 0.0f &&!burnFlag)
        {
            Explosion();
            StartCoroutine(SetTime());
            burnFlag = true;
        }

        if(GameManager.SelectReSet)
        {
            countdownSecond = Maxcount;
            gameObject.layer = 3;
            spriteRenderer.enabled = true;
            spriteRenderer.color = ResetColor;
            burnFlag = false;
            ignitionFlag = false;
            First_ignitionFlag = false;
            boxCol1.enabled = true;
            boxCol2.enabled = true;
            capsuleCol.enabled = false;
            GameManager.CloneBomDestroy();
        }
    }
    //爆発のタイミングが可視化できるように、残り時間に応じでカラーを徐々に変更
    public IEnumerator explosion_Count()
    {
        yield return new WaitForSeconds(1f);
        while(spriteRenderer.color.r <= 1)
        {
            spriteRenderer.color -= new Color(0, 0.005f, 0.005f, 0);
            yield return null;
        }
    }
    //オブジェクトに接触時カウント開始
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(!ignitionFlag)
        {
            Instantiate(ignitionEffect, transform.position, transform.rotation);
            First_ignitionFlag = true;
            ignitionFlag = true;
            SoundManager SM = SoundManager.Instance;
            SM.SettingPlaySE17();
        }
    }
    //爆発時の他オブジェクトへ処理
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Untagged"))
        {
            collision.GetComponent<MeshRenderer>().enabled = false;
            collision.GetComponent<BoxCollider2D>().enabled = false;
        }
        if(collision.CompareTag("arrow"))
        {
            Destroy(collision.gameObject);
        }
        if(collision.CompareTag("Player"))
        {
            GameManager.GameReset();
        }
        if(collision.CompareTag("Bom"))
        {
            Explosion();
        }
    }
    //爆弾爆発時の処理
    private void Explosion()
    {
        Instantiate(explosionEffect, transform.position, transform.rotation);
        boxCol1.enabled = false;
        boxCol2.enabled = false;
        capsuleCol.enabled = true;
        spriteRenderer.color = defaultColor;
        spriteRenderer.enabled = false;
        GameManager.CloneBomDestroy();
        SoundManager SM = SoundManager.Instance;
        SM.SettingPlaySE18();

    }
    //爆弾点火時の処理
    private void Ignition()
    {
        if(ignitionFlag)
        {
            countdownSecond -= Time.deltaTime;
            boxCol2.enabled = false;
            if(First_ignitionFlag)
            {
               First_ignitionFlag = false;
               StartCoroutine(explosion_Count());
            }
        }
    }
    private IEnumerator SetTime()
    {
        yield return new WaitForSeconds(1.0f);
        capsuleCol.enabled = false;
    }
}

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
    [SerializeField] float countdownSecond = 5;
    public bool ignitionFlag;
    public bool First_ignitionFlag;
    private void FixedUpdate()
    {
        Ignition();

        if(countdownSecond <= 4.8f)
        {
            gameObject.layer = 3;
        }
        if(countdownSecond <= 0.0f)
        {
            Explosion();
        }
        if(countdownSecond < 0.5f)
        {
          gameObject.layer = 0;
        }
    }
    //爆発のタイミングが可視化できるように、残り時間に応じでカラーを徐々に変更
    public IEnumerator explosion_Count()
    {
        SpriteRenderer sprite = gameObject.GetComponent<SpriteRenderer>();
        yield return new WaitForSeconds(1f);
        while(sprite.color.a <= 1)
        {
            sprite.color -= new Color(0, 0.005f, 0.005f, 0);
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
        if(collision.CompareTag("Untagged")|| (collision.CompareTag("Item")) || (collision.CompareTag("arrow")))
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
        Destroy(gameObject);
        capsuleCol.enabled = true;
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
}

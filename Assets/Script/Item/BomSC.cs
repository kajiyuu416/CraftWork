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
    private Color resetColor = new Color(1f, 1f, 1f, 1f);
    private float countdownSecond = 5;
    private  const float maxcount = 5;
    public bool ignitionFlag;
    private bool first_ignitionFlag;
    private bool burnFlag;
    private const int defaultLayer = 0;
    private const int itemLayer = 3;
    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    private void Update()
    {
        Ignition();
        BomCount();
        //フラグ、カウント、コライダー、レンダラーのリセット
        if(GameManager.SelectReSet)
        {
            countdownSecond = maxcount;
            gameObject.layer = 3;
            spriteRenderer.enabled = true;
            spriteRenderer.color = resetColor;
            burnFlag = false;
            ignitionFlag = false;
            first_ignitionFlag = false;
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
            spriteRenderer.color -= new Color(0, 0.01f, 0.01f, 0);
            yield return null;
        }
    }
    //オブジェクトに接触時カウントダウン開始
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(!ignitionFlag)
        {
            Instantiate(ignitionEffect, transform.position, transform.rotation);
            first_ignitionFlag = true;
            ignitionFlag = true;
            SoundManager SM = SoundManager.Instance;
            SM.SettingPlaySE17();
        }
    }
    //爆発時の他オブジェクトへ処理
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //ヒットしたオブジェクトのタグが"Untagged"の場合
        //コライダーとメッシュレンダラーを非表示にする
        if(collision.CompareTag("Untagged"))
        {
            collision.GetComponent<MeshRenderer>().enabled = false;
            collision.GetComponent<BoxCollider2D>().enabled = false;
        }
        //ヒットしたオブジェクトのタグが"arrow"の場合
        //オブジェクトの削除
        if(collision.CompareTag("arrow"))
        {
            Destroy(collision.gameObject);
        }
        //ヒットしたオブジェクトのタグが"Player"の場合
        //リセットフラグを返す
        if(collision.CompareTag("Player"))
        {
            GameManager.GameReset();
        }
        //ヒットしたオブジェクトのタグが"Bom"、"CloneBom"の場合
        //爆発処理
        if(collision.CompareTag("Bom")|| collision.CompareTag("CloneBom"))
        {
            Explosion();
        }
    }
    //爆弾爆発時の処理
    //ボックスコライダーとスプライトレンダラーの非表示、カラー変更
    //カプセルコライダー、エフェクトの表示
    private void Explosion()
    {
        Instantiate(explosionEffect, transform.position, transform.rotation);
        boxCol1.enabled = false;
        boxCol2.enabled = false;
        capsuleCol.enabled = true;
        spriteRenderer.color = defaultColor;
        spriteRenderer.enabled = false;
        SoundManager.Instance.SettingPlaySE18();

    }
    //爆弾爆発前のカウントダウン処理
    //爆弾のレイヤー変更
    private void BomCount()
    {
        if(countdownSecond <= 4.8f)
        {
            gameObject.layer = itemLayer;
        }
        if(countdownSecond < 0.5f)
        {
            gameObject.layer = defaultLayer;
        }
        if(countdownSecond <= 0.0f && !burnFlag)
        {
            Explosion();
            StartCoroutine(SetTime());
            burnFlag = true;
        }
    }
    //爆弾点火時の処理
    private void Ignition()
    {
        if(ignitionFlag)
        {
            countdownSecond -= Time.deltaTime;
            boxCol2.enabled = false;
            if(first_ignitionFlag)
            {
               first_ignitionFlag = false;
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

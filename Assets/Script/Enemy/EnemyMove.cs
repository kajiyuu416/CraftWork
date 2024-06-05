using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    [SerializeField] GameObject deathEffect;
    [SerializeField] GameObject damageEffect;
    [SerializeField] GameObject DropItem;
    [SerializeField] SpriteRenderer SpriteRenderer;
    [SerializeField] float migration_width;
    [SerializeField] float moveSpeed;
    public int ememy_HitPoint;
    private const int greenHP = 3;
    private const int bleuHP = 2;
    private const int redHP = 1;
    private Rigidbody2D enemy_rigid;
    private Vector3 StartPos;

    public enum SelectNum
    {
        zero, one, two, tree
    }
    public SelectNum selectNumber;

    private void Awake()
    {
        enemy_rigid = GetComponent<Rigidbody2D>();
        StartPos = transform.position;
    }
    //エネミーの体力管理、リセットフラグが返ったとき、生成したエネミーの削除
    private void Update()
    {
        EnemyHealth();

        if(GameManager.SelectReSet)
        {
            GameManager.CloneEnemyDestroy();
        }
    }

    private void FixedUpdate()
    {
        SelectMove();
    }

    //弓矢接触時処理,プレイヤー接触時処理
    private void OnCollisionEnter2D(Collision2D collision)
    {
        const int arrowDamage = 1;  

        if(collision.gameObject.tag == ("arrow"))
        {
            ememy_HitPoint = ememy_HitPoint - arrowDamage;
            Enemy_Damage();
        }
        if(collision.gameObject.tag == ("Player"))
        {
            GameManager.GameReset();
        }
    }
    //爆弾接触時
    private void OnTriggerEnter2D(Collider2D other)
    {
        const int BomDamage = 10;

        if(other.CompareTag("Bom") || other.CompareTag("CloneBom"))
        {
            ememy_HitPoint = ememy_HitPoint - BomDamage;
            Enemy_Damage();
        }
    }
    //エネミーの体力に応じてカラーの変更
    //エネミー撃破処理
    private void EnemyHealth()
    {
        if(ememy_HitPoint == greenHP)
        {
            SpriteRenderer.color = Color.green;
        }
        else if(ememy_HitPoint == bleuHP)
        {
            SpriteRenderer.color = Color.blue;
        }
        else if(ememy_HitPoint == redHP)
        {
            SpriteRenderer.color = Color.red;
        }
        else if(ememy_HitPoint <= 0)
        {
            Enemy_Destroy();
        }

        if(BossEnemySC.bossEnemyDeath)
        {
            Enemy_Destroy();
        }
    }
    //エネミーがダメージを受けたときエフェクト表示
    private void Enemy_Damage()
    {
        Instantiate(damageEffect, transform.position, transform.rotation);
    }
    //エネミー撃破時ランダム関数を呼び出し、dropの値と同じ値だったら矢の弾数を回復するアイテムを生成する
    private void Enemy_Destroy()
    {
        int rnd = Random.Range(1, 9);
        const int drop = 8;
        if(rnd == drop)
        {
            Instantiate(DropItem, transform.position, transform.rotation);
        }
        Instantiate(deathEffect, transform.position, transform.rotation);
        Destroy(gameObject);
    }


    //インスペクターで指定したselectNumberの動作を実行
    private void SelectMove()
    {
        if(selectNumber == SelectNum.zero)
        {
        }
        else if(selectNumber == SelectNum.one)
        {
            Transform_Up_Down();
        }
        else if(selectNumber == SelectNum.two)
        {
            Transform_Left_Right();
        }
        else if(selectNumber == SelectNum.tree)
        {
            Transform_Alldirections();
        }
    }

    //エネミーの上下移動、左右移動　移動スピードと移動幅をインスペクターで指定
    private void Transform_Up_Down()
    {
        float PosY = StartPos.y + Mathf.Sin(Time.time * moveSpeed) * migration_width;
        enemy_rigid.MovePosition(new Vector3(StartPos.x, PosY, StartPos.z));
    }
    private void Transform_Left_Right()
    {
        float PosX = StartPos.x + (Mathf.PingPong(Time.time * moveSpeed, migration_width));
        enemy_rigid.MovePosition(new Vector3(PosX, StartPos.y, StartPos.z));
    }
    private void Transform_Alldirections()
    {
        float PosX = StartPos.x + (Mathf.PingPong(Time.time * moveSpeed, migration_width));
        float PosY = StartPos.y + Mathf.Sin(Time.time * moveSpeed) * migration_width;
        enemy_rigid.MovePosition(new Vector3(PosX, PosY, StartPos.z));
    }
}

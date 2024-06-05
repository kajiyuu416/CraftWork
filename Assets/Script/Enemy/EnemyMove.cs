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
        if(collision.gameObject.tag == ("arrow"))
        {
            ememy_HitPoint = ememy_HitPoint - 1;
            Enemy_Damage();
        }
        if(collision.gameObject.tag == ("Player"))
        {
            GameManager.GameReset();
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Bom") || other.CompareTag("CloneBom"))
        {
            ememy_HitPoint = ememy_HitPoint - 10;
            Enemy_Damage();
        }
    }
    //エネミーの体力に応じての処理
    private void EnemyHealth()
    {
        if(ememy_HitPoint == 3)
        {
            SpriteRenderer.color = Color.green;
        }
        else if(ememy_HitPoint == 2)
        {
            SpriteRenderer.color = Color.blue;
        }
        else if(ememy_HitPoint == 1)
        {
            SpriteRenderer.color = Color.red;
        }
        else if(ememy_HitPoint <= 0)
        {
            Enemy_Destroy();
        }

        if(BossEnemySC.BossEnemyDeath)
        {
            Enemy_Destroy();
        }
    }
    private void Enemy_Damage()
    {
        Instantiate(damageEffect, transform.position, transform.rotation);
    }
    private void Enemy_Destroy()
    {
        int rnd = Random.Range(1, 9);
        if(rnd == 8)
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

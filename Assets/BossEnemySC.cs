using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BossEnemySC : MonoBehaviour
{
    [SerializeField] GameObject Player;
    [SerializeField] GameObject deathEffect;
    [SerializeField] GameObject damageEffect1;
    [SerializeField] GameObject damageEffect2;
    [SerializeField] Image BossEnemy_bar_image;
    [SerializeField] Image BossEnemy_Remaining_image;
    [SerializeField] TextMeshProUGUI text;
    [SerializeField] float moveSpeed;
    public int ememy_HitPoint;
    public float ActionCount;
    private CapsuleCollider2D head;
    private BoxCollider2D body;
    private Animator animator;
    private Rigidbody2D rigid;
    private bool Combat_state;
    private Vector2 move;


    private void Awake()
    {
        body = GetComponentInChildren<BoxCollider2D>();
        head = GetComponentInChildren<CapsuleCollider2D>();
        rigid = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }
    //弓矢接触時処理,プレイヤー接触時処理
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == ("Player"))
        {
            GameManager.GameReset();
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Bom"))
        {
            ememy_HitPoint = ememy_HitPoint - 25;
            Enemy_Damage1();
        }
    }

    void Update()
    {
        EnemyHealth();
        Enemy_battle();
        if(ActionCount < 0)
        {
            ActionCount = 10.0f;
            RandomAction();
            Debug.Log("randomAction");
        }
        rigid.velocity = move;
        animator.SetFloat("Speed",move.magnitude);
    }
    //エネミーの体力に応じての処理
    private void EnemyHealth()
    {
        if(ememy_HitPoint <= 500 && ememy_HitPoint > 400)
        {
            moveSpeed = 1;
        }
        else if(ememy_HitPoint <= 400 && ememy_HitPoint > 250)
        {
            moveSpeed = 1.5f;
        }
        else if(ememy_HitPoint <= 250 && ememy_HitPoint > 100)
        {

        }
        else if(ememy_HitPoint <= 100 && ememy_HitPoint > 0)
        {
         
        }
        else if(ememy_HitPoint <= 0)
        {
            Enemy_Destroy();
        }
    }
    private void RandomAction()
    {
        int rnd = Random.Range(1, 10);
        Debug.Log(rnd);
        if(rnd == 1 || rnd == 2)
        {
            Debug.Log("アクション1を行います");
        }
        else if(rnd == 3 || rnd == 4)
        {
            Debug.Log("アクション2を行います");
        }
        else if(rnd == 5 || rnd == 6)
        {
            Debug.Log("アクション3を行います");
        }
        else if(rnd == 7)
        {
            Debug.Log("アクション4を行います");
        }
        else if(rnd == 8)
        {
            Debug.Log("アクション5を行います");
        }
        else if(rnd == 9)
        {
            Debug.Log("アクション6を行います");
        }

    }
    //プレイヤーが一定の範囲に入ると戦闘入る処理
    private void Enemy_battle()
    {
        if(Combat_state)
        {
            BossEnemy_bar_image.enabled = true;
            BossEnemy_Remaining_image.enabled = true;
            BossEnemy_Remaining_image.fillAmount = ememy_HitPoint / 500.0f;
            text.text = ememy_HitPoint.ToString();
            ActionCount -=Time.deltaTime;
        }
        else if(!Combat_state)
        {
            BossEnemy_bar_image.enabled = false;
            BossEnemy_Remaining_image.enabled = false;
            text.text = "";
        }

        if(Vector2.Distance(transform.position, Player.transform.position) > 20.0f)
        {
            Debug.Log("非戦闘");
            Combat_state = false;
            return;
        }
        Debug.Log("戦闘開始");
        Combat_state = true;
        transform.position = Vector2.MoveTowards(transform.position, new Vector2(Player.transform.position.x, Player.transform.position.y), moveSpeed * Time.deltaTime);
    }

    public void Enemy_Damage1()
    {
        Instantiate(damageEffect1, transform.position, transform.rotation);
    }
    public void Enemy_Damage2()
    {
        Instantiate(damageEffect2, transform.position, transform.rotation);
    }
    public void Enemy_Destroy()
    {
        Combat_state = false;
        Instantiate(deathEffect, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}

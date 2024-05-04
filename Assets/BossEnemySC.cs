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
    [SerializeField] GameObject summon_Obj1;
    [SerializeField] GameObject summon_Obj2;
    [SerializeField] GameObject summon_Obj3;
    [SerializeField] Image BossEnemy_bar_image;
    [SerializeField] Image BossEnemy_Remaining_image;
    [SerializeField] TextMeshProUGUI text;
    [SerializeField] SpriteRenderer sprite;
    [SerializeField] float moveSpeed;
    public int ememy_HitPoint;
    public float ActionCount;
    private CapsuleCollider2D head;
    private BoxCollider2D body;
    private Animator animator;
    private Rigidbody2D rigid;
    private bool Combat_state;
    private bool is_crouch = false;
    private bool NonAction;
    private bool First_form = false;
    private bool Second_form = false;
    private bool Thirdd_form = false;


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
        if(ActionCount < 0 &&!NonAction)
        {
            RandomAction();
        }
        animator.SetBool("Crouching", is_crouch);
    }
    //エネミーの体力に応じての処理
    private void EnemyHealth()
    {
        if(ememy_HitPoint <= 500 && ememy_HitPoint > 400)
        {
            moveSpeed = 1;
            First_form = true;
        }
        else if(ememy_HitPoint <= 400 && ememy_HitPoint > 250)
        {
            moveSpeed = 1.5f;
            Second_form = true;
            First_form = false;
        }
        else if(ememy_HitPoint <= 250 && ememy_HitPoint > 0)
        {
            Thirdd_form = true;
            Second_form = false;
            First_form = false;
        }
        else if(ememy_HitPoint <= 0)
        {
            Enemy_Destroy();
        }
    }
    //10秒毎にランダムで特定のアクションを実施
    private void RandomAction()
    {
        int rnd = Random.Range(5, 7);
        var sumons_pos1 = transform.position + new Vector3(3, 0, 0);
        var sumons_pos2 = transform.position + new Vector3(-3, 0, 0);
        var sumons_pos3 = transform.position + new Vector3(0, 3, 0);
        var sumons_pos4 = transform.position + new Vector3(0, -3, 0);
        var speed = 10;
        var sumons_pos = transform.rotation;
        sumons_pos = Quaternion.Euler(90, 0, 0);

        if(rnd == 1 || rnd == 2 && First_form)
        {
            Crouch();
            Instantiate(summon_Obj1, sumons_pos1, sumons_pos);
            Instantiate(summon_Obj1, sumons_pos2, sumons_pos);
        }
        else if(rnd == 1 || rnd == 2 && Second_form || Thirdd_form)
        {
            Crouch();
            Instantiate(summon_Obj1, sumons_pos1, sumons_pos);
            Instantiate(summon_Obj2, sumons_pos2, sumons_pos);
            Instantiate(summon_Obj1, sumons_pos3, sumons_pos);
            Instantiate(summon_Obj2, sumons_pos4, sumons_pos);
        }

        if(rnd == 3 || rnd == 4)
        {
            Crouch();
            Instantiate(summon_Obj2, sumons_pos3, sumons_pos);
            Instantiate(summon_Obj2, sumons_pos4, sumons_pos);
        }
        else if(rnd == 3 || rnd == 4 && Second_form || Thirdd_form)
        {
            Crouch();
            Instantiate(summon_Obj2, sumons_pos1, sumons_pos);
            Instantiate(summon_Obj1, sumons_pos2, sumons_pos);
            Instantiate(summon_Obj2, sumons_pos3, sumons_pos);
            Instantiate(summon_Obj1, sumons_pos4, sumons_pos);
        }

        if(rnd == 5 || rnd == 6)
        {
            ActionCount = 10.0f;
            Debug.Log("アクション3を行います");
            Vector3 force = new Vector2(100,0);
            Instantiate(summon_Obj3,transform.position,transform.rotation);
            //Vector2.MoveTowards(transform.position, new Vector2(Player.transform.position.x, Player.transform.position.y), speed * Time.deltaTime);
        }

        if(rnd == 7)
        {
            ActionCount = 10.0f;
            Debug.Log("アクション4を行います");
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
            NonAction = true;
            return;
        }

        Debug.Log("戦闘開始");
        Combat_state = true;
        if(transform.position.x > Player.transform.position.x)
        {
            sprite.flipX = true;
        }
        else if(transform.position.x < Player.transform.position.x)
        {
            sprite.flipX = false;
        }

        if(!NonAction)
        {
          transform.position = Vector2.MoveTowards(transform.position, new Vector2(Player.transform.position.x, Player.transform.position.y), moveSpeed * Time.deltaTime);
        }

        animator.SetFloat("Speed",transform.position.magnitude);
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
    private void Crouch()
    {
        is_crouch = true;
        NonAction = true;
        Invoke("stand_up", 3.0f);
        animator.SetTrigger("Crouch");
    }
    private void stand_up()
    {
        is_crouch = false;
        NonAction = false;
        ActionCount = 10.0f;
    }
}

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
    [SerializeField] GameObject summon_Obj4;
    [SerializeField] MeshRenderer meshRenderer;
    [SerializeField] BoxCollider2D boxCollider2D;
    [SerializeField] Image BossEnemy_bar_image;
    [SerializeField] Image BossEnemy_Remaining_image;
    [SerializeField] TextMeshProUGUI text;
    [SerializeField] SpriteRenderer sprite;
    public int ememy_HitPoint;
    private float moveSpeed;
    public float ActionCount  = 10.0f;
    public static bool BossEnemyDeath = false;
    private bool Combat_state;
    private bool NonAction;
    private bool nonsumonEnemys;
    private bool is_crouch = false;
    private CapsuleCollider2D head;
    private BoxCollider2D body;
    private Animator animator;
    private Vector3 OriginPosition;
    private const int MaxHP = 500;
    private const float MaxActionCount = 10.0f;

    private void Awake()
    {
        body = GetComponentInChildren<BoxCollider2D>();
        head = GetComponentInChildren<CapsuleCollider2D>();
        animator = GetComponent<Animator>();
        OriginPosition = transform.position;
    }
    //  ボム接触時処理,プレイヤー接触時処理
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == ("Player"))
        {
            GameManager.GameReset();
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Bom") || other.CompareTag("CloneBom"))
        {
            ememy_HitPoint = ememy_HitPoint - 25;
            Enemy_Damage1();
        }
    }
    private void Update()
    {
        EnemyHealth();
        Enemy_battle();
        if(ActionCount < 0 &&!NonAction)
        {
            RandomAction();
        }
        animator.SetBool("Crouching", is_crouch);

        if(GameManager.SelectReSet)
        {
            transform.position = OriginPosition;
            ememy_HitPoint = MaxHP;
            ActionCount = MaxActionCount;
            moveSpeed = 1.5f;
        }
    }
    //エネミーの体力に応じての処理
    private void EnemyHealth()
    {
        if(ememy_HitPoint <= 500 && ememy_HitPoint > 400)
        {
            moveSpeed = 1.5f;
        }
        else if(ememy_HitPoint <= 400 && ememy_HitPoint > 250)
        {
            moveSpeed = 2.0f;
        }
        else if(ememy_HitPoint <= 250 && ememy_HitPoint > 0)
        {
            moveSpeed = 2.5f;
        }
        else if(ememy_HitPoint <= 0)
        {
            Enemy_Destroy();
        }
    }
    //10秒毎にランダムで特定のアクションを実施
    //1.2　エネミー召喚　3.4 ボム投的 5 Playeの位置に移動疎外の罠を設置
    private void RandomAction()
    {
        int rnd = Random.Range(1, 6);
        var summons_pos1 = transform.position + new Vector3(3, 0, 0);
        var summons_pos2 = transform.position + new Vector3(-3, 0, 0);
        var summons_pos3 = transform.position + new Vector3(0, 3, 0);
        var summons_pos4 = transform.position + new Vector3(0, -3, 0);
        var summons_pos = transform.rotation;
        summons_pos = Quaternion.Euler(90, 0, 0);

        GameObject[] enemyObjects = GameObject.FindGameObjectsWithTag("CloneEnemy");
        if(enemyObjects.Length > 8)
        {
            Debug.Log("10体以上いるよ");
            nonsumonEnemys = true;
        }
        else if(enemyObjects.Length < 8)
        {
            Debug.Log("10体以下");
            nonsumonEnemys = false;
        }
        if(rnd == 1)
        {
            if(ememy_HitPoint <= 500 && ememy_HitPoint > 350 && !nonsumonEnemys)
            {
                Debug.Log("体力が350以上でa行動");
                Crouch();
                Instantiate(summon_Obj1, summons_pos1, summons_pos);
                Instantiate(summon_Obj1, summons_pos2, summons_pos);
            }
            else if(ememy_HitPoint <= 350 && ememy_HitPoint > 0 && !nonsumonEnemys)
            {
                Debug.Log("体力が350以下でa行動");
                Crouch();
                Instantiate(summon_Obj1, summons_pos1, summons_pos);
                Instantiate(summon_Obj2, summons_pos2, summons_pos);
                Instantiate(summon_Obj1, summons_pos3, summons_pos);
                Instantiate(summon_Obj2, summons_pos4, summons_pos);
            }
            else
            {
                RandomAction();
                Debug.Log("aの行動ができなかった為、再抽選を行います");
            }
        }
        else if(rnd == 2)
        {
            if(ememy_HitPoint <= 500 && ememy_HitPoint > 350 && !nonsumonEnemys)
            {
                Debug.Log("体力が350以上でｂ行動");
                Crouch();
                Instantiate(summon_Obj2, summons_pos3, summons_pos);
                Instantiate(summon_Obj2, summons_pos4, summons_pos);
            }
            else if(ememy_HitPoint <= 350 && ememy_HitPoint > 0 && !nonsumonEnemys)
            {
                Debug.Log("体力が350以下でｂ行動");
                Crouch();
                Instantiate(summon_Obj2, summons_pos1, summons_pos);
                Instantiate(summon_Obj1, summons_pos2, summons_pos);
                Instantiate(summon_Obj2, summons_pos3, summons_pos);
                Instantiate(summon_Obj1, summons_pos4, summons_pos);
            }
            else
            {
                RandomAction();
                Debug.Log("bの行動ができなかった為、再抽選を行います");
            }

        }
        else if(rnd == 3 || rnd == 4)
        {
            throw_Bom();
        }
        else if(rnd == 5)
        {
            Gravity_Hole();
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
            meshRenderer.enabled = true;
            boxCollider2D.enabled = true;
        }
        else if(!Combat_state)
        {
            BossEnemy_bar_image.enabled = false;
            BossEnemy_Remaining_image.enabled = false;
            meshRenderer.enabled = false;
            boxCollider2D.enabled = false;
            text.text = "";
        }
        if(Vector2.Distance(transform.position, Player.transform.position) > 18.0f)
        {
            Combat_state = false;
            NonAction = false;
            return;
        }
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
          transform.position = Vector2.MoveTowards
                (transform.position, new Vector2(Player.transform.position.x, Player.transform.position.y), moveSpeed * Time.deltaTime);
        }
   
        animator.SetFloat("Speed",transform.position.magnitude);
    }
    //Enemyのダメージ・撃破・ランダムアクション処理
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
        BossEnemyDeath = true;
        Instantiate(deathEffect, transform.position,Quaternion.Euler(-90, 0, 0));
        Destroy(gameObject);
        SoundManager SM = SoundManager.Instance;
        SM.StopBGM();
        SM.SettingPlaySE21();
    }
    private void Crouch()
    {
        is_crouch = true;
        NonAction = true;
        SoundManager SM = SoundManager.Instance;
        SM.SettingPlaySE19();
        Invoke("Stand_up", 3.0f);
        animator.SetTrigger("Crouch");
    }
    private void Stand_up()
    {
        is_crouch = false;
        NonAction = false;
        ActionCount = 10.0f;
    }
    private void throw_Bom()
    {
        var Bom = Instantiate(summon_Obj3);
        Bom.transform.position = transform.position;
        Vector2 vec = Player.transform.position - transform.position;
        Bom.GetComponent<Rigidbody2D>().velocity = vec * 2.0f;
        SoundManager SM = SoundManager.Instance;
        SM.SettingPlaySE5();
        ActionCount = 10.0f;
    }
    private void Gravity_Hole()
    {
        Instantiate(summon_Obj4, Player.transform.position,Quaternion.Euler(180, 0, 0));
        SoundManager SM = SoundManager.Instance;
        SM.SettingPlaySE20();
        ActionCount = 10.0f;
    }
}

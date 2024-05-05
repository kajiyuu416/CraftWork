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
    [SerializeField] GameObject gameObj;
    [SerializeField] Image BossEnemy_bar_image;
    [SerializeField] Image BossEnemy_Remaining_image;
    [SerializeField] TextMeshProUGUI text;
    [SerializeField] SpriteRenderer sprite;
    [SerializeField] float moveSpeed;
    public int ememy_HitPoint;
    public float ActionCount;
    public static bool BossEnemyDeath = false;
    private CapsuleCollider2D head;
    private BoxCollider2D body;
    private Animator animator;
    private bool Combat_state;
    private bool NonAction;
    private bool is_crouch = false;
    private bool First_form = false;
    private bool Second_form = false;
    private bool Thirdd_form = false;

    private void Awake()
    {
        body = GetComponentInChildren<BoxCollider2D>();
        head = GetComponentInChildren<CapsuleCollider2D>();
        animator = GetComponent<Animator>();
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
        if(other.CompareTag("Bom"))
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
    }
    //エネミーの体力に応じての処理
    private void EnemyHealth()
    {
        if(ememy_HitPoint <= 500 && ememy_HitPoint > 400)
        {
            moveSpeed = 1.5f;
            First_form = true;
        }
        else if(ememy_HitPoint <= 400 && ememy_HitPoint > 250)
        {
            moveSpeed = 2.0f;
            Second_form = true;
            First_form = false;
        }
        else if(ememy_HitPoint <= 250 && ememy_HitPoint > 0)
        {
            moveSpeed = 2.5f;
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

        if(rnd == 1)
        {
            if(First_form)
            {
                Crouch();
                Instantiate(summon_Obj1, summons_pos1, summons_pos);
                Instantiate(summon_Obj1, summons_pos2, summons_pos);
             
            }
            else if(Second_form || Thirdd_form)
            {
                Crouch();
                Instantiate(summon_Obj1, summons_pos1, summons_pos);
                Instantiate(summon_Obj2, summons_pos2, summons_pos);
                Instantiate(summon_Obj1, summons_pos3, summons_pos);
                Instantiate(summon_Obj2, summons_pos4, summons_pos);
            }
        }
        else if(rnd == 2)
        {
            if(First_form)
            {
                Crouch();
                Instantiate(summon_Obj2, summons_pos3, summons_pos);
                Instantiate(summon_Obj2, summons_pos4, summons_pos);
            }
            else if(Second_form || Thirdd_form)
            {
                Crouch();
                Instantiate(summon_Obj2, summons_pos1, summons_pos);
                Instantiate(summon_Obj1, summons_pos2, summons_pos);
                Instantiate(summon_Obj2, summons_pos3, summons_pos);
                Instantiate(summon_Obj1, summons_pos4, summons_pos);
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
            gameObj.SetActive(true);
        }
        else if(!Combat_state)
        {
            BossEnemy_bar_image.enabled = false;
            BossEnemy_Remaining_image.enabled = false;
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
          transform.position = Vector2.MoveTowards(transform.position, new Vector2(Player.transform.position.x, Player.transform.position.y), moveSpeed * Time.deltaTime);
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
        var summons_pos = transform.rotation;
        summons_pos = Quaternion.Euler(-90, 0, 0);
        Combat_state = false;
        BossEnemyDeath = true;
        Instantiate(deathEffect, transform.position,summons_pos);
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
        var summons_pos = transform.rotation;
        summons_pos = Quaternion.Euler(180, 0, 0);
        Instantiate(summon_Obj4, Player.transform.position, summons_pos);
        SoundManager SM = SoundManager.Instance;
        SM.SettingPlaySE20();
        ActionCount = 10.0f;
    }
}

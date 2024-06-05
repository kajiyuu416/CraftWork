using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class BossEnemySC : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] GameObject[] effects;
    [SerializeField] GameObject[] summon_Objcts;
    [SerializeField] MeshRenderer meshRenderer;
    [SerializeField] BoxCollider2D boxCollider2D;
    [SerializeField] Image bossEnemy_bar_image;
    [SerializeField] Image bossEnemy_Remaining_image;
    [SerializeField] SpriteRenderer sprite;
    [SerializeField] TextMeshProUGUI text;
    public int ememy_HitPoint;
    private float moveSpeed;
    public float actionCount  = 10.0f;
    public static bool bossEnemyDeath = false;
    private bool combat_state;  //ボスエネミーの戦闘状態制御フラグ
    private bool nonAction; 　//フラグが返っている間は、プレイヤーの追従を行わない
    private bool nonsumonEnemys;　　//フロアの特定のタグが付いたオブジェクトが一定の数を超えた場合フラグが返る
    private bool is_crouch = false; 
    private CapsuleCollider2D head;
    private BoxCollider2D body;
    private Animator animator;
    private Vector3 originPosition;
    private const int maxHP = 500;
    private const int triggerHP = 350;
    private const int halfHP = 250;
    private const int bomDamage = 25;
    private const float maxactionCount = 10.0f;
    private const float defaultspeed = 1.5f;
    private const float litleearlyspeed = 2.0f;
    private const float earlyspeed = 2.5f;

    //ゲーム開始時、子供にしている頭、体のコライダー、アニメーター、初期座標を取得
    private void Awake()
    {
        if(head == null || body == null)
        {
            body = GetComponentInChildren<BoxCollider2D>();
            head = GetComponentInChildren<CapsuleCollider2D>();
        }
        animator = GetComponent<Animator>();
        originPosition = transform.position;
    }
    //  ボム接触時処理
    //  プレイヤー接触時リセット
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == ("Player"))
        {
            GameManager.GameReset();
        }
    }
    //  ボス接触時　ボスの体力をマイナス、エフェクトの生成
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Bom") || other.CompareTag("CloneBom"))
        {
            ememy_HitPoint = ememy_HitPoint - bomDamage;
            Enemy_Damage1();
        }
    }
    //  カウントダウンを行い、0になるとランダムアクションを実行する
    // リセットフラグが返ると、位置,体力,カウントダウンの値,移動スピードのリセットを行う

    private void Update()
    {
        EnemyHealth();
        Enemy_battle();
        if(actionCount < 0 &&!nonAction)
        {
            RandomAction();
        }
        animator.SetBool("Crouching", is_crouch);

        if(GameManager.SelectReSet)
        {
            transform.position = originPosition;
            ememy_HitPoint = maxHP;
            actionCount = maxactionCount;
            moveSpeed = defaultspeed;
        }
    }
    //エネミーの体力に応じて移動スピードを変更する
    //エネミーの体力が0になると、ボス撃破の関数を呼び出す
    private void EnemyHealth()
    {
        if(ememy_HitPoint <= maxHP && ememy_HitPoint > triggerHP)
        {
            moveSpeed = defaultspeed;
        }
        else if(ememy_HitPoint <= triggerHP && ememy_HitPoint > halfHP)
        {
            moveSpeed = litleearlyspeed;
        }
        else if(ememy_HitPoint <= halfHP && ememy_HitPoint > 0)
        {
            moveSpeed = earlyspeed;
        }
        else if(ememy_HitPoint <= 0)
        {
            Enemy_Destroy();
        }
    }
    //10秒毎にランダムで特定のアクションを実施
    //1.2　エネミー召喚　3.4 ボム投的 5 Playeの位置に移動阻害の罠を設置
    private void RandomAction()
    {
        int rnd = Random.Range(1, 6);
        const int maxfloornum = 8;
        var summons_pos1 = transform.position + new Vector3(3, 0, 0);
        var summons_pos2 = transform.position + new Vector3(-3, 0, 0);
        var summons_pos3 = transform.position + new Vector3(0, 3, 0);
        var summons_pos4 = transform.position + new Vector3(0, -3, 0);
        var summons_pos = transform.rotation;
        summons_pos = Quaternion.Euler(90, 0, 0);



        //シーン内のCloneEnemyの数を制限
        //8体以上エネミーが存在する場合はフラグを返し、ランダムアクションの制御を行う
        GameObject[] enemyObjects = GameObject.FindGameObjectsWithTag("CloneEnemy");
        if(enemyObjects.Length > maxfloornum && !nonsumonEnemys)
        {
            nonsumonEnemys = true;
        }
        else if(enemyObjects.Length < maxfloornum && nonsumonEnemys)
        {
            nonsumonEnemys = false;
        }

        //rndが1,2の場合の処理　エネミーの召喚を行うオブジェクトの生成
        //ボスエネミーの体力が350以下になると、生成数を増やす
        //特定のフラグ("CloneEnemy")が付いたオブジェクトが8体以上存在する場合、生成のアクションは行わず再度ランダムアクションを行う
        if(rnd == 1)
        {
            if(ememy_HitPoint <= maxHP && ememy_HitPoint > triggerHP && !nonsumonEnemys)
            {
                Crouch();
                Instantiate(summon_Objcts[0], summons_pos1, summons_pos);
                Instantiate(summon_Objcts[0], summons_pos2, summons_pos);
            }
            else if(ememy_HitPoint <= triggerHP && ememy_HitPoint > 0 && !nonsumonEnemys)
            {
                Crouch();
                Instantiate(summon_Objcts[0], summons_pos1, summons_pos);
                Instantiate(summon_Objcts[1], summons_pos2, summons_pos);
                Instantiate(summon_Objcts[0], summons_pos3, summons_pos);
                Instantiate(summon_Objcts[1], summons_pos4, summons_pos);
            }
            else
            {
                RandomAction();
            }
        }
        else if(rnd == 2)
        {
            if(ememy_HitPoint <= maxHP && ememy_HitPoint > triggerHP && !nonsumonEnemys)
            {
                Crouch();
                Instantiate(summon_Objcts[1], summons_pos3, summons_pos);
                Instantiate(summon_Objcts[1], summons_pos4, summons_pos);
            }
            else if(ememy_HitPoint <= triggerHP && ememy_HitPoint > 0 && !nonsumonEnemys)
            {
                Crouch();
                Instantiate(summon_Objcts[1], summons_pos1, summons_pos);
                Instantiate(summon_Objcts[0], summons_pos2, summons_pos);
                Instantiate(summon_Objcts[1], summons_pos3, summons_pos);
                Instantiate(summon_Objcts[0], summons_pos4, summons_pos);
            }
            else
            {
                RandomAction();
            }

        }
        //rndが3,4の場合の処理 プレイヤーへ向かって点火済みのボムを投げる
        else if(rnd == 3 || rnd == 4)
        {
            Throw_Bom();
        }
        //rndが5の場合の処理 プレイヤーの座標に移動阻害の罠を生成する
        else if(rnd == 5)
        {
            Gravity_Hole();
        }
    }
    //プレイヤーが一定の範囲に入ると戦闘入る処理
    private void Enemy_battle()
    {
        //ボスエネミーの体力表示、テキスト表示
        if(combat_state)
        {
            bossEnemy_bar_image.enabled = true;
            bossEnemy_Remaining_image.enabled = true;
            bossEnemy_Remaining_image.fillAmount = ememy_HitPoint / 500.0f;
            text.text = ememy_HitPoint.ToString();
            actionCount -=Time.deltaTime;
            meshRenderer.enabled = true;
            boxCollider2D.enabled = true;
        }
        else if(!combat_state)
        {
            bossEnemy_bar_image.enabled = false;
            bossEnemy_Remaining_image.enabled = false;
            meshRenderer.enabled = false;
            boxCollider2D.enabled = false;
            text.text = "";
        }
        //プレイヤーとボスエネミーの距離を取り20以下になると戦闘開始状態のフラグが返る
        if(Vector2.Distance(transform.position, player.transform.position) > 18.0f)
        {
            combat_state = false;
            nonAction = false;
            return;
        }
        combat_state = true;
        //プレイヤーの向きに応じて、ボスエネミーの向きを変更
        if(transform.position.x > player.transform.position.x)
        {
            sprite.flipX = true;
        }
        else if(transform.position.x < player.transform.position.x)
        {
            sprite.flipX = false;
        }
        //プレイヤーに向かって移動
        if(!nonAction)
        {
          transform.position = Vector2.MoveTowards
                (transform.position, new Vector2(player.transform.position.x, player.transform.position.y), moveSpeed * Time.deltaTime);
        }
   
        animator.SetFloat("Speed",transform.position.magnitude);
    }
    //Enemyのダメージ・撃破・ランダムアクション処理
public void Enemy_Damage1()
    {
        Instantiate(effects[1], transform.position, transform.rotation);
    }
    public void Enemy_Damage2()
    {
        Instantiate(effects[2], transform.position, transform.rotation);
    }
    public void Enemy_Destroy()
    {
        var sumons_pos = transform.rotation;
        sumons_pos = Quaternion.Euler(-90, 0, 0);
        combat_state = false;
        bossEnemyDeath = true;
        Instantiate(effects[0], transform.position, sumons_pos);
        Destroy(gameObject);
        SoundManager.Instance.StopBGM();
        SoundManager.Instance.SettingPlaySE21();
    }
    //エネミー召喚と同時に呼び出される関数
    //アニメーションの変更、位置の固定
    private void Crouch()
    {
        is_crouch = true;
        nonAction = true;
        SoundManager.Instance.SettingPlaySE19();
        Invoke("Stand_up", 3.0f);
        animator.SetTrigger("Crouch");
    }
    //Crouch()の後に呼び出される関数
    //アニメーションの変更、位置の固定を解除
    private void Stand_up()
    {
        is_crouch = false;
        nonAction = false;
        actionCount = maxactionCount;
    }
    //ボムの生成、生成したボムにプレイヤーとボス自身の距離を引いた値にthrowPowerを合わせvelocityの変更を行う
    private void Throw_Bom()
    {
        var Bom = Instantiate(summon_Objcts[2]);
        float throwPower = 2.0f;

        Bom.transform.position = transform.position;
        Vector2 vec = player.transform.position - transform.position;
        Bom.GetComponent<Rigidbody2D>().velocity = vec * throwPower;
        SoundManager.Instance.SettingPlaySE5();
        actionCount = maxactionCount;
    }
    //グラビディーホールの生成、プレイヤーの移動阻害
    private void Gravity_Hole()
    {
        var sumons_pos = transform.rotation;
        sumons_pos = Quaternion.Euler(180, 0, 0);
        Instantiate(summon_Objcts[3], player.transform.position,sumons_pos);
        SoundManager.Instance.SettingPlaySE20();
        actionCount = maxactionCount;
    }
}

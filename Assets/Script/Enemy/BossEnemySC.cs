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
    private bool combat_state;  //�{�X�G�l�~�[�̐퓬��Ԑ���t���O
    private bool nonAction; �@//�t���O���Ԃ��Ă���Ԃ́A�v���C���[�̒Ǐ]���s��Ȃ�
    private bool nonsumonEnemys;�@�@//�t���A�̓���̃^�O���t�����I�u�W�F�N�g�����̐��𒴂����ꍇ�t���O���Ԃ�
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

    //�Q�[���J�n���A�q���ɂ��Ă��铪�A�̂̃R���C�_�[�A�A�j���[�^�[�A�������W���擾
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
    //  �{���ڐG������
    //  �v���C���[�ڐG�����Z�b�g
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == ("Player"))
        {
            GameManager.GameReset();
        }
    }
    //  �{�X�ڐG���@�{�X�̗̑͂��}�C�i�X�A�G�t�F�N�g�̐���
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Bom") || other.CompareTag("CloneBom"))
        {
            ememy_HitPoint = ememy_HitPoint - bomDamage;
            Enemy_Damage1();
        }
    }
    //  �J�E���g�_�E�����s���A0�ɂȂ�ƃ����_���A�N�V���������s����
    // ���Z�b�g�t���O���Ԃ�ƁA�ʒu,�̗�,�J�E���g�_�E���̒l,�ړ��X�s�[�h�̃��Z�b�g���s��

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
    //�G�l�~�[�̗̑͂ɉ����Ĉړ��X�s�[�h��ύX����
    //�G�l�~�[�̗̑͂�0�ɂȂ�ƁA�{�X���j�̊֐����Ăяo��
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
    //10�b���Ƀ����_���œ���̃A�N�V���������{
    //1.2�@�G�l�~�[�����@3.4 �{�����I 5 Playe�̈ʒu�Ɉړ��j�Q��㩂�ݒu
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



        //�V�[������CloneEnemy�̐��𐧌�
        //8�̈ȏ�G�l�~�[�����݂���ꍇ�̓t���O��Ԃ��A�����_���A�N�V�����̐�����s��
        GameObject[] enemyObjects = GameObject.FindGameObjectsWithTag("CloneEnemy");
        if(enemyObjects.Length > maxfloornum && !nonsumonEnemys)
        {
            nonsumonEnemys = true;
        }
        else if(enemyObjects.Length < maxfloornum && nonsumonEnemys)
        {
            nonsumonEnemys = false;
        }

        //rnd��1,2�̏ꍇ�̏����@�G�l�~�[�̏������s���I�u�W�F�N�g�̐���
        //�{�X�G�l�~�[�̗̑͂�350�ȉ��ɂȂ�ƁA�������𑝂₷
        //����̃t���O("CloneEnemy")���t�����I�u�W�F�N�g��8�̈ȏ㑶�݂���ꍇ�A�����̃A�N�V�����͍s�킸�ēx�����_���A�N�V�������s��
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
        //rnd��3,4�̏ꍇ�̏��� �v���C���[�֌������ē_�΍ς݂̃{���𓊂���
        else if(rnd == 3 || rnd == 4)
        {
            Throw_Bom();
        }
        //rnd��5�̏ꍇ�̏��� �v���C���[�̍��W�Ɉړ��j�Q��㩂𐶐�����
        else if(rnd == 5)
        {
            Gravity_Hole();
        }
    }
    //�v���C���[�����͈̔͂ɓ���Ɛ퓬���鏈��
    private void Enemy_battle()
    {
        //�{�X�G�l�~�[�̗͕̑\���A�e�L�X�g�\��
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
        //�v���C���[�ƃ{�X�G�l�~�[�̋��������20�ȉ��ɂȂ�Ɛ퓬�J�n��Ԃ̃t���O���Ԃ�
        if(Vector2.Distance(transform.position, player.transform.position) > 18.0f)
        {
            combat_state = false;
            nonAction = false;
            return;
        }
        combat_state = true;
        //�v���C���[�̌����ɉ����āA�{�X�G�l�~�[�̌�����ύX
        if(transform.position.x > player.transform.position.x)
        {
            sprite.flipX = true;
        }
        else if(transform.position.x < player.transform.position.x)
        {
            sprite.flipX = false;
        }
        //�v���C���[�Ɍ������Ĉړ�
        if(!nonAction)
        {
          transform.position = Vector2.MoveTowards
                (transform.position, new Vector2(player.transform.position.x, player.transform.position.y), moveSpeed * Time.deltaTime);
        }
   
        animator.SetFloat("Speed",transform.position.magnitude);
    }
    //Enemy�̃_���[�W�E���j�E�����_���A�N�V��������
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
    //�G�l�~�[�����Ɠ����ɌĂяo�����֐�
    //�A�j���[�V�����̕ύX�A�ʒu�̌Œ�
    private void Crouch()
    {
        is_crouch = true;
        nonAction = true;
        SoundManager.Instance.SettingPlaySE19();
        Invoke("Stand_up", 3.0f);
        animator.SetTrigger("Crouch");
    }
    //Crouch()�̌�ɌĂяo�����֐�
    //�A�j���[�V�����̕ύX�A�ʒu�̌Œ������
    private void Stand_up()
    {
        is_crouch = false;
        nonAction = false;
        actionCount = maxactionCount;
    }
    //�{���̐����A���������{���Ƀv���C���[�ƃ{�X���g�̋������������l��throwPower�����킹velocity�̕ύX���s��
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
    //�O���r�f�B�[�z�[���̐����A�v���C���[�̈ړ��j�Q
    private void Gravity_Hole()
    {
        var sumons_pos = transform.rotation;
        sumons_pos = Quaternion.Euler(180, 0, 0);
        Instantiate(summon_Objcts[3], player.transform.position,sumons_pos);
        SoundManager.Instance.SettingPlaySE20();
        actionCount = maxactionCount;
    }
}

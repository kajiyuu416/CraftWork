using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    [SerializeField] GameObject deathEffect;
    [SerializeField] SpriteRenderer SpriteRenderer;
    [SerializeField] float migration_width;
    [SerializeField] float moveSpeed;
    public int ememy_HitPoint;
    private Color red;
    private Color bleu;
    private Color green;
    private Rigidbody2D enemy_rigid;
    private Vector3 StartPos;
    public enum SelectNum
    {
      zero,one, two, tree, four, five
    }
    public SelectNum selectNumber;

    private void Awake()
    {
        enemy_rigid = GetComponent<Rigidbody2D>();
        StartPos = transform.position;
        Enemy_Color();
    }
    private void Update()
    {
        EnemyHealth();
    }
    private void FixedUpdate()
    {
        SelectMove();
    }

    //�|��ڐG������,�v���C���[�ڐG������
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == ("arrow"))
        {
            ememy_HitPoint = ememy_HitPoint - 1;
        }
        if(collision.gameObject.tag == ("Player"))
        {
            GameManager.GameReset();
        }
    }
    //�G�l�~�[�̗̑͂ɉ����Ă̏���
    private void EnemyHealth()
    {
        if(ememy_HitPoint == 3)
        {
            SpriteRenderer.color = green;
        }
        else if(ememy_HitPoint == 2)
        {
            SpriteRenderer.color = bleu;
        }
        else if(ememy_HitPoint == 1)
        {
            SpriteRenderer.color = red;
        }
        else if(ememy_HitPoint <= 0)
        {
            DestroyEnemy();
        }
    }
    private void Enemy_Color()
    {
        red = new Color(255, 0, 0);
        green = new Color(0, 255, 0);
        bleu = new Color(0, 0, 255);
    }
    private void DestroyEnemy()
    {
        Instantiate(deathEffect, transform.position, transform.rotation);
        Destroy(gameObject);
    }


    //�C���X�y�N�^�[�Ŏw�肵��selectNumber�̓�������s
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    public Vector2 force;
    private Rigidbody2D enemy_rigid;
    public int ememy_HitPoint;
    private void Awake()
    {
        enemy_rigid = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        if(ememy_HitPoint <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        enemy_rigid.AddForce(force, ForceMode2D.Force);

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("arrow"))
        {
            ememy_HitPoint = ememy_HitPoint - 1; 
        }
    }

}

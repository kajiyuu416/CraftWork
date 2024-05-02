using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossEnemySC : MonoBehaviour
{
    [SerializeField] GameObject Player;
    [SerializeField] GameObject deathEffect;
    [SerializeField] GameObject damageEffect1;
    [SerializeField] GameObject damageEffect2;
    [SerializeField] CapsuleCollider2D head;
    [SerializeField] BoxCollider2D body;
    [SerializeField] Image BossEnemy_bar_image;
    [SerializeField] Image BossEnemy_Remaining_image;
    [SerializeField] float moveSpeed;
    public int ememy_HitPoint;
    private bool Combat_state;

    private void Awake()
    {
        body = GetComponentInChildren<BoxCollider2D>();
        head = GetComponentInChildren<CapsuleCollider2D>();
    }
    //ã|ñÓê⁄êGéûèàóù,ÉvÉåÉCÉÑÅ[ê⁄êGéûèàóù
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

    // Update is called once per frame
    void Update()
    {
        EnemyHealth();
        if(Combat_state)
        {
            BossEnemy_bar_image.enabled = true;
            BossEnemy_Remaining_image.enabled = true;
            BossEnemy_Remaining_image.fillAmount = ememy_HitPoint / 500.0f;
        }else if(!Combat_state)
        {
            BossEnemy_bar_image.enabled = false;
            BossEnemy_Remaining_image.enabled = false;
        }

        if(Vector2.Distance(transform.position, Player.transform.position) > 20.0f)
        {
            Debug.Log("îÒêÌì¨");
            Combat_state = false;
            return;
        }
        Debug.Log("êÌì¨äJén");
        Combat_state = true;
        transform.position = Vector2.MoveTowards(transform.position, new Vector2(Player.transform.position.x, Player.transform.position.y), moveSpeed * Time.deltaTime);
    }
    private void EnemyHealth()
    {
        if(ememy_HitPoint == 500)
        {
       
        }
        else if(ememy_HitPoint == 400)
        {
    
        }
        else if(ememy_HitPoint == 250)
        {
      
        }
        else if(ememy_HitPoint <= 0)
        {
            Enemy_Destroy();
        }
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
        Instantiate(deathEffect, transform.position, transform.rotation);
        Combat_state = false;
        Destroy(gameObject);
    }
}

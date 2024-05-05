using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityHole : MonoBehaviour
{
    public float DestroyCount = 8.0f;

    private void Update()
    {
        DestroyCount -= Time.deltaTime;

        if(DestroyCount < 0)
        {
            Destroy(gameObject);
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        Rigidbody2D rigid = collision.gameObject.GetComponent<Rigidbody2D>();
        Vector3 vector3 = transform.position - collision.gameObject.transform.position;
        vector3.Normalize();
        if(collision.CompareTag("Player"))
        {
            rigid.velocity *= 0.15f;
            rigid.AddForce(vector3 * rigid.mass * 120.0f);
        }
        else
        {
            rigid.AddForce(-vector3 * rigid.mass * 30.0f);
        }

    }
}

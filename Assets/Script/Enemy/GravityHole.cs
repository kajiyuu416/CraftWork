using UnityEngine;

public class GravityHole : MonoBehaviour
{
    private float DestroyCount = 8.0f;
    //カウントが0又はリセットフラグが返ったときに削除
    private void Update()
    {
        DestroyCount -= Time.deltaTime;

        if(DestroyCount < 0)
        {
            Destroy(gameObject);
        }
        if(GameManager.SelectReSet)
        {
            GameManager.CloneEnemyDestroy();
        }
    }
    //プレイヤーがコライダー内に触れている間、中心にプレイヤーが移動するよう力を加える
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

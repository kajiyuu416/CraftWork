using UnityEngine;

public class GravityHole : MonoBehaviour
{
    private float DestroyCount = 8.0f;
    //�J�E���g��0���̓��Z�b�g�t���O���Ԃ����Ƃ��ɍ폜
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
    //�v���C���[���R���C�_�[���ɐG��Ă���ԁA���S�Ƀv���C���[���ړ�����悤�͂�������
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

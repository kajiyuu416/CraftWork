using UnityEngine;

public class bodyDamage : MonoBehaviour
{
    [SerializeField] BossEnemySC be;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == ("arrow"))
        {
            be.Enemy_Damage1();
            be.ememy_HitPoint = be.ememy_HitPoint - 3;
            Debug.Log("body");
        }
    }
}

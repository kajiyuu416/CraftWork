using UnityEngine;
public class headDamage : MonoBehaviour
{
    [SerializeField] BossEnemySC be;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == ("arrow"))
        {
            be.Enemy_Damage2();
            be.ememy_HitPoint = be.ememy_HitPoint - 6;
            Debug.Log("head");
        }
    }
}

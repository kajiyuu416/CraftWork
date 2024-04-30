using UnityEngine;

public class arrowSC : MonoBehaviour
{
    public EdgeCollider2D EdgeCol;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == ("Enemy"))
        {
            Destroy(gameObject);
        }
    }

}

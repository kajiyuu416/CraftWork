using UnityEngine;

public class arrowSC : MonoBehaviour
{
    public EdgeCollider2D EdgeCol;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == ("Enemy") || collision.gameObject.tag == ("CloneEnemy"))
        {
            Destroy(gameObject);
        }
    }
    private void Update()
    {
        if(GameManager.SelectReSet)
        {
            Destroy(gameObject);
        }
    }
}

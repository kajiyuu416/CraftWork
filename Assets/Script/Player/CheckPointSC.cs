using UnityEngine;
public class CheckPointSC : MonoBehaviour
{
    public bool CheckPoint = false;
    public GameObject CP;
    //CheckPoint�X�V
    private void Update()
    {
        if(CheckPoint)
        {
            Destroy(CP);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
           CheckPoint = true;
        }
    }
}

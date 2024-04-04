using UnityEngine;

public class CheckPointSC : MonoBehaviour
{
    public bool CheckPoint = false;
    public GameObject CP;
    // Update is called once per frame
    public static CheckPointSC Instance
    {
        get; private set;
    }
    void Update()
    {
        if(CheckPoint)
        {
            Destroy(CP);
            Debug.Log("aaa");
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

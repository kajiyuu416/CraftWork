using UnityEngine;

public class CheckPointSC : MonoBehaviour
{
    public bool CheckPoint = false;
    public GameObject CP;
    // Update is called once per frame
    void Update()
    {
        if(CheckPoint)
        {
            Destroy(CP);
            Debug.Log("�`�F�b�N�|�C���g�̍X�V���s���܂���");
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

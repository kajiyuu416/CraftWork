using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class HitEfect : MonoBehaviour
{
    public GameObject hitEfect;
    public Vector3 generationPosition = Vector3.zero;

    private bool hitFlag = false;

    private void Awake()
    {
        //generationPosition.z = -1.5f;
    }
    void Update()
    {
        generationPosition.x = transform.position.x;
        generationPosition.y = transform.position.y;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Item")&&!hitFlag)
        {
            GameObject hE = Instantiate(hitEfect, generationPosition, Quaternion.Euler(0f, 0f, 0f));
            hitFlag = true;
            SoundManager SM = SoundManager.Instance;
            SM.SettingPlaySE();
            Debug.Log("ëŒè€äOÇÃItemÇ…ìñÇΩÇËÇ‹ÇµÇΩ");

        }

    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.CompareTag("Item")&&hitFlag)
        {
            hitFlag = false;
        }
    }

}

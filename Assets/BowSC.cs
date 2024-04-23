using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class BowSC : MonoBehaviour
{
    private SpriteRenderer origin_Sprite;
    private Vector3 origin_Rotate = new Vector3(0, 0, -40);
    public Transform origin_Transform;

    private void Awake()
    {
        origin_Sprite = GetComponent<SpriteRenderer>();
        origin_Transform = GetComponent<Transform>();
    }
    // Update is called once per frame
    void Update()
    {
        if(origin_Sprite.flipX)
        {
           
            Debug.Log("å¸Ç´ïœçX");
        }
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class BowSC : MonoBehaviour
{
    [SerializeField] GameObject arrow;
    Rigidbody2D rigid2d;

    private SpriteRenderer origin_Sprite;
    public static BowSC Instance
    {
        get; private set;
    }
    private void Awake()
    {
        origin_Sprite = GetComponent<SpriteRenderer>();
        rigid2d = arrow.GetComponent<Rigidbody2D>();
    }
    // Update is called once per frame
    void Update()
    {
        rotationChange();
    }

    private void rotationChange()
    {
        Transform origin_transform = this.transform;
        if(origin_Sprite.flipX)
        {
            Vector3 localAngle = origin_transform.localEulerAngles;
            localAngle.z = 40.0f;
            origin_transform.localEulerAngles = localAngle;
        }
        else
        {
            Vector3 localAngle = origin_transform.localEulerAngles;
            localAngle.z = -40.0f;
            origin_transform.localEulerAngles = localAngle;
        }
    }
    public void RighitShot()
    {
        GameObject geneObj = Instantiate(arrow, transform.position, Quaternion.Euler(0f, 0f, -45f));
        Rigidbody2D geneObjRB = geneObj.GetComponent<Rigidbody2D>();
        Vector2 force = new Vector2(15.0f, 0);
        geneObjRB.AddForce(force, ForceMode2D.Impulse);
 
    }
    public void LeftShot()
    {
        GameObject geneObj = Instantiate(arrow, transform.position, Quaternion.Euler(0f, 0f, 140.0f));
        Rigidbody2D geneObjRB = geneObj.GetComponent<Rigidbody2D>();
        Vector2 force = new Vector2(15.0f, 0);
        geneObjRB.AddForce(-force, ForceMode2D.Impulse);
    }

}

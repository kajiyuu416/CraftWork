using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class TorchSC : MonoBehaviour
{
    [SerializeField] float countdownSecond = 30;
    [SerializeField] Sprite torch_On_Sprite;
    [SerializeField] Sprite torch_Off_Sprite;
    //private Text timeText;
    private SpriteRenderer Origin_Sprite;
    private bool torch_little_off;
    private bool torch_off;
    private Light TorchLight;
    // Start is called before the first frame update
    private void Awake()
    {
        TorchLight = GetComponent<Light>();
        Origin_Sprite = GetComponent<SpriteRenderer>();
    }
    void FixedUpdate()
    {
        countdownSecond -= Time.deltaTime;
        var span = new TimeSpan(0, 0, (int) countdownSecond);
        //timeText.text = span.ToString(@"mm\:ss");

        if(countdownSecond <= 10.0f &&!torch_little_off)
        {  // 10•b‚É‚È‚Á‚½‚Æ‚«‚Ìˆ—
            torch_little_off = true;
            TorchLight.range = 5;
            Debug.Log("‰Î‚ª‚ ‚Æ­‚µ‚ÅÁ‚¦‚Ü‚·");
        }
        if(countdownSecond <= 0 && !torch_off)
        {  // 0•b‚É‚È‚Á‚½‚Æ‚«‚Ìˆ—
            torch_off = true;
            TorchLight.range = 0;
            Origin_Sprite.sprite = torch_Off_Sprite;
            Debug.Log("‰Î‚ªŠ®‘S‚ÉÁ‚¦‚Ü‚µ‚½");
        }

        if(countdownSecond <= 0)
        {
            countdownSecond = 0;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("supplyArea"))
        {
            countdownSecond = 30;
            Debug.Log("aaa");
        }
    }
}

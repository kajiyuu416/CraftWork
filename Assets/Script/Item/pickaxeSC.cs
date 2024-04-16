using UnityEngine;
using UnityEngine.UI;

public class pickaxeSC : MonoBehaviour
{
    public int Use_Pickaxe_Count = 5;
    private BoxCollider2D BoxCollider2D;
    new Rigidbody2D rigidbody2D;
    public static pickaxeSC Instance
    {
        get; private set;
    }

    private void Awake()
    {
    if(Instance != null)
     {
      Destroy(gameObject);
      return;
     }
     Instance = this;
     Use_Pickaxe_Count = 5;
     BoxCollider2D = GetComponent<BoxCollider2D>();
     rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if(Use_Pickaxe_Count == 0)
        {
            PlayerController Pc = PlayerController.Instance;
            Destroy(gameObject);
            Pc.ItemLost();
            Debug.Log(Use_Pickaxe_Count);
        }
        if(Use_Pickaxe_Count == 1)
        {
            rigidbody2D.constraints = RigidbodyConstraints2D.None;
            rigidbody2D.constraints = RigidbodyConstraints2D.FreezeRotation;
            Debug.Log(Use_Pickaxe_Count);
        }
        if(Use_Pickaxe_Count == 2)
        {
            rigidbody2D.constraints = RigidbodyConstraints2D.None;
            rigidbody2D.constraints = RigidbodyConstraints2D.FreezeRotation;
            Debug.Log(Use_Pickaxe_Count);
        }
        if(Use_Pickaxe_Count == 3)
        {
            rigidbody2D.constraints = RigidbodyConstraints2D.None;
            rigidbody2D.constraints = RigidbodyConstraints2D.FreezeRotation;
            Debug.Log(Use_Pickaxe_Count);
        }
        if(Use_Pickaxe_Count == 4)
        {
            rigidbody2D.constraints = RigidbodyConstraints2D.None;
            rigidbody2D.constraints = RigidbodyConstraints2D.FreezeRotation;

            Debug.Log(Use_Pickaxe_Count);
        }
        if(Use_Pickaxe_Count == 5)
        {
            rigidbody2D.constraints = RigidbodyConstraints2D.FreezeRotation;
            Debug.Log(Use_Pickaxe_Count);
        }
        else if(Use_Pickaxe_Count < 0)
        {
            Use_Pickaxe_Count = 0;
            PlayerController Pc = PlayerController.Instance;
            Destroy(gameObject);
            Pc.ItemLost();
        }

    }
    //Todo;; オブジェクトの当たり判定を対象のオブジェクトと接触してから一定時間判定を消し、カウントのダブルカウントを防ぐ
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Crumbling_rock"))
        {
            rigidbody2D.constraints = RigidbodyConstraints2D.FreezeAll;
        }
    }
    public void addCount()
    {
        Use_Pickaxe_Count--;
    }
}

using UnityEngine;
using UnityEngine.UI;
public class pickaxeSC : MonoBehaviour
{
    public int Use_Pickaxe_Count = 5;
    private const int MaxCount = 5;
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
     Use_Pickaxe_Count = MaxCount;
     rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        Use_Pickaxe();
        if(GameManager.SelectReSet)
        {
            transform.position = PlayerController.CP;
            Use_Pickaxe_Count = MaxCount;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Crumbling_rock"))
        {
            rigidbody2D.constraints = RigidbodyConstraints2D.FreezeAll;
        }
    }
    public void AddCount()
    {
        Use_Pickaxe_Count--;
    }
    //Pickaxeが使用されるごとにカウントを減少させる
    public void Use_Pickaxe()
    {
        if(Use_Pickaxe_Count == 0)
        {
            PlayerController Pc = PlayerController.Instance;
            Destroy(gameObject);
            Pc.ItemLost();
        }
        if(Use_Pickaxe_Count == 1 || Use_Pickaxe_Count == 2 || Use_Pickaxe_Count == 3 || Use_Pickaxe_Count == 4 || Use_Pickaxe_Count == 5)
        {
            rigidbody2D.constraints = RigidbodyConstraints2D.None;
            rigidbody2D.constraints = RigidbodyConstraints2D.FreezeRotation;
        }
        else if(Use_Pickaxe_Count < 0)
        {
            Use_Pickaxe_Count = 0;
            PlayerController Pc = PlayerController.Instance;
            Destroy(gameObject);
            Pc.ItemLost();
        }
    }
}

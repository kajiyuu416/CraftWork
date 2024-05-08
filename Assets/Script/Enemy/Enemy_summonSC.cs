using UnityEngine;

public class Enemy_summonSC : MonoBehaviour
{
    [SerializeField] GameObject appearance_Efect;
    [SerializeField] GameObject summons_Enemy;
    private void Start()
    {
        Invoke("Summon_Enemy", 2.0f);
    }
    private void Summon_Enemy()
    {
        var sumons_pos = transform.rotation;
        sumons_pos = Quaternion.Euler(0, 0, 0);
        Instantiate(appearance_Efect, transform.position,transform.rotation);
        Instantiate(summons_Enemy, transform.position,sumons_pos);
        Destroy(gameObject);
    }


}

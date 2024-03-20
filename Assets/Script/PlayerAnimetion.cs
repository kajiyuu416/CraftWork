using UnityEngine;
public class PlayerAnimetion : MonoBehaviour
{
    private PlayerController Pc;
    private Animator animator;

    private void Awake()
    {
        Pc = GetComponent<PlayerController>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        animator.SetFloat("Speed", Pc.GetMove().magnitude);
        animator.SetInteger("Side", Pc.GetSideAnim());
        animator.SetBool("Hold", Pc.NowHoldItem);
    }
}

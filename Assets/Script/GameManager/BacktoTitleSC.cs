using UnityEngine;
using UnityEngine.UI;
public class BacktoTitleSC : MonoBehaviour
{
    public Button BackButton;
    void Start()
    {
        BackButton = GetComponent<Button>();
        BackButton.onClick.AddListener(() => {
            GameManager.BacktoTitle();
        });
    }
}

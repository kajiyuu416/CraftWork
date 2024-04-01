using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class CancelSC : MonoBehaviour
{
    public Button CancelButton;
    void Start()
    {
        CancelButton = GetComponent<Button>();
        CancelButton.onClick.AddListener(() => {
            GameManager.SelectCancel();
        });
    }
}

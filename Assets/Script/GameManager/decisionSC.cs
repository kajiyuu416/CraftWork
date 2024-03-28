using UnityEngine;
using UnityEngine.UI;
public class decisionSC : MonoBehaviour
{
    public Button decisionButton;
    void Start()
    {
        decisionButton = GetComponent<Button>();
        decisionButton.onClick.AddListener(() => {
            GameManager.GameReset();
        });
        Debug.Log("Reset");
    }
}

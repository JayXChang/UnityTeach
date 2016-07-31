using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UIPanel : MonoBehaviour
{
    [SerializeField] Text textScore;
    [SerializeField] Text textCountDown;
    [SerializeField] Button btnReplay;
    [SerializeField] GameObject endObj;

    public int TextScore { set { textScore.text = value.ToString(); } }

    public int TextCountDown { set { textCountDown.text = value.ToString(); } }

    public bool IsEndObjActive { set { endObj.SetActive(value); } }

    public UnityAction OnClickBtnReplay { set { btnReplay.onClick.AddListener(value); } }
}

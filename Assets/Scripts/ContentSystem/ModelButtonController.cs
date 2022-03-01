using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ModelButtonController : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI modelNameText = null;
    public TextMeshProUGUI ModelNameText { get => modelNameText; }

    public ModelProperties ownModel { private get; set; }
    private Button ownButton;
    private ContentController cc;

    private void Awake()
    {
        cc = FindObjectOfType<ContentController>();
        ownButton = GetComponent<Button>();
        ownButton.onClick.AddListener(VisualizeObject);
    }

    private void VisualizeObject()
    {
        cc.LoadContent(ownModel.downloadUrl);
    }
}
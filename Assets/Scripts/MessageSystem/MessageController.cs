using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MessageController : MonoBehaviour
{
    [SerializeField]
    private Button downloadButton = null;
    [SerializeField]
    private Button cancelButton = null;
    [SerializeField]
    private TextMeshProUGUI messageText = null;

    private ContentController cc;

    private void Awake()
    {
        cc = FindObjectOfType<ContentController>();

        downloadButton.onClick.AddListener(Download);
        cancelButton.onClick.AddListener(Cancel);
    }

    private void Download()
    {
        cc.LoadContent(messageText.text);
        Cancel();
    }

    private void Cancel()
    {
        gameObject.SetActive(false);
    }
}

using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [Header("User Interface Creating References")]
    [SerializeField]
    private GameObject modelButton = null;
    [SerializeField]
    private Transform parentObjectForButtons = null;

    [Header("Logout System")]
    [SerializeField]
    private Animator logoutAnimator = null;
    [SerializeField]
    private Button profileButton = null;

    private JsonDownloader downloader;

    private void Start()
    {
        profileButton.onClick.AddListener(ShowLogoutPanel);
    }

    private void ShowLogoutPanel()
    {
        logoutAnimator.SetBool("Show", !logoutAnimator.GetBool("Show"));
    }

    private void OnEnable()
    {
        downloader = FindObjectOfType<JsonDownloader>();
        downloader.jsonAttached += OnJsonAttached;
    }

    private void OnDisable()
    {
        downloader.jsonAttached -= OnJsonAttached;
    }

    private void OnJsonAttached(string webAddress)
    {
        downloader.AllModels.ForEach(x =>
        {
            ModelButtonController mbc = Instantiate(modelButton, parentObjectForButtons).GetComponent<ModelButtonController>();
            mbc.ownModel = x;
            mbc.ModelNameText.text = x.assetName;
        });
    }
}
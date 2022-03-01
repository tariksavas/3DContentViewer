using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class LoadingController : MonoBehaviour
{
    private Image ownImage;

    private void Awake()
    {
        ownImage = GetComponent<Image>();
        gameObject.SetActive(false);
    }

    public void StartProgress(UnityWebRequest uwr)
    {
        gameObject.SetActive(true);
        ownImage.fillAmount = 0;

        StartCoroutine(StartProgressCoroutine(uwr));
    }

    private IEnumerator StartProgressCoroutine(UnityWebRequest uwr)
    {
        while (uwr.downloadProgress < 1)
        {
            ownImage.fillAmount = uwr.downloadProgress;
            yield return null;
        }
        gameObject.SetActive(false);
    }
}
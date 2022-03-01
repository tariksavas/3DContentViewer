using UnityEngine;

public class JsonDownloadExceptionHandler : ExceptionHandler
{
    private JsonDownloader downloader;

    protected override void Awake()
    {
        base.Awake();

        downloader = FindObjectOfType<JsonDownloader>();
    }

    private void OnEnable()
    {
        downloader.jsonAttached += OnJsonAttached;
        downloader.error += OnError;
    }

    private void OnDisable()
    {
        downloader.jsonAttached -= OnJsonAttached;
        downloader.error -= OnError;
    }

    private void OnError(string e)
    {
        ownText.text = e;
        ownText.color = Color.red;
        SetEnable();
    }

    private void OnJsonAttached(string webAddress)
    {
        ownText.text = "The json file was successfully downloaded from: \"" + webAddress + "\"";
        ownText.color = Color.green;
        SetEnable();
    }
}

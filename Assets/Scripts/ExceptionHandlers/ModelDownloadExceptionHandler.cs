using UnityEngine;

public class ModelDownloadExceptionHandler : ExceptionHandler
{
    private void OnEnable()
    {
        ModelDownloader.error += OnError;
        ContentController.successful += OnSuccessful;
    }

    private void OnDisable()
    {
        ModelDownloader.error -= OnError;
        ContentController.successful -= OnSuccessful;
    }

    private void OnError(string e)
    {
        ownText.text = e;
        ownText.color = Color.red;
        ownText.verticalAlignment = TMPro.VerticalAlignmentOptions.Middle;
        SetEnable();
    }

    private void OnSuccessful()
    {
        ownText.text = "The content has been downloaded successfully.";
        ownText.color = Color.green;
        ownText.verticalAlignment = TMPro.VerticalAlignmentOptions.Bottom;
        SetEnable();
    }
}
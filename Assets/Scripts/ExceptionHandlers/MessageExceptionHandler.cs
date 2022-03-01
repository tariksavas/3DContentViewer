using System;
using UnityEngine;

public class MessageExceptionHandler : ExceptionHandler
{
    [SerializeField]
    private string wrongURLWarning = string.Empty;
    [SerializeField]
    private string successfulText = string.Empty;

    private void OnEnable()
    {
        MessageSender.error += OnError;
        MessageSender.successful += OnSuccessful;
    }

    private void OnDisable()
    {
        MessageSender.error -= OnError;
        MessageSender.successful -= OnSuccessful;
    }

    private void OnError(Exception e)
    {
        if (e is ArgumentException)
            ownText.text = wrongURLWarning;
        else
            ownText.text = e.Message;

        SetEnable();
    }

    private void OnSuccessful()
    {
        ownText.text = successfulText;

        SetEnable();
    }
}
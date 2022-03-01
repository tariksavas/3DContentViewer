using System;
using TMPro;
using UnityEngine;

public class LoginExceptionHandler : ExceptionHandler
{
    [SerializeField]
    private string wrongNickWarning = string.Empty;

    private void OnEnable()
    {
        LoginController.error += OnError;
    }

    private void OnDisable()
    {
        LoginController.error -= OnError;
    }

    private void OnError(Exception e)
    {
        if (e is ArgumentException)
            ownText.text = wrongNickWarning;
        else
            ownText.text = e.Message;

        SetEnable();
    }
}
using Firebase.Database;
using Firebase.Extensions;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MessageSender : MonoBehaviour
{
    [Header("MessageBox Referances")]
    [SerializeField]
    private TMP_InputField userNickInput = null;
    [SerializeField]
    private TMP_InputField urlInput = null;
    [SerializeField]
    private Button urlSendButton = null;
    [SerializeField]
    private Button currentModelSendButton = null;

    private DatabaseReference referance;

    public static event Action<Exception> error;
    public static event Action successful;

    private void Awake()
    {
        referance = FirebaseDatabase.DefaultInstance.RootReference;
    }

    private void Start()
    {
        urlSendButton.onClick.AddListener(URLSend);
        currentModelSendButton.onClick.AddListener(ModelSend);
    }

    private void URLSend()
    {
        if (!CheckURLInput())
        {
            ArgumentException e = new ArgumentException();
            error?.Invoke(e);
            return;
        }
        Send(userNickInput.text, urlInput.text);
    }

    private void ModelSend()
    {
        Send(userNickInput.text, ModelDownloader.currentModelURL);
    }

    private void Send(string _nick, string bundleURL)
    {
        if (bundleURL == string.Empty)
        {
            Exception e = new Exception("No message to send!");
            error?.Invoke(e);
            return;
        }

        bool send = false;

        referance.Child("Users").GetValueAsync().ContinueWithOnMainThread(task =>
        {
            DataSnapshot snapshot = task.Result;
            if (snapshot.GetRawJsonValue() != null)
            {
                foreach (DataSnapshot ds in snapshot.Children)
                {
                    UserData userData = JsonUtility.FromJson<UserData>(ds.GetRawJsonValue());

                    if (userData.nick == _nick)
                    {
                        userData.message = bundleURL + " " + Time.time;
                        referance.Child("Users").Child(userData.id).SetRawJsonValueAsync(JsonUtility.ToJson(userData));

                        send = true;
                    }
                }

                if (send)
                    successful?.Invoke();
                else
                {
                    Exception e = new Exception("User not found");
                    error?.Invoke(e);
                    return;
                }
            }
        });
    }

    private bool CheckURLInput()
    {
        foreach (char character in urlInput.text)
        {
            if (character == ' ')
                return false;
        }
        return true;
    }
}
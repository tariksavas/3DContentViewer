using Firebase.Auth;
using Firebase.Database;
using System;
using UnityEngine;
using TMPro;

public class MessageHandler : MonoBehaviour
{
    [SerializeField]
    private GameObject messagePopUp = null;
    [SerializeField]
    private TextMeshProUGUI messageText = null;

    private DatabaseReference userRefs;
    private FirebaseAuth auth;

    private void Awake()
    {
        auth = FirebaseAuth.DefaultInstance;
        userRefs = FirebaseDatabase.DefaultInstance.GetReference("Users");
    }

    private void OnEnable()
    {
        auth.StateChanged += AuthStateChanged;
    }

    private void OnDisable()
    {
        auth.StateChanged -= AuthStateChanged;
        userRefs.Child(auth.CurrentUser.UserId).ChildChanged -= OnMessageReceive;
    }

    private void AuthStateChanged(object sender, EventArgs e)
    {
        if (auth.CurrentUser != null)
            userRefs.Child(auth.CurrentUser.UserId).ChildChanged += OnMessageReceive;
    }

    private void OnMessageReceive(object sender, ChildChangedEventArgs e)
    {
        if (e.Snapshot.GetRawJsonValue() != null)
        {
            string message = e.Snapshot.GetRawJsonValue();
            string confirmedMessage = string.Empty;

            //Remove time values
            message = message.Split(' ')[0];

            foreach (char character in message)
            {
                if (character != '"')
                    confirmedMessage += character;
            }

            messageText.text = confirmedMessage;
            messagePopUp.SetActive(true);
        }
    }
}

public class Message
{
    public string message = string.Empty;
}
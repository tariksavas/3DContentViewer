using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;

public class JsonDownloader : MonoBehaviour
{
    private List<ModelProperties> allModels = new List<ModelProperties>();
    public List<ModelProperties> AllModels { get => allModels; }

    public event Action<string> jsonAttached;
    public event Action<string> error;

    private string webAddress = "https://raw.githubusercontent.com/tariksavas/ContentForEditor/main/DownloadList.txt";

    private void Start()
    {
        StartCoroutine(GetJson());
    }

    private IEnumerator GetJson()
    {
        var www = new UnityWebRequest(webAddress, UnityWebRequest.kHttpVerbGET);
        string path = Application.persistentDataPath + "/DownloadList.json";
        www.downloadHandler = new DownloadHandlerFile(path);

        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
            error?.Invoke(www.error);
        else
        {
            StreamReader reader = new StreamReader(path);
            string data_text = reader.ReadToEnd();
            allModels = JsonConvert.DeserializeObject<List<ModelProperties>>(data_text);
            jsonAttached?.Invoke(webAddress);
            reader.Close();
        }
    }
}

[Serializable]
public class ModelProperties
{
    public string assetName = string.Empty;
    public string downloadUrl = string.Empty;
}
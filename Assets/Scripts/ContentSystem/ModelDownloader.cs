using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Networking;

public class ModelDownloader : MonoBehaviour
{
    private LoadingController lc;
    private UnityWebRequest www = null;

    public static event Action<string> error;
    public static string currentModelURL = "";

    private void Awake()
    {
        lc = FindObjectOfType<LoadingController>(true);
    }

    public void GetBundleObject(string BundleFolder, UnityAction<GameObject> callback, Transform bundleParent)
    {
        //returns if download is already in progress
        if (www != null && www.downloadProgress < 1)
            return;

        StartCoroutine(GetDisplayBundleRoutine(BundleFolder, callback, bundleParent));
    }

    private IEnumerator GetDisplayBundleRoutine(string bundleURL, UnityAction<GameObject> callback, Transform bundleParent)
    {
        www = UnityWebRequestAssetBundle.GetAssetBundle(bundleURL);
        lc.StartProgress(www);

        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
            error?.Invoke(www.error);

        else
        {
            AssetBundle bundle = DownloadHandlerAssetBundle.GetContent(www);

            if (bundle != null)
            {
                string rootAssetPath = bundle.GetAllAssetNames()[0];
                GameObject arObject = Instantiate(bundle.LoadAsset(rootAssetPath) as GameObject, bundleParent);
                bundle.Unload(false);
                callback(arObject);

                currentModelURL = bundleURL;
            }

            else
                error?.Invoke("Not a valid asset bundle");
        }
    }
}
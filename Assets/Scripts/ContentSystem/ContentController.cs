using System;
using UnityEngine;

public class ContentController : MonoBehaviour
{
    public static event Action successful;
    private ModelDownloader modelDownloader;

    private void Awake()
    {
        modelDownloader = FindObjectOfType<ModelDownloader>();
    }

    private void OnEnable()
    {
        LoginController.loggedOut += DestroyAllChildren;
    }

    private void OnDisable()
    {
        LoginController.loggedOut -= DestroyAllChildren;
    }

    public void LoadContent(string downloadUrl)
    {
        DestroyAllChildren();
        modelDownloader.GetBundleObject(downloadUrl, OnContentLoaded, transform);
    }

    private void OnContentLoaded(GameObject content)
    {
        successful?.Invoke();

        SetPosAndRotContent(content.transform);

#if UNITY_EDITOR
        RefreshShaderModels(content);
#endif

    }

    private void SetPosAndRotContent(Transform _content)
    {
        Transform mainCam = Camera.main.transform;

        Vector3 tempPos = mainCam.position + (mainCam.forward * 2f);
        tempPos.y = mainCam.position.y - 1.5f;
        _content.position = tempPos;
    }

    private void RefreshShaderModels(GameObject _content)
    {
        //Handling pink error for materials in only editor
        var renderers = _content.GetComponentsInChildren<Renderer>();
        foreach (var renderer in renderers)
        {
            Shader shader = Shader.Find(renderer.material.shader.name);
            renderer.material.shader = shader;
        }
    }

    private void DestroyAllChildren()
    {
        foreach (Transform child in transform)
            Destroy(child.gameObject);
    }
}
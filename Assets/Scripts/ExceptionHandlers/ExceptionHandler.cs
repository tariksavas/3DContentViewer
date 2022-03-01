using System.Collections;
using TMPro;
using UnityEngine;

public class ExceptionHandler : MonoBehaviour
{
    [SerializeField]
    private float closeTime = 3f;

    private Coroutine coroutine;

    protected TextMeshProUGUI ownText;

    protected virtual void Awake()
    {
        ownText = GetComponent<TextMeshProUGUI>();
        ownText.enabled = false;
    }

    protected void SetEnable()
    {
        ownText.enabled = true;

        if (coroutine != null)
            StopCoroutine(coroutine);

        coroutine = StartCoroutine(CloseObject());
    }

    private IEnumerator CloseObject()
    {
        yield return new WaitForSeconds(closeTime);
        ownText.enabled = false;
    }
}
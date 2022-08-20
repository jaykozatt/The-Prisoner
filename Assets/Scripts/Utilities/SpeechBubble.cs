using System.Collections;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class SpeechBubble : MonoBehaviour 
{
    SpriteRenderer requestBubble;
    SpriteRenderer textContainer;
    TextMeshPro textMesh;
    Vector3 originalPos;

    [Min(0)] public float padding = .1f;

    Coroutine typer;

    private void Start() {
        originalPos = transform.position;
        requestBubble = transform.parent.Find("Request").GetComponent<SpriteRenderer>();
        textContainer = GetComponentsInChildren<SpriteRenderer>()[1];
        textMesh = GetComponentInChildren<TextMeshPro>();
        DOTween.Init();

        gameObject.SetActive(false);
    }

    private void LateUpdate() {
        transform.position =
            requestBubble.bounds.center + Vector3.up * (requestBubble.bounds.extents.y)
        ;

        textContainer.size = textMesh.bounds.size + padding * new Vector3(3,1,1);
    }

    public void Say(string message)
    {
        gameObject.SetActive(true);

        if (typer != null) StopCoroutine(typer);
        typer = StartCoroutine(Typer(message));
    }

    IEnumerator Typer(string message)
    {
        textMesh.text = "";
        transform.DOScale(Vector3.one,0.75f);

        textMesh.text = message;

        yield return new WaitForSeconds(4);

        transform.DOScale(Vector3.zero,0.5f)
        .OnComplete(()=>gameObject.SetActive(false));
        

    }
}
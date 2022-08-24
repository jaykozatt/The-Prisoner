using UnityEngine;
using DG.Tweening;

public class PlayerController : MonoBehaviour 
{
    struct InputAxis 
    {
        public string name {get; private set;}
        public float value { get => Input.GetAxis(name);}
        public bool started { get => Input.GetButtonDown(name);}
        public bool held { get => Input.GetButton(name);}
        public bool ended { get => Input.GetButtonUp(name);}
        public InputAxis(string name) 
        {
            this.name = name;
        }
    }

    InputAxis xInput = new InputAxis("Horizontal");
    InputAxis yInput = new InputAxis("Vertical");
    InputAxis proceedInput = new InputAxis("Proceed");

    Tween tween;

    private void Awake() 
    {
        DOTween.Init();
    }

    private void Update() 
    {
        if (yInput.started && yInput.value < 0)
        {
            if (tween.IsActive()) tween.Kill();
            tween = transform.DOLocalRotate(Vector3.up * 180, 1f).SetEase(Ease.OutQuad);
        }
        else if (xInput.started)
        {
            if (tween.IsActive()) tween.Kill();
            tween = transform.DOLocalRotate(Vector3.up * 90 * xInput.value, .5f).SetEase(Ease.OutQuad);
        }
        else if (!yInput.held && !xInput.held)
        {
            if (tween.IsActive()) tween.Kill();
            tween = transform.DOLocalRotate(Vector3.zero, .5f).SetEase(Ease.OutQuad);
        }

        if (Input.GetButtonDown("Proceed"))
        {
            Storyteller.Instance.Continue();
        }
    }
}
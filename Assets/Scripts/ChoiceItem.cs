using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Image))]
public class ChoiceItem : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler, IPointerExitHandler, IPointerDownHandler
{
    ChoiceBox _choiceBox;
    internal Image background;

    public void OnPointerDown(PointerEventData eventData)
    {
        _choiceBox.OnChoiceDown(this);
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        _choiceBox.OnChoiceSelected(this);
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        _choiceBox.OnChoiceEnter(this);
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        _choiceBox.OnChoiceExit(this);
    }


    private void Awake() {
        _choiceBox = GetComponentInParent<ChoiceBox>();
        background = GetComponent<Image>();   
        _choiceBox.Subscribe(this);     
    }
}
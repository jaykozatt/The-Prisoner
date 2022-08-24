using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Ink.Runtime;
using UnityEngine.EventSystems;

public class ChoiceBox : StaticInstance<ChoiceBox>
{
    GameObject _template;
    GameObject _instance;
    GameObject _parentContainer;

    [SerializeField] Color choiceIdle;
    [SerializeField] Color choiceHover;
    [SerializeField] Color choiceClicked;

    List<ChoiceItem> choiceList;

    public void OnChoiceEnter(ChoiceItem choice) 
    {
        choice.background.color = choiceHover;
    }
    public void OnChoiceSelected(ChoiceItem choice)
    {
        choice.background.color = choiceHover;

        int index = choice.transform.GetSiblingIndex();
        print($"Choice {index} selected.");
        Storyteller.Instance.MakeChoice(index-1);

        _parentContainer.SetActive(false);
    }
    public void OnChoiceDown(ChoiceItem choice)
    {
        print($"Choice {choice} down.");
        choice.background.color = choiceClicked;
    }
    public void OnChoiceExit(ChoiceItem choice)
    {
        choice.background.color = choiceIdle;
    }

    protected override void Awake() 
    {
        base.Awake();
        _template = transform.GetChild(0).gameObject;
        _template.SetActive(false);
        _parentContainer = transform.parent.parent.parent.gameObject;
        _parentContainer.SetActive(false);
    }    

    private void Start() {
    }

    public void Subscribe(ChoiceItem button) 
    {
        if (choiceList == null)
        {
            choiceList = new List<ChoiceItem>();
        }

        choiceList.Add(button);
    }   

    public void LoadChoices(List<Choice> list)
    {
        _parentContainer.SetActive(true);
        
        this.Clear();

        TextMeshProUGUI textMesh;
        foreach(Choice choice in list)
        {
            _instance = Instantiate(_template, transform);
            _instance.SetActive(true);

            textMesh = _instance.GetComponentInChildren<TextMeshProUGUI>();
            textMesh.text = choice.text;
        }
    }

    public void Add(Choice choice) 
    {
        _instance = Instantiate(_template, transform);
        _instance.SetActive(true);

        TextMeshProUGUI textMesh = _instance.GetComponentInChildren<TextMeshProUGUI>();
        textMesh.text = choice.text;
    }

    public void Clear()
    {
        foreach(Transform child in transform)
        {
            if (child.gameObject.activeInHierarchy) Destroy(child);
        }
    }
    
}
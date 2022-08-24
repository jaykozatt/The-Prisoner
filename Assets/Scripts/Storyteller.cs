using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using TMPro;

public class Storyteller : StaticInstance<Storyteller> 
{
    public TextMeshProUGUI dialogueText;
    public InkScript script;

    [Header("Settings")]
    [Min(.1f)] public float typingSpeed = 1f;

    public bool hasReadNote = false;
    private bool isTyping = false;
    private Coroutine typer;

    protected override void Awake() {
        base.Awake();
        script.Initialize();
    }    

    private void Start() 
    {
        dialogueText.text = "";
    }

    public void Continue()
    {
        if (hasReadNote)
        {
            GetNextLine();
        }
    }

    void GetNextLine()
    {
        if (typer != null) StopCoroutine(typer);

        string line;
        if (script.TryNextLine(out line))
            typer = StartCoroutine(Typer(line));
        else
            Clear();
    }

    void DisplayChoices() 
    {
        if (script.currentTags.Any<string>(tag => tag.Equals("Trust/Betray")))
        {
            // Enable Trust/Betray control panel
        }
        else
        {
            ChoiceBox.Instance.LoadChoices(script.choices);
        }

    }

    public void MakeChoice(int index) 
    {
        // Ink's index starts on 1, instead of 0
        script.Choose(index);
        Continue();
    }

    public void Clear()
    {
        dialogueText.text = "";
    }

    IEnumerator Typer(string message) 
    {
        dialogueText.text = "";

        foreach(char letter in message.ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(1/typingSpeed);
        }

        if (script.choiceAvailable)
            DisplayChoices();
    }

}
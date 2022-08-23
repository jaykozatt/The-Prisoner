using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueManager : StaticInstance<DialogueManager> 
{
    public TextMeshProUGUI dialogueBox;
    public InkScript script;


    protected override void Awake() {
        base.Awake();
        script.Initialize();
    }    

    private void Start() {
        dialogueBox.text = "";

        string nextLine;
        while (script.TryNextLine(out nextLine))
        {
            dialogueBox.text += nextLine;
            if (script.choiceAvailable)
            {
                ChoiceBox.Instance.LoadChoices(script.choices);   
            }
        }   
    }

    public void MakeChoice(int index) 
    {
        script.Choose(index);
    }

}
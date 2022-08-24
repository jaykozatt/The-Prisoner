using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ink.Runtime;

[System.Serializable]
public class InkScript
{
    public TextAsset inkAsset;
    Story _story;


    public bool choiceAvailable {
        get => _story.currentChoices.Count > 0;
    }
    public List<Choice> choices {
        get => _story.currentChoices;
    }
    public List<string> currentTags {
        get => _story.currentTags;
    }

    public void Initialize() {
        _story = new Story(inkAsset.text);
    }

    public bool TryNextLine(out string line)
    {
        if (_story.canContinue)
        {
            line = _story.Continue();
            return true;
        }
        else
        {
            line = "";
            return false;
        }
    }

    public void Choose(int index) 
    {
        _story.ChooseChoiceIndex(index);
    }

    public T Get<T>(string varName) => (T) _story.variablesState[varName];
    public void Set<T>(string varName, T value) => _story.variablesState[varName] = value;

    public void RegisterObserver(string varName, Ink.Runtime.Story.VariableObserver observer) 
    {
        _story.ObserveVariable(varName, observer);
    }


}

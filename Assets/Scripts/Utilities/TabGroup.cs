using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TabGroup : MonoBehaviour 
{
    List<TabElement> TabElements;
    public TextMeshProUGUI title;
    public Color tabIdle;
    public Color tabHover;
    public Color tabSelected;
    public TabElement selectedTab;
    public List<GameObject> objectsToSwap;

    public void Subscribe(TabElement button) 
    {
        if (TabElements == null)
        {
            TabElements = new List<TabElement>();
        }

        TabElements.Add(button);
    }   

    public void OnTabEnter(TabElement button)
    {
        ResetTabs();
        if (selectedTab == null || button != selectedTab)
            button.background.color = tabHover;
    }
    public void OnTabExit(TabElement button)
    {
        ResetTabs();
    }
    public void OnTabSelected(TabElement button)
    {
        title.text = button.gameObject.name;
        selectedTab = button;
        ResetTabs();
        button.background.color = tabSelected;
        int index = button.transform.GetSiblingIndex();
        for (int i=0; i<objectsToSwap.Count; i++)
        {
            if (i == index)
            {
                objectsToSwap[i].SetActive(true);
            }
            else
            {
                objectsToSwap[i].SetActive(false);
            }
        }
    }

    public void ResetTabs()
    {
        foreach(TabElement button in TabElements)
        {
            if (selectedTab != null && button == selectedTab) continue;
            button.background.color = tabIdle;
        }
    }
}
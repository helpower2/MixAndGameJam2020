using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = Unity.Mathematics.Random;

[AddComponentMenu("TabMenu/TabGroup")]
public class TabGroup : MonoBehaviour
{

    public Sprite tabIdle;
    public Sprite tabHover;
    public Sprite tabActive;

    //public List<GameObject> objectsToSwap =new List<GameObject>();
    
    private TabButton _selectedTab;
    public List<TabButton> _tabButtons = new List<TabButton>();

    private void Start()
    {
        _selectedTab = _tabButtons[0] ?? throw new Exception("there are no TabButtons!");
        ResetTabs();
        OnTabSelected(_selectedTab);
    }

    private void Update()
    {
        CheckInput();
    }

    private void CheckInput()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            SetActive(_selectedTab.index - 1);//down
        }
        
        if (Input.GetKeyDown(KeyCode.E)) 
                SetActive(_selectedTab.index + 1);//up
        
    }

    private void SetActive(int index)
    {
        if (_selectedTab != null)
            _selectedTab.Deselect();
        ResetTabs();
        
        if (index < 0)
            OnTabSelected(_tabButtons.Where(x => x.index == _tabButtons.Count -1).ToArray()[0]);
        else if (index >= _tabButtons.Count)
            OnTabSelected(_tabButtons.Where(x => x.index == 0).ToArray()[0]);
        else
            OnTabSelected(_tabButtons.Where(x => x.index == index).ToArray()[0]);
    }

    public void AddTab(TabButton tabButton)
    {
        _tabButtons.Add(tabButton);
    }

    public void OnTabEnter(TabButton button)
    {
        ResetTabs();
    }
    public void OnTabExit(TabButton button)
    {
        ResetTabs();
    }
    public void OnTabSelected(TabButton button)
    {
        _selectedTab =  button;
        ResetTabs();
        
        _tabButtons.ForEach((o) => o.objectToSwap.SetActive(o == button));
    }

    public void ResetTabs()
    {
        _tabButtons.Except(new TabButton[1]{_selectedTab}).ToList().ForEach(x=>x.Deselect());
        _selectedTab.Select();
    }
}

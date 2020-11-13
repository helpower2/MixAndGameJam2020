using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[AddComponentMenu("TabMenu/TabButton")]
[RequireComponent(typeof(Image))]
public class TabButton : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler ,IPointerExitHandler
{
    [Header("General")]
    public bool updateGraphics = true;
    public TabGroup tabGroup;
    public Image background;

    public GameObject objectToSwap;

    [Header("events")]
    public  UnityEvent onTabSelected = new UnityEvent();
    public  UnityEvent onTabHover = new UnityEvent();
    public  UnityEvent onTabDeselected = new UnityEvent();

    [HideInInspector]
    public int index;
    
    
    private void Reset()
    {
        tabGroup = transform.GetComponentInParent<TabGroup>();
        background = GetComponent<Image>();
    }

    // Start is called before the first frame update
    public void Awake()
    {
        tabGroup.AddTab(this);
        index = transform.GetSiblingIndex();
    }
    

    public void OnPointerEnter(PointerEventData eventData)
    {
       tabGroup.OnTabEnter(this);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        tabGroup.OnTabSelected(this);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        tabGroup.OnTabExit(this);
    }

    public void Select()
    {
        if (updateGraphics)
            background.sprite = tabGroup.tabActive;
        
        onTabSelected.Invoke();
    }

    public void Hover()
    {
        if (updateGraphics)
            background.sprite = tabGroup.tabHover;
        onTabHover.Invoke();
    }

    public void Deselect()
    {
        if (updateGraphics)
            background.sprite = tabGroup.tabIdle;
        onTabDeselected.Invoke();
    }
}

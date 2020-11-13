using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
#if UNITY_EDITOR
    using UnityEditor;
#endif

[ExecuteInEditMode()]
public class ProgressBar : MonoBehaviour
{
    
#if UNITY_EDITOR
    [MenuItem("GameObject/UI/Linear Progress Bar")]
    public static void AddLinearProgressBar()
    {
        GameObject gameObject = Instantiate(Resources.Load<GameObject>("UI/Linear Progress Bar"), Selection.activeGameObject.transform);
        //gameObject.transform.SetParent(Selection.activeGameObject.transform);
    }
    
    [MenuItem("GameObject/UI/Radial Progress bar")]
    public static void AddRadialProgressBar()
    {
        GameObject gameObject = Instantiate(Resources.Load<GameObject>("UI/Radial Progress bar"), Selection.activeGameObject.transform);
        //gameObject.transform.SetParent(Selection.activeGameObject.transform);
    }
#endif

    public float Minimum
    {
        get => minimum;
        set
        {
            minimum = value;
            SetCurrentFill();
        }
    }
    public float Maximum
    {
        get => maximum;
        set
        {
            maximum = value;
            SetCurrentFill();
        }
    }
    public float Current
    {
        get => current;
        set
        {
            current = value;
            SetCurrentFill();
        }
    }

    public Color FillColor
    {
        get => fillColor;
        set
        {
            fillColor = value;
            SetColors();
        } }

    public Color BaseColor
    {
        get => baseColor;
        set
        {
            baseColor = value;
            SetColors();
        } 
    }
    
    [SerializeField]
    private float minimum = 0;
    
    [SerializeField]
    private float maximum = 100;
    
    [SerializeField]
    private float current;

    [SerializeField]
    private Color fillColor;
   
    [SerializeField]
    private Color baseColor;
    
    
    

    [SerializeField]
    private Image empty;
    
    [SerializeField]
    private Image mask;
    
    [SerializeField]
    private Image fill;

    public void Reset()
    {
        fill = transform.Find("Fill").GetComponent<Image>();
        mask = transform.Find("Mask").GetComponent<Image>();
        empty = transform.GetComponent<Image>();

        fillColor= Color.yellow;
        baseColor= Color.white;
        SetColors();
    }
    
   
    
    private void OnValidate()
    {
        SetCurrentFill();
        SetColors();
    }

    public void SetCurrent(float amount)
    {
        current = amount;
    }

    public void SetCurrent(int amount)
    {
        current = amount;
    }
    
    
    private void SetCurrentFill()
    {
        var currentOffset = current - minimum;
        var maximumOffset = maximum - minimum;
        var fillAmount = currentOffset / maximumOffset;
        mask.fillAmount = fillAmount;
    }

    private void SetColors()
    {
        fill.color = fillColor;
        empty.color = baseColor;
    }
    
}

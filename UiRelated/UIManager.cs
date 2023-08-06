using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{

    [SerializeField] private ActionButtonScript[] actionButtons;

    [SerializeField] GameObject tooltip;

    private KeyCode action1, action2, action3, action4, action5;

    private TMPro.TextMeshProUGUI tooltipText;



    private static UIManager instance;


    public static UIManager MyInstance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<UIManager>();
            }
            return instance;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        tooltipText = tooltip.GetComponentInChildren<TMPro.TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateStackSize(IClickable clickable)
    {
        if(clickable.MyCount >1)
        {
            clickable.MyStackText.text = clickable.MyCount.ToString();
            clickable.MyStackText.color = Color.white;
            clickable.MyIcon.color = Color.white;
        }
        else
        {
            clickable.MyStackText.color = new Color(0, 0, 0, 0);
            clickable.MyIcon.color = Color.white;
        }
        if(clickable.MyCount == 0)
        {
            clickable.MyIcon.color = new Color(0, 0, 0, 0);
            clickable.MyStackText.color = new Color(0, 0, 0, 0);
        }
    }
    public void ClickActionButton(string buttonName)
    {
        Array.Find(actionButtons, x => x.gameObject.name == buttonName).MyButton.onClick.Invoke(); 
    }

    public void ShowTooltip(IDescribable description)
    {
        tooltip.SetActive(true);
        tooltipText.text = description.GetDescription();
    }

    public void HideTooltip()
    {
        tooltip.SetActive(false);
    }
   
    public void RefreshTooltip(IDescribable description)
    {
        tooltipText.text = description.GetDescription();
    }

    public void ClearStackCount(IClickable clickable)
    {
        clickable.MyStackText.color = new Color(0, 0, 0, 0);
        clickable.MyIcon.color = Color.white;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using StarterAssets;

public class LootWindow : MonoBehaviour
{
  

    private static LootWindow instance;

    [SerializeField] private LootButton[] lootButtons;

    private List<List<Item>> pages = new List<List<Item>>();

    private List<Item> droppedLoot = new List<Item>();

    private int pageIndex= 0;

    
    [SerializeField] private CanvasGroup canvasGroup;

    [SerializeField] private TextMeshProUGUI pageNumber;

    [SerializeField] private GameObject nextBtn, previousBtn;

    [SerializeField] private Item[] items;

    public List<List<Item>> MyPages { get => pages; }
    public bool IsOpen
    {
        get { return canvasGroup.alpha > 0; }
    }

    public static LootWindow MyInstance
    {
        get
        {
            if (instance == null)
            {
                instance = GameObject.FindObjectOfType<LootWindow>();
                instance = GameObject.FindObjectOfType<LootWindow>();
            }
            return instance;
        }
    }

    

    public void CreatePages(List<Item> items)
    {
        if (!IsOpen)
        {
            List<Item> page = new List<Item>();

            droppedLoot = items;

            for (int i = 0; i < items.Count; i++)
            {
                page.Add(items[i]);

                if (page.Count == 4 || i == items.Count - 1)
                {
                    MyPages.Add(page);
                    page = new List<Item>();
                }
            }
            AddLoot();
            Open();
        }
       
    }

    private void AddLoot()
    {
        if(MyPages.Count > 0)
        {
            pageNumber.text = pageIndex + 1 + "/" + MyPages.Count;

            previousBtn.SetActive(pageIndex > 0);

            nextBtn.SetActive(MyPages.Count > 1 && pageIndex < MyPages.Count - 1);

            for (int i = 0; i < MyPages[pageIndex].Count; i++)
            {
                if(MyPages[pageIndex][i] != null)
                {
                    lootButtons[i].MyIcon.sprite = MyPages[pageIndex][i].MyIcon;

                    lootButtons[i].MyLoot = MyPages[pageIndex][i];

                    lootButtons[i].gameObject.SetActive(true);

                    string title = string.Format("<color={0}>{1}</color>", QualityColor.MyColors[MyPages[pageIndex][i].MyQuality], MyPages[pageIndex][i].MyTitle);

                    lootButtons[i].MyTitle.text = title;
                }
                
            }
        }
    
    }

    public void NextPage()
    {
        if(pageIndex < MyPages.Count -1)
        {
            pageIndex++;

            ClearButtons();

            AddLoot();
        }
    }

    public void PreviousPage()
    {
        if(pageIndex > 0)
        {
            pageIndex--;

            ClearButtons();

            AddLoot();
        }
    }

    public void ClearButtons()
    {
        foreach (LootButton btn in lootButtons)
        {
            btn.gameObject.SetActive(false);
        }
    }

    public void TakeLoot(Item loot)
    {
        MyPages[pageIndex].Remove(loot);

        droppedLoot.Remove(loot);

        if (MyPages[pageIndex].Count == 0)
        {
            MyPages.Remove(MyPages[pageIndex]);

            if (pageIndex == MyPages.Count && pageIndex > 0)
            {
                pageIndex--;
            }

            AddLoot();
        }
    }

    private void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
    }
    public void Open()
    {
        canvasGroup.alpha = 1;
        canvasGroup.blocksRaycasts = true;
        InputManager.MyInstance.lootOpen = true;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

    }

    public void Close()
    {
        MyPages.Clear();
        canvasGroup.alpha = 0;
        canvasGroup.blocksRaycasts = false;
        ClearButtons();
        InputManager.MyInstance.lootOpen = false;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    

 

}

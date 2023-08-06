
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ActionButtonScript : MonoBehaviour, IPointerClickHandler, IClickable
{

    public IUsable MyUseable { get; set; }

    [SerializeField] private TextMeshProUGUI stackSize;

    private Stack<IUsable> useables = new Stack<IUsable>();

    private int count;

    public Button MyButton { get; private set; }

    [SerializeField] Image icon;
    public Image MyIcon
    {
        get
        {
            return icon;
        }
        set
        {
            icon = value;
        }
    }

    public int MyCount
    {
        get
        {
            return count;
        }
    }

    public TextMeshProUGUI MyStackText
    {
        get { return stackSize; }
    }

    public Stack<IUsable> Useables
    {
        get
        {
            return useables;
        }
        set
        {
            if(value.Count > 0)
            {
                MyUseable = value.Peek();
            }
            else
            {
                MyUseable = null;
            }
           
                useables = value;
          
            
        }
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        if(eventData.button == PointerEventData.InputButton.Left)
        {
            if(HandScript.MyInstance.MyMoveable != null && HandScript.MyInstance.MyMoveable is IUsable)
            {
                SetUseable(HandScript.MyInstance.MyMoveable as IUsable);
            }
        }
    }

    public void OnClick()
    {
        if (HandScript.MyInstance.MyMoveable == null)
        {
            if (MyUseable != null)
            {

                MyUseable.Use();
                

            }
            else if(Useables != null && Useables.Count > 0)
            {
                Useables.Peek().Use();
            }
           

        }
       
    }

    public void SetUseable(IUsable useable)
    {
        if(useable is Item)
        {
            Useables = InventoryScript.MyInstance.GetUsables(useable);
            count = Useables.Count;
            InventoryScript.MyInstance.FromSlot.MyIcon.color = Color.white;
            InventoryScript.MyInstance.FromSlot = null;
        }
        else
        {
            Useables.Clear();
            this.MyUseable = useable;
        }
        count = Useables.Count;

        UpdateVisual();

        UIManager.MyInstance.RefreshTooltip(MyUseable as IDescribable);
    }

    public void UpdateVisual()
    {
        MyIcon.sprite = HandScript.MyInstance.Put().MyIcon;
        MyIcon.color = Color.white;

        if(count > 1)
        {
            UIManager.MyInstance.UpdateStackSize(this);
        }
        else if(MyUseable is Armor)
        {
            UIManager.MyInstance.ClearStackCount(this);
        }
    }

    public void UpdateItemCount(Item item)
    {
        if(item is IUsable && Useables.Count > 0)
        {
            if(Useables.Peek().GetType() == item.GetType())
            {
                Useables = InventoryScript.MyInstance.GetUsables(item as IUsable);

                count = Useables.Count;

                UIManager.MyInstance.UpdateStackSize(this);
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        MyButton = GetComponent<Button>();
        MyButton.onClick.AddListener(OnClick);
        InventoryScript.MyInstance.itemCountChangedEvent += new ItemCountChanged(UpdateItemCount);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

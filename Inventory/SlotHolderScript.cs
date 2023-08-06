using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlotHolderScript : MonoBehaviour
{
    [SerializeField] private GameObject slotPrefab;
    [SerializeField] private int _slotCount = 126;
   
    private List<SlotScript> slots = new List<SlotScript>();

    public List<SlotScript> MySlots { get => slots; set => slots = value; }

    private void Awake()
    {
        AddSlots(126);
    }
    public void AddSlots(int slotCount)
    {
        slotCount = _slotCount;

        for (int i = 0; i < slotCount; i++)
        {
            SlotScript slot = Instantiate(slotPrefab, transform).GetComponent<SlotScript>();
            slot.MySlotholder = this;
            slots.Add(slot);
        }
    }

    public bool AddItem(Item item)
    {
        foreach (SlotScript slot in slots)
        {
            if(slot.IsEmpty)
            {
                slot.AddItem(item);
                return true;
            }
        }
        return false;
    }
}

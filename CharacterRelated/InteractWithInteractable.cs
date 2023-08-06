using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractWithInteractable : MonoBehaviour
{
    public bool lootBagEmpty = false;
    
    [SerializeField] private float interactionDistance = 3f;
    private RaycastHit hitInfo;
    [SerializeField] private NPC currentInteractable;
    [SerializeField] private Enemy currentEnemy;

    private Vendor currentVendor = null;

    private static InteractWithInteractable instance;
    public static InteractWithInteractable MyInstance
    {
        get
        {
            if (instance == null)
            {
                instance = GameObject.FindObjectOfType<InteractWithInteractable>();
                instance = GameObject.FindObjectOfType<InteractWithInteractable>();
            }
            return instance;
        }
    }

    public Enemy MyCurrentEnemy { get => currentEnemy; }
    public NPC MyCurrentInteractable { get => currentInteractable; }
    public Vendor MyCurrentVendor { get => currentVendor; }

    private void Update()
    {
        if(currentInteractable is Vendor)
        {
            currentVendor = currentInteractable.GetComponent<Vendor>();
        }
        
        SetInteractable();
        SetEnemy();

        if (lootBagEmpty)
        {
            currentInteractable.StopInteract();
        }

    }

    public void SetInteractable()
    {
        if (Physics.Raycast(transform.position, transform.forward, out hitInfo, interactionDistance))
        {
            NPC interactable = hitInfo.collider.GetComponent<NPC>();
            
            
            if (interactable != null)
            {
                
                if (interactable != currentInteractable)
                {
                    currentInteractable = interactable;
                   
                    //currentInteractable.ShowInteractPrompt();

                }

              
                if (Input.GetKeyDown(KeyCode.F))
                {
                    currentInteractable.Interact();
                   
                }
              
            }
            else
            {
                
                currentInteractable = null;
            }
        }
        else
        {
            
            currentInteractable = null;
        }
    }

    public void SetEnemy()
    {
        if (Physics.Raycast(transform.position, transform.forward, out hitInfo, interactionDistance))
        {
            Enemy enemy = hitInfo.collider.GetComponent<Enemy>();


            if (enemy != null)
            {

                if (enemy != currentEnemy)
                {
                    currentEnemy = enemy;

                }


            }
            else
            {

                currentEnemy = null;
            }
        }
        else
        {

            currentEnemy = null;
        }
    }
    
    
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Questlog : MonoBehaviour
{
    [SerializeField] private GameObject questPrefab;

    [SerializeField] private Transform questParent;

    private Quest selected;

    private QuestScript selecteQuestScript;

    private static Questlog instance;

    public static Questlog MyInstance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<Questlog>();
            }
            return instance;
        }


    }
    public void AcceptQuest(Quest quest)
    {

        foreach (CollectObjective o in quest.MyCollectObjectives)
        {
            InventoryScript.MyInstance.itemCountChangedEvent += new ItemCountChanged(o.UpdateItemCount);
           

        }
        foreach (KillObjective o in quest.MyKillObjectives)
        {
            PlayerManager.instance.killConfirmedEvent += new KillConfirmed(o.UpdateKillCount);


        }

        GameObject go = Instantiate(questPrefab, questParent);

            QuestScript qs = go.GetComponent<QuestScript>();
            quest.MyQuestScript = qs;
            qs.MyQuest = quest;
            
            
            selected = quest;
            selecteQuestScript = qs;

           foreach(CollectObjective o in selected.MyCollectObjectives)
             {
                o.UpdateItemCount();
             }
            ShowQuestInformation();
            CheckCompletion();
        
    }
        
    private void ShowQuestInformation()
    {
        string objective = "\nObjectives\n";

        foreach (Objective obj in selected.MyCollectObjectives)
        {
            objective += obj.MyType + ": " + obj.MyCurrentAmount + "/" + obj.MyAmount + "\n";
        }

        selected.MyQuestScript.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = selected.MyTitle + "\n" + selected.MyDescription + "\n" + objective;
    }

    public void UpdateSelected()
    {
        ShowQuestInformation();
    }

    public void CheckCompletion()
    {
        selecteQuestScript.IsComplete();
    }

    public void CompleteQuest()
    {


        foreach (CollectObjective o in selected.MyCollectObjectives)
        {
            InventoryScript.MyInstance.itemCountChangedEvent -= new ItemCountChanged(o.UpdateItemCount);


        }
        foreach (KillObjective o in selected.MyKillObjectives)
        {
            PlayerManager.instance.killConfirmedEvent -= new KillConfirmed(o.UpdateKillCount);


        }
        //if (selected.IsComplete)
        //{
        //    selected = null;
        //}
    }
}

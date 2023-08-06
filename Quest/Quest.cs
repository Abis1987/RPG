using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Quest
 
{
    public bool alreadyGived = false;

    [SerializeField] private string title;

    [SerializeField] private string description;

   

    public QuestScript MyQuestScript { get; set; }

    public string MyTitle { get => title; set => title = value; }
    public string MyDescription { get => description; set => description = value; }

    [SerializeField] private CollectObjective[] collectObjectives;
    [SerializeField] private KillObjective[] killObjectives;
    public CollectObjective[] MyCollectObjectives { get => collectObjectives; }

    public KillObjective[] MyKillObjectives { get => killObjectives; }
    public bool IsComplete
    {
        get
        {
            foreach (Objective o in collectObjectives)
            {
                if (!o.IsComplete)
                {
                    return false;
                }
            }

            foreach (Objective o in killObjectives)
            {
                if (!o.IsComplete)
                {
                    return false;
                }
            }
            return true;
        }
    }

   
}

[System.Serializable]
public abstract class Objective
{
    [SerializeField] private int amount;

    private int currentAmount;

    [SerializeField] private string type;

    public int MyAmount { get => amount; }
    public int MyCurrentAmount { get => currentAmount; set => currentAmount = value; }
    public string MyType { get => type; }

    public bool IsComplete
    {
        get
        {
            return MyCurrentAmount >= MyAmount;
        }
    }
}
[System.Serializable]
public class CollectObjective : Objective
{
    
    public void UpdateItemCount(Item item) 
    {
        if(MyType.ToLower() == item.MyTitle.ToLower())
        {
            MyCurrentAmount = InventoryScript.MyInstance.GetItemCount(item.MyTitle);

            Questlog.MyInstance.UpdateSelected();
            Questlog.MyInstance.CheckCompletion();
        }
        
    }

    public void UpdateItemCount()
    {

        MyCurrentAmount = InventoryScript.MyInstance.GetItemCount(MyType);

        Questlog.MyInstance.UpdateSelected();
        Questlog.MyInstance.CheckCompletion();
    }


}

[System.Serializable]
public class KillObjective : Objective
{

    public void UpdateKillCount(Enemy enemy)
    {
        if(MyType == enemy.MyType)
        {
            MyCurrentAmount++;

            Questlog.MyInstance.UpdateSelected();
            Questlog.MyInstance.CheckCompletion();
        }
    }
}
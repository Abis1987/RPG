using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestScript : MonoBehaviour
{
    public Quest MyQuest { get; set; }
    public bool MarkedComplete { get => markedComplete; }

    private bool markedComplete = false;

    public void IsComplete()
    {
        if (MyQuest.IsComplete && !markedComplete)
        {
            markedComplete = true;
            GetComponentInChildren<TMPro.TextMeshProUGUI>().text = null;
            Player.MyInstance.GetComponent<QuestGiver>().GiveQuest();

        }
       
    }
    public void CompleteQuest()
    {
        if (MyQuest.IsComplete)
        {

        }
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestGiver : MonoBehaviour
{

    [SerializeField] private Quest[] quests;

    [SerializeField] private Questlog tmpLog;

    public Quest[] MyQuests { get => quests; }

    public void GiveQuest()
    {
        for (int i = 0; i < quests.Length; i++)
        {
         
            if (!quests[i].alreadyGived)
            {
                tmpLog.AcceptQuest(quests[i]);
                quests[i].alreadyGived = true;
                break;
            }
           
                
            
        }

    }

   

    private void Start()
    {
        GiveQuest();
    }
}

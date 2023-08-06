using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void KillConfirmed(Enemy enemy);
public class PlayerManager : MonoBehaviour
{
    public event KillConfirmed killConfirmedEvent;
    #region Singleton
    public static PlayerManager instance;

    private void Awake()
    {
        instance = this;
    }

    #endregion

    public GameObject player;

    public void OnKillConfirmed(Enemy enemy)
    {
        if(killConfirmedEvent != null)
        {
            killConfirmedEvent(enemy);
        }
    }
}

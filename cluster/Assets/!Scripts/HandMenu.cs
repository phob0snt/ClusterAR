using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandMenu : MonoBehaviour
{
    public void ExitGamemode()
    {
        switch (GameManager.CurrentGamemode)
        {
            case (DNCManager):
                DNCManager.Instance.EndSession();
                break;
            case (BangerManager):
                BangerManager.Instance.EndSession();
                break;
        }
        
    }
}

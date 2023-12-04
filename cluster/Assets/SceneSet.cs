
using Microsoft.MixedReality.Toolkit.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSet : MonoBehaviour
{
    private void Awake()
    {
        Transform playerPos = GameManager.Instance.playerPos;
        transform.position = new Vector3(playerPos.position.x, playerPos.position.y + 0.5f, playerPos.position.z) + playerPos.transform.forward * 1.2f;
        transform.rotation = Quaternion.LookRotation(transform.position - playerPos.position);
        transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);
    }
    public void SetScene()
    {
        GetComponent<BoxCollider>().enabled = false;
        GetComponent<MeshRenderer>().enabled = false;
        switch (GameManager.CurrentGamemode)
        {
            case DNCManager:
                DNCManager.Instance.StartSession();
                break;
            case BangerManager:
                BangerManager.Instance.StartSession();
                break;
            case DeathRoadManager:
                DeathRoadManager.Instance.StartSession();
                break;
            case WaverManager:
                WaverManager.Instance.StartSession();
                break;
        }
    }

    public void OnDrop(ManipulationEventData _)
    {
        transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);
    }
}

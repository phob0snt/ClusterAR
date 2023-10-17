using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProtectorState : MonoBehaviour
{
    public bool isProtected;

    private void Update()
    {
        RaycastHit[] hits;
        Debug.DrawRay(transform.position, transform.right);
        hits = Physics.RaycastAll(transform.position, transform.right, 0.2f);
        try
        {
            foreach (var hit in hits)
            {
                if (hit.transform.gameObject.GetComponent<Protector>() != null)
                {
                    isProtected = true;
                }
                    
            }    
        }
        catch { }
    }
}

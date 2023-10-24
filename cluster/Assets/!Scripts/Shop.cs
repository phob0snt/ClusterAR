using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Shop : MonoBehaviour
{
    public abstract void ToggleItems(string item);
    public abstract void GoBack();
}

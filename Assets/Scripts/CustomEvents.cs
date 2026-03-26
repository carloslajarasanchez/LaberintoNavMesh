using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CustomEvents : MonoBehaviour
{
    public static CustomEvents Instance;
    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
    public UnityEvent OnKeyTaken = new UnityEvent();
    public UnityEvent OnFinishArrived = new UnityEvent();
}

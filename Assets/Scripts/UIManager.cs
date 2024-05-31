using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
public class UIManager : MonoBehaviour
{
    [SerializeField]
    private Button getItemButton;

    private void Start()
    {
        getItemButton.onClick.AddListener(GetItems);
    }

    private void GetItems() 
    {      
        EventService.Instance.OnGetItemEvent.InvokeEvent();
    }

}

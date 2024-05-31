using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameService : GenericMonoSingleton<GameService>
{
    [SerializeField]
    private UIManager uiManager;
    [SerializeField]
    private ItemDisplay itemDisplay;
    [SerializeField]
    private ItemDiscriptionDisplay itemDiscriptionDisplay;
    [SerializeField]
    private ItemManager itemManager;


    public UIManager GetUIManager() => uiManager;
    public ItemDisplay GetItemDisplay() => itemDisplay;
    public ItemDiscriptionDisplay GetItemDiscDisplay() => itemDiscriptionDisplay;
    public ItemManager GetItemManager() => itemManager;
}

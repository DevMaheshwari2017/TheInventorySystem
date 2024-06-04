using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameService : GenericMonoSingleton<GameService>
{
    [SerializeField]
    private UIManager uiManager;
    [SerializeField]
    private ItemDiscriptionDisplay itemDiscriptionDisplay;


    public UIManager GetUIManager() => uiManager;
    public ItemDiscriptionDisplay GetItemDiscDisplay() => itemDiscriptionDisplay;
}

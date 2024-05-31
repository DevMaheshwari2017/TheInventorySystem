using UnityEngine;

[CreateAssetMenu(fileName = "New sprite", menuName = "ScriptableObject/Items")]
public class SO : ScriptableObject
{
    public Sprite img;
    public float weight;
    public string itemDiscription;
    public Rarity rarity;
    public Itemtypes itemtypes;
    public int buyCost;
    public int sellCost;
    public string itemName;
}

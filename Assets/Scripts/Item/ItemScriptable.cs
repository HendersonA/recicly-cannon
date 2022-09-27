using UnityEngine;

[CreateAssetMenu(fileName = "ItemScriptable", menuName = "Scriptable/Item", order = 0)]
public class ItemScriptable : ScriptableObject
{
    public int softCoins;
    public int itemCollectedCount;
    public int itemCount;
    public GameObject itemObject;
    public GameObject itemBullet;
    public MaterialEnum materialEnum;
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New Item Database", menuName = "Inventory System/Items/Database")]
public class ItemDataBaseObject : ScriptableObject, ISerializationCallbackReceiver
{
    public ItemObject[] ItemObjects;
    //public Dictionary<int, ItemObject> GetItem = new Dictionary<int, ItemObject>();
    [ContextMenu("Update ID's")]
    public void UpdateID()
    {
        for(int i = 0; i < ItemObjects.Length; i++)
        {
            if (ItemObjects[i].data.Id != i)
            {
                ItemObjects[i].data.Id = i;
            }
        }
    }
    public void OnAfterDeserialize()
    {
        UpdateID();
    }

    public void OnBeforeSerialize()
    {
        //GetItem = new Dictionary<int, ItemObject>();
    }
}

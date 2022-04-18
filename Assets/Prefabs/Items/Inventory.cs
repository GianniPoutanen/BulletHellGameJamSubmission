using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public List<GameObject> inventoryUIObjects = new List<GameObject>();
    public List<Item> itemsCollected = new List<Item>();

    private void Start()
    {
        UpdateUI();
    }

    public void ClearUI()
    {
        foreach (var uiObject in inventoryUIObjects)
        {
            uiObject.SetActive(false);
        }
    }

    public void UpdateUI()
    {
        ClearUI();
        int index = 0;
        foreach (var item in itemsCollected)
        {
            inventoryUIObjects[index].SetActive(true);
            inventoryUIObjects[index].GetComponent<Image>().sprite = item.sprite;
            index++;
        }
    }

    public static void AddToInventory(Item item)
    {
        Inventory inventory = GameObject.FindGameObjectWithTag("Inventory").GetComponent<Inventory>();
        if (inventory.itemsCollected.Contains(item))
        {
            inventory.itemsCollected[inventory.itemsCollected.IndexOf(item)].amount += item.amount;
        }
        else
        {
            inventory.itemsCollected.Add(item);
        }
        inventory.UpdateUI();
    }
}

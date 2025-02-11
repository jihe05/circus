using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Item : MonoBehaviour
{
    public Image itemImage;
    public Text itemStack;
   public ItemData itemData;

    public void SetItemData(ItemData _itemData)
    {
        itemData = _itemData;
        itemImage.sprite = itemData.icon;
        itemStack.text = itemData.maxStack.ToString();
    }
}


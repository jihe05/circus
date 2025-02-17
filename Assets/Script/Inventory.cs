using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public static Inventory Instance;

    public Item item;

    public GameObject group;

    private List<Toggle> itemToggles = new List<Toggle>();


    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        
    }

    public void AddItem(ItemData itemData)
    {
        // ������ ������ ���� �� �θ� ����
        GameObject newItem = Instantiate(item.gameObject, group.transform);
        newItem.transform.localPosition = Vector3.zero;

        // Item ������Ʈ�� ������ itemData ����
        Item itemComponent = newItem.GetComponent<Item>();
        ToggleEventAdd(itemData, newItem, itemComponent);
    }
        
        
    private void ToggleEventAdd(ItemData itemData, GameObject newItem, Item itemComponent)
    {
        if (itemComponent != null)
        {
            itemComponent.SetItemData(itemData);

            // ���� �߰��� �����ۿ� ���� Toggle�� ����Ʈ�� �߰�
            Toggle itemToggle = newItem.GetComponent<Toggle>();
            if (itemToggle != null)
            {
                itemToggles.Add(itemToggle); // ����Ʈ�� �߰�

                // �̺�Ʈ ������ ���
                itemToggle.onValueChanged.AddListener((isOn) =>
                {
                    if (isOn)
                    {
                        foreach (var toggle in itemToggles)
                        {
                            if (toggle != itemToggle)
                            {
                                toggle.isOn = false;
                            }
                        }

                    }
                    else
                    {

                    }
                });
            }
            else
            {
                Debug.Log("Toggle ������Ʈ�� �����ϴ�.");
            }
        }
    }

    public void RemoveItem(GameObject scanObject)
    {
        if (itemToggles == null || scanObject == null)
            return;

        ObjData objData = scanObject.GetComponent<ObjData>();

        // itemToggles ����Ʈ���� �ش� �������� ����
        for (int i = 0; i < itemToggles.Count; i++)
        {
            if (objData.itemData == itemToggles[i].GetComponent<Item>().itemData) // ������ ������ ��
            { 
                if (objData.itemData.isStackable)
                {
                    Debug.Log(objData.itemData.maxStack);
                    objData.itemData.maxStack--;
                    if (objData.itemData.maxStack == 0)
                    {
                        scanObject.SetActive(false);
                        Destroy(itemToggles[i].gameObject);

                        // ����Ʈ���� ����
                        itemToggles.RemoveAt(i);
                        break;
                    }
                }
                if(!objData.itemData.isStackable)
                {

                    scanObject.SetActive(false);
                    Destroy(itemToggles[i].gameObject);

                    // ����Ʈ���� ����
                    itemToggles.RemoveAt(i);
                    break;
                }
            }
         
        }
    }
}

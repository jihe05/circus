using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public static Inventory Instance;

    public Item item;

    public GameObject group;

   public List<Toggle> itemToggles = new List<Toggle>();

    Item itemComponent;

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
        itemComponent = newItem.GetComponent<Item>();
        ToggleEventAdd(itemData, newItem);
    }

    private void ToggleEventAdd(ItemData itemData, GameObject newItem)
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
            if (objData.itemData == itemToggles[i].GetComponent<Item>().itemData)
            {
                if (objData.itemData.isStackable)
                {
                    if (objData.itemData.maxStack > 0)
                    {
                        objData.itemData.maxStack--;
                        itemComponent.SetItemData(objData.itemData);
                    }

                    if (objData.itemData.maxStack == 0)
                    {
                        Destroy(itemToggles[i].gameObject);

                        // ����Ʈ���� ����
                        itemToggles.RemoveAt(i);
                        break;
                    }
                    scanObject.SetActive(false);

                }
                else // ���� �Ұ����� ������
                {
                    Destroy(itemToggles[i].gameObject);
                    // ����Ʈ���� ����
                    itemToggles.RemoveAt(i);
                    scanObject.SetActive(false);

                    break;
                }
              

            }
        }
    }

    //�������� �����m True ������ Fales��ȯ �޼���
    public bool HasItem(string itemName)
    {
        foreach (var toggle in itemToggles)
        {
            Item item = toggle.GetComponent<Item>();
            if (item != null && item.itemData.itemName == itemName)
            {
                return true;
            }
        }
        return false;
    }
}


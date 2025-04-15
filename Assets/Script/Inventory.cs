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
        // 아이템 프리팹 생성 후 부모 설정
        GameObject newItem = Instantiate(item.gameObject, group.transform);
        newItem.transform.localPosition = Vector3.zero;

        // Item 컴포넌트를 가져와 itemData 적용
        itemComponent = newItem.GetComponent<Item>();
        ToggleEventAdd(itemData, newItem);
    }

    private void ToggleEventAdd(ItemData itemData, GameObject newItem)
    {
        if (itemComponent != null)
        {
            itemComponent.SetItemData(itemData);

            // 새로 추가된 아이템에 대한 Toggle을 리스트에 추가
            Toggle itemToggle = newItem.GetComponent<Toggle>();
            if (itemToggle != null)
            {
                itemToggles.Add(itemToggle); // 리스트에 추가

                // 이벤트 리스너 등록
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
                Debug.Log("Toggle 컴포넌트가 없습니다.");
            }
        }
    }
    

    public void RemoveItem(GameObject scanObject)
    {
        if (itemToggles == null || scanObject == null)
            return;

        ObjData objData = scanObject.GetComponent<ObjData>();

        // itemToggles 리스트에서 해당 아이템을 제거
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

                        // 리스트에서 삭제
                        itemToggles.RemoveAt(i);
                        break;
                    }
                    scanObject.SetActive(false);

                }
                else // 스택 불가능한 아이템
                {
                    Destroy(itemToggles[i].gameObject);
                    // 리스트에서 삭제
                    itemToggles.RemoveAt(i);
                    scanObject.SetActive(false);

                    break;
                }
              

            }
        }
    }

    //아이템이 있으몀 True 없으면 Fales반환 메서드
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


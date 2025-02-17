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
        // 아이템 프리팹 생성 후 부모 설정
        GameObject newItem = Instantiate(item.gameObject, group.transform);
        newItem.transform.localPosition = Vector3.zero;

        // Item 컴포넌트를 가져와 itemData 적용
        Item itemComponent = newItem.GetComponent<Item>();
        ToggleEventAdd(itemData, newItem, itemComponent);
    }
        
        
    private void ToggleEventAdd(ItemData itemData, GameObject newItem, Item itemComponent)
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
                    else
                    {

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
            if (objData.itemData == itemToggles[i].GetComponent<Item>().itemData) // 아이템 데이터 비교
            { 
                if (objData.itemData.isStackable)
                {
                    Debug.Log(objData.itemData.maxStack);
                    objData.itemData.maxStack--;
                    if (objData.itemData.maxStack == 0)
                    {
                        scanObject.SetActive(false);
                        Destroy(itemToggles[i].gameObject);

                        // 리스트에서 삭제
                        itemToggles.RemoveAt(i);
                        break;
                    }
                }
                if(!objData.itemData.isStackable)
                {

                    scanObject.SetActive(false);
                    Destroy(itemToggles[i].gameObject);

                    // 리스트에서 삭제
                    itemToggles.RemoveAt(i);
                    break;
                }
            }
         
        }
    }
}

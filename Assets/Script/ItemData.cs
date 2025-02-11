using UnityEngine;

public enum ItemType { Consumable , KeyItem }

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]
public class ItemData : ScriptableObject
{
    public string itemName;         // 아이템 이름
    public Sprite icon;             // 아이템 아이콘 (이미지)
    public ItemType itemType;       // 아이템 유형 (소모품, 장비, 재료 등)
    public bool isStackable;        // 중첩 가능 여부
    public int maxStack = 1;        // 최대 스택 개수 (중첩 가능하면 값 지정)
}
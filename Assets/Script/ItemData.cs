using UnityEngine;

public enum ItemType { Consumable , KeyItem }

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]
public class ItemData : ScriptableObject
{
    public string itemName;         // ������ �̸�
    public Sprite icon;             // ������ ������ (�̹���)
    public ItemType itemType;       // ������ ���� (�Ҹ�ǰ, ���, ��� ��)
    public bool isStackable;        // ��ø ���� ����
    public int maxStack = 1;        // �ִ� ���� ���� (��ø �����ϸ� �� ����)
}
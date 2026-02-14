using Project.Scripts;
using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

public class ItemSystemTest : MonoBehaviour
{
    [SerializeField] private ItemType _itemType;

    [Inject] private ItemSystem _itemSystem;

    [Button]
    public void CreateRandomItem()
    {
        var item = _itemSystem.CreateRandomItem();
        
        LogItem(item);
    }

    [Button]
    public void CreateItemByType()
    {
        var item = _itemSystem.CreateItemByType(_itemType);

        LogItem(item);
    }

    [Button]
    public void DropItemByType()
    {
        var item = _itemSystem.CreateItemByType(_itemType);
        _itemSystem.DropItem(item, Vector3.zero);
    }

    private void LogItem(ItemData item)
    {
        Debug.Log($"Item: {item}");
        Debug.Log($"Modifier: {item.Modifier.GetType().Name}");
        Debug.Log($"Sprite: {item.Sprite.name}"); 
        Debug.Log("----------Stats----------");
        
        foreach (var itemStat in item.Stats)
        {
            Debug.Log(itemStat.GetType().Name);
        }
        
        Debug.Log("-------------------------------------------------------------");
    }
}
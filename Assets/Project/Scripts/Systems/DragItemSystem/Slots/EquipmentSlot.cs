namespace Project.Scripts
{
    public class EquipmentSlot : InventorySlot
    {
        public override bool CanAccept(ItemData itemData)
        {
            if (itemData == null)
            {
                return false;
            }

            return itemData.Type == AllowedItemType;
        }

        protected override void OnItemPlaced(InventoryItem item)
        {
            if (item == null)
            {
                return;
            }

            if (EquipmentSystem == null)
            {
                return;
            }

            EquipmentSystem.EquipItem(item.ItemData);
        }

        protected override void OnItemRemoved(InventoryItem item)
        {
            if (item == null)
            {
                return;
            }

            if (EquipmentSystem == null)
            {
                return;
            }

            EquipmentSystem.RemoveItem(item.ItemData.Type);
        }
    }
}
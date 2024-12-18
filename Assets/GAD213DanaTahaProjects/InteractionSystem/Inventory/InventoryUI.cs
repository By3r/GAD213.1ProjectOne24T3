using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    #region Variables
    public PlayerInventory playerInventory;
    public Button[] slotButtons;
    public Sprite emptySlotSprite;
    public MedicineCrafting medicineCrafting; 
    public TreeCurer treeCurer; 
    #endregion

    public void UpdateInventoryUI()
    {
        for (int i = 0; i < slotButtons.Length; i++)
        {
            Image buttonImage = slotButtons[i].GetComponent<Image>();

            if (playerInventory.inventorySprites[i] != null)
            {
                buttonImage.sprite = playerInventory.inventorySprites[i];
                slotButtons[i].interactable = true;
            }
            else
            {
                buttonImage.sprite = emptySlotSprite;
                slotButtons[i].interactable = false;
            }

            int slotIndex = i;
            slotButtons[i].onClick.RemoveAllListeners();
            slotButtons[i].onClick.AddListener(() => OnSlotClicked(slotIndex));
        }
    }

    public void OnSlotClicked(int slotIndex)
    {
        Sprite flaskSprite = playerInventory.GetFlaskSprite(slotIndex);

        if (flaskSprite != null)
        {
            if (treeCurer.treeCurePanel.activeSelf)
            {
                bool placed = treeCurer.PlaceFlaskInSlot(flaskSprite);

                if (placed)
                {
                    playerInventory.RemoveFlask(slotIndex);
                }
            }
            else if (medicineCrafting.craftingUI.activeSelf)
            {
                bool placed = medicineCrafting.PlaceInCraftingSlot(flaskSprite);

                if (placed)
                {
                    playerInventory.RemoveFlask(slotIndex);
                }
            }
        }
    }
}

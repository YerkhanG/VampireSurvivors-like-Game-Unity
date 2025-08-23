using UnityEngine;
using UnityEngine.EventSystems;

public class BackpackSlotUI : MonoBehaviour, IDropHandler
{
    public int backpackSlotIndex;
    public SpellBackpackController spellBackpackController;
    public SpellQueueUIManager uiManager;


    public void OnDrop(PointerEventData eventData)
    {
        DraggableSpellUI draggedSpell = eventData.pointerDrag.GetComponent<DraggableSpellUI>();
        if (draggedSpell == null) return;

        Debug.Log($"OnDrop: Dropped spell from index {draggedSpell.originalBackpackIndex} onto slot {backpackSlotIndex}");

        if (draggedSpell.isFromBackpack)
        {
            spellBackpackController.SwapSpellsInBackpack(draggedSpell.originalBackpackIndex, backpackSlotIndex);
        }
        else
        {
            spellBackpackController.MoveSpellToBackpack(draggedSpell.originalQueueIndex, backpackSlotIndex);
        }
    }

    void Start()
    {
        uiManager = FindAnyObjectByType<SpellQueueUIManager>();
        spellBackpackController = FindAnyObjectByType<SpellBackpackController>();
        backpackSlotIndex = transform.GetSiblingIndex();
    }

}
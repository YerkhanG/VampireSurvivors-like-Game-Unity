using UnityEngine;
using UnityEngine.EventSystems;

public class SpellSlotUI : MonoBehaviour, IDropHandler
{
    public int queIndex;
    private SpellQueue spellQueue;
    private SpellQueueUIManager uiManager;

    private SpellBackpackController spellBackpackController;
    public void Start()
    {
        queIndex = transform.GetSiblingIndex();
        spellQueue = FindAnyObjectByType<SpellQueue>();
        uiManager = FindAnyObjectByType<SpellQueueUIManager>();
        spellBackpackController = FindAnyObjectByType<SpellBackpackController>();
        Debug.Log($"SpellSlotUI {gameObject.name} initialized with queIndex: {queIndex}");
    }
    public void OnDrop(PointerEventData eventData)
    {
        DraggableSpellUI draggedSpell = eventData.pointerDrag.GetComponent<DraggableSpellUI>();
        if (draggedSpell == null) return;

        Debug.Log($"OnDrop: Dropped spell from index {draggedSpell.originalIndex} onto slot {queIndex}");

        DraggableSpellUI existingSpell = GetExistingSpell();
        if (draggedSpell.isFromBackpack)
        {
            spellBackpackController.MoveSpellToQueue(draggedSpell.originalBackpackIndex,queIndex);
        }
        else if (existingSpell != null && existingSpell != draggedSpell)
        {
            uiManager.SwapSpellsInUI(draggedSpell, existingSpell);
        }
        else
        {
            // Handle move to empty slot
            int fromIndex = draggedSpell.originalIndex;

            draggedSpell.transform.SetParent(transform);
            draggedSpell.transform.localPosition = Vector3.zero;

            // Update references
            draggedSpell.originalParent = transform;
            draggedSpell.originalIndex = queIndex;

            // Update backend (move spell)
            spellQueue.SwapActiveSpells(fromIndex, queIndex);
        }
    }


    private DraggableSpellUI GetExistingSpell()
    {
        // Only check direct children
        foreach (Transform child in transform)
        {
            DraggableSpellUI spell = child.GetComponent<DraggableSpellUI>();
            if (spell != null) return spell;
        }
        return null;
    }
}
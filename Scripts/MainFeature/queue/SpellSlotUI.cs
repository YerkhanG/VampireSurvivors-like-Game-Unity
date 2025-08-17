using UnityEngine;
using UnityEngine.EventSystems;

public class SpellSlotUI : MonoBehaviour, IDropHandler
{
    public int queIndex;
    private SpellQueue spellQueue;
    private SpellQueueUIManager uiManager;
    public void Start()
    {
        spellQueue = FindAnyObjectByType<SpellQueue>();
        uiManager = FindAnyObjectByType<SpellQueueUIManager>();
    }
    public void OnDrop(PointerEventData eventData)
    {
        DraggableSpellUI draggedSpell = eventData.pointerDrag.GetComponent<DraggableSpellUI>();
        if (draggedSpell != null)
        {
            // Check if there's already a spell in this slot by looking for existing children
            DraggableSpellUI existingSpell = GetExistingSpell(draggedSpell);
            Debug.Log("Existing spell to check: ", existingSpell.gameObject);
            if (existingSpell != null)
            {
                // SWAP CASE: There's already a spell in this slot
                Debug.Log($"Swapping dragged spell from slot {draggedSpell.originalIndex} with existing spell in slot {queIndex}");
                uiManager.SwapSpellsInUI(draggedSpell, existingSpell);
            }
            else
            {
                // MOVE CASE: Slot is empty, just move the spell here
                Debug.Log($"Moving spell from slot {draggedSpell.originalIndex} to empty slot {queIndex}");
                draggedSpell.transform.SetParent(this.transform);
                draggedSpell.transform.localPosition = Vector3.zero;
                draggedSpell.transform.localScale = Vector3.one;
                draggedSpell.transform.localRotation = Quaternion.identity;
                
                // Update references
                draggedSpell.originalParent = this.transform;
                draggedSpell.originalIndex = queIndex;
                
                // Update backend for move (this is trickier - you might need a MoveSpell method)
                // For now, you could use swap if the target slot has a null spell
                spellQueue.SwapActiveSpells(draggedSpell.originalIndex, queIndex);
            }
        }
    }
    
    private DraggableSpellUI GetExistingSpell(DraggableSpellUI draggedSpell)
    {
        // Look through direct children only, excluding the dragged spell
        for (int i = 0; i < transform.childCount; i++)
        {
            Transform child = transform.GetChild(i);
            DraggableSpellUI spellUI = child.GetComponent<DraggableSpellUI>();
            
            if (spellUI != null && spellUI != draggedSpell)
            {
                return spellUI;
            }
        }
        return null;
    }
}
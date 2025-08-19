using System;
using UnityEngine;
using UnityEngine.UI;

public class SpellQueueUIManager : MonoBehaviour
{
    public GameObject spellIconPrefab;
    public Transform spellQueuePanel;
    private SpellSlotUI[] queue;
    public SpellQueue spellQueue;
    void Start()
    {
        queue = spellQueuePanel.GetComponentsInChildren<SpellSlotUI>();
        spellQueue = FindAnyObjectByType<SpellQueue>();
        RefreshAllSlots();
    }

    public void AddSpellToUI(BaseSpell spell, int slotIndex)
    {
        GameObject icon = Instantiate(spellIconPrefab, spellQueuePanel.GetChild(slotIndex));
        icon.GetComponent<Image>().sprite = spell.spellIcon;
    }

    public void UpdateSpellIcon(int slotIndex, BaseSpell spell)
    {
        if (slotIndex >= queue.Length) return;

        Transform slotTransform = queue[slotIndex].transform;

        // Remove old icon if exists
        for (int i = slotTransform.childCount - 1; i >= 0; i--)
        {
            if (slotTransform.GetChild(i).GetComponent<DraggableSpellUI>() != null)
                DestroyImmediate(slotTransform.GetChild(i).gameObject);
        }

        // Add new icon if spell exists
        if (spell != null)
        {
            GameObject icon = Instantiate(spellIconPrefab, slotTransform);
            icon.GetComponent<Image>().sprite = spell.spellIcon;
            icon.GetComponent<DraggableSpellUI>();
        }
    }
    public void SwapSpellsInUI(DraggableSpellUI draggedSpell, DraggableSpellUI existingSpell)
    {
        if (draggedSpell == null || existingSpell == null) return;

        // Get slot indices from the original positions
        int index1 = draggedSpell.originalIndex;
        int index2 = existingSpell.transform.parent.GetSiblingIndex();

        Debug.Log($"SwapSpellsInUI Debug:");
        Debug.Log($"  Dragged spell original index: {index1}");
        Debug.Log($"  Existing spell slot index: {index2}");
        Debug.Log($"  Dragged spell name: {draggedSpell.gameObject.transform.parent.name}");
        Debug.Log($"  Existing spell name: {existingSpell.gameObject.name}");

        // Store the target parent (where draggedSpell is being dropped)
        Transform targetParent = existingSpell.transform.parent;
        
        // Move existing spell to dragged spell's original position
        existingSpell.transform.SetParent(draggedSpell.originalParent);
        existingSpell.transform.localPosition = Vector3.zero;
        
        // Move dragged spell to the target position
        draggedSpell.transform.SetParent(targetParent);
        draggedSpell.transform.localPosition = Vector3.zero;

        // Update backend
        spellQueue.SwapActiveSpells(index1, index2);
    }
   public void RefreshAllSlots()
    {
        for (int i = 0; i < queue.Length; i++)
        {
            BaseSpell spell = (i < spellQueue.queue.Count) ? spellQueue.queue[i] : null;
            UpdateSpellIcon(i, spell);
        }
    }
}
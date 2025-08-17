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
            icon.GetComponent<DraggableSpellUI>(); // Make sure it has this component
        }
    }
    public void SwapSpellsInUI(DraggableSpellUI spell1, DraggableSpellUI spell2)
    {
        if (spell1 == null || spell2 == null) return;

        Transform parent1 = spell1.transform.parent;
        Transform parent2 = spell2.transform.parent;
        
        // Get indices from slot positions
        int index1 = parent1.GetSiblingIndex();
        int index2 = parent2.GetSiblingIndex();

        // Swap UI elements
        spell1.transform.SetParent(parent2);
        spell1.transform.localPosition = Vector3.zero;
        spell1.transform.localScale = Vector3.one;

        spell2.transform.SetParent(parent1);
        spell2.transform.localPosition = Vector3.zero;
        spell2.transform.localScale = Vector3.one;

        // Update drag components
        spell1.originalParent = parent2;
        spell1.originalIndex = index2;
        spell2.originalParent = parent1;
        spell2.originalIndex = index1;

        // CRITICAL: Update data model
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
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
public class SpellBackpackController : MonoBehaviour
{
    public GameObject backpackPanel;
    public int slotCount;
    public GameObject slotPrefab;
    public List<BaseSpell> backpackSpells = new List<BaseSpell>();
    private SpellQueue spellQueue;
    private SpellQueueUIManager uiManager;
    private bool isBackpackOpen = false;
    private NewActions inputActions;
    void Awake()
    {
        inputActions = new NewActions();
    }

    void Start()
    {
        spellQueue = FindAnyObjectByType<SpellQueue>();
        uiManager = FindAnyObjectByType<SpellQueueUIManager>();
        for (int i = 0; i < slotCount; i++)
        {
            backpackSpells.Add(null);
        }
        for (int i = 0; i < slotCount; i++)
        {
            Slot slot = Instantiate(slotPrefab, backpackPanel.transform).GetComponent<Slot>();
        }
        backpackPanel.SetActive(false);
    }
    private void OnEnable()
    {
        inputActions.Player.Backpack.performed += OnBackpackToggle;
        inputActions.Enable();
    }
    private void OnDisable()
    {
        inputActions.Player.Backpack.performed -= OnBackpackToggle;
        inputActions.Disable();
    }
    private void OnBackpackToggle(InputAction.CallbackContext context)
    {
        isBackpackOpen = !isBackpackOpen;
        backpackPanel.SetActive(isBackpackOpen);
    }
    public void AddSpellToBackpack(BaseSpell spell, int index)
    {
        if (index < 0 || index >= backpackSpells.Count) return;

        backpackSpells[index] = spell;
        UpdateUIinBackpack();
    }
    private void UpdateUIinBackpack()
    {
        for (int i = 0; i < backpackSpells.Count; i++)
        {
            Transform bslot = backpackPanel.transform.GetChild(i);

            foreach (Transform child in bslot)
            {
                if (child.GetComponent<DraggableSpellUI>() != null)
                    DestroyImmediate(child.gameObject);
            }
            if (backpackSpells[i] != null)
            {
                GameObject icon = Instantiate(uiManager.spellIconPrefab, bslot);
                icon.GetComponent<Image>().sprite = backpackSpells[i].spellIcon;

                DraggableSpellUI draggable = icon.GetComponent<DraggableSpellUI>();
                draggable.isFromBackpack = true;
                draggable.originalBackpackIndex = i;
            }
        }
    }
    public void MoveSpellToQueue(int backpackIndex, int queueIndex)
    {
        BaseSpell spell = backpackSpells[backpackIndex];
        backpackSpells[backpackIndex] = null;
        Debug.Log("MoveSpellToQueue Indeces: " + backpackIndex + " " + queueIndex);
        spellQueue.queue[queueIndex] = spell;
        uiManager.UpdateSpellIcon(queueIndex, spell);
        UpdateUIinBackpack();
    }
    public void MoveSpellToBackpack(int queueIndex, int backpackIndex)
    {
        BaseSpell spell = spellQueue.queue[queueIndex];
        spellQueue.queue[queueIndex] = null;
        uiManager.UpdateSpellIcon(queueIndex, null);
        Debug.Log("MoveSpellToBackpack Indeces: " + queueIndex + backpackIndex);
        backpackSpells[backpackIndex] = spell;
        UpdateUIinBackpack();
    }
    public void SwapSpellsInBackpack(int index1, int index2)
    {
        if (index1 < 0 || index2 < 0 || index1 >= backpackSpells.Count || index2 >= backpackSpells.Count) return;
        
        BaseSpell temp = backpackSpells[index1];
        backpackSpells[index1] = backpackSpells[index2];
        backpackSpells[index2] = temp;
        
        UpdateUIinBackpack();
    }
}
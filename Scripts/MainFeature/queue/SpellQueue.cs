using System.Collections.Generic;
using UnityEngine;

public class SpellQueue : MonoBehaviour
{
    public List<BaseSpell> queue = new List<BaseSpell>(7); 
    public List<BaseSpell> spellLibrary = new List<BaseSpell>(30);
    private float _currentCooldown = 3f;
    private int _currentIndex = 0;
    private NewActions mouseActions;
    private SpellQueueUIManager uiManager;
    void Awake()
    {
        mouseActions = new NewActions();
        uiManager = FindAnyObjectByType<SpellQueueUIManager>();
    }
    void OnEnable()
    {
        mouseActions.Player.Cast.Enable();
    }
    void OnDisable()
    {
        mouseActions.Player.Cast.Disable();
    }
    public void Update()
    {
        if (queue.Count == 0) return; // Don't try to cast if queue is empty
        if (_currentCooldown > 0)
        {
            _currentCooldown -= Time.deltaTime;
        }
        else
        {
            CastSpellInqueue();
        }
    }

    private void CastSpellInqueue()
    {
        if (_currentIndex >= queue.Count || queue[_currentIndex] == null)
        {
            queue.RemoveAt(_currentIndex);
            _currentIndex = Mathf.Clamp(_currentIndex, 0, queue.Count - 1);
            if (queue.Count == 0) return;
        }

        // Get input
        Vector2 castDirection;
        try 
        {
            castDirection = mouseActions.Player.Cast.ReadValue<Vector2>();
        }
        catch (System.Exception e)
        {
            Debug.LogError("Failed to read cast input: " + e.Message);
            return;
        }

        BaseSpell spell = queue[_currentIndex];
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(new Vector3(castDirection.x, castDirection.y, Camera.main.nearClipPlane));
        Vector2 direction = (mouseWorldPos - transform.position).normalized;
        
        spell.Cast(transform, direction);
        _currentCooldown = spell.spellCooldown;
        _currentIndex = (_currentIndex + 1) % queue.Count;
    }
    public void EquipSpells(int libraryIndex, int spellQueueIndex)
    {
        queue[spellQueueIndex] = spellLibrary[libraryIndex];
        uiManager.UpdateSpellIcon(spellQueueIndex, queue[spellQueueIndex]);
    }
    public void SwapActiveSpells(int index1, int index2)
    {
        if (index1 < 0 || index2 < 0 || index1 >= queue.Count || index2 >= queue.Count) return;
        BaseSpell temp = queue[index1];
        Debug.Log("Trying to change spells in backend: " + queue[index1].spellName + " " + queue[index2].spellName);
        queue[index1] = queue[index2];
        queue[index2] = temp;
        Debug.Log("Changed spells in backend: " + index1 + " " + index2);
    }
    public void RemoveSpell(int index) => queue.RemoveAt(index);
}
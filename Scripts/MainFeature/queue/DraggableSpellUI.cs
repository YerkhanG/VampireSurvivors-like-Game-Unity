using UnityEngine;
using UnityEngine.EventSystems;

public class DraggableSpellUI : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private CanvasGroup canvasGroup;
    public int originalIndex;
    public Transform originalParent;
    void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        Debug.Log($"DraggableSpellUI Awake: {gameObject.name}, canvasGroup found: {canvasGroup != null}");
    } 
    private static DraggableSpellUI currentlyDraggedItem;
    public void OnBeginDrag(PointerEventData eventData)
    {
        if (canvasGroup)
        {
            originalParent = transform.parent;
            originalIndex = originalParent.GetSiblingIndex();
            
            Debug.Log($"OnBeginDrag: Spell {gameObject.name} starting from slot index {originalIndex}");
        
            canvasGroup.blocksRaycasts = false;
            canvasGroup.alpha = 0.6f;
        }
        currentlyDraggedItem = this;
        transform.SetParent(GameObject.FindGameObjectWithTag("MainFeatureCanvas").transform);
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        currentlyDraggedItem = null;
        if (canvasGroup)
        {
            canvasGroup.blocksRaycasts = true;
            canvasGroup.alpha = 1f;
        }
        if (eventData.pointerCurrentRaycast.gameObject == null || eventData.pointerCurrentRaycast.gameObject.GetComponentInParent<SpellSlotUI>() == null)
        {
            transform.SetParent(originalParent);
            transform.SetSiblingIndex(originalIndex);
            transform.localPosition = Vector3.zero;
            transform.localScale = Vector3.one;
        }
    }
    
}

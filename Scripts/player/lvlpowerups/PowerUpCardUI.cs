using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PowerUpCardUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Image icon;
    public TextMeshProUGUI cardName;
    public TextMeshProUGUI description;
    private Button button;
    public PowerUp AssignedPowerUp { get; private set; }

    private System.Action<PowerUp> onCardSelected;

    private void Awake()
    {

        button = GetComponent<Button>();

        if (button != null)
        {
            button.onClick.AddListener(OnButtonClick);
        }
        else
        {
            Debug.LogError("No Button component found on PowerUpCardUI!", this);
        }
    }
    public void Initialize(PowerUp p, System.Action<PowerUp> selectedCallback)
    {
        AssignedPowerUp = p;
        onCardSelected = selectedCallback;
        icon.sprite = p.sprite;
        cardName.text = p.powerUpName;
        description.text = p.description;
    }
    private void OnButtonClick()
    {
        onCardSelected?.Invoke(AssignedPowerUp);
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        transform.localScale = Vector3.one * 1.1f;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        transform.localScale = Vector3.one;
    }
    private void OnDestroy()
    {
        if (button != null)
        {
            button.onClick.RemoveListener(OnButtonClick);
        }
    }
}

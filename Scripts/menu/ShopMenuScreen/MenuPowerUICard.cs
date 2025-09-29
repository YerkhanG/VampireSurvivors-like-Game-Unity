using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace menu.ShopMenuScreen
{
    public class MenuPowerUICard : MonoBehaviour
    {
        [SerializeField]private Button thisSlotsButton;
        /*public System.Action<string, int> OnBuy;*/
        [SerializeField]private  MenuPowerUp thisSlotsPowerUp;
        [SerializeField]private Image thisSlotsIcon;
        [SerializeField]private TextMeshProUGUI thisSlotsNameText;
        [SerializeField]private TextMeshProUGUI thisDescriptionText;
        void Awake()
        {
            thisDescriptionText.text = thisSlotsPowerUp.description;
            thisSlotsIcon.sprite = thisSlotsPowerUp.menuPowerSprite;
            thisSlotsNameText.text = thisSlotsPowerUp.name;
            var buttonTextTMP = thisSlotsButton.GetComponentInChildren<TextMeshProUGUI>();
            buttonTextTMP.text = thisSlotsPowerUp.cost.ToString();
            thisSlotsButton.onClick.AddListener(() =>
                MenuPowerUpController.instance.PurchasePowerUp(thisSlotsPowerUp.ID, thisSlotsPowerUp.GetCostForLevel()));
        }
    }
}
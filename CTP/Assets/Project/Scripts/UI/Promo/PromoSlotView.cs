using System;
using System.Collections;
using RedPanda.Project.Services.UI.Promo.Data;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace RedPanda.Project.UI.Promo
{
    public class PromoSlotView : MonoBehaviour
    {
        public event EventHandler<PromoSlotViewDataModel> PurchaseButtonClicked;
        
        [SerializeField] private TMP_Text _titleText;
        [SerializeField] private Image _rewardImage;
        [SerializeField] private Image _backgroundImage;
        [SerializeField] private TMP_Text _priceText;
        [SerializeField] private Button _purchaseButton;

        private PromoSlotViewDataModel _viewDataModel;
        
        private void OnEnable()
        {
            _purchaseButton.onClick.AddListener(PurchaseButtonClickedHandler);
        }

        private void OnDisable()
        {
            _purchaseButton.onClick.RemoveListener(PurchaseButtonClickedHandler);
        }
        
        public void Initialize(PromoSlotViewDataModel viewDataModel)
        {
            _viewDataModel = viewDataModel;
            
            InitTitle(viewDataModel.Title);
            InitRewardImage(viewDataModel.RewardIcon);
            InitBackgroundImage(viewDataModel.BackgroundIcon);
            InitPrice(viewDataModel.Price);
        }
        
        private void InitTitle(string title)
        {
            _titleText.SetText(title.ToUpper());
        }
        
        private void InitRewardImage(Sprite icon)
        {
            _rewardImage.sprite = icon;
        }
        
        private void InitBackgroundImage(Sprite icon)
        {
            _backgroundImage.sprite = icon;
        }

        private void InitPrice(int price)
        {
            _priceText.SetText($"x{price.ToString()}");
        }
        
        private void PurchaseButtonClickedHandler()
        {
            OnPurchaseButtonClicked();
        }

        private void OnPurchaseButtonClicked()
        {
            PurchaseButtonClicked?.Invoke(this, _viewDataModel);
        }
    }
}
using System;
using System.Collections.Generic;
using RedPanda.Project.Services.UI.Promo.Data;
using UnityEngine;
using UnityEngine.UI;

namespace RedPanda.Project.UI.Promo
{
    public class PromoView : View
    {
        public event EventHandler<PromoSlotViewDataModel> SlotPurchaseButtonClicked;
        public event EventHandler CloseButtonClicked;
        
        [SerializeField] private CurrencyCounterView _currencyCounter;
        [SerializeField] private Button _closeButton;
        [SerializeField] private ScrollRect _scrollRect;
        [SerializeField] private Transform _blockRoot;
        [SerializeField] private PromoBlockView _blockTemplate;

        private List<PromoBlockView> _blockViews = new();

        private void OnEnable()
        {
            _closeButton.onClick.AddListener(CloseButtonClickedHandler);
        }

        private void OnDisable()
        {
            _closeButton.onClick.RemoveListener(CloseButtonClickedHandler);
        }

        public void Initialize(int currencyValue, List<PromoBlockViewDataModel> blockViewDataModels)
        {
            SetCurrencyCounterValue(currencyValue);
            InitBlocks(blockViewDataModels);
            InitScroll();
        }

        public void SetCurrencyCounterValue(int currencyValue)
        {
            _currencyCounter.SetValue(currencyValue);
        }

        private void InitScroll()
        {
            _scrollRect.verticalNormalizedPosition = 1f;
        }

        private void InitBlocks(List<PromoBlockViewDataModel> blockViewDataModels)
        {
            ClearSlots();

            foreach (var blockViewDataModel in blockViewDataModels)
            {
                var blockView = CreateSlotView(_blockTemplate, _blockRoot);
                blockView.Initialize(blockViewDataModel);
                blockView.SlotPurchaseButtonClicked += SlotPurchaseButtonClickedHandler;
                _blockViews.Add(blockView);
            }
        }

        private void ClearSlots()
        {
            foreach (var blockView in _blockViews)
            {
                DestroyImmediate(blockView.gameObject);
            }
            
            _blockViews.Clear();
        }

        private PromoBlockView CreateSlotView(PromoBlockView blockTemplate, Transform parent)
        {
            return Instantiate(blockTemplate, parent);
        }

        private void OnSlotPurchaseButtonClicked(PromoSlotViewDataModel slotViewDataModel)
        {
            SlotPurchaseButtonClicked?.Invoke(this, slotViewDataModel);
        }
        
        private void OnCloseButtonClicked()
        {
            CloseButtonClicked?.Invoke(this, EventArgs.Empty);
        }
        
        private void SlotPurchaseButtonClickedHandler(object sender, PromoSlotViewDataModel slotViewDataModel)
        {
            OnSlotPurchaseButtonClicked(slotViewDataModel);
        }
        
        private void CloseButtonClickedHandler()
        {
            OnCloseButtonClicked();
        }
    }
}
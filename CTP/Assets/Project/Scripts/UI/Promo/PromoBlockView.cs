using System;
using System.Collections.Generic;
using RedPanda.Project.Services.UI.Promo.Data;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace RedPanda.Project.UI.Promo
{
    public class PromoBlockView : MonoBehaviour
    {
        public event EventHandler<PromoSlotViewDataModel> SlotPurchaseButtonClicked;
        
        [SerializeField] private ScrollRect _scrollRect;
        [SerializeField] private TMP_Text _tittleText;
        [SerializeField] private Transform _slotRoot;
        [SerializeField] private PromoSlotView _slotTemplate;
        
        private PromoBlockViewDataModel _viewDataModel;
        private List<PromoSlotView> _slotViews = new();

        public void Initialize(PromoBlockViewDataModel viewDataModel)
        {
            _viewDataModel = viewDataModel;
            
            InitTitle(viewDataModel.Title);
            InitSlots(viewDataModel.SlotViewModels);
            InitScroll();
        }
        
        private void InitScroll()
        {
            _scrollRect.horizontalNormalizedPosition = 0;
        }

        private void InitTitle(string title)
        {
            _tittleText.SetText(title);
        }

        private void InitSlots(List<PromoSlotViewDataModel> slotViewDataModels)
        {
            ClearSlots();

            foreach (var slotViewDataModel in slotViewDataModels)
            {
                var slotView = CreateSlotView(_slotTemplate, _slotRoot);
                slotView.Initialize(slotViewDataModel);
                slotView.PurchaseButtonClicked += SlotPurchaseButtonClickedHandler;
                _slotViews.Add(slotView);
            }
        }

        private void ClearSlots()
        {
            foreach (var slotView in _slotViews)
            {
                DestroyImmediate(slotView.gameObject);
            }
            
            _slotViews.Clear();
        }

        private PromoSlotView CreateSlotView(PromoSlotView slotTemplate, Transform parent)
        {
            return Instantiate(slotTemplate, parent);
        }
        
        private void SlotPurchaseButtonClickedHandler(object sender, PromoSlotViewDataModel slotViewDataModel)
        {
            OnPurchaseButtonClicked(slotViewDataModel);
        }

        private void OnPurchaseButtonClicked(PromoSlotViewDataModel slotViewDataModel)
        {
            SlotPurchaseButtonClicked?.Invoke(this, slotViewDataModel);
        }
    }
}
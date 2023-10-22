using System;
using System.Collections.Generic;
using System.Linq;
using Grace.DependencyInjection;
using RedPanda.Project.Commands;
using RedPanda.Project.Interfaces;
using RedPanda.Project.Services.Interfaces;
using RedPanda.Project.Services.UI.Promo.Data;
using RedPanda.Project.Services.UI.Promo.Providers;
using RedPanda.Project.UI.Promo;
using UnityEngine;

namespace RedPanda.Project.Services.UI.Promo
{
    public class PromoViewControl : UIControl<PromoView>
    {
        private readonly IPromoContentProvider _contentProvider;
        private readonly IUserService _userService;
        private readonly IPromoService _promoService;
        private readonly IUIService _uiService;

        private readonly Dictionary<PromoSlotViewDataModel, IPromoModel> _promoProductStorage = new();

        public PromoViewControl(string viewName, GameObject parent, IExportLocatorScope exportLocatorScope)
            : base(viewName, parent, exportLocatorScope)
        {
            _contentProvider = exportLocatorScope.Locate<IPromoContentProvider>();
            _userService = exportLocatorScope.Locate<IUserService>();
            _promoService = exportLocatorScope.Locate<IPromoService>();
            _uiService = exportLocatorScope.Locate<IUIService>();
            
            InitializeView(_promoService);
        }

        private void InitializeView(IPromoService promoService)
        {
            var currencyValue = _userService.Currency;
            
            var promoModels = promoService.GetPromos();
            var blockViewDataModels = PrepareBlockViewDataModels(promoModels);

            view.Initialize(currencyValue, blockViewDataModels);
        }

        private List<PromoBlockViewDataModel> PrepareBlockViewDataModels(IReadOnlyList<IPromoModel> promoModels)
        {
            _promoProductStorage.Clear();
            
            var promoModelGroups = promoModels.GroupBy(promoModel => _contentProvider.BlockSortOrder[promoModel.Type]);
            var blockViewModels = new List<PromoBlockViewDataModel>();
            
            foreach (var group in promoModelGroups)
            {
                var blockTitle = group.First().Type.ToString().ToUpper();
                
                var sortedPromoModels = group.OrderBy(promoModel => _contentProvider.SlotSortOrder[promoModel.Rarity]);
                var slotViewModels = CreateSlotsViewModels(sortedPromoModels);
                
                var blockViewDataModel = new PromoBlockViewDataModel(blockTitle, slotViewModels);
                blockViewModels.Add(blockViewDataModel);
            }

            return blockViewModels;
        }

        private List<PromoSlotViewDataModel> CreateSlotsViewModels(IEnumerable<IPromoModel> promoModels)
        {
            var slotViewModels = new List<PromoSlotViewDataModel>();
            foreach (var promoModel in promoModels)
            {
                var slotViewModel = _contentProvider.GetSlotViewData(promoModel);
                slotViewModels.Add(slotViewModel);
                _promoProductStorage.Add(slotViewModel, promoModel);
            }
            return slotViewModels;
        }

        private void UpdateCurrencyViewValue()
        {
            view.SetCurrencyCounterValue(_userService.Currency);
        }
        
        private void LaunchPurchasingProcess(PromoSlotViewDataModel slotViewDataModel)
        {
            if (_promoProductStorage.TryGetValue(slotViewDataModel, out var promoModel))
            {
                var purchaseCommand = new PurchaseCommand(_userService, promoModel);
                purchaseCommand.Execute();
            }
        }
        
        protected override void OnViewShown()
        {
            base.OnViewShown();
            _userService.CurrencyValueChanged += CurrencyValueChangedHandler;
            view.SlotPurchaseButtonClicked += SlotPurchaseButtonClickedHandler;
            view.CloseButtonClicked += CloseButtonClickedHandler;
        }
        
        protected override void OnViewHided()
        {
            base.OnViewHided();
            _userService.CurrencyValueChanged -= CurrencyValueChangedHandler;
            view.SlotPurchaseButtonClicked -= SlotPurchaseButtonClickedHandler;
            view.CloseButtonClicked -= CloseButtonClickedHandler;
        }

        private void CurrencyValueChangedHandler(object sender, EventArgs e)
        {
            UpdateCurrencyViewValue();
        }
        
        private void SlotPurchaseButtonClickedHandler(object sender, PromoSlotViewDataModel slotViewDataModel)
        {
            LaunchPurchasingProcess(slotViewDataModel);
        }
        
        private void CloseButtonClickedHandler(object sender, EventArgs e)
        {
            _uiService.Close(view.name);
        }
    }
}
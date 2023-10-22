using System.Collections.Generic;
using System.Linq;
using RedPanda.Project.Data;
using RedPanda.Project.Interfaces;
using RedPanda.Project.Services.UI.Promo.Config;
using RedPanda.Project.Services.UI.Promo.Data;
using UnityEngine;

namespace RedPanda.Project.Services.UI.Promo.Providers
{
    public class PromoContentProvider : IPromoContentProvider
    {
        private const string ConfigPath = "Configs/PromoContentConfig";
        private const string DefaultIconKey = "default";
        private PromoContentConfig _contentConfig;

        private Dictionary<string, Sprite> _rewardIconStorage = new();
        private Dictionary<string, Sprite> _slotBackgroundStorage = new();
        
        public Dictionary<PromoType, int> BlockSortOrder { get; private set; }
        public Dictionary<PromoRarity, int> SlotSortOrder { get; private set; }

        public PromoContentProvider()
        {
            Initialize();
        }

        private void Initialize()
        {
            _contentConfig = LoadConfig();

            _rewardIconStorage = _contentConfig.RewardIcons.ToDictionary(sprite => sprite.name, sprite => sprite);
            _slotBackgroundStorage = _contentConfig.SlotBackgrounds.ToDictionary(sprite => sprite.name, sprite => sprite);
            BlockSortOrder = _contentConfig.BlockSortOrder.ToDictionary(itemData => itemData.SortType, itemData => itemData.SortPriority);
            SlotSortOrder = _contentConfig.SlotSortOrder.ToDictionary(itemData => itemData.SortType, itemData => itemData.SortPriority);
        }
        
        public PromoSlotViewDataModel GetSlotViewData(IPromoModel promoModel)
        {
            var title = promoModel.Title;
            var rewardIconKey = promoModel.GetIcon();
            var rewardIcon = GetRewardIcon(rewardIconKey);
            var backgroundIconKey = $"background_{promoModel.Rarity.ToString().ToLower()}";
            var backgroundIcon = GetBackgroundIcon(backgroundIconKey);
            var price = promoModel.Cost;

            return new PromoSlotViewDataModel(title, rewardIcon, backgroundIcon, price);
        }

        public Dictionary<PromoType, int> GetBlockSortOrder()
        {
            throw new System.NotImplementedException();
        }

        public Dictionary<PromoRarity, int> GetSlotSortOrder()
        {
            throw new System.NotImplementedException();
        }

        private Sprite GetRewardIcon(string iconKey)
        {
            if (_rewardIconStorage.ContainsKey(iconKey))
            {
                return _rewardIconStorage[iconKey];
            }

            DebugLogError($"RewardIcon not found by key {iconKey}");
            return _rewardIconStorage[DefaultIconKey];
        }
        
        private Sprite GetBackgroundIcon(string iconKey)
        {
            if (_slotBackgroundStorage.ContainsKey(iconKey))
            {
                return _slotBackgroundStorage[iconKey];
            }

            DebugLogError($"BackgroundIcon not found by key {iconKey}");
            return _slotBackgroundStorage[DefaultIconKey];
        }
        
        private PromoContentConfig LoadConfig()
        {
            return Resources.Load<PromoContentConfig>(ConfigPath);
        }

        private void DebugLogError(string message)
        {
            Debug.LogError($"[PromoContentProvider]{message}");
        }
    }
}
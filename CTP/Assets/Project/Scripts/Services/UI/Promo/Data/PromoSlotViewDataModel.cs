using UnityEngine;

namespace RedPanda.Project.Services.UI.Promo.Data
{
    public class PromoSlotViewDataModel
    {
        private string _title;
        private Sprite _rewardIcon;
        private Sprite _backgroundIcon;
        private int _price;

        public PromoSlotViewDataModel(string title, Sprite rewardIcon, Sprite backgroundIcon, int price)
        {
            _title = title;
            _rewardIcon = rewardIcon;
            _backgroundIcon = backgroundIcon;
            _price = price;
        }

        public string Title => _title;
        public Sprite RewardIcon => _rewardIcon;
        public Sprite BackgroundIcon => _backgroundIcon;
        public int Price => _price;
    }
}
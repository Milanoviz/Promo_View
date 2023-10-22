using System.Collections.Generic;

namespace RedPanda.Project.Services.UI.Promo.Data
{
    public class PromoBlockViewDataModel
    {
        private string _title;
        private List<PromoSlotViewDataModel> _slotViewModels;

        public PromoBlockViewDataModel(string title, List<PromoSlotViewDataModel> slotViewModels)
        {
            _title = title;
            _slotViewModels = slotViewModels;
        }

        public string Title => _title;
        public List<PromoSlotViewDataModel> SlotViewModels => _slotViewModels;
    }
}
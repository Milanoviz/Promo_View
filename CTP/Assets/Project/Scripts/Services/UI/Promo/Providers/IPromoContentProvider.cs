using System.Collections.Generic;
using RedPanda.Project.Data;
using RedPanda.Project.Interfaces;
using RedPanda.Project.Services.UI.Promo.Data;

namespace RedPanda.Project.Services.UI.Promo.Providers
{
    public interface IPromoContentProvider
    {
        Dictionary<PromoType, int> BlockSortOrder { get; }
        Dictionary<PromoRarity, int> SlotSortOrder { get; }
        
        PromoSlotViewDataModel GetSlotViewData(IPromoModel promoModel);
    }
}
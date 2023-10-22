using System.Collections.Generic;
using RedPanda.Project.Data;
using UnityEngine;

namespace RedPanda.Project.Services.UI.Promo.Config
{
    [CreateAssetMenu(fileName = "PromoContentConfig", menuName = "ViewContent/Promo/PromoContentConfig", order = 1)]
    public class PromoContentConfig : ScriptableObject
    {
        [Header("Icons")]
        [SerializeField] private List<Sprite> rewardIcons = new();
        [SerializeField] private Object rewardIconsFolder;
        [Space]
        [SerializeField] private List<Sprite> slotBackgrounds = new();
        [SerializeField] private Object slotBackgroundsFolder;
        [Header("Sort orders settings")]
        [SerializeField] private List<SortOrderItemData<PromoType>> blockSortOrder = new();
        [SerializeField] private List<SortOrderItemData<PromoRarity>> slotSortOrder = new();
        
        public List<Sprite> RewardIcons => rewardIcons;
        public List<Sprite> SlotBackgrounds => slotBackgrounds;
        public List<SortOrderItemData<PromoType>> BlockSortOrder => blockSortOrder;
        public List<SortOrderItemData<PromoRarity>> SlotSortOrder => slotSortOrder;
    }
}
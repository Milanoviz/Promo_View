using System;
using UnityEngine;

namespace RedPanda.Project.Services.UI.Promo.Config
{
    [Serializable]
    public class SortOrderItemData<T> where T : Enum
    {
        [SerializeField] private T sortType;
        [SerializeField] private int sortPriority;

        public T SortType => sortType;
        public int SortPriority => sortPriority;
    }
}
using RedPanda.Project.Interfaces;
using RedPanda.Project.Services.Interfaces;
using UnityEngine;

namespace RedPanda.Project.Commands
{
    public class PurchaseCommand : ICommand
    {
        private readonly IUserService _userService;
        private readonly IPromoModel _promoModel;

        public PurchaseCommand(IUserService userService, IPromoModel promoModel)
        {
            _userService = userService;
            _promoModel = promoModel;
        }

        public void Execute()
        {
            if (_userService.HasCurrency(_promoModel.Cost))
            {
                _userService.ReduceCurrency(_promoModel.Cost);
                Debug.Log($"Purchase successful. Product name: {GetProductName(_promoModel)}");
            }
            else
            {
                Debug.LogError($"Purchase error. Product name: {GetProductName(_promoModel)}");
            }
        }

        private string GetProductName(IPromoModel promoModel)
        {
            return $"{promoModel.Rarity}_{promoModel.Type}";
        }
    }
}
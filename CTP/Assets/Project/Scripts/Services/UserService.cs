using System;
using RedPanda.Project.Services.Interfaces;

namespace RedPanda.Project.Services
{
    public sealed class UserService : IUserService
    {
        public event EventHandler CurrencyValueChanged;
        public int Currency { get; private set; }
        
        public UserService()
        {
            Currency = 1000;
        }

        void IUserService.AddCurrency(int delta)
        {
            Currency += delta;
            OnCurrencyValueChanged();
        }

        void IUserService.ReduceCurrency(int delta)
        {
            Currency -= delta;
            OnCurrencyValueChanged();
        }
        
        bool IUserService.HasCurrency(int amount)
        {
            return Currency >= amount;
        }

        private void OnCurrencyValueChanged()
        {
            CurrencyValueChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}
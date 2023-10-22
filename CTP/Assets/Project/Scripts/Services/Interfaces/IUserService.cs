using System;

namespace RedPanda.Project.Services.Interfaces
{
    public interface IUserService
    {
        event EventHandler CurrencyValueChanged;

        int Currency { get; }
        void AddCurrency(int delta);
        void ReduceCurrency(int delta);
        bool HasCurrency(int amount);
    }
}
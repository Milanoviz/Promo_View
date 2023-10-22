using System.Collections.Generic;
using RedPanda.Project.Interfaces;

namespace RedPanda.Project.Services.Interfaces
{
    public interface IPromoService
    {
        IReadOnlyList<IPromoModel> GetPromos();
    }
}
using Grace.DependencyInjection;
using RedPanda.Project.Services;
using RedPanda.Project.Services.Factories;
using RedPanda.Project.Services.Interfaces;
using RedPanda.Project.Services.UI;
using RedPanda.Project.Services.UI.Lobby;
using RedPanda.Project.Services.UI.Promo;
using RedPanda.Project.Services.UI.Promo.Providers;
using UnityEngine;

namespace RedPanda.Project
{
    public sealed class Initializer : MonoBehaviour
    {
        private DependencyInjectionContainer _container = new();
        
        private void Awake()
        {
            _container.Configure(block =>
            {
                block.Export<UserService>().As<IUserService>().Lifestyle.Singleton();
                block.Export<PromoService>().As<IPromoService>().Lifestyle.Singleton();
                block.Export<UIService>().As<IUIService>().Lifestyle.Singleton();
                block.Export<ViewControlFactory>().As<IViewControlRegistrar>()
                                                  .As<IViewControlFactory>().Lifestyle.Singleton();
                block.Export<PromoContentProvider>().As<IPromoContentProvider>().Lifestyle.Singleton();
            });

            _container.Locate<IUserService>();
            _container.Locate<IPromoService>();
            
            RegisterViewControls();
            _container.Locate<IUIService>().Show("LobbyView");
        }

        private void RegisterViewControls()
        {
            _container.Locate<IViewControlRegistrar>().RegisterUIControl("LobbyView", (viewName, canvas) => new LobbyViewControl(viewName, canvas, _container));
            _container.Locate<IViewControlRegistrar>().RegisterUIControl("PromoView", (viewName, canvas) => new PromoViewControl(viewName, canvas, _container));
        }
    }
}
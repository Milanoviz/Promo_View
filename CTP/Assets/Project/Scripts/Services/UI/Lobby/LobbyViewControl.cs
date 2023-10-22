using System;
using Grace.DependencyInjection;
using RedPanda.Project.Services.Interfaces;
using RedPanda.Project.UI;
using UnityEngine;

namespace RedPanda.Project.Services.UI.Lobby
{
    public class LobbyViewControl : UIControl<LobbyView>
    {
        private IUIService _uiService;
        
        public LobbyViewControl(string viewName, GameObject parent, IExportLocatorScope exportLocatorScope) 
            : base(viewName, parent, exportLocatorScope)
        {
            _uiService = exportLocatorScope.Locate<IUIService>();
        }
        
        protected override void OnViewShown()
        {
            view.ShowPromoButtonClicked += ShowPromoButtonClickedHandler;
        }

        protected override void OnViewHided()
        {
            view.ShowPromoButtonClicked -= ShowPromoButtonClickedHandler;
        }
        
        private void ShowPromoButtonClickedHandler(object sender, EventArgs e)
        {
            _uiService.Show("PromoView");
        }
    }
}
using System.Collections.Generic;
using Grace.DependencyInjection;
using RedPanda.Project.Services.Factories;
using RedPanda.Project.Services.Interfaces;
using UnityEngine;

namespace RedPanda.Project.Services.UI
{
    public sealed class UIService : IUIService
    {
        private readonly IExportLocatorScope _container;
        private readonly IViewControlFactory _viewControlFactory;
        private readonly GameObject _canvas;

        private readonly Dictionary<string, IUIControl> _activeViewControls = new();

        public UIService(IExportLocatorScope container)
        {
            _container = container;
            _viewControlFactory = container.Locate<IViewControlFactory>();
            _canvas = Object.Instantiate(Resources.Load<GameObject>("UI/Canvas"));
            _canvas.name = "Canvas";
        }

        void IUIService.Show(string viewName)
        {
            var viewControl = _viewControlFactory.CreateViewControl(viewName, _canvas);
            _activeViewControls.Add(viewName, viewControl);
        }

        public void Close(string viewName)
        {
            if (_activeViewControls.TryGetValue(viewName, out var viewControl))
            {
                viewControl.Close();
                _activeViewControls.Remove(viewName);
            }
        }
    }
}
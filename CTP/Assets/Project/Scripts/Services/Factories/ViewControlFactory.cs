using System;
using System.Collections.Generic;
using RedPanda.Project.Services.UI;
using UnityEngine;

namespace RedPanda.Project.Services.Factories
{
    public class ViewControlFactory : IViewControlFactory, IViewControlRegistrar
    {
        private Dictionary<string, Func<string, GameObject, IUIControl>> _viewControlStorage = new();
        
        public void RegisterUIControl(string viewName, Func<string, GameObject, IUIControl> viewControlCreator)
        {
            _viewControlStorage[viewName] = viewControlCreator;
        }
        
        public IUIControl CreateViewControl(string viewName, GameObject canvas)
        {
            if (_viewControlStorage.TryGetValue(viewName, out var controllerCreator))
            {
                return controllerCreator(viewName, canvas);
            }
            
            Debug.LogError($"Unknown viewName: {viewName}");
            return null;
        }
    }
}
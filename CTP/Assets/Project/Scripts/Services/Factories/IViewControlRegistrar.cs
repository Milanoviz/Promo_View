using System;
using RedPanda.Project.Services.UI;
using UnityEngine;

namespace RedPanda.Project.Services.Factories
{
    public interface IViewControlRegistrar
    {
        void RegisterUIControl(string viewName, Func<string, GameObject, IUIControl> viewControlCreator);
    }
}
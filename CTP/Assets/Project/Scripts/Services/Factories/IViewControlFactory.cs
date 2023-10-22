using RedPanda.Project.Services.UI;
using UnityEngine;

namespace RedPanda.Project.Services.Factories
{
    public interface IViewControlFactory
    {
        IUIControl CreateViewControl(string viewName, GameObject canvas);
    }
}
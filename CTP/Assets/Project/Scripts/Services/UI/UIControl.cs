using System.Threading.Tasks;
using Grace.DependencyInjection;
using RedPanda.Project.UI;
using UnityEngine;

namespace RedPanda.Project.Services.UI
{
    public class UIControl<T> : IUIControl where T : View
    {
        private readonly IExportLocatorScope _exportLocatorScope;
        protected T view;

        private float _viewActionDuration = 1.0f;
        private Vector2 _viewShowDirection = new(0, -1f);
        private Vector2 _viewHideDirection = new(0, 1f);
        
        public UIControl(string viewName, GameObject parent, IExportLocatorScope exportLocatorScope)
        {
            _exportLocatorScope = exportLocatorScope;
            view = Object.Instantiate(Resources.Load<T>($"UI/{viewName}"), parent.transform);
            view.name = viewName;
            _exportLocatorScope.Inject(view);

            _ = ShowViewAsync();
        }

        public void Close()
        { 
            _ = HideViewAsync();
        }

        protected virtual void OnViewShown(){}
        protected virtual void OnViewHided(){}
        
        private async Task ShowViewAsync()
        {
            var startTime = Time.time;
            var rectTransform = view.GetComponent<RectTransform>();
            var canvasGroup = view.GetComponent<CanvasGroup>();
            var startPos = rectTransform.anchoredPosition;
            var endPos = startPos + _viewShowDirection * _viewActionDuration;

            rectTransform.anchoredPosition = endPos;
            canvasGroup.alpha = 0.0f;

            while (Time.time - startTime < _viewActionDuration)
            {
                var t = (Time.time - startTime) / _viewActionDuration;
                rectTransform.anchoredPosition = Vector2.Lerp(endPos, startPos, t);
                canvasGroup.alpha = t;
                await Task.Yield();
            }

            rectTransform.anchoredPosition = startPos;
            canvasGroup.alpha = 1.0f;
            
            OnViewShown();
        }
        
        private async Task HideViewAsync()
        {
            var startTime = Time.time;
            var rectTransform = view.GetComponent<RectTransform>();
            var canvasGroup = view.GetComponent<CanvasGroup>();
            var startPos = rectTransform.anchoredPosition;
            var endPos = startPos + _viewHideDirection * _viewActionDuration;

            while (Time.time - startTime < _viewActionDuration)
            {
                var t = (Time.time - startTime) / _viewActionDuration;
                rectTransform.anchoredPosition = Vector2.Lerp(startPos, endPos, t);
                canvasGroup.alpha = 1.0f - t;
                await Task.Yield();
            }

            rectTransform.anchoredPosition = endPos;
            canvasGroup.alpha = 0.0f;

            Object.Destroy(view.gameObject);
            OnViewHided();
        }
    }
}
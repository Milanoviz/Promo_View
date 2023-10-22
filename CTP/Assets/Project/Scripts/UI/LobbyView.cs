using System;
using UnityEngine;
using UnityEngine.UI;

namespace RedPanda.Project.UI
{
    public sealed class LobbyView : View
    { 
        public event EventHandler ShowPromoButtonClicked;

        [SerializeField] private Button _showPromoButton;

        private void OnEnable()
        {
            _showPromoButton.onClick.AddListener(ShowPromoButtonClickedHandler);
        }

        private void OnDisable()
        {
            _showPromoButton.onClick.RemoveListener(ShowPromoButtonClickedHandler);
        }

        private void ShowPromoButtonClickedHandler()
        {
            OnShowPromoButtonClicked();
        }

        private void OnShowPromoButtonClicked()
        {
            ShowPromoButtonClicked?.Invoke(this, EventArgs.Empty);
        }
    }
}
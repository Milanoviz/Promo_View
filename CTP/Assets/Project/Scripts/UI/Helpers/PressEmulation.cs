using UnityEngine;
using UnityEngine.EventSystems;

namespace RedPanda.Project.UI.Helpers
{
    public class PressEmulation : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        [SerializeField] private float _scaleMultiplier = 0.95f;
        
        private Vector3 _originalScale;
        
        private void Awake()
        {
            _originalScale = transform.localScale;
        }
        
        public void OnPointerDown(PointerEventData eventData)
        {
            transform.localScale = _originalScale * _scaleMultiplier;
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            transform.localScale = _originalScale;
        }
    }
}
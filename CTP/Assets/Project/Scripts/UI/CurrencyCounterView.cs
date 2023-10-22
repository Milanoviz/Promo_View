using TMPro;
using UnityEngine;

namespace RedPanda.Project.UI
{
    public class CurrencyCounterView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _counter;

        public void SetValue(int value)
        {
            _counter.SetText(value.ToString());
        }
    }
}
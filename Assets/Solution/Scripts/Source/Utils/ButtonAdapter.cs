using AreYouFruits.Disposables;
using AreYouFruits.Disposables.Unity;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Greg.Utils
{
    public abstract class ButtonAdapter : MonoBehaviour
    {
        [SerializeField] private Button button;
        
        private ActionDisposable<(UnityEvent, UnityAction)> subscription;
        
        private void Awake()
        {
            subscription = button.onClick.SubscribeAsDisposable(OnClick);
        }

        private void OnDestroy()
        {
            subscription.Dispose();
        }

        protected abstract void OnClick();
    }
}
using System;
using AreYouFruits.Disposables;
using UnityEngine.InputSystem;

namespace Greg.Utils.Extensions
{
    public static class InputActionExtensions
    {
        public static ActionDisposable SubscribePerformedAsDisposable(this InputAction inputAction, Action<InputAction.CallbackContext> handler)
        {
            inputAction.performed += handler;
            return ActionDisposable.Create(() => inputAction.performed -= handler);
        }
    }
}
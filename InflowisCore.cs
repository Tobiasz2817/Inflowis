using CoreUtility;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Inflowis {
    public static class InflowisCore {
        public static PlayerInput PlayerInput;
        internal static InflowisConfig Config;
        
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        static void Initialize() {
            PlayerInput = PersistentFactory.CreateComponent<PlayerInput>();
            PlayerInput.notificationBehavior = PlayerNotifications.InvokeCSharpEvents;
            PlayerInput.actions = Config.InputActionAsset;
            PlayerInput.defaultControlScheme = Config.DefaultScheme;
            PlayerInput.ActivateInput();
        }
    }
} 
using UnityEngine;
using UnityEngine.InputSystem;

namespace Inflowis {
    [CreateAssetMenu(menuName = "Content/Config/Inflows", fileName = "InflowsConfig")]
    public class InflowisConfig : ScriptableObject {
        [Space]
        [SerializeField]
        internal InputActionAsset InputActionAsset;
        
        [SerializeField]
        [ControlScheme(nameof(InputActionAsset))]
        internal string DefaultScheme;
    }
}
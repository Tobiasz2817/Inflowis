using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Inflowis {
    public static class InputExtension {
        public static string GetBind(this InputActionReference actionReference) {
            if (actionReference == null || actionReference.action == null) {
#if UNITY_EDITOR
                Debug.LogWarning("InputActionReference is null or does not reference a valid action.");
#endif
                return string.Empty;
            }

            var inputAction = actionReference.action;
            foreach (var binding in inputAction.bindings)
            {
                if (!binding.isComposite)
                {
                    var control = inputAction.controls.FirstOrDefault();
                    if (control == null) 
                        continue;

                    return control.shortDisplayName ?? control.displayName;
                }
                
                // Is Composite
                var compositeControls = inputAction.controls.
                    Select(control => control.shortDisplayName ?? control.displayName).
                    ToList();

                if (compositeControls.Count <= 0) 
                    continue;
                
                return string.Join("+", compositeControls);
            }

            return string.Empty;

        }
    }
}
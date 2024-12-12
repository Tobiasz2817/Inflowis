using System;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Inflowis {
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, Inherited = true, AllowMultiple = false)]
    public class ControlSchemeAttribute : PropertyAttribute {
        internal readonly string AssetName;
        public ControlSchemeAttribute(string assetName) {
            AssetName = assetName;
        }
    }
    
    [CustomPropertyDrawer(typeof(ControlSchemeAttribute))]
    public class ControlSchemeDrawer : PropertyDrawer {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {
            var target = (ControlSchemeAttribute)attribute;
            
            if (property.propertyType != SerializedPropertyType.String) {
                EditorGUI.LabelField(position, "ERROR:", "Type must be string type");
                return;
            }
            
            if (string.IsNullOrEmpty(target.AssetName)) {
                EditorGUI.LabelField(position, "ERROR:", "AssetName cannot be null or empty");
                return;
            }

            var inputAsset = property.serializedObject.targetObject?.GetType().
                GetFields(BindingFlags.Instance | BindingFlags.DeclaredOnly | BindingFlags.NonPublic | BindingFlags.Public).
                FirstOrDefault((f) => f.Name == target.AssetName);
            
            if (inputAsset == null) {
                EditorGUI.LabelField(position, "ERROR:", "Didn't find InputActionAsset by passed name");
                return;
            }
            
            var asset = inputAsset.GetValue(property.serializedObject.targetObject) as InputActionAsset;
            if (!asset) {
                EditorGUI.LabelField(position, "ERROR:", "Value type from passed name didn't compare to needed type (InputActionAsset)");
                return;
            }
            
            var names = asset.controlSchemes.
                Select((control) => control.name).
                ToArray();
            
            if (names.Length == 0) return;
            
            var selectedUnit = property.stringValue;
            var selectedIndex = Array.IndexOf(names, selectedUnit);
            if (selectedIndex == -1) selectedIndex = 0; 

            selectedIndex = EditorGUI.Popup(position, label.text, selectedIndex, names);
            property.stringValue = names[selectedIndex];
        }
    }
}
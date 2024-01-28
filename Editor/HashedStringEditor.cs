using UnityEditor;
using UnityEngine;

namespace Peg.ToolboxEditor
{
    /// <summary>
    /// Draws a HasheString as a text field and a label that displays the resulting hash.
    /// </summary>
    [CustomPropertyDrawer(typeof(HashedString), true)]
    public class HashedStringDrawer : PropertyDrawer
    {
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return (EditorGUIUtility.singleLineHeight * 2) + EditorGUIUtility.standardVerticalSpacing;
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginProperty(position, label, property);

            var valueProperty = property.FindPropertyRelative("_Value");
            string value = valueProperty.stringValue;

            Rect firstLine = EditorGUI.PrefixLabel(position, label);
            firstLine.height = EditorGUIUtility.singleLineHeight;

            EditorGUI.PropertyField(firstLine, valueProperty, GUIContent.none);

            Rect secondLine = firstLine;
            secondLine.y += EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing;

            EditorGUI.BeginDisabledGroup(true);
            EditorGUI.TextField(secondLine, "Hashed Value", HashedString.StringToHash(value).ToString());
            EditorGUI.EndDisabledGroup();

            EditorGUI.EndProperty();
        }

    }

}
using UnityEngine;
using UnityEditor;

[CustomPropertyDrawer(typeof(ReadOnlyAttribute))]
public class ReadOnlyDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty prop, GUIContent label)
    {
        var valueStr = prop.propertyType switch
        {
            SerializedPropertyType.Float => prop.floatValue.ToString("F3"),
            SerializedPropertyType.Integer => prop.intValue.ToString(),
            SerializedPropertyType.String => prop.stringValue,
            SerializedPropertyType.Boolean => prop.boolValue.ToString(),
            SerializedPropertyType.Enum => prop.enumNames[prop.enumValueIndex],
            _ => "(not supported)"
        };

        EditorGUI.LabelField(position, label.text, valueStr);
    }
}
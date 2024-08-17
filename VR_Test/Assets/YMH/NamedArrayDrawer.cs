using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(NamedArrayAttribute))]
public class NamedArrayDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        NamedArrayAttribute names = attribute as NamedArrayAttribute;

        int index = int.Parse(property.propertyPath.Split('[', ']')[1]);
        label.text = names.names[index];

        EditorGUI.PropertyField(position, property, label, true);
    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        return EditorGUI.GetPropertyHeight(property, label, true);
    }
}

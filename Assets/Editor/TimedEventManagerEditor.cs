using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using UnityEditorInternal;
using System.Linq;
using System;
using System.Reflection;
using System.Collections;


[CustomEditor(typeof(TimedEventManager))]
public class TimedEventManagerEditor : Editor
{
    private ReorderableList m_events;

    private void OnEnable()
    {
        m_events = new ReorderableList(serializedObject,
                serializedObject.FindProperty("EventEntries"),
                true, true, true, true);

        m_events.drawElementCallback = (Rect rect, int index, bool isActive, bool isFocused) =>
        {
            var element = m_events.serializedProperty.GetArrayElementAtIndex(index);
            rect.y += 2;

            var totalWidth = rect.width / 5;

            EditorGUI.PropertyField(
                new Rect(rect.x, rect.y, totalWidth, EditorGUIUtility.singleLineHeight),
                element.FindPropertyRelative("TimeOffset"), GUIContent.none);

            EditorGUI.PropertyField(
                new Rect(rect.x + totalWidth, rect.y, totalWidth, EditorGUIUtility.singleLineHeight),
                element.FindPropertyRelative("Observer"), GUIContent.none);

            if (element.FindPropertyRelative("Observer") != null)
            {
                object propObject = GetParent(element.FindPropertyRelative("Observer"));
                TimedEventEntry entry = propObject as TimedEventEntry;
                EditorGUI.LabelField(
                new Rect(rect.x + totalWidth * 2, rect.y, totalWidth, EditorGUIUtility.singleLineHeight),
                entry.Observer.ObserverName);

                //EditorGUI.PropertyField(
                //    new Rect(rect.x + totalWidth * 2, rect.y, totalWidth, EditorGUIUtility.singleLineHeight),
                //    element.FindPropertyRelative("Observer").FindPropertyRelative("ObserverName"), GUIContent.none);
                //var myClassProps = element.FindPropertyRelative("Observer").GetEndProperty();
                //Debug.Log(myClassProps.name);
                //while (myClassProps.NextVisible(true))
                //{
                //    Debug.Log(myClassProps.name);
                //    //if (myClassProps.name == "Observer")
                //    //{
                //    //    var myClassProps2 = myClassProps.GetArrayElementAtIndex(0);
                //    //    while (myClassProps2.NextVisible(true))
                //    //    {
                //    //        Debug.Log(myClassProps2.name);
                //    //    }
                //    //        //        EditorGUI.LabelField(
                //    //        //            new Rect(rect.x + totalWidth * 2, rect.y, totalWidth, EditorGUIUtility.singleLineHeight),
                //    //        //            myClassProps.FindPropertyRelative("ObserverName").stringValue);
                //    //}
                //}

            }

            EditorGUI.PropertyField(
                new Rect(rect.x + totalWidth * 3, rect.y, totalWidth, EditorGUIUtility.singleLineHeight),
                element.FindPropertyRelative("TypeOfEvent"), GUIContent.none);

            
            EditorGUI.PropertyField(
                new Rect(rect.x + totalWidth * 4, rect.y, totalWidth, EditorGUIUtility.singleLineHeight),
                element.FindPropertyRelative("TargetObject"), GUIContent.none); 
        };
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        m_events.DoLayoutList();
        serializedObject.ApplyModifiedProperties();
    }

    public object GetParent(SerializedProperty prop)
    {
        var path = prop.propertyPath.Replace(".Array.data[", "[");
        object obj = prop.serializedObject.targetObject;
        var elements = path.Split('.');
        foreach (var element in elements.Take(elements.Length - 1))
        {
            if (element.Contains("["))
            {
                var elementName = element.Substring(0, element.IndexOf("["));
                var index = Convert.ToInt32(element.Substring(element.IndexOf("[")).Replace("[", "").Replace("]", ""));
                obj = GetValue(obj, elementName, index);
            }
            else
            {
                obj = GetValue(obj, element);
            }
        }
        return obj;
    }

    public object GetValue(object source, string name)
    {
        if (source == null)
            return null;
        var type = source.GetType();
        var f = type.GetField(name, BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance);
        if (f == null)
        {
            var p = type.GetProperty(name, BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance | BindingFlags.IgnoreCase);
            if (p == null)
                return null;
            return p.GetValue(source, null);
        }
        return f.GetValue(source);
    }

    public object GetValue(object source, string name, int index)
    {
        var enumerable = GetValue(source, name) as IEnumerable;
        var enm = enumerable.GetEnumerator();
        while (index-- >= 0)
            enm.MoveNext();
        return enm.Current;
    }

}
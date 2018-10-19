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

        m_events.drawElementBackgroundCallback = (Rect rect, int index, bool isActive, bool isFocused) =>
        {
            /// Set background color based on name of observer object (todo: refactor this so it's not so ugly, and perhaps configurable from editor)
            var element = m_events.serializedProperty.GetArrayElementAtIndex(index);
            TimedEventEntry bgEntry = GetParent(element.FindPropertyRelative("Observer")) as TimedEventEntry;
            if (bgEntry != null && bgEntry.Observer != null)
            {
                if (isActive)
                {
                    EditorGUI.DrawRect(rect, Color.blue);
                }
                else if (bgEntry.Observer.ObserverName.Contains("Eagan"))
                {
                    EditorGUI.DrawRect(rect, Color.white);
                }
                else if (bgEntry.Observer.ObserverName.Contains("Kate"))
                {
                    EditorGUI.DrawRect(rect, Color.gray);
                }
                else
                {
                    EditorGUI.DrawRect(rect, Color.clear);
                }
            }
        };

        m_events.drawElementCallback = (Rect rect, int index, bool isActive, bool isFocused) =>
        {
            int spacing = 2;
            var element = m_events.serializedProperty.GetArrayElementAtIndex(index);
            rect.y += 2;

            float standardWidth = rect.width / 6f;

            float widthSoFar = rect.x;

            EditorGUI.PropertyField(
                new Rect(rect.x, rect.y, standardWidth/2f, EditorGUIUtility.singleLineHeight),
                element.FindPropertyRelative("TimeOffset"), GUIContent.none);

            widthSoFar += (standardWidth / 2f) + spacing;

            EditorGUI.PropertyField(
                new Rect(widthSoFar, rect.y, standardWidth*.75f, EditorGUIUtility.singleLineHeight),
                element.FindPropertyRelative("Observer"), GUIContent.none);

            widthSoFar += (standardWidth * .75f) + spacing;

            TimedEventEntry entry = GetParent(element.FindPropertyRelative("Observer")) as TimedEventEntry;

            if (entry != null && entry.Observer != null)
            {
                EditorGUI.LabelField(new Rect(widthSoFar, rect.y, standardWidth, EditorGUIUtility.singleLineHeight), entry.Observer.ObserverName);
                widthSoFar += (standardWidth) + spacing;
            }

            EditorGUI.PropertyField(
                new Rect(widthSoFar, rect.y, standardWidth * 1.5f, EditorGUIUtility.singleLineHeight),
                element.FindPropertyRelative("TypeOfEvent"), GUIContent.none);
            widthSoFar += (standardWidth * 1.5f) + spacing;

            if (entry != null && entry.TypeOfEvent == TimedEventType.LookAtObject) {
                EditorGUI.PropertyField(
                new Rect(widthSoFar, rect.y, standardWidth * .75f, EditorGUIUtility.singleLineHeight),
                element.FindPropertyRelative("TargetObject"), GUIContent.none);

                widthSoFar += (standardWidth * .75f) + spacing;

                if (entry != null && entry.TargetObject != null)
                {
                    EditorGUI.LabelField(new Rect(widthSoFar, rect.y, standardWidth, EditorGUIUtility.singleLineHeight), entry.TargetObject.TargetName);
                    widthSoFar += (standardWidth) + spacing;
                }
            }
        };
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        var lastElement = m_events.serializedProperty.GetArrayElementAtIndex(serializedObject.FindProperty("EventEntries").arraySize - 1);
        if (lastElement != null)
        {
            TimedEventEntry entry = GetParent(lastElement.FindPropertyRelative("Observer")) as TimedEventEntry;
            float timeOffset = entry != null ? entry .TimeOffset : 120;
            GUILayout.Label("Starting Time Offset: (" + serializedObject.FindProperty("StartingTimeOffset").floatValue.ToString() + "/" + timeOffset.ToString() + ")", GUILayout.Width(260));
            serializedObject.FindProperty("StartingTimeOffset").floatValue = GUILayout.HorizontalSlider(serializedObject.FindProperty("StartingTimeOffset").floatValue, 0f, timeOffset);
            GUILayout.Space(10);
        }
        GUILayout.Label("Timed Events:", GUILayout.Width(90));
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
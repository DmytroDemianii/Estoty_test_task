using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace Code.Gameplay.UnitStats.Behaviours.Editor
{
	[CustomEditor(typeof(Stats))]
	public class StatsInspector : UnityEditor.Editor
	{
		private Stats _stats;
		private Dictionary<StatType, float> _baseStats;
		private FieldInfo _baseStatsField;

		private void OnEnable()
		{
			_stats = (Stats) target;

			_baseStatsField = typeof(Stats).GetField("_baseStats", BindingFlags.NonPublic | BindingFlags.Instance);
			if (_baseStatsField != null)
			{
				_baseStats = (Dictionary<StatType, float>) _baseStatsField.GetValue(_stats);
			}
		}

		public override void OnInspectorGUI()
		{
			serializedObject.Update();

			if (_baseStats == null || _baseStatsField == null)
			{
				EditorGUILayout.HelpBox("Failed to access _baseStats dictionary via reflection.", MessageType.Error);
				DrawDefaultInspector();
				return;
			}

            EditorGUILayout.Space();
            EditorGUILayout.HelpBox("Base: Editable value | Total: Current value with modifiers", MessageType.Info);
            EditorGUILayout.Space();

            foreach (StatType statType in Enum.GetValues(typeof(StatType)))
            {
                if (statType == StatType.Unknown) continue;

                if (_baseStats.TryGetValue(statType, out float baseValue))
                {
                    float totalValue = _stats.GetStat(statType);
                    
                    EditorGUILayout.BeginHorizontal();
                    
                    EditorGUI.BeginChangeCheck();
                    float newBaseValue = EditorGUILayout.FloatField(statType.ToString(), baseValue);
                    if (EditorGUI.EndChangeCheck())
                    {
                        _stats.SetBaseStat(statType, newBaseValue);
                        EditorUtility.SetDirty(_stats);
                    }

                    GUI.enabled = false;
                    EditorGUILayout.LabelField($"→ Total: {totalValue:F1}", GUILayout.Width(100));
                    GUI.enabled = true;

                    EditorGUILayout.EndHorizontal();
                }
            }

            if (GUI.changed)
            {
                _baseStats = (Dictionary<StatType, float>)_baseStatsField.GetValue(_stats);
            }

            serializedObject.ApplyModifiedProperties();
            
            if (Application.isPlaying)
            {
                Repaint();
            }
        }
    }
}
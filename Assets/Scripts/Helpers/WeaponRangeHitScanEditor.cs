using System.Collections.Generic;
using Attacks;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;

namespace Helpers
{
    [CustomEditor(typeof(WeaponRangeHitScan))]
    [CanEditMultipleObjects]
    public class WeaponRangeHitScanEditor : Editor
    {
        private SerializedProperty _firingRateProp;
        private SerializedProperty _damageProp;
        private SerializedProperty _shootingRangeProp;

        private SerializedProperty _possibleHitLayerProp;
        private SerializedProperty _targetTagProp;

        private SerializedProperty _gunHitEffectProp;
        private SerializedProperty _hitEffectDurationProp;

        private SerializedProperty _useAmmoProp;
        private SerializedProperty _maxAmmoProp;
        private SerializedProperty _maxAmmoPerClipProp;
        private SerializedProperty _reloadTimeProp;

        private SerializedProperty _playerInputProp;

        private GameObject _gameObject;

        private void OnEnable()
        {
            _firingRateProp = serializedObject.FindProperty("firingRate");
            _damageProp = serializedObject.FindProperty("damage");
            _shootingRangeProp = serializedObject.FindProperty("shootingRange");

            _possibleHitLayerProp = serializedObject.FindProperty("possibleHitLayer");
            _targetTagProp = serializedObject.FindProperty("targetTag");

            _gunHitEffectProp = serializedObject.FindProperty("gunHitEffect");
            _hitEffectDurationProp = serializedObject.FindProperty("hitEffectDuration");

            _useAmmoProp = serializedObject.FindProperty("useAmmo");
            _maxAmmoProp = serializedObject.FindProperty("maxAmmo");
            _maxAmmoPerClipProp = serializedObject.FindProperty("maxAmmoPerClip");
            _reloadTimeProp = serializedObject.FindProperty("reloadTime");

            _playerInputProp = serializedObject.FindProperty("playerInput");
        }
        
        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            EditorGUILayout.LabelField("Basic Gun Config", EditorStyles.boldLabel);
            _firingRateProp.floatValue = EditorGUILayout.FloatField("Firing Rate", _firingRateProp.floatValue);
            _damageProp.intValue = EditorGUILayout.IntField("Damage", _damageProp.intValue);
            _shootingRangeProp.floatValue = EditorGUILayout.FloatField("Shooting Range", _shootingRangeProp.floatValue);
            EditorGUILayout.Space();

            EditorGUILayout.LabelField("Gun Physics Config", EditorStyles.boldLabel);
            _possibleHitLayerProp.intValue = LayerMaskField("Possible Hit Layer", _possibleHitLayerProp.intValue);
            _targetTagProp.stringValue = EditorGUILayout.TagField("Target Tag", _targetTagProp.stringValue);
            EditorGUILayout.Space();

            EditorGUILayout.LabelField("Hit Effect Config", EditorStyles.boldLabel);
            EditorGUILayout.PropertyField(_gunHitEffectProp, new GUIContent("Gun Hit Effect Prefab"));
            _hitEffectDurationProp.floatValue =
                EditorGUILayout.FloatField("Hit Effect Duration", _hitEffectDurationProp.floatValue);
            EditorGUILayout.Space();

            EditorGUILayout.LabelField("Ammo Config", EditorStyles.boldLabel);
            _useAmmoProp.boolValue = GUILayout.Toggle(_useAmmoProp.boolValue, "Use Ammo");
            if (_useAmmoProp.boolValue)
            {
                _maxAmmoProp.intValue = EditorGUILayout.IntField("Max Ammo", _maxAmmoProp.intValue);
                _maxAmmoPerClipProp.intValue =
                    EditorGUILayout.IntField("Max Ammo Per Clip", _maxAmmoPerClipProp.intValue);
                _reloadTimeProp.floatValue = EditorGUILayout.FloatField("Reload Time", _reloadTimeProp.floatValue);
            }
            EditorGUILayout.Space();

            EditorGUILayout.PropertyField(_playerInputProp, new GUIContent("Player Input"));

            serializedObject.ApplyModifiedProperties();
        }
        
        private LayerMask LayerMaskField(string label, LayerMask layerMask)
        {
            var layerNumbers = new List<int>();
            var layers = InternalEditorUtility.layers;
            
            foreach (var t in layers)
                layerNumbers.Add(LayerMask.NameToLayer(t));

            var maskWithoutEmpty = 0;
            for (var i = 0; i < layerNumbers.Count; i++)
            {
                if (((1 << layerNumbers[i]) & layerMask.value) != 0)
                    maskWithoutEmpty |= (1 << i);
            }

            maskWithoutEmpty = EditorGUILayout.MaskField(label, maskWithoutEmpty, layers);

            var mask = 0;
            for (int i = 0; i < layerNumbers.Count; i++)
            {
                if ((maskWithoutEmpty & (1 << i)) > 0)
                    mask |= (1 << layerNumbers[i]);
            }

            layerMask.value = mask;

            return layerMask;
        }
    }
}
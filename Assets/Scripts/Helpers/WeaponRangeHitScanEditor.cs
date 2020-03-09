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
        private SerializedProperty firingRateProp;
        private SerializedProperty damageProp;
        private SerializedProperty shootingRangeProp;

        private SerializedProperty possibleHitLayerProp;
        private SerializedProperty targetTagProp;

        private SerializedProperty gunHitEffectProp;
        private SerializedProperty hitEffectDurationProp;

        private SerializedProperty useAmmoProp;
        private SerializedProperty maxAmmoProp;
        private SerializedProperty maxAmmoPerClipProp;
        private SerializedProperty reloadTimeProp;

        private SerializedProperty playerInputProp;

        private GameObject gameObject;

        private void OnEnable()
        {
            firingRateProp = serializedObject.FindProperty("firingRate");
            damageProp = serializedObject.FindProperty("damage");
            shootingRangeProp = serializedObject.FindProperty("shootingRange");

            possibleHitLayerProp = serializedObject.FindProperty("possibleHitLayer");
            targetTagProp = serializedObject.FindProperty("targetTag");

            gunHitEffectProp = serializedObject.FindProperty("gunHitEffect");
            hitEffectDurationProp = serializedObject.FindProperty("hitEffectDuration");

            useAmmoProp = serializedObject.FindProperty("useAmmo");
            maxAmmoProp = serializedObject.FindProperty("maxAmmo");
            maxAmmoPerClipProp = serializedObject.FindProperty("maxAmmoPerClip");
            reloadTimeProp = serializedObject.FindProperty("reloadTime");

            playerInputProp = serializedObject.FindProperty("playerInput");
        }
        
        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            EditorGUILayout.LabelField("Basic Gun Config", EditorStyles.boldLabel);
            firingRateProp.floatValue = EditorGUILayout.FloatField("Firing Rate", firingRateProp.floatValue);
            damageProp.intValue = EditorGUILayout.IntField("Damage", damageProp.intValue);
            shootingRangeProp.floatValue = EditorGUILayout.FloatField("Shooting Range", shootingRangeProp.floatValue);
            EditorGUILayout.Space();

            EditorGUILayout.LabelField("Gun Physics Config", EditorStyles.boldLabel);
            possibleHitLayerProp.intValue = LayerMaskField("Possible Hit Layer", possibleHitLayerProp.intValue);
            targetTagProp.stringValue = EditorGUILayout.TagField("Target Tag", targetTagProp.stringValue);
            EditorGUILayout.Space();

            EditorGUILayout.LabelField("Hit Effect Config", EditorStyles.boldLabel);
            EditorGUILayout.PropertyField(gunHitEffectProp, new GUIContent("Gun Hit Effect Prefab"));
            hitEffectDurationProp.floatValue =
                EditorGUILayout.FloatField("Hit Effect Duration", hitEffectDurationProp.floatValue);
            EditorGUILayout.Space();

            EditorGUILayout.LabelField("Ammo Config", EditorStyles.boldLabel);
            useAmmoProp.boolValue = GUILayout.Toggle(useAmmoProp.boolValue, "Use Ammo");
            if (useAmmoProp.boolValue)
            {
                maxAmmoProp.intValue = EditorGUILayout.IntField("Max Ammo", maxAmmoProp.intValue);
                maxAmmoPerClipProp.intValue =
                    EditorGUILayout.IntField("Max Ammo Per Clip", maxAmmoPerClipProp.intValue);
                reloadTimeProp.floatValue = EditorGUILayout.FloatField("Reload Time", reloadTimeProp.floatValue);
            }
            EditorGUILayout.Space();

            EditorGUILayout.PropertyField(playerInputProp, new GUIContent("Player Input"));

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
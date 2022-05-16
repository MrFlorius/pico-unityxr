﻿// Copyright © 2015-2021 Pico Technology Co., Ltd. All Rights Reserved.

using UnityEditor;
using UnityEngine;
using Unity.XR.PXR;


namespace Unity.XR.PXR.Editor
{
    [CanEditMultipleObjects]
    [CustomEditor(typeof(PXR_OverLay))]
    public class PXR_OverLayEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            var guiContent = new GUIContent();
            foreach (PXR_OverLay overlayTarget in targets)
            {
                EditorGUILayout.LabelField("Overlay Display Order", EditorStyles.boldLabel);
                guiContent.text = "Overlay Type";
                overlayTarget.overlayType = (PXR_OverLay.OverlayType)EditorGUILayout.EnumPopup(guiContent, overlayTarget.overlayType);
                guiContent.text = "Layer Depth";
                overlayTarget.layerDepth = EditorGUILayout.IntField(guiContent, overlayTarget.layerDepth);

                EditorGUILayout.Separator();
                guiContent.text = "Overlay Shape";
                EditorGUILayout.LabelField(guiContent, EditorStyles.boldLabel);
                overlayTarget.overlayShape = (PXR_OverLay.OverlayShape)EditorGUILayout.EnumPopup(guiContent, overlayTarget.overlayShape);

                EditorGUILayout.Separator();
                EditorGUILayout.LabelField("Overlay Textures", EditorStyles.boldLabel);
                guiContent.text = "External Surface";
                overlayTarget.isExternalAndroidSurface = EditorGUILayout.Toggle(guiContent, overlayTarget.isExternalAndroidSurface);
                var labelControlRect = EditorGUILayout.GetControlRect();
                EditorGUI.LabelField(new Rect(labelControlRect.x, labelControlRect.y, labelControlRect.width / 2, labelControlRect.height), new GUIContent("Left Texture", "Texture used for the left eye"));
                EditorGUI.LabelField(new Rect(labelControlRect.x + labelControlRect.width / 2, labelControlRect.y, labelControlRect.width / 2, labelControlRect.height), new GUIContent("Right Texture", "Texture used for the right eye"));

                var textureControlRect = EditorGUILayout.GetControlRect(GUILayout.Height(64));
                overlayTarget.layerTextures[0] = (Texture)EditorGUI.ObjectField(new Rect(textureControlRect.x, textureControlRect.y, 64, textureControlRect.height), overlayTarget.layerTextures[0], typeof(Texture), false);
                overlayTarget.layerTextures[1] = (Texture)EditorGUI.ObjectField(new Rect(textureControlRect.x + textureControlRect.width / 2, textureControlRect.y, 64, textureControlRect.height), overlayTarget.layerTextures[1] != null ? overlayTarget.layerTextures[1] : overlayTarget.layerTextures[0], typeof(Texture), false);

                overlayTarget.isDynamic = EditorGUILayout.Toggle(new GUIContent("Dynamic Texture"), overlayTarget.isDynamic);

                EditorGUILayout.Separator();
                EditorGUILayout.LabelField("Color Scale And Offset", EditorStyles.boldLabel);
                guiContent.text = "Override Color Scale";
                overlayTarget.overrideColorScaleAndOffset = EditorGUILayout.Toggle(guiContent, overlayTarget.overrideColorScaleAndOffset);
                if (overlayTarget.overrideColorScaleAndOffset)
                {
                    guiContent.text = "Color Scale";
                    Vector4 colorScale = EditorGUILayout.Vector4Field(guiContent, overlayTarget.colorScale);

                    guiContent.text = "Color Offset";
                    Vector4 colorOffset = EditorGUILayout.Vector4Field(guiContent, overlayTarget.colorOffset);
                    overlayTarget.SetLayerColorScaleAndOffset(colorScale, colorOffset);
                }
            }

            if (GUI.changed)
            {
                UnityEditor.SceneManagement.EditorSceneManager.MarkSceneDirty(UnityEngine.SceneManagement.SceneManager.GetActiveScene());
            }
        }
    }
}


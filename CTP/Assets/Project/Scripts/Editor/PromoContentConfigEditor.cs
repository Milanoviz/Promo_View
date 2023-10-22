using RedPanda.Project.Services.UI.Promo.Config;
using RedPanda.Project.Services.UI.Promo.Data;
using UnityEditor;
using UnityEngine;

namespace Project.Scripts.Editor
{
    [CustomEditor(typeof(PromoContentConfig))]
    public class PromoContentConfigEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            EditorGUILayout.Space();
            if (GUILayout.Button("Refresh Reward Icons"))
            {
                RefreshConfig("rewardIcons", "rewardIconsFolder");
            }
            if (GUILayout.Button("Refresh Slot Backgrounds"))
            {
                RefreshConfig("slotBackgrounds", "slotBackgroundsFolder");
            }
        }
		
        private void RefreshConfig(string resourcesPropertyName, string folderPropertyName)
        {
            var resourcesProperty = serializedObject.FindProperty(resourcesPropertyName);
            var folderProperty = serializedObject.FindProperty(folderPropertyName);
			
            resourcesProperty.ClearArray();

            var folder = folderProperty.objectReferenceValue;
            if (folder == null)
            {
                Debug.LogError("ResourceFolder is null");
                return;
            }
			
            var folderPath = AssetDatabase.GetAssetPath(folderProperty.objectReferenceValue);
            var filter = "t:sprite";
            var assetPaths = FindFilePaths(filter, folderPath);

            for (var i = 0; i < assetPaths.Length; i++)
            {
                var index = i;
                var assetPath = assetPaths[index];
				
                var sprite = AssetDatabase.LoadAssetAtPath<Sprite>(assetPath);
                resourcesProperty.InsertArrayElementAtIndex(index);
                resourcesProperty.GetArrayElementAtIndex(index).objectReferenceValue = sprite;
            }

            serializedObject.ApplyModifiedProperties();
            EditorUtility.SetDirty(target);
            AssetDatabase.SaveAssetIfDirty(target);
        }
        
        private static string[] FindFilePaths(string unityEditorFilter, params string[] searchInFolders)
        {
            var paths = AssetDatabase.FindAssets(unityEditorFilter, searchInFolders);
            for (int i = 0; i < paths.Length; i++)
            {
                paths[i] = AssetDatabase.GUIDToAssetPath(paths[i]);
            }

            return paths;
        }
    }
}

/// Code by Victor Nunes - vhndev@outlook.com  - https://github.com/vhng ///

#if UNITY_EDITOR
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace Vhn
{
	    
        
	[CreateAssetMenu(fileName = "Build Scenes Preset", menuName = "Build Scenes Preset", order = 1)]
	public class ScenesPreset : ScriptableObject
	{
		public List<SceneAsset> scenes = new List<SceneAsset>();
	}
	
    public class EditorScenesPreset : EditorWindow
    {
	    public static ScenesPreset scenePreset = null;

	    // Add menu item named "My Window" to the Window menu
	    [MenuItem("Tools/Build Scenes Preset Loader")]
	    public static void ShowWindow()
	    {
		    //Show existing window instance. If one doesn't exist, make one.
		    EditorWindow.GetWindow(typeof(EditorScenesPreset), false, "Build Scenes Preset Loader");
	    }
	    private void OnGUI()
	    {
		    scenePreset = (ScenesPreset) EditorGUILayout.ObjectField("Preset", 
			    scenePreset, typeof(Object), false);

		    GUILayout.Space(20f);
		    
		    if (scenePreset != null)
				DrawSceneValues();
			
		    GUILayout.Space(20f);
		    if (GUILayout.Button("Set on Build Settings"))
			    SetScenesOnBuildSettings();
	    }
	    private void SetScenesOnBuildSettings()
	    {
		    List<EditorBuildSettingsScene> newSceneBuildSettings = new List<EditorBuildSettingsScene>();

		    foreach (var scene in scenePreset.scenes)
		    {
			    var scenePath = AssetDatabase.GetAssetPath(scene);
			    newSceneBuildSettings.Add(new EditorBuildSettingsScene(scenePath, true));
		    }

		    EditorBuildSettings.scenes = newSceneBuildSettings.ToArray();


		    EditorUtility.DisplayDialog(
			    "Sucess",
			    $"Settings from {scenePreset.name} was applied to the editor.", "Ok");
	    }
	    private void DrawSceneValues()
	    {
		    GUILayout.Label("Scenes on preset:");
		    for (int i = 0; i < scenePreset.scenes.Count; i++)
		    {
			    var currentScene = scenePreset.scenes[i];
			    //EditorGUILayout.HelpBox(currentScene.name, MessageType.None);
			    currentScene = (SceneAsset) EditorGUILayout.ObjectField(currentScene, typeof(SceneAsset),
				    false);
		    }
	    }
    }
}

#endif
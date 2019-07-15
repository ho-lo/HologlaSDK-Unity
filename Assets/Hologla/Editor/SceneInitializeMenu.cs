using Hologla;
using System.IO;
using UnityEditor;
using UnityEditor.Build;
using UnityEngine;
#if UNITY_IOS
using UnityEngine.XR.iOS;
#endif
using static UnityEditor.AssetDatabase;
using static UnityEditor.PrefabUtility;

public class SceneInitializeMenu
{
	const string HOLOGLA_CAMERA_PARENT_PATH = "Assets/Hologla/Prefabs/HologlaCameraParent.prefab";
	const string HOLOGLA_INPUT_PATH = "Assets/Hologla/Prefabs/HologlaInput.prefab";
	const string PLAYMENU_PATH = "Assets/Hologla/Prefabs/Samples/PlayMenu.prefab";
	const string HOLOGLA_YUV_MATERIAL_PATH = "Assets/Hologla/Materials/HologlaYUVMaterial.mat";

	[MenuItem("Hologla/Initialize Project with ARKit")]
	static void InitProjectForARKit()
	{
		//ARKitプラグインがないままぷiOSプラットフォームにするとスクリプトにエラーが出るので、最初にプラグインの有無をチェックする.
		if (false == Directory.Exists(Application.dataPath + "/UnityARKitPlugin"))
		{
			EditorUtility.DisplayDialog("エラー", "ARKitのプラグインがインポートされていないため、処理を中断しました。", "OK");

			return;
		}

		//プラットフォームがiOSになっていない場合は切り替える.
#if !UNITY_IOS
		EditorUserBuildSettings.SwitchActiveBuildTargetAsync(BuildTargetGroup.iOS, BuildTarget.iOS);
#endif

		return;
	}

	[MenuItem("Hologla/Initialize Scene with ARKit")]
	static void InitSceneForARKit()
	{
		//ARKitプラグインがないままぷiOSプラットフォームにするとスクリプトにエラーが出るので、最初にプラグインの有無をチェックする.
		if (false == Directory.Exists(Application.dataPath + "/UnityARKitPlugin"))
		{
			EditorUtility.DisplayDialog("エラー", "ARKitのプラグインがインポートされていないため、処理を中断しました。", "OK");

			return;
		}

		//デフォルトのMainCameraをDeactivate
		if (Camera.main != null)
		{
			Undo.RecordObject(Camera.main.gameObject, "Deactivate Camera");
			Camera.main.gameObject.SetActive(false);
		}

		//Hologlaのオブジェクト群を配置
		GameObject hologlaCameraParentRoot = InstantiatePrefab(LoadAssetAtPath<GameObject>(HOLOGLA_CAMERA_PARENT_PATH)) as GameObject;
		Undo.RegisterCreatedObjectUndo(hologlaCameraParentRoot, "Create " + hologlaCameraParentRoot.name);
		GameObject hologlaInput = InstantiatePrefab(LoadAssetAtPath<GameObject>(HOLOGLA_INPUT_PATH)) as GameObject;
		Undo.RegisterCreatedObjectUndo(hologlaInput, "Create " + hologlaInput.name);
//		var playMenu = InstantiatePrefab(LoadAssetAtPath<GameObject>(PLAYMENU_PATH)) as GameObject;
//		Undo.RegisterCreatedObjectUndo(playMenu, "Create " + playMenu.name);

		//ルートのHologlaCameraParentの子のHologlaCameraを取得(HologlaCameraManagerが付いてる方).
		GameObject hologlaCameraParent = hologlaCameraParentRoot.transform.Find("HologlaCamera").gameObject;

		//ARCameraManagerを配置
#if UNITY_IOS
        GameObject arCameraManager = new GameObject("ARCameraManager");
        UnityARCameraManager unityARCameraManager = arCameraManager.AddComponent<UnityARCameraManager>( );
		unityARCameraManager.m_camera = hologlaCameraParent.GetComponent<Camera>( );
        unityARCameraManager.planeDetection = UnityARPlaneDetection.Horizontal;
		Undo.RegisterCreatedObjectUndo(arCameraManager, "Create " + arCameraManager.name);
#endif

		//HologlaCameraManagerのArBackgroundMaterialにHologlaYUVMaterialを設定
		Material hologlaYuvMaterial = LoadAssetAtPath<Material>(HOLOGLA_YUV_MATERIAL_PATH);
		HologlaCameraManager hologlaCameraManager = hologlaCameraParent.GetComponent<HologlaCameraManager>();
		hologlaCameraManager.arBackgroundMaterial = hologlaYuvMaterial;

		return;
	}


}
using Hologla;
using System.IO;
using UnityEditor;
using UnityEditor.Build;
using UnityEditor.Events;
using UnityEngine;
#if UNITY_IOS && false
using UnityEngine.XR.iOS;
#elif UNITY_ANDROID
using UnityEngine.SpatialTracking;
using UnityEngine.UI;
#endif
using static UnityEditor.AssetDatabase;
using static UnityEditor.PrefabUtility;
using System.Linq;

public class SceneInitializeMenu
{
	const string HOLOGLA_CAMERA_PARENT_PATH = "Assets/Hologla/Prefabs/HologlaCameraParent.prefab";
	const string HOLOGLA_INPUT_PATH = "Assets/Hologla/Prefabs/HologlaInput.prefab";
	const string PLAYMENU_PATH = "Assets/Hologla/Prefabs/Samples/PlayMenu.prefab";

	const string AR_KIT_CAMERA_USAGE_DESCRIPTION = "ARKit";

	[MenuItem("Hologla/Initialize Project")]
	static void InitProject()
	{
		//====================iOS用====================.
		//カメラを使用するための表記が設定されていない場合は適当に設定.
		if( 0 == PlayerSettings.iOS.cameraUsageDescription.Length ){
			PlayerSettings.iOS.cameraUsageDescription = AR_KIT_CAMERA_USAGE_DESCRIPTION;
		}
		//iOSの最小ターゲットバージョンをARKit用に設定する.
		float iosVersion = float.Parse(PlayerSettings.iOS.targetOSVersionString);
		if( 11.0f > iosVersion ){
			PlayerSettings.iOS.targetOSVersionString = "11.0";
		}
		//iOSを64bitに設定.
		//0 - None, 1 - ARM64, 2 - Universal..
		if( 1 != PlayerSettings.GetArchitecture(BuildTargetGroup.iOS) ){
			PlayerSettings.SetArchitecture(BuildTargetGroup.iOS, 1);
		}

		//====================Android用====================.
		//AndroidのMinSDKVersionが低い場合はARCore向けに対応しているバージョンにする.
		if( AndroidSdkVersions.AndroidApiLevel24 > PlayerSettings.Android.minSdkVersion ){
			PlayerSettings.Android.minSdkVersion = AndroidSdkVersions.AndroidApiLevel24;
		}
		//ARCore向けにVulkenを使用しないようにする.
		if( true == PlayerSettings.GetGraphicsAPIs(BuildTarget.Android).Contains(UnityEngine.Rendering.GraphicsDeviceType.Vulkan) ) {
			UnityEngine.Rendering.GraphicsDeviceType[] graphicsDeviceTypeArray = new UnityEngine.Rendering.GraphicsDeviceType[1] { UnityEngine.Rendering.GraphicsDeviceType.OpenGLES3 };
			PlayerSettings.SetGraphicsAPIs(BuildTarget.Android, graphicsDeviceTypeArray);
		}
		//ARM64向けにビルドさせるためにScriptBackendをIL2CPPにする.
		if( ScriptingImplementation.IL2CPP != PlayerSettings.GetScriptingBackend(BuildTargetGroup.Android) ) {
			PlayerSettings.SetScriptingBackend(BuildTargetGroup.Android, ScriptingImplementation.IL2CPP);
		}
		//ARM64向けにビルドさせる設定にする.
		if( 0 == (PlayerSettings.Android.targetArchitectures & AndroidArchitecture.ARM64) ){
			PlayerSettings.Android.targetArchitectures = AndroidArchitecture.ARM64;
		}

		return;
	}

	[MenuItem("Hologla/Initialize Scene")]
	static void InitScene()
	{
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

		HologlaCameraManager hologlaCameraManager = hologlaCameraParent.GetComponent<HologlaCameraManager>();

		ApplyHologlaInputSetting(hologlaCameraManager, hologlaInput.GetComponent<HologlaInput>( ));

		//AR機能利用用にARSession用コンポーネントの有無を確認し、ない場合は生成する.
		if( 0 == GameObject.FindObjectsOfType<UnityEngine.XR.ARFoundation.ARSession>().Length ){
			//ARSession用オブジェクトを生成する(4.1.5現在、メニューから追加できるGameObjectのARSessionと同じもの).
			ObjectFactory.CreateGameObject("AR Session", typeof(UnityEngine.XR.ARFoundation.ARSession), typeof(UnityEngine.XR.ARFoundation.ARInputManager));
		}

		return;
	}

	//HologlaInputに初期の関連づけを行う.
	static void ApplyHologlaInputSetting(HologlaCameraManager hologlaCameraManager, HologlaInput hologlaInput)
	{
		GazeInput gazeInput ;

		gazeInput = hologlaCameraManager.GetComponent<GazeInput>( );
		UnityEventTools.RemovePersistentListener(hologlaInput.onPressLeftAndRight, gazeInput.InputLeftAndRightEvent);
		UnityEventTools.AddPersistentListener(hologlaInput.onPressLeftAndRight, gazeInput.InputLeftAndRightEvent);
		UnityEventTools.RemovePersistentListener(hologlaInput.LeftButtonComp.onClick, gazeInput.InputLeftEvent);
		UnityEventTools.AddPersistentListener(hologlaInput.LeftButtonComp.onClick, gazeInput.InputLeftEvent);
		UnityEventTools.RemovePersistentListener(hologlaInput.RightButtonComp.onClick, gazeInput.InputRightEvent);
		UnityEventTools.AddPersistentListener(hologlaInput.RightButtonComp.onClick, gazeInput.InputRightEvent);

		return;
	}

}
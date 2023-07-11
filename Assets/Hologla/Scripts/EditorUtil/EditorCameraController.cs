using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// エディタ上での確認用にキーボード操作でカメラを移動、回転させるためのクラス.
// W、A、S、D、Q、E、のキーでそれぞれ、前、左、後ろ、右、下、上方向にそれぞれ平行移動、各種矢印キー、対応した方向を向くように回転、Rキーで位置と向きのリセットが可能.
public class EditorCameraController : MonoBehaviour
{
#if UNITY_EDITOR
	// 動かす対象となるオブジェクト(nullのままであれば、自身が自動的に設定される).
	[SerializeField]private GameObject targetCameraObject = null;

	// 1秒あたりに平行移動する距離(メートル単位).
	[SerializeField]private float movementMeterPerSec = 0.5f;
	// 1秒あたりに回転する角度.
	[SerializeField]private float rotationAnglePerSec = 30.0f;

	// カメラ回転時に回転の向きを常にワールドの方向に対して行うようにするフラグ.
	[SerializeField]private bool isRotationWorld = true;

	private Vector3 initialPosition = Vector3.zero;
	private Quaternion initialRotation = Quaternion.identity;

    // Update is called once per frame
    void Update( )
    {
		if( null == targetCameraObject ){
			return;
		}
		// キーボード入力による平行移動制御.
		MoveCameraObject( );
		// キーボード入力による回転制御.
		RotateCameraObject( );
		// Rキー入力で位置と向きをリセットする.
		ResetCameraObjectTransform( );

		return;
    }

	private void Awake( )
	{
		initialPosition = transform.position;
		initialRotation = transform.rotation;

		if( null == targetCameraObject ){
			targetCameraObject = gameObject;
		}

		return;
	}

	private void MoveCameraObject( )
	{
		float movementSpeed = 0.0f;

		movementSpeed = movementMeterPerSec * Time.deltaTime;
		if( Input.GetKey(KeyCode.W) ){
			targetCameraObject.transform.position += (targetCameraObject.transform.forward * movementSpeed);
		}
		if( Input.GetKey(KeyCode.S) ){
			targetCameraObject.transform.position += (targetCameraObject.transform.forward * -1.0f * movementSpeed);
		}
		if( Input.GetKey(KeyCode.A) ){
			targetCameraObject.transform.position += (targetCameraObject.transform.right * -1.0f * movementSpeed);
		}
		if( Input.GetKey(KeyCode.D) ){
			targetCameraObject.transform.position += (targetCameraObject.transform.right * movementSpeed);
		}
		if( Input.GetKey(KeyCode.Q) ){
			targetCameraObject.transform.position += (targetCameraObject.transform.up * -1.0f * movementSpeed);
		}
		if( Input.GetKey(KeyCode.E) ){
			targetCameraObject.transform.position += (targetCameraObject.transform.up * movementSpeed);
		}

		return;
	}

	private void RotateCameraObject( )
	{
		float rotationAngle = 0.0f;

		rotationAngle = rotationAnglePerSec * Time.deltaTime;
		if( Input.GetKey(KeyCode.UpArrow) ){
			if( isRotationWorld == false ){
				targetCameraObject.transform.rotation *= Quaternion.AngleAxis(rotationAngle * -1.0f, targetCameraObject.transform.right);
			}
			else{
				targetCameraObject.transform.eulerAngles += Quaternion.AngleAxis(rotationAngle * -1.0f, Vector3.right).eulerAngles;
			}
		}
		if( Input.GetKey(KeyCode.DownArrow) ){
			if( isRotationWorld == false ){
				targetCameraObject.transform.rotation *= Quaternion.AngleAxis(rotationAngle, targetCameraObject.transform.right);
			}
			else{
				targetCameraObject.transform.eulerAngles += Quaternion.AngleAxis(rotationAngle, Vector3.right).eulerAngles;
			}
		}
		if( Input.GetKey(KeyCode.LeftArrow) ){
			if( isRotationWorld == false ){
				targetCameraObject.transform.rotation *= Quaternion.AngleAxis(rotationAngle * -1.0f, targetCameraObject.transform.up);
			}
			else{
				targetCameraObject.transform.eulerAngles += Quaternion.AngleAxis(rotationAngle * -1.0f, Vector3.up).eulerAngles;
			}
		}
		if( Input.GetKey(KeyCode.RightArrow) ){
			if( isRotationWorld == false ){
				targetCameraObject.transform.rotation *= Quaternion.AngleAxis(rotationAngle, targetCameraObject.transform.up);
			}
			else{
				targetCameraObject.transform.eulerAngles += Quaternion.AngleAxis(rotationAngle, Vector3.up).eulerAngles;
			}
		}

		return;
	}

	private void ResetCameraObjectTransform( )
	{
		if( Input.GetKeyDown(KeyCode.R) ){
			targetCameraObject.transform.position = initialPosition;
			targetCameraObject.transform.rotation = initialRotation;
		}

		return;
	}

// #if UNITY_EDITOR.
#endif
}

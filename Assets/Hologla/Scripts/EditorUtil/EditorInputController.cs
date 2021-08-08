using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

// エディタ上での確認用に、入力操作をキー操作で行えるようにするためのクラス.
// デフォルトではZキーでだんグラの左ボタン、Xキーでだんグラの右ボタンの入力を行える.
public class EditorInputController : MonoBehaviour
{
#if UNITY_EDITOR
	// 入力を与える対象となるHologlaInputクラス.
	[SerializeField]private Hologla.HologlaInput targetHologlaInput = null;

	// だんグラの左ボタンに対応するキー設定.
	[SerializeField]private KeyCode leftButtonKey = KeyCode.Z;
	// だんグラの右ボタンに対応するキー設定.
	[SerializeField]private KeyCode rightButtonKey = KeyCode.X;

    // Update is called once per frame
    void Update()
    {
		if( null == targetHologlaInput ){
			return;
		}
		PointerEventData eventData;

		eventData = new PointerEventData(EventSystem.current);
		eventData.button = PointerEventData.InputButton.Left;

		// だんグラの左ボタンに対応するキー(デフォルトZキー)を押した際に、左ボタン用のオブジェクトに押した時用のイベントを飛ばす.
		if( Input.GetKeyDown(leftButtonKey) ){
			ExecuteEvents.Execute<IPointerDownHandler>(targetHologlaInput.LeftButtonComp.gameObject, eventData, (handler, eventDataArg) =>
			{
				handler.OnPointerDown((PointerEventData)eventDataArg);
			});
		}
		// だんグラの左ボタンに対応するキー(デフォルトZキー)を離した際に、左ボタン用のオブジェクトに離した時用のイベントを飛ばす.
		if( Input.GetKeyUp(leftButtonKey) ){
			ExecuteEvents.Execute<IPointerUpHandler>(targetHologlaInput.LeftButtonComp.gameObject, eventData, (handler, eventDataArg) =>
			{
				handler.OnPointerUp((PointerEventData)eventDataArg);
			});
			targetHologlaInput.LeftButtonComp.OnPointerClick(eventData);
		}
		// だんグラの右ボタンに対応するキー(デフォルトXキー)を押した際に、右ボタン用のオブジェクトに押した時用のイベントを飛ばす.
		if( Input.GetKeyDown(rightButtonKey) ){
			ExecuteEvents.Execute<IPointerDownHandler>(targetHologlaInput.RightButtonComp.gameObject, eventData, (handler, eventDataArg) =>
			{
				handler.OnPointerDown((PointerEventData)eventDataArg);
			});
		}
		// だんグラの右ボタンに対応するキー(デフォルトXキー)を離した際に、右ボタン用のオブジェクトに離した時用のイベントを飛ばす.
		if( Input.GetKeyUp(rightButtonKey) ){
			ExecuteEvents.Execute<IPointerUpHandler>(targetHologlaInput.RightButtonComp.gameObject, eventData, (handler, eventDataArg) =>
			{
				handler.OnPointerUp((PointerEventData)eventDataArg);
			});
			targetHologlaInput.RightButtonComp.OnPointerClick(eventData);
		}

		return;
    }

	private void Awake( )
	{
		if( null == targetHologlaInput ){
			targetHologlaInput = GetComponent<Hologla.HologlaInput>( );
		}

		return;
	}

	// #if UNITY_EDITOR.
#endif
}

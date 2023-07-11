using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.Events;


namespace Hologla{

	public enum ClickType{
		LeftClick,
		RightClick,
		LeftAndRightClick,
	};

	public interface IGazeInteract{
		void OnSelect( );
		void OnDeselect( );
		void OnClick(ClickType clickType);
	}

	public class GazeInteractive : MonoBehaviour, IGazeInteract{

		// 視線中央のカーソルで選択された際に呼び出されるイベント.
		[SerializeField]private UnityEvent onGazeSelect = new UnityEvent( );
		// 視線中央のカーソルでの選択が解除された際に呼び出されるイベント.
		[SerializeField]private UnityEvent onGazeDeselect = new UnityEvent( );
		// 左ボタンがクリックされた際に呼び出されるイベント.
		[SerializeField]private UnityEvent onLeftClick = new UnityEvent( );
		// 右ボタンがクリックされた際に呼び出されるイベント.
		[SerializeField]private UnityEvent onRightClick = new UnityEvent( );
		// 左右同時に押された際に呼び出されるイベント.
		[SerializeField]private UnityEvent onLeftAndRightClick = new UnityEvent( );
		// 左右どちらかのボタンがクリックされた際に呼び出されるイベント.
		[SerializeField]private UnityEvent onAnyClick = new UnityEvent( );
		// 視線中央のカーソルで選択され続けている際に呼び出されるイベント.
		[SerializeField]private UnityEvent onKeepSelect = new UnityEvent( );

		// 選択中に表示されるフレーム用のオブジェクト.
		[SerializeField]private GameObject selectFrame = null;
		public GameObject SelectFrame { get => selectFrame; set => selectFrame = value; }

		// 選択され続けている際にイベントを呼び出すかどうか.
		[SerializeField]private bool isCallKeepSelectEvent = false;
		public bool IsCallKeepSelectEvent
		{
			get => isCallKeepSelectEvent;
			set{
				isCallKeepSelectEvent = value;
				if( true == isCallKeepSelectEvent && true == isSelect ){
					StartKeepSelectEventMonitor( );
				}
			}
		}

		// 選択され続けている際にイベントを呼び出す場合に、何秒ごとにイベントを呼び出すか(0で毎フレーム).
		[SerializeField]private float keepSelectEventIntervalSec = 0.0f;
		public float KeepSelectEventIntervalSec { get => keepSelectEventIntervalSec; set => keepSelectEventIntervalSec = value; }

		private bool isSelect = false;
		private IEnumerator keepSelectEventCallMonitor = null;

		// Use this for initialization
		void Start( )
		{
			return;
		}
	
		// Update is called once per frame
		void Update( )
		{
			return;
		}

		public void OnClick(ClickType clickType)
		{
			switch( clickType ){
				case ClickType.LeftClick:
					onLeftClick.Invoke( );
					break;
				case ClickType.RightClick:
					onRightClick.Invoke( );
					break;
				case ClickType.LeftAndRightClick:
					onLeftAndRightClick.Invoke( );
					break;
			}
			onAnyClick.Invoke( );

			return;
		}

		public void OnSelect( )
		{
			isSelect = true;

			onGazeSelect.Invoke( );
			if( true == IsCallKeepSelectEvent ){
				StartKeepSelectEventMonitor( );
			}

			return;
		}

		public void OnDeselect( )
		{
			isSelect = false;

			onGazeDeselect.Invoke( );

			EndKeepSelectEventMonitor( );

			return;
		}

		public void SwitchSelectFrame(bool isValid)
		{
			if( null != selectFrame ){
				selectFrame.SetActive(isValid);
			}

			return;
		}

		// 選択され続けている際に呼び出すイベントの監視開始処理.
		private void StartKeepSelectEventMonitor( )
		{
			if( null != keepSelectEventCallMonitor ){
				return;
			}
			keepSelectEventCallMonitor = MonitorKeepSelectEventCall( );
			StartCoroutine(keepSelectEventCallMonitor);

			return;
		}

		private void EndKeepSelectEventMonitor( )
		{
			if( null == keepSelectEventCallMonitor ){
				return;
			}
			StopCoroutine(keepSelectEventCallMonitor);
			keepSelectEventCallMonitor = null;

			return;
		}

		private IEnumerator MonitorKeepSelectEventCall()
		{
			while( true == isCallKeepSelectEvent && true == isSelect){
				yield return new WaitForSeconds(keepSelectEventIntervalSec);
				if( true == isCallKeepSelectEvent ){
					onKeepSelect.Invoke( );
				}
			}
		}

	}


}

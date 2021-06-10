using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.Events;


namespace Hologla{

	// 親階層にGazeInteractiveコンポーネントがある場合、その各イベントを呼ぶ.
	public class GazeInteractiveChild : MonoBehaviour, IGazeInteract
	{
		[SerializeField]private IGazeInteract parentGazeInteractive = null;

		private void Awake( )
		{
			if( null == parentGazeInteractive ){
				parentGazeInteractive = transform.parent.GetComponent<IGazeInteract>( );
			}

			return;
		}

		public void OnClick(ClickType clickType)
		{
			if( null != parentGazeInteractive ){
				parentGazeInteractive.OnClick(clickType);
			}
			return;
		}

		public void OnDeselect( )
		{
			if( null != parentGazeInteractive ){
				parentGazeInteractive.OnDeselect( );
			}
			return;
		}

		public void OnSelect( )
		{
			if( null != parentGazeInteractive ){
				parentGazeInteractive.OnSelect( );
			}
			return;
		}
	}

}

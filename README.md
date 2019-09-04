# HologlaSDK-Unity

## ライセンスについて
MITライセンスになります。
Assets/Hologla/Lisence.txtをご確認ください。

## プラグインの動作環境について
Unity2017.x以降のバージョンを想定しています。  
また、基本的にARKit、またはARCoreを利用してiOS、Android端末にて動作させることを想定しているため、iOS、またはAndroidプラットフォームの際に対応したARKit/ARCoreのプラグインをインポートしているプロジェクトデータではないと正常に動作しません。  
- 動作確認済みARKitのUntyプラグイン
https://bitbucket.org/Unity-Technologies/unity-arkit-plugin/src/default/
- 動作確認済みARCoreのUnityプラグイン
https://github.com/google-ar/arcore-unity-sdk

※現在はベータ版です、内容は予告なく変更されることがあります。

## 導入手順について
Unityプロジェクトを開く、または既存のプロジェクトにunitypackageをインポートします。

#### iOS
ARKitのUnityプラグインがプロジェクトにインポートされていない場合はインポートします。

Unityの上部メニューの[Hologla]から[Initialize Project with ARKit]と[Initialize Scene with ARKit]を順番に選択し、実行します。

**※もし「HologlaCamera」にある「HologlaCameraManager」コンポーネントの「ArBackgroundMaterial」がNone場合は、  
「HologlaCamera」にある「HologlaCameraManager」コンポーネントの「ArBackgroundMaterial」に「Hologla」→「Material」フォルダ内の「HologlaYUVMaterial」を設定してください。
(Unityのバージョン等によって正常に設定されないことがあります。)**

以上の設定で画面表示、入力関連のセットアップは完了となります。

#### Android
ARCoreのUnityプラグインがプロジェクトにインポートされていない場合はインポートします。

Unityの上部メニューの[Hologla]から[Initialize Project with ARCore]と[Initialize Scene with ARCore]を順番に選択し、実行します。

**※もし「HologlaCamera」にある「HologlaCameraManager」コンポーネントの「ArBackgroundMaterial」がNone場合は、  
「HologlaCamera」にある「HologlaCameraManager」コンポーネントの「ArBackgroundMaterial」に「GoogleARCore」→「SDK」→「Materials」フォルダ内の「ARBackground」を設定してください。
(Unityのバージョン等によって正常に設定されないことがあります。)**
<br>
**※Unity2018.1.x以前のバージョンを使用している場合は、別途AndroidのPlayerSettingsから「XRSettings」の「ARCoreSupported」にチェックを入れる必要があります。**

以上の設定で画面表示、入力関連のセットアップは完了となります。


## 各種Prefabについて
- HologlaCameraParent

だんグラの表示に関する制御を行っています。  
HologlaCameraManagerコンポーネントより、AR/MR/VR、1眼、2眼等の切り替えの設定や端末サイズ対応用の設定が行えます。  
**以下インスペクター上での項目の概要**

| インスペクター上での項目名 | 概要 |
|-----|-----|
| CurrentViewMode | AR/MR/VRモードの設定 |
| CurrentEyeMode | 1眼/2眼モードの設定 |
| VrBackgroundLayer | VRモード時の背景用にVRモード時のみに表示するLayerの設定 |
| VrBackgroundObjList | VRモード時の背景用にVRモード時のみに表示するオブジェクトリストの設定 |
| VrClearFlag | VRモード時のUnityのカメラのClearFlagsの設定 |
| InterpupillaryDistance | 瞳孔間距離の設定 |
| ArMrCollisionObjList | AR/MRモード時のみ有効にしたい現実ベースのコリジョンを持ったオブジェクトを設定する |
| CurrentViewSize | 端末サイズ対応用の表示領域サイズの設定|
| NearClippingPlane | UnityのカメラのClippingPlanesのNearの設定 |
| FarClippingPlane | UnityのカメラのClippingPlanesのFarの設定 |
| ArBackgroundMaterial | ARモード時に端末のカメラの映像を映すために使用するマテリアルの指定 |


- HologlaInput

だんグラの入力の管理をしています。  
左ボタン、右ボタンそれぞれの押下や同時押しを検出できます。  
HologlaInputオブジェクトのOnPressLeftAndRightに左右のボタンを同時押しした際のイベント、  
LeftButtonオブジェクトのOnClickに左ボタンをクリックした際のイベント、  
RightButtonオブジェクトのOnClickに右ボタンをクリックした際のイベント  
をそれぞれ設定することができるようになっています。

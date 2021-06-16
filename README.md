# HologlaSDK-Unity

## ライセンスについて
MITライセンスになります。
Assets/Hologla/Lisence.txtをご確認ください。

## AR機能の動作環境について
Unity2019.x以降のバージョンを想定しています。 <br>
基本的にARKit、またはARCoreを利用してiOS、Android端末にて動作させることを想定しているため、 <br>
ARFoundation及びARKit、ARCore等のプラグインを使用します(PackageManager設定にて設定しています)。 <br>
現在動作確認しているバージョンは4.1.5です。 <br>

また、動作確認には対応している端末が必要です。
#### iOS
iPhone SE、iPhone 6s以降の端末 <br>
対応端末一覧は以下ページの下部参照 <br>
https://www.apple.com/jp/augmented-reality/

#### Android
Android 7.0以降の対応端末 <br>
対応端末一覧は以下ページの下部参照 <br>
https://developers.google.com/ar/devices

## 導入手順について
1. Unityプロジェクトを作成、または既存のプロジェクトを開いた後にPackageManagerよりAR Foundationをインストールします。 <br>
その後、必要に応じて、ARKit XR Plugin、ARCore XR Pluginをそれぞれインストールします。 <br>
(iOSのみであれば、ARKit XR Pluginのみ、Androidのみであれば、ARCore XR Pluginのみのインストールで問題ありません。) <br>

2. UnityのProject SettingsからXR Plug-in Managementの項目を開きます。 <br>
必要に応じて、iOSのタブのARKitのチェックボックス、AndroidのタブのARCoreのチェックボックスにチェックを入れます。 <br>
(iOS、Andoridのどちらか片方しか利用しない場合は、利用する方のみチェックを入れる形でも問題ありません。) <br>

3. PackageManagerでの設定完了後、本SDKのunitypackageをインポートします。 <br>

4. Unityの上部メニューの[Hologla]から[Initialize Project]と選択し、実行します。 <br>
ここまででプロジェクトの設定は完了です。 <br>

5. 作業をするシーンを作成、または開いたら、 Unityの上部メニューの[Hologla]から[Initialize Scene]を選択、実行してシーン用の設定を実行します。 <br>

以上の設定でプロジェクトの共通設定と、シーン内の画面表示、入力関連のセットアップは完了となります。 <br>

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

## ハンズオン用資料
https://docs.google.com/presentation/d/e/2PACX-1vQFCwxnd4akv10a2nxZxjaFumjAWRBxp0yZCywk6EEduSmpu5QViMlhMjvkS5lG3_lnbkrBArx7U6hl/pub?slide=id.p


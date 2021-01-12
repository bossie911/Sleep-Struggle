warning: LF will be replaced by CRLF in Assets/Scenes/Level_1.unity.
The file will have its original line endings in your working directory
warning: LF will be replaced by CRLF in Assets/Scenes/Level_2.unity.
The file will have its original line endings in your working directory
warning: LF will be replaced by CRLF in Assets/Scenes/Level_3.unity.
The file will have its original line endings in your working directory
[1mdiff --git a/Assets/PlaceTurret.cs b/Assets/PlaceTurret.cs[m
[1mindex a1094f5..4bf59cc 100644[m
[1m--- a/Assets/PlaceTurret.cs[m
[1m+++ b/Assets/PlaceTurret.cs[m
[36m@@ -20,8 +20,6 @@[m [mpublic class PlaceTurret : MonoBehaviour[m
     TileManager manager;[m
     DreamFuel dreamFuel; [m
 [m
[31m-    static DreamFactorySelector selector;[m
[31m-[m
     private Rigidbody ghost, turretGhost, factoryGhost, mineGhost, totemGhost, candleGhost;[m
 [m
     //Place the turret slightly higher (looks better)[m
[36m@@ -50,7 +48,6 @@[m [mpublic class PlaceTurret : MonoBehaviour[m
         totemGhost = GameObject.Find("TotemGhost").GetComponent<Rigidbody>(); [m
 [m
         manager = GameObject.Find("TileManager").GetComponent<TileManager>();[m
[31m-        selector = GameObject.Find("DreamFuelPanel").GetComponent<DreamFactorySelector>();[m
         dreamFuel = GameObject.Find("DreamFuelPanel").GetComponent<DreamFuel>(); [m
 [m
         fow_layer = 1 << 8; [m
[36m@@ -58,29 +55,29 @@[m [mpublic class PlaceTurret : MonoBehaviour[m
 [m
     void Update()[m
     {[m
[31m-        switch (selector.SelectedPlaceable)[m
[32m+[m[32m        switch (TowerSelector.Instance.SelectedPlaceable)[m
         {[m
[31m-            case DreamFactorySelector.Placeables.Turret: [m
[32m+[m[32m            case TowerSelector.Placeables.Turret:[m[41m [m
                 ghost = turretGhost;[m
                 currentOffset = turretOffset;[m
                 resourceCost = 50f;[m
                 break;[m
 [m
[31m-            case DreamFactorySelector.Placeables.Factory:[m
[32m+[m[32m            case TowerSelector.Placeables.Factory:[m
                 ghost = factoryGhost;[m
                 currentOffset = factoryOffset;[m
                 break;[m
 [m
[31m-            case DreamFactorySelector.Placeables.Mine:[m
[32m+[m[32m            case TowerSelector.Placeables.Mine:[m
                 ghost = mineGhost;[m
                 break;[m
 [m
[31m-            case DreamFactorySelector.Placeables.Totem:[m
[32m+[m[32m            case TowerSelector.Placeables.Totem:[m
                 ghost = totemGhost;[m
                 resourceCost = 100f; [m
                 break;[m
 [m
[31m-            case DreamFactorySelector.Placeables.Candle:[m
[32m+[m[32m            case TowerSelector.Placeables.Candle:[m
                 ghost = candleGhost;[m
                 resourceCost = 20f;[m
                 break;[m
[36m@@ -98,25 +95,25 @@[m [mpublic class PlaceTurret : MonoBehaviour[m
         {[m
             if (IfCanPlace(ray))[m
             {[m
[31m-                switch (selector.SelectedPlaceable)[m
[32m+[m[32m                switch (TowerSelector.Instance.SelectedPlaceable)[m
                 {[m
[31m-                    case DreamFactorySelector.Placeables.Turret:[m
[32m+[m[32m                    case TowerSelector.Placeables.Turret:[m
                         Place(point, currentTile, turretPrefab, currentOffset);[m
                         break;[m
 [m
[31m-                    case DreamFactorySelector.Placeables.Factory:[m
[32m+[m[32m                    case TowerSelector.Placeables.Factory:[m
                         Place(point, currentTile, factoryPrefab, currentOffset);[m
                         break;[m
 [m
[31m-                    case DreamFactorySelector.Placeables.Mine:[m
[32m+[m[32m                    case TowerSelector.Placeables.Mine:[m
                         Place(point, currentTile, Mine, currentOffset);[m
                         break;[m
 [m
[31m-                    case DreamFactorySelector.Placeables.Totem:[m
[32m+[m[32m                    case TowerSelector.Placeables.Totem:[m
                         Place(point, currentTile, Totem, currentOffset);[m
                         break;[m
 [m
[31m-                    case DreamFactorySelector.Placeables.Candle:[m
[32m+[m[32m                    case TowerSelector.Placeables.Candle:[m
                         Place(point, currentTile, Candle, currentOffset);[m
                         break;[m
                 }[m
[1mdiff --git a/Assets/Scenes/Level_1.unity b/Assets/Scenes/Level_1.unity[m
[1mindex d49c0df..166f32d 100644[m
[1m--- a/Assets/Scenes/Level_1.unity[m
[1m+++ b/Assets/Scenes/Level_1.unity[m
[36m@@ -1193,7 +1193,7 @@[m [mPrefabInstance:[m
     - target: {fileID: 2900115979173712071, guid: ff867816aff16bb4eb1ea8c55e9b8edd,[m
         type: 3}[m
       propertyPath: m_RootOrder[m
[31m-      value: 3[m
[32m+[m[32m      value: 4[m
       objectReference: {fileID: 0}[m
     - target: {fileID: 2900115979173712071, guid: ff867816aff16bb4eb1ea8c55e9b8edd,[m
         type: 3}[m
[36m@@ -1337,7 +1337,6 @@[m [mGameObject:[m
   - component: {fileID: 935426722}[m
   - component: {fileID: 935426721}[m
   - component: {fileID: 935426720}[m
[31m-  - component: {fileID: 935426723}[m
   m_Layer: 5[m
   m_Name: DreamFuelPanel[m
   m_TagString: Untagged[m
[36m@@ -1383,7 +1382,7 @@[m [mMonoBehaviour:[m
   baseGeneration: 3[m
   generationDelay: 1[m
   addedGeneration: 0[m
[31m-  dreamFuelDisp: {fileID: 953360570}[m
[32m+[m[32m  dreamFuelDisplay: {fileID: 953360570}[m
 --- !u!114 &935426721[m
 MonoBehaviour:[m
   m_ObjectHideFlags: 0[m
[36m@@ -1421,23 +1420,6 @@[m [mCanvasRenderer:[m
   m_PrefabAsset: {fileID: 0}[m
   m_GameObject: {fileID: 935426718}[m
   m_CullTransparentMesh: 0[m
[31m---- !u!114 &935426723[m
[31m-MonoBehaviour:[m
[31m-  m_ObjectHideFlags: 0[m
[31m-  m_CorrespondingSourceObject: {fileID: 0}[m
[31m-  m_PrefabInstance: {fileID: 0}[m
[31m-  m_PrefabAsset: {fileID: 0}[m
[31m-  m_GameObject: {fileID: 935426718}[m
[31m-  m_Enabled: 1[m
[31m-  m_EditorHideFlags: 0[m
[31m-  m_Script: {fileID: 11500000, guid: 1d4bdaa7542bfeb43bd57577fc55b93f, type: 3}[m
[31m-  m_Name: [m
[31m-  m_EditorClassIdentifier: [m
[31m-  FactoryButton: {fileID: 0}[m
[31m-  TurretButton: {fileID: 303706932}[m
[31m-  mineButton: {fileID: 0}[m
[31m-  totemButton: {fileID: 0}[m
[31m-  candleButton: {fileID: 776535228}[m
 --- !u!1 &953360568[m
 GameObject:[m
   m_ObjectHideFlags: 0[m
[36m@@ -2138,6 +2120,7 @@[m [mRectTransform:[m
   - {fileID: 776535227}[m
   - {fileID: 1962109913}[m
   - {fileID: 16407225}[m
[32m+[m[32m  - {fileID: 1512168940}[m
   m_Father: {fileID: 0}[m
   m_RootOrder: 9[m
   m_LocalEulerAnglesHint: {x: 0, y: 0, z: 0}[m
[36m@@ -25225,6 +25208,59 @@[m [mTransform:[m
   m_Father: {fileID: 0}[m
   m_RootOrder: 0[m
   m_LocalEulerAnglesHint: {x: 0, y: 0, z: 0}[m
[32m+[m[32m--- !u!1 &1512168939[m
[32m+[m[32mGameObject:[m
[32m+[m[32m  m_ObjectHideFlags: 0[m
[32m+[m[32m  m_CorrespondingSourceObject: {fileID: 0}[m
[32m+[m[32m  m_PrefabInstance: {fileID: 0}[m
[32m+[m[32m  m_PrefabAsset: {fileID: 0}[m
[32m+[m[32m  serializedVersion: 6[m
[32m+[m[32m  m_Component:[m
[32m+[m[32m  - component: {fileID: 1512168940}[m
[32m+[m[32m  - component: {fileID: 1512168941}[m
[32m+[m[32m  m_Layer: 5[m
[32m+[m[32m  m_Name: TowerSelectorHandler[m
[32m+[m[32m  m_TagString: Untagged[m
[32m+[m[32m  m_Icon: {fileID: 0}[m
[32m+[m[32m  m_NavMeshLayer: 0[m
[32m+[m[32m  m_StaticEditorFlags: 0[m
[32m+[m[32m  m_IsActive: 1[m
[32m+[m[32m--- !u!224 &1512168940[m
[32m+[m[32mRectTransform:[m
[32m+[m[32m  m_ObjectHideFlags: 0[m
[32m+[m[32m  m_CorrespondingSourceObject: {fileID: 0}[m
[32m+[m[32m  m_PrefabInstance: {fileID: 0}[m
[32m+[m[32m  m_PrefabAsset: {fileID: 0}[m
[32m+[m[32m  m_GameObject: {fileID: 1512168939}[m
[32m+[m[32m  m_LocalRotation: {x: -0, y: -0, z: -0, w: 1}[m
[32m+[m[32m  m_LocalPosition: {x: 0, y: 0, z: 0}[m
[32m+[m[32m  m_LocalScale: {x: 1, y: 1, z: 1}[m
[32m+[m[32m  m_Children: [][m
[32m+[m[32m  m_Father: {fileID: 1120280796}[m
[32m+[m[32m  m_RootOrder: 6[m
[32m+[m[32m  m_LocalEulerAnglesHint: {x: 0, y: 0, z: 0}[m
[32m+[m[32m  m_AnchorMin: {x: 0.5, y: 0.5}[m
[32m+[m[32m  m_AnchorMax: {x: 0.5, y: 0.5}[m
[32m+[m[32m  m_AnchoredPosition: {x: -36.776367, y: -18.951736}[m
[32m+[m[32m  m_SizeDelta: {x: 100, y: 100}[m
[32m+[m[32m  m_Pivot: {x: 0.5, y: 0.5}[m
[32m+[m[32m--- !u!114 &1512168941[m
[32m+[m[32mMonoBehaviour:[m
[32m+[m[32m  m_ObjectHideFlags: 0[m
[32m+[m[32m  m_CorrespondingSourceObject: {fileID: 0}[m
[32m+[m[32m  m_PrefabInstance: {fileID: 0}[m
[32m+[m[32m  m_PrefabAsset: {fileID: 0}[m
[32m+[m[32m  m_GameObject: {fileID: 1512168939}[m
[32m+[m[32m  m_Enabled: 1[m
[32m+[m[32m  m_EditorHideFlags: 0[m
[32m+[m[32m  m_Script: {fileID: 11500000, guid: 1d4bdaa7542bfeb43bd57577fc55b93f, type: 3}[m
[32m+[m[32m  m_Name:[m[41m [m
[32m+[m[32m  m_EditorClassIdentifier:[m[41m [m
[32m+[m[32m  FactoryButton: {fileID: 0}[m
[32m+[m[32m  TurretButton: {fileID: 303706932}[m
[32m+[m[32m  mineButton: {fileID: 0}[m
[32m+[m[32m  totemButton: {fileID: 0}[m
[32m+[m[32m  candleButton: {fileID: 776535228}[m
 --- !u!1 &1574986181[m
 GameObject:[m
   m_ObjectHideFlags: 0[m
[36m@@ -106869,7 +106905,7 @@[m [mPrefabInstance:[m
     - target: {fileID: 2900115979173712071, guid: ff867816aff16bb4eb1ea8c55e9b8edd,[m
         type: 3}[m
       propertyPath: m_RootOrder[m
[31m-      value: 2[m
[32m+[m[32m      value: 3[m
       objectReference: {fileID: 0}[m
     - target: {fileID: 2900115979173712071, guid: ff867816aff16bb4eb1ea8c55e9b8edd,[m
         type: 3}[m
[1mdiff --git a/Assets/Scenes/Level_2.unity b/Assets/Scenes/Level_2.unity[m
[1mindex 3e46e24..54f531d 100644[m
[1m--- a/Assets/Scenes/Level_2.unity[m
[1m+++ b/Assets/Scenes/Level_2.unity[m
[36m@@ -23959,6 +23959,7 @@[m [mRectTransform:[m
   - {fileID: 1433103176}[m
   - {fileID: 591083772}[m
   - {fileID: 174033714}[m
[32m+[m[32m  - {fileID: 1381331643}[m
   m_Father: {fileID: 0}[m
   m_RootOrder: 9[m
   m_LocalEulerAnglesHint: {x: 0, y: 0, z: 0}[m
[36m@@ -24792,6 +24793,59 @@[m [mAudioSource:[m
     type: 3}[m
   m_PrefabInstance: {fileID: 1329056702}[m
   m_PrefabAsset: {fileID: 0}[m
[32m+[m[32m--- !u!1 &1381331642[m
[32m+[m[32mGameObject:[m
[32m+[m[32m  m_ObjectHideFlags: 0[m
[32m+[m[32m  m_CorrespondingSourceObject: {fileID: 0}[m
[32m+[m[32m  m_PrefabInstance: {fileID: 0}[m
[32m+[m[32m  m_PrefabAsset: {fileID: 0}[m
[32m+[m[32m  serializedVersion: 6[m
[32m+[m[32m  m_Component:[m
[32m+[m[32m  - component: {fileID: 1381331643}[m
[32m+[m[32m  - component: {fileID: 1381331644}[m
[32m+[m[32m  m_Layer: 5[m
[32m+[m[32m  m_Name: TowerSelectorHandler[m
[32m+[m[32m  m_TagString: Untagged[m
[32m+[m[32m  m_Icon: {fileID: 0}[m
[32m+[m[32m  m_NavMeshLayer: 0[m
[32m+[m[32m  m_StaticEditorFlags: 0[m
[32m+[m[32m  m_IsActive: 1[m
[32m+[m[32m--- !u!224 &1381331643[m
[32m+[m[32mRectTransform:[m
[32m+[m[32m  m_ObjectHideFlags: 0[m
[32m+[m[32m  m_CorrespondingSourceObject: {fileID: 0}[m
[32m+[m[32m  m_PrefabInstance: {fileID: 0}[m
[32m+[m[32m  m_PrefabAsset: {fileID: 0}[m
[32m+[m[32m  m_GameObject: {fileID: 1381331642}[m
[32m+[m[32m  m_LocalRotation: {x: -0, y: -0, z: -0, w: 1}[m
[32m+[m[32m  m_LocalPosition: {x: 0, y: 0, z: 0}[m
[32m+[m[32m  m_LocalScale: {x: 1, y: 1, z: 1}[m
[32m+[m[32m  m_Children: [][m
[32m+[m[32m  m_Father: {fileID: 696003043}[m
[32m+[m[32m  m_RootOrder: 7[m
[32m+[m[32m  m_LocalEulerAnglesHint: {x: 0, y: 0, z: 0}[m
[32m+[m[32m  m_AnchorMin: {x: 0.5, y: 0.5}[m
[32m+[m[32m  m_AnchorMax: {x: 0.5, y: 0.5}[m
[32m+[m[32m  m_AnchoredPosition: {x: 0, y: 0}[m
[32m+[m[32m  m_SizeDelta: {x: 100, y: 100}[m
[32m+[m[32m  m_Pivot: {x: 0.5, y: 0.5}[m
[32m+[m[32m--- !u!114 &1381331644[m
[32m+[m[32mMonoBehaviour:[m
[32m+[m[32m  m_ObjectHideFlags: 0[m
[32m+[m[32m  m_CorrespondingSourceObject: {fileID: 0}[m
[32m+[m[32m  m_PrefabInstance: {fileID: 0}[m
[32m+[m[32m  m_PrefabAsset: {fileID: 0}[m
[32m+[m[32m  m_GameObject: {fileID: 1381331642}[m
[32m+[m[32m  m_Enabled: 1[m
[32m+[m[32m  m_EditorHideFlags: 0[m
[32m+[m[32m  m_Script: {fileID: 11500000, guid: 1d4bdaa7542bfeb43bd57577fc55b93f, type: 3}[m
[32m+[m[32m  m_Name:[m[41m [m
[32m+[m[32m  m_EditorClassIdentifier:[m[41m [m
[32m+[m[32m  FactoryButton: {fileID: 1250755027}[m
[32m+[m[32m  TurretButton: {fileID: 1519009249}[m
[32m+[m[32m  mineButton: {fileID: 0}[m
[32m+[m[32m  totemButton: {fileID: 0}[m
[32m+[m[32m  candleButton: {fileID: 1241136201}[m
 --- !u!1 &1392730304[m
 GameObject:[m
   m_ObjectHideFlags: 0[m
[36m@@ -24804,7 +24858,6 @@[m [mGameObject:[m
   - component: {fileID: 1392730309}[m
   - component: {fileID: 1392730308}[m
   - component: {fileID: 1392730307}[m
[31m-  - component: {fileID: 1392730306}[m
   m_Layer: 5[m
   m_Name: DreamFuelPanel[m
   m_TagString: Untagged[m
[36m@@ -24833,23 +24886,6 @@[m [mRectTransform:[m
   m_AnchoredPosition: {x: 8, y: 8}[m
   m_SizeDelta: {x: 177.93982, y: 65.903625}[m
   m_Pivot: {x: 0, y: 0}[m
[31m---- !u!114 &1392730306[m
[31m-MonoBehaviour:[m
[31m-  m_ObjectHideFlags: 0[m
[31m-  m_CorrespondingSourceObject: {fileID: 0}[m
[31m-  m_PrefabInstance: {fileID: 0}[m
[31m-  m_PrefabAsset: {fileID: 0}[m
[31m-  m_GameObject: {fileID: 1392730304}[m
[31m-  m_Enabled: 1[m
[31m-  m_EditorHideFlags: 0[m
[31m-  m_Script: {fileID: 11500000, guid: 1d4bdaa7542bfeb43bd57577fc55b93f, type: 3}[m
[31m-  m_Name: [m
[31m-  m_EditorClassIdentifier: [m
[31m-  FactoryButton: {fileID: 1250755027}[m
[31m-  TurretButton: {fileID: 1519009249}[m
[31m-  mineButton: {fileID: 0}[m
[31m-  totemButton: {fileID: 0}[m
[31m-  candleButton: {fileID: 1241136201}[m
 --- !u!114 &1392730307[m
 MonoBehaviour:[m
   m_ObjectHideFlags: 0[m
[36m@@ -24867,7 +24903,7 @@[m [mMonoBehaviour:[m
   baseGeneration: 1[m
   generationDelay: 1[m
   addedGeneration: 0[m
[31m-  dreamFuelDisp: {fileID: 421499224}[m
[32m+[m[32m  dreamFuelDisplay: {fileID: 421499224}[m
 --- !u!114 &1392730308[m
 MonoBehaviour:[m
   m_ObjectHideFlags: 0[m
[1mdiff --git a/Assets/Scenes/Level_3.unity b/Assets/Scenes/Level_3.unity[m
[1mindex bb198b5..4eae1e0 100644[m
[1m--- a/Assets/Scenes/Level_3.unity[m
[1m+++ b/Assets/Scenes/Level_3.unity[m
[36m@@ -133,7 +133,6 @@[m [mGameObject:[m
   - component: {fileID: 129751433}[m
   - component: {fileID: 129751432}[m
   - component: {fileID: 129751431}[m
[31m-  - component: {fileID: 129751430}[m
   m_Layer: 5[m
   m_Name: DreamFuelPanel[m
   m_TagString: Untagged[m
[36m@@ -162,23 +161,6 @@[m [mRectTransform:[m
   m_AnchoredPosition: {x: 8, y: 8}[m
   m_SizeDelta: {x: 177.93982, y: 65.903625}[m
   m_Pivot: {x: 0, y: 0}[m
[31m---- !u!114 &129751430[m
[31m-MonoBehaviour:[m
[31m-  m_ObjectHideFlags: 0[m
[31m-  m_CorrespondingSourceObject: {fileID: 0}[m
[31m-  m_PrefabInstance: {fileID: 0}[m
[31m-  m_PrefabAsset: {fileID: 0}[m
[31m-  m_GameObject: {fileID: 129751428}[m
[31m-  m_Enabled: 1[m
[31m-  m_EditorHideFlags: 0[m
[31m-  m_Script: {fileID: 11500000, guid: 1d4bdaa7542bfeb43bd57577fc55b93f, type: 3}[m
[31m-  m_Name: [m
[31m-  m_EditorClassIdentifier: [m
[31m-  FactoryButton: {fileID: 897149759}[m
[31m-  TurretButton: {fileID: 1560556196}[m
[31m-  mineButton: {fileID: 0}[m
[31m-  totemButton: {fileID: 2038998096}[m
[31m-  candleButton: {fileID: 761259387}[m
 --- !u!114 &129751431[m
 MonoBehaviour:[m
   m_ObjectHideFlags: 0[m
[36m@@ -196,7 +178,7 @@[m [mMonoBehaviour:[m
   baseGeneration: 1[m
   generationDelay: 1[m
   addedGeneration: 0[m
[31m-  dreamFuelDisp: {fileID: 554236051}[m
[32m+[m[32m  dreamFuelDisplay: {fileID: 554236051}[m
 --- !u!114 &129751432[m
 MonoBehaviour:[m
   m_ObjectHideFlags: 0[m
[36m@@ -430,6 +412,7 @@[m [mRectTransform:[m
   - {fileID: 1920142117}[m
   - {fileID: 2129055854}[m
   - {fileID: 1831555828}[m
[32m+[m[32m  - {fileID: 1131840237}[m
   m_Father: {fileID: 0}[m
   m_RootOrder: 8[m
   m_LocalEulerAnglesHint: {x: 0, y: 0, z: 0}[m
[36m@@ -24974,6 +24957,59 @@[m [mTransform:[m
   m_Father: {fileID: 1266650744}[m
   m_RootOrder: 0[m
   m_LocalEulerAnglesHint: {x: 0, y: 0, z: 0}[m
[32m+[m[32m--- !u!1 &1131840236[m
[32m+[m[32mGameObject:[m
[32m+[m[32m  m_ObjectHideFlags: 0[m
[32m+[m[32m  m_CorrespondingSourceObject: {fileID: 0}[m
[32m+[m[32m  m_PrefabInstance: {fileID: 0}[m
[32m+[m[32m  m_PrefabAsset: {fileID: 0}[m
[32m+[m[32m  serializedVersion: 6[m
[32m+[m[32m  m_Component:[m
[32m+[m[32m  - component: {fileID: 1131840237}[m
[32m+[m[32m  - component: {fileID: 1131840238}[m
[32m+[m[32m  m_Layer: 5[m
[32m+[m[32m  m_Name: TowerSelectorHandler[m
[32m+[m[32m  m_TagString: Untagged[m
[32m+[m[32m  m_Icon: {fileID: 0}[m
[32m+[m[32m  m_NavMeshLayer: 0[m
[32m+[m[32m  m_StaticEditorFlags: 0[m
[32m+[m[32m  m_IsActive: 1[m
[32m+[m[32m--- !u!224 &1131840237[m
[32m+[m[32mRectTransform:[m
[32m+[m[32m  m_ObjectHideFlags: 0[m
[32m+[m[32m  m_CorrespondingSourceObject: {fileID: 0}[m
[32m+[m[32m  m_PrefabInstance: {fileID: 0}[m
[32m+[m[32m  m_PrefabAsset: {fileID: 0}[m
[32m+[m[32m  m_GameObject: {fileID: 1131840236}[m
[32m+[m[32m  m_LocalRotation: {x: -0, y: -0, z: -0, w: 1}[m
[32m+[m[32m  m_LocalPosition: {x: 0, y: 0, z: 0}[m
[32m+[m[32m  m_LocalScale: {x: 1, y: 1, z: 1}[m
[32m+[m[32m  m_Children: [][m
[32m+[m[32m  m_Father: {fileID: 459738574}[m
[32m+[m[32m  m_RootOrder: 8[m
[32m+[m[32m  m_LocalEulerAnglesHint: {x: 0, y: 0, z: 0}[m
[32m+[m[32m  m_AnchorMin: {x: 0.5, y: 0.5}[m
[32m+[m[32m  m_AnchorMax: {x: 0.5, y: 0.5}[m
[32m+[m[32m  m_AnchoredPosition: {x: 0, y: 0}[m
[32m+[m[32m  m_SizeDelta: {x: 100, y: 100}[m
[32m+[m[32m  m_Pivot: {x: 0.5, y: 0.5}[m
[32m+[m[32m--- !u!114 &1131840238[m
[32m+[m[32mMonoBehaviour:[m
[32m+[m[32m  m_ObjectHideFlags: 0[m
[32m+[m[32m  m_CorrespondingSourceObject: {fileID: 0}[m
[32m+[m[32m  m_PrefabInstance: {fileID: 0}[m
[32m+[m[32m  m_PrefabAsset: {fileID: 0}[m
[32m+[m[32m  m_GameObject: {fileID: 1131840236}[m
[32m+[m[32m  m_Enabled: 1[m
[32m+[m[32m  m_EditorHideFlags: 0[m
[32m+[m[32m  m_Script: {fileID: 11500000, guid: 1d4bdaa7542bfeb43bd57577fc55b93f, type: 3}[m
[32m+[m[32m  m_Name:[m[41m [m
[32m+[m[32m  m_EditorClassIdentifier:[m[41m [m
[32m+[m[32m  FactoryButton: {fileID: 897149759}[m
[32m+[m[32m  TurretButton: {fileID: 1560556196}[m
[32m+[m[32m  mineButton: {fileID: 0}[m
[32m+[m[32m  totemButton: {fileID: 2038998096}[m
[32m+[m[32m  candleButton: {fileID: 761259387}[m
 --- !u!1 &1139931248[m
 GameObject:[m
   m_ObjectHideFlags: 0[m
[1mdiff --git a/Assets/Scripts/ResourceManagement/DreamFactorySelector.cs b/Assets/Scripts/ResourceManagement/DreamFactorySelector.cs[m
[1mdeleted file mode 100644[m
[1mindex 9a59901..0000000[m
[1m--- a/Assets/Scripts/ResourceManagement/DreamFactorySelector.cs[m
[1m+++ /dev/null[m
[36m@@ -1,92 +0,0 @@[m
[31m-﻿using System.Collections;[m
[31m-using System.Collections.Generic;[m
[31m-using UnityEngine;[m
[31m-using UnityEngine.UI;[m
[31m-[m
[31m-public class DreamFactorySelector : MonoBehaviour[m
[31m-{[m
[31m-    public Button FactoryButton;[m
[31m-    public Button TurretButton;[m
[31m-    public Button mineButton;[m
[31m-    public Button totemButton;[m
[31m-    public Button candleButton;[m
[31m-[m
[31m-    private Placeables _selectedPlaceable;[m
[31m-[m
[31m-    public enum Placeables[m
[31m-    {[m
[31m-        Turret,[m
[31m-        Factory,[m
[31m-        Mine,[m
[31m-        Totem,[m
[31m-        Candle[m
[31m-    }[m
[31m-[m
[31m-    public Placeables SelectedPlaceable[m
[31m-    {[m
[31m-        get { return _selectedPlaceable; }[m
[31m-        private set { _selectedPlaceable = value; }[m
[31m-    }[m
[31m-[m
[31m-    void Start()[m
[31m-    {[m
[31m-        if(FactoryButton != null){[m
[31m-            Button button1 = FactoryButton.GetComponent<Button>();[m
[31m-            button1.onClick.AddListener(TaskOnClickFactory);[m
[31m-        }[m
[31m-[m
[31m-        if (TurretButton != null)[m
[31m-        {[m
[31m-            Button button2 = TurretButton.GetComponent<Button>();[m
[31m-            button2.onClick.AddListener(TaskOnClickTurret);[m
[31m-        }[m
[31m-[m
[31m-        if (mineButton != null)[m
[31m-        {[m
[31m-            Button button3 = mineButton.GetComponent<Button>();[m
[31m-            button3.onClick.AddListener(TaskOnClickMine);[m
[31m-        }[m
[31m-[m
[31m-        if (totemButton != null)[m
[31m-        {[m
[31m-            Button button4 = totemButton.GetComponent<Button>();[m
[31m-            button4.onClick.AddListener(TaskOnClickTotem);[m
[31m-        }[m
[31m-[m
[31m-        if (candleButton != null)[m
[31m-        {[m
[31m-            Button button5 = candleButton.GetComponent<Button>();[m
[31m-            button5.onClick.AddListener(TaskOnClickCandle);[m
[31m-        }[m
[31m-    }[m
[31m-[m
[31m-    private void TaskOnClickFactory()[m
[31m-    {[m
[31m-        if (FactoryButton)[m
[31m-            _selectedPlaceable = Placeables.Factory;[m
[31m-    }[m
[31m-[m
[31m-    private void TaskOnClickTurret()[m
[31m-    {[m
[31m-        if (TurretButton)[m
[31m-            _selectedPlaceable = Placeables.Turret;[m
[31m-    }[m
[31m-[m
[31m-    private void TaskOnClickMine()[m
[31m-    {[m
[31m-        if (mineButton)[m
[31m-            _selectedPlaceable = Placeables.Mine;[m
[31m-    }[m
[31m-[m
[31m-    private void TaskOnClickTotem()[m
[31m-    {[m
[31m-        if (totemButton)[m
[31m-            _selectedPlaceable = Placeables.Totem;[m
[31m-    }[m
[31m-[m
[31m-    private void TaskOnClickCandle()[m
[31m-    {[m
[31m-        if (candleButton)[m
[31m-            _selectedPlaceable = Placeables.Candle;[m
[31m-    }[m
[31m-}[m
[1mdiff --git a/Assets/Scripts/ResourceManagement/DreamFactorySelector.cs.meta b/Assets/Scripts/ResourceManagement/DreamFactorySelector.cs.meta[m
[1mdeleted file mode 100644[m
[1mindex c3b89fa..0000000[m
[1m--- a/Assets/Scripts/ResourceManagement/DreamFactorySelector.cs.meta[m
[1m+++ /dev/null[m
[36m@@ -1,11 +0,0 @@[m
[31m-fileFormatVersion: 2[m
[31m-guid: 1d4bdaa7542bfeb43bd57577fc55b93f[m
[31m-MonoImporter:[m
[31m-  externalObjects: {}[m
[31m-  serializedVersion: 2[m
[31m-  defaultReferences: [][m
[31m-  executionOrder: 0[m
[31m-  icon: {instanceID: 0}[m
[31m-  userData: [m
[31m-  assetBundleName: [m
[31m-  assetBundleVariant: [m
[1mdiff --git a/Assets/Scripts/ResourceManagement/DreamFuel.cs b/Assets/Scripts/ResourceManagement/DreamFuel.cs[m
[1mindex 155c866..86fec83 100644[m
[1m--- a/Assets/Scripts/ResourceManagement/DreamFuel.cs[m
[1m+++ b/Assets/Scripts/ResourceManagement/DreamFuel.cs[m
[36m@@ -10,19 +10,14 @@[m [mpublic class DreamFuel : MonoBehaviour[m
 [m
     public float baseGeneration = 1f;[m
     public float generationDelay = 1f;[m
[31m-    protected float generationTimer;[m
[32m+[m[32m    private float generationTimer;[m
     public float addedGeneration;[m
 [m
[31m-    public Text dreamFuelDisp;[m
[32m+[m[32m    public Text dreamFuelDisplay;[m
 [m
[31m-    // Update is called once per frame[m
     void Update()[m
     {[m
         ResourceGeneration();[m
[31m-[m
[31m-        //voor nu displayt het baseGeneration, moet veranderen naar basegeneration + addedGeneration[m
[31m-        //dreamFuelDisp.text = "" + currentResourceValue + "/" + baseGeneration;[m
[31m-        dreamFuelDisp.text = currentResourceValue.ToString();[m
     }[m
 [m
     //ResourceGeneration manages the amount of DreamFuel that is added per x amount of time.[m
[36m@@ -36,5 +31,7 @@[m [mpublic class DreamFuel : MonoBehaviour[m
             currentResourceValue += baseGeneration;[m
             generationTimer = 0f;[m
         }[m
[32m+[m[41m        [m
[32m+[m[32m        dreamFuelDisplay.text = currentResourceValue.ToString();[m
     }[m
 }[m

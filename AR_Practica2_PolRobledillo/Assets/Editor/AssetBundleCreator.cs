using UnityEngine;
using UnityEditor;
using System;

public class AssetBundleCreator
{
    [MenuItem("Assets/Create Asset Bundles")]
    private static void BuildAllAssetBundles()
    {
        string assetBundleDirectoryPath = Application.dataPath + "/AssetBundles";
        try
        {
            BuildPipeline.BuildAssetBundles(assetBundleDirectoryPath, BuildAssetBundleOptions.None, EditorUserBuildSettings.activeBuildTarget);
        }
        catch (Exception e)
        {
            Debug.LogError("Error creating Asset Bundles: " + e.Message);
        }
    }
}

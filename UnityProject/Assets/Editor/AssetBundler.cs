using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif
using System;
using System.IO;

public class AssetBundler
{
    private static string TEMP_BUILD_FOLDER = "Temp/AssetBundles";
    private static string BUNDLE_FILENAME = "mod.assets";
    private static string OUTPUT_FOLDER = "../content";

    #region Internal bundler Variables
    BuildTarget target = BuildTarget.StandaloneWindows;
    #endregion

    [MenuItem("Kitchen/Build Asset Bundle")]
    public static void BuildAssetBundle()
    {
        Debug.LogFormat("Creating \"{0}\" AssetBundle...", BUNDLE_FILENAME);

        AssetBundler bundler = new();

        if (Application.platform == RuntimePlatform.OSXEditor) bundler.target = BuildTarget.StandaloneOSX;

        bool success = false;

        try
        {
            // Check if there any assets to be included
            bundler.WarnIfAssetsAreNotTagged();
            bundler.CheckForAssets();

            // Delete the contents of OUTPUT_FOLDER
            bundler.CleanBuildFolder();

            // Lastly, create the asset bundle itself and copy it to the output folder
            bundler.CreateAssetBundle();

            success = true;
        }
        catch (Exception e)
        {
            Debug.LogErrorFormat("Failed to build AssetBundle: {0}\n{1}", e.Message, e.StackTrace);
        }

        if (success)
        {
            Debug.LogFormat("{0} Build complete! Output: {1}", System.DateTime.Now.ToLocalTime(), OUTPUT_FOLDER + "/" + BUNDLE_FILENAME);
        }
    }

    /// <summary>
    /// Delete and recreate the OUTPUT_FOLDER to ensure a clean build.
    /// </summary>
    protected void CleanBuildFolder()
    {
        Debug.LogFormat("Cleaning {0}...", OUTPUT_FOLDER);

        if (Directory.Exists(OUTPUT_FOLDER))
        {
            Directory.Delete(OUTPUT_FOLDER, true);
        }

        Directory.CreateDirectory(OUTPUT_FOLDER);
    }

    /// <summary>
    /// Build the AssetBundle itself and copy it to the OUTPUT_FOLDER.
    /// </summary>
    protected void CreateAssetBundle()
    {
        Debug.Log("Building AssetBundle...");

        //Build all AssetBundles to the TEMP_BUILD_FOLDER
        if (!Directory.Exists(TEMP_BUILD_FOLDER))
        {
            Directory.CreateDirectory(TEMP_BUILD_FOLDER);
        }

#pragma warning disable 618
        // Build the asset bundle with the CollectDependencies flag. This is necessary or else ScriptableObjects will
        // not be accessible within the asset bundle. Unity has deprecated this flag claiming it is now always active,
        // but due to a bug we must still include it (and ignore the warning).
        BuildPipeline.BuildAssetBundles(
            TEMP_BUILD_FOLDER,
            BuildAssetBundleOptions.UncompressedAssetBundle | BuildAssetBundleOptions.CollectDependencies,
            target);
#pragma warning restore 618

        // We are only interested in the BUNDLE_FILENAME bundle (and not the extra AssetBundle or the manifest files
        // that Unity makes), so just copy that to the final output folder
        string srcPath = Path.Combine(TEMP_BUILD_FOLDER, BUNDLE_FILENAME);
        string destPath = Path.Combine(OUTPUT_FOLDER, BUNDLE_FILENAME);
        File.Copy(srcPath, destPath, true);
    }

    /// <summary>
    /// Print a warning for all prefab and audioclip assets that are not currently tagged to be in this AssetBundle.
    /// </summary>
    protected void WarnIfAssetsAreNotTagged()
    {
        string[] assetGUIDs = AssetDatabase.FindAssets("t:prefab,t:audioclip");

        foreach (var assetGUID in assetGUIDs)
        {
            string path = AssetDatabase.GUIDToAssetPath(assetGUID);

            var importer = AssetImporter.GetAtPath(path);
            if (!importer.assetBundleName.Equals(BUNDLE_FILENAME))
            {
                Debug.LogWarningFormat("Asset \"{0}\" is not tagged for {1} and will not be included in the AssetBundle!", path, BUNDLE_FILENAME);
            }
        }

    }

    /// <summary>
    /// Verify that there is at least one thing to be included in the asset bundle.
    /// </summary>
    protected void CheckForAssets()
    {
        string[] assetsInBundle = AssetDatabase.FindAssets(string.Format("t:prefab,t:audioclip,t:scriptableobject,b:", BUNDLE_FILENAME));
        if (assetsInBundle.Length == 0)
        {
            throw new Exception(string.Format("No assets have been tagged for inclusion in the {0} AssetBundle.", BUNDLE_FILENAME));
        }
    }
}

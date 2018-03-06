﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

/// <summary>
/// AssetBundle管理器
/// </summary>
public class AssetBundleMgr
{
    private static readonly string streamingAssetPath = Application.streamingAssetsPath;
    private static readonly string persistentDataPath = Application.persistentDataPath;
    private static readonly string wwwStreamingAssetPath =
#if UNITY_ANDROID
        Application.streamingAssetsPath;
#else
        "file://" + Application.streamingAssetsPath;
#endif
    private static AssetBundleMgr instance;
    private static AssetBundleLoader assetBundleLoader;

    private AssetBundleMgr()
    {
        if (null == assetBundleLoader)
        {
            GameObject go = new GameObject("AssetBundleLoader");
            GameObject.DontDestroyOnLoad(go);
            assetBundleLoader = go.AddComponent<AssetBundleLoader>();
        }
    }

    public static AssetBundleMgr GetInstance()
    {
        if (null == instance)
        {
            instance = new AssetBundleMgr();
        }
        return instance;
    }

    /// <summary>
    /// 从StreamingAssetPath加载(同步)
    /// </summary>
    /// <param name="bundleName"></param>
    /// <returns></returns>
    public static AssetBundle LoadFromStreamingAssetPath(string bundleName)
    {
        return AssetBundle.LoadFromFile(Path.Combine(streamingAssetPath, bundleName));
    }

    /// <summary>
    /// 从PersistantDataPath加载(同步)
    /// </summary>
    /// <param name="bundleName"></param>
    /// <returns></returns>
    public static AssetBundle LoadFromPersistantDataPath(string bundleName)
    {
        return AssetBundle.LoadFromFile(Path.Combine(persistentDataPath, bundleName));
    }

    /// <summary>
    /// 从StreamingAssetPath加载(异步)
    /// </summary>
    /// <param name="bundleName"></param>
    /// <returns></returns>
    public static void LoadFromStreamingAssetPathAsync(string bundleName, Action<AssetBundle> callback)
    {
        assetBundleLoader.LoadAssetBundleAsync(Path.Combine(streamingAssetPath, bundleName), callback);
    }

    /// <summary>
    /// 从PersistantDataPath加载(异步)
    /// </summary>
    /// <param name="bundleName"></param>
    /// <param name="callback"></param>
    public static void LoadFromPersistantDataPathAsync(string bundleName, Action<AssetBundle> callback)
    {
        assetBundleLoader.LoadAssetBundleAsync(Path.Combine(persistentDataPath, bundleName), callback);
    }

    /// <summary>
    /// 使用WWW从本地加载bundle(异步)
    /// </summary>
    /// <param name="bundleName"></param>
    /// <param name="callback"></param>
    /// <param name="version"></param>
    public static void LoadFromWWWLocalAsync(string bundleName, Action<AssetBundle> callback, int version)
    {
        assetBundleLoader.LoadAssetBundleAsync(Path.Combine(wwwStreamingAssetPath, bundleName), callback, BundleLoadType.WWW, version);
    }

    /// <summary>
    /// 使用WWW从网络URL地址加载bundle(异步)
    /// </summary>
    /// <param name="url"></param>
    /// <param name="callback"></param>
    /// <param name="version"></param>
    public static void LoadFromWWWUrlAsync(string url, Action<AssetBundle> callback, int version)
    {
        assetBundleLoader.LoadAssetBundleAsync(url, callback, BundleLoadType.WWW, version);
    }

    /// <summary>
    /// 使用WWWCacheOrDownload从网络URL加载bundle(异步)
    /// </summary>
    /// <param name="url"></param>
    /// <param name="callback"></param>
    /// <param name="version"></param>
    public static void LoadFromWWWCacheOrDownloadAsync(string url, Action<AssetBundle> callback, int version)
    {
        assetBundleLoader.LoadAssetBundleAsync(url, callback, BundleLoadType.LoadFromCacheOrDownload, version);
    }

    public static void LoadFromWebRequestAsync(string url, Action<AssetBundle> callback, int version)
    {
        assetBundleLoader.LoadAssetBundleAsync(url, callback, BundleLoadType.WebRequest, version);
    }
}

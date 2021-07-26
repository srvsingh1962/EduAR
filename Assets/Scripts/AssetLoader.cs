using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class AssetLoader : MonoBehaviour
{
    public List<GameObject> loadedAssets = new List<GameObject>();

    void Start()
    {
        Addressables.LoadAssetsAsync<GameObject>("AlphabetsModel", OnLoadDone);
    }

    private void OnLoadDone(GameObject obj)
    {
        loadedAssets.Add(obj);
    }
}

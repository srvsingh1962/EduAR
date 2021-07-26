using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class Demo : MonoBehaviour
{
    public AssetReference localmodel;

    [SerializeField] Transform spwanedobject;

    void Start()
    {
        loadmodel();
    }

    private void loadmodel()
    {
        Debug.Log("");
        localmodel.InstantiateAsync(Vector3.zero, Quaternion.identity, spwanedobject);
    }

    void Update()
    {
        
    }
}

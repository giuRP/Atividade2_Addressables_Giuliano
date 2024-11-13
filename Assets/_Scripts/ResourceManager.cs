using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class ResourceManager : MonoBehaviour
{
    public AssetReference assetReference;

    private AsyncOperationHandle<GameObject> operationHandle;
    private List<GameObject> prefabs = new List<GameObject>();

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (prefabs.Count <= 0)
                LoadAsset();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (prefabs.Count > 0)
                UnloadAsset();
        }
    }

    private void LoadAsset()
    {
        operationHandle = assetReference.InstantiateAsync(transform.position, Quaternion.identity);
        operationHandle.Completed += handle => { prefabs.Add(operationHandle.Result); };
    }

    private void UnloadAsset()
    {
        foreach(GameObject prefab in prefabs)
        {
            Addressables.ReleaseInstance(prefab);
        }
        prefabs.Clear();
    }
}

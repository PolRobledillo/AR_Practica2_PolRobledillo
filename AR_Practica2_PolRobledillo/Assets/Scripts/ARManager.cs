using System.Collections.Generic;
using System.IO;
using UnityEngine;
using System.Collections;

public class ARManager : MonoBehaviour
{
    public GameObject target;
    public Transform spawnPosition;
    public GameObject currentModel;
    public int currentModelIndex = 0;
    public GameObject placeHolderModel;
    public List<GameObject> fbxList = new List<GameObject>();

    void Start()
    {
        LoadAssetBundles();
    }

    public void LoadAssetBundles()
    {
        StartCoroutine(TryLoadAllAssetBundles());
    }

    IEnumerator TryLoadAllAssetBundles()
    {
        string persistentPath = Application.persistentDataPath;

        if (!Directory.Exists(persistentPath))
        {
            yield break;
        }

        string[] files = Directory.GetFiles(persistentPath);

        if (files.Length == 0)
        {
            yield break;
        }

        foreach (string filePath in files)
        {
            if (Path.GetExtension(filePath).ToLower() != "")
            {
                continue;
            }

            AssetBundle bundle = AssetBundle.LoadFromFile(filePath);

            if (bundle == null)
            {
                continue;
            }

            GameObject[] fbxAssets = bundle.LoadAllAssets<GameObject>();

            if (fbxAssets.Length == 0)
            {
                bundle.Unload(false);
                continue;
            }

            foreach (GameObject fbx in fbxAssets)
            {
                fbxList.Add(fbx);
            }

            bundle.Unload(false);
        }


        if (fbxList.Count > 0)
        {
            placeHolderModel.SetActive(false);
            currentModel = Instantiate(fbxList[0], spawnPosition.transform);
        }
        else
        {
            placeHolderModel.SetActive(true);
        }
    }
    public void LoadNextFBX()
    {
        if (fbxList.Count == 0)
        {
            return;
        }
        if (currentModel != null)
        {
            Destroy(currentModel);
        }
        currentModelIndex = currentModelIndex + 1 >= fbxList.Count ? 0 : currentModelIndex + 1;
        currentModel = Instantiate(fbxList[currentModelIndex], spawnPosition.transform);
    }
    public void LoadPreviousFBX()
    {
        if (fbxList.Count == 0)
        {
            return;
        }
        if (currentModel != null)
        {
            Destroy(currentModel);
        }
        currentModelIndex = currentModelIndex - 1 < 0 ? fbxList.Count - 1 : currentModelIndex - 1;
        currentModel = Instantiate(fbxList[currentModelIndex], spawnPosition.transform);
    }
}

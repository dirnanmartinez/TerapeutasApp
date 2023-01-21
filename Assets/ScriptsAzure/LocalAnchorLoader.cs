using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public static class LocalAnchorLoader
{ 
    static string anchorDir = "AnchorAppData";
    static string fileName = "AzureAnchors.txt";

    public static void Save(RootAnchorData data)
    {
        if (!DirectoryExists())
            Directory.CreateDirectory(Application.persistentDataPath + "/" + anchorDir);

        string jsonDataString = JsonUtility.ToJson(data, true);
        string filePath = GetFullPath();
        File.WriteAllText(filePath, jsonDataString);
    }

    public static RootAnchorData Load()
    {
        if (SaveExists())
        {
            try
            {
                string filePath = GetFullPath();
                string file = File.ReadAllText(filePath);
                RootAnchorData data = JsonUtility.FromJson<RootAnchorData>(file);
                return data;
            }
            catch (Exception e)
            {
                Debug.Log("Error: " + e);
            }
        }

        return null;
    }

    private static bool SaveExists()
    {
        return File.Exists(GetFullPath());
    }

    private static bool DirectoryExists()
    {
        return File.Exists(Application.persistentDataPath + "/" + anchorDir);
    }

    private static string GetFullPath()
    {
        return Application.persistentDataPath + "/" + anchorDir + "/" + fileName;
    }
}




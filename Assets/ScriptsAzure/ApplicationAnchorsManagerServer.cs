
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Networking;


public class ApplicationAnchorsManagerServer : MonoBehaviour
{
    public static ApplicationAnchorsManagerServer Instance;
    [SerializeField] private AzureAnchorLifeCycleManager anchorLifeCycleManager;
    public List<GameObject> ARobjects = new List<GameObject>();

    public List<ServerGetResponses.Activity> activities = new List<ServerGetResponses.Activity>();
    public List<ServerGetResponses.Step> steps = new List<ServerGetResponses.Step>();
    [SerializeField] private ServerGetResponses.Step step = new ServerGetResponses.Step();
    [SerializeField] private ServerGetResponses.InteractiveSpace interactiveSpace = new ServerGetResponses.InteractiveSpace();

    private readonly string urlActivitiesOnUser = "https://serviciotfg.azurewebsites.net/api/Activities/GetActivities?owner=";
    private readonly string urlStepsOnActivity = "https://serviciotfg.azurewebsites.net/api/Steps/GetSteps?idActivity=";
    private readonly string urlUpdateStep = "https://serviciotfg.azurewebsites.net/api/Steps/UpdateStep";

    private List<GameObject> CurrentARObjects = new List<GameObject>();
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    void Start()
    {
        GameManager.Instance.OnLocalLoginSuccess += OnLocalLoginSuccess;
        GameManager.Instance.OnARObjectPlaced += GenerateAnchorOnARObjectPlaced;

        anchorLifeCycleManager.OnCreateAnchorSucceeded += OnCreateAnchorSucceeded;
        anchorLifeCycleManager.OnAnchorPlaced += OnAnchorPlaced;

        GameManager.Instance.OnRegisterSpace += OnRegisterSpace;
    }

    #region Crear Anchor
    private void OnLocalLoginSuccess()
    {

        anchorLifeCycleManager.StartAzureSession();

    }

    private void GenerateAnchorOnARObjectPlaced(GameObject aRObject)
    {
        Debug.Log("New Anchor for: " + aRObject.name);

        ARobjects.Add(aRObject);
        anchorLifeCycleManager.CreateAzureAnchor(aRObject);
    }

    private void OnCreateAnchorSucceeded(AnchorData anchorData)
    {
        string anchorID = anchorData.AnchorID;
        UIAzureManager.Instance.TextCanvas = "Guardando Anchor, por favor espere....";
        PostAnchorData(anchorID);
    }

    private void PostAnchorData(string anchorID)
    {
        StartCoroutine(PostServer(anchorID));
    }

    #endregion

    #region consumir anchor
    private void OnRegisterSpace()
    {

    }

    public void TryToFindAnchor(string anchorID)
    {
        anchorLifeCycleManager.FindAzureAnchor(anchorID);
    }

    int tempIndex;
    private void OnAnchorPlaced(ObjectAnchorData objectAnchorData)
    {

        GameManager.Instance.CurrentARObjectNameOnStep = "Column"; // Debug - No la necesitas sea mismo nombre en servidor y en los objetos;

        List<ServerManager.Items.Item> itemsList = GameManager.Instance.ServerManager.newItemsCollection.items.ToList();
        ServerManager.Items.Item item = itemsList.Find(x => x.Name == GameManager.Instance.CurrentARObjectNameOnStep);

        if (item.Name == "")
            return;

        GameObject result = CheckBundle(item.Name);

        if (result == null)
        {
            StartCoroutine(AsynGetBundle(item, objectAnchorData.aRObjectContainer));
        }
        else
        {
            GameObject model3DObject = Instantiate(result, objectAnchorData.aRObjectContainer.transform);
            model3DObject.name = item.Name;
            objectAnchorData.aRObjectContainer.transform.GetChild(0).gameObject.SetActive(false);
            ARobjects.Add(model3DObject);
        }
    }

    public GameObject CheckBundle(string bundleName)
    {
        Debug.Log("I will check: " + bundleName);
        GameObject result = CurrentARObjects.Find(x => x.name == bundleName);
        return result;
    }

    public void AddBundle(GameObject aRObject)
    {
        Debug.Log("New Bundle : " + aRObject.name);
        CurrentARObjects.Add(aRObject);
    }
    private IEnumerator AsynGetBundle(ServerManager.Items.Item item, GameObject aRContainer)
    {
        string urlAssetBundle = item.URLBundleModel;
        Debug.Log("Get 3D Model: " + item.Name + " " + urlAssetBundle);
        UnityWebRequest serverRequest = UnityWebRequestAssetBundle.GetAssetBundle(urlAssetBundle);
        yield return serverRequest.SendWebRequest();
        if (serverRequest.result == UnityWebRequest.Result.Success)
        {
            AssetBundle model3D = DownloadHandlerAssetBundle.GetContent(serverRequest);
            if (model3D != null)
            {
                GameObject model3DObject = Instantiate(model3D.LoadAsset(model3D.GetAllAssetNames()[0]) as GameObject, aRContainer.transform);
                model3DObject.name = item.Name;
                aRContainer.transform.GetChild(0).gameObject.SetActive(false);
                ARobjects.Add(model3DObject);
                CurrentARObjects.Add(model3DObject);
                AssetBundle.UnloadAllAssetBundles(false);
            }
            else
            {
                UIAzureManager.Instance.TextCanvas = "No es posible conectar con server";
                Debug.Log("Not a valid Assets Bundle");
            }
        }
        else
        {
            Debug.Log("Error: " + serverRequest.result);
        }
    }

    public void DeleteARObjects() // llamar 
    {
        if (ARobjects.Count == 0)
        {
            return;
        }
        foreach (var aRObject in ARobjects)
        {
            Destroy(aRObject);
        }

        CurrentARObjects = new List<GameObject>();
        ARobjects = new List<GameObject>();
        GameManager.Instance.CurrentStepForSaveAnchor = 0;
    }
    #endregion;
    #region Server


    public void GetDataFromServer(string urlComplement, GetTypeServer getType)
    {
        StartCoroutine(GETServer(urlComplement, getType));
    }
    private IEnumerator GETServer(string urlComplementOne, GetTypeServer getType)
    {
        string fullURL = GetFullURL(urlComplementOne, getType);
        UnityWebRequest serverRequest = UnityWebRequest.Get(fullURL);
        yield return serverRequest.SendWebRequest();
        if (serverRequest.result == UnityWebRequest.Result.Success)
        {
            string jsonResult = serverRequest.downloadHandler.text;

            switch (getType)
            {
                case GetTypeServer.Activities:
                    activities = JsonConvert.DeserializeObject<List<ServerGetResponses.Activity>>(jsonResult);
                    break;
                case GetTypeServer.Steps:
                    steps = JsonConvert.DeserializeObject<List<ServerGetResponses.Step>>(jsonResult);
                    break;
            }
        }
        else
        {
            Debug.Log("Error: " + serverRequest.result);
        }
    }

    private string GetFullURL(string urlComplement, GetTypeServer getType)
    {
        string fullURL = string.Empty;
        switch (getType)
        {
            case GetTypeServer.Activities:
                fullURL = urlActivitiesOnUser + urlComplement;
                break;
            case GetTypeServer.Steps:
                fullURL = urlStepsOnActivity + urlComplement;
                break;
        }

        return fullURL;
    }

    private IEnumerator PostServer(string anchorID)
    {
        BodyPut serverPostRequest = new BodyPut();

        serverPostRequest.id = steps[GameManager.Instance.CurrentStepForSaveAnchor].id;
        serverPostRequest.groupal = steps[GameManager.Instance.CurrentStepForSaveAnchor].groupal;
        serverPostRequest.isSupervised = steps[GameManager.Instance.CurrentStepForSaveAnchor].isSupervised;
        int tempID;
        if (GameManager.Instance.CurrentStepForSaveAnchor == 0)
        {
            tempID = 1;

        }
        else
        {
            tempID = 2;
        }
        serverPostRequest.interactiveSpace3DId = tempID; // To Change
        serverPostRequest.interactiveSpaceName = steps[GameManager.Instance.CurrentStepForSaveAnchor].interactiveSpaceName;
        serverPostRequest.anchorId = anchorID;
        serverPostRequest.typeOfStep = steps[GameManager.Instance.CurrentStepForSaveAnchor].type;

        string jsonString = JsonConvert.SerializeObject(serverPostRequest);

        Debug.Log(jsonString);
        Debug.Log(urlUpdateStep);


        UnityWebRequest webRequest = UnityWebRequest.Put(urlUpdateStep, jsonString);

        webRequest.SetRequestHeader("Content-Type", "application/json");

        yield return webRequest.SendWebRequest();


        if (webRequest.result == UnityWebRequest.Result.Success)
        {
            UIAzureManager.Instance.TextCanvas = "Anchor Guardada";
            Debug.Log("Yeiii Anchor saved " + anchorID);
            GameManager.Instance.CurrentStepForSaveAnchor++;
        }
        else
        {
            Debug.Log("ServerPost eror" + webRequest.error);
        }
    }
    #endregion
}

[Serializable]
public class ServerGetResponses
{
    [Serializable]
    public class Activity
    {
        public int id;
        public string name;
        public string description;
        public string finalMessageOK;
        public string finalMessageError;
        public double maxTime;
        public string owner;
    }

    [Serializable]
    public class Step
    {
        public int id;
        public bool groupal;
        public bool isSupervised;
        public string interactiveSpaceName;
        public string anchorId;
        public List<StepDescription> stepDescriptions;
        public object feedbackPath;
        public int? previousStep;
        public string type;

        [Serializable]
        public class Action
        {
            public string description;
            public string feedbackMessage;
            public string actionType;
            public string animationIdPrefab;
        }
        [Serializable]
        public class Entity
        {
            public string entityPath;
            public string entityName;
            public int x;
            public int y;
            public int z;
            public int rotX;
            public int rotY;
            public int rotZ;
            public int scaleX;
            public int scaleY;
            public int scaleZ;
            public List<Action> actions;
        }

        [Serializable]
        public class StepDescription
        {
            public int id;
            public string description;
            public List<Entity> entities;
        }
    }

    [Serializable]
    public class InteractiveSpace
    {
        public int id;
        public string name;
        public string description;
        public string visibility;
        public string anchorId;
        public string owner;
    }
}

[Serializable]
public class BodyPut
{
    public int id;
    public bool groupal;
    public bool isSupervised;
    public long interactiveSpace3DId;
    public string interactiveSpaceName;
    public string anchorId;
    public string typeOfStep;
}

public enum GetTypeServer
{
    Activities,
    Steps,
    Step,
    InteractiveSpace3D,
}

[Serializable]
public class RootAnchorData
{
    public List<ActivityData> activities = new List<ActivityData>();
}
[Serializable]
public class ActivityData
{
    public int ActivityId;
    public List<AnchorData> AnchorsSaved = new List<AnchorData>();
}

[Serializable]
public class AnchorData
{
    public string ARObjectName;
    public string AnchorID;
    public Vector3 AnchorPosition;
    public Quaternion AnchorRotation;
}
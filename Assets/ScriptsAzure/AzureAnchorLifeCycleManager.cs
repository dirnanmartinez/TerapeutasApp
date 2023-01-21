
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Microsoft.Azure.SpatialAnchors;
using Microsoft.Azure.SpatialAnchors.Unity;
using System;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class AzureAnchorLifeCycleManager : MonoBehaviour
{

    [SerializeField] private SpatialAnchorManager cloudManager;

    private GameObject gameObjectToAnchor;
    [SerializeField] private int timeToDeleteLocalAnchor = 90;
    [SerializeField] private GameObject anchorPrefab;
    public string currentAzureAnchorID = "";
    public string removeAnchor = "Inactive";

    private CloudSpatialAnchor currentCloudAnchor;
    private AnchorLocateCriteria anchorLocateCriteria;
    private CloudSpatialAnchorWatcher currentWatcher;

    public GameObject GameObjectToAnchor { private get => gameObjectToAnchor; set => gameObjectToAnchor = value; }

    private readonly Queue<Action> dispatchQueue = new Queue<Action>();

    public SpatialAnchorManager SpatialAnchorManager { get => cloudManager; }
    
    void Start()
    {
        cloudManager.AnchorLocated += CloudManager_AnchorLocated;
        anchorLocateCriteria = new AnchorLocateCriteria();

    }


    // Update is called once per frame
    void Update()
    {
        lock (dispatchQueue)
        {
            if (dispatchQueue.Count > 0)
            {
                dispatchQueue.Dequeue()();
            }
        }
    }

    private void OnDestroy()
    {
        if (cloudManager != null && cloudManager.Session != null)
        {
            cloudManager.DestroySession();
        }
        if (currentWatcher != null)
        {
            currentWatcher.Stop();
            currentWatcher = null;
        }
    }

    public async void StartAzureSession()
    {
        OnStartASASession?.Invoke();

        Debug.Log("Starting Azure session... please wait...");
  
        if (cloudManager.Session == null)
        {
            await cloudManager.CreateSessionAsync();
        }

        await cloudManager.StartSessionAsync();

        Debug.Log("Azure session started successfully");

        UIAzureManager.Instance.TextCanvas = "Azure se ha iniciado correctamente";
    }

    public async void StopAzureSession()
    {
        OnEndASASession?.Invoke();

        Debug.Log("Stopping Azure session... please wait...");
        cloudManager.StopSession();

        await cloudManager.ResetSessionAsync();

        Debug.Log("Azure session stopped successfully");
    }

    public async void CreateAzureAnchor(GameObject aRObjectToAnchor)
    {
        Debug.Log("Azure will try to create anchor for: " + aRObjectToAnchor.name);
        removeAnchor = "Active";
        OnCreateAnchorStarted?.Invoke();
        aRObjectToAnchor.CreateNativeAnchor();

        OnCreateLocalAnchor?.Invoke();

        await Task.Delay(1000);

        CloudSpatialAnchor localCloudAnchor = new CloudSpatialAnchor();

        localCloudAnchor.LocalAnchor = await aRObjectToAnchor.FindNativeAnchor().GetPointer();

        if (localCloudAnchor.LocalAnchor == IntPtr.Zero)
        {
            Debug.Log("Didn't get the local anchor...");
            return;
        }
        else
        {
            Debug.Log("Local anchor created");

        }

        localCloudAnchor.Expiration = DateTimeOffset.Now.AddDays(timeToDeleteLocalAnchor);

        while (!cloudManager.IsReadyForCreate)
        {
            await Task.Delay(330);
            float createProgress = cloudManager.SessionStatus.RecommendedForCreateProgress;
            UIAzureManager.Instance.TextCanvas = string.Format($"Capturando el entorno: {createProgress:0%}");
            QueueOnUpdate(new Action(() => Debug.Log($"Move your device to capture more environment data: {createProgress:0%}")));
        }

        bool success;
        try
        {
            Debug.Log("Creating Azure anchor... please wait...");
            UIAzureManager.Instance.TextCanvas = "Creando Anchor, por favor espere...";
            await cloudManager.CreateAnchorAsync(localCloudAnchor);

            currentCloudAnchor = localCloudAnchor;
            localCloudAnchor = null;

            success = currentCloudAnchor != null;
            if (success)
            {
                Debug.Log($"Azure anchor with ID '{currentCloudAnchor.Identifier}' created successfully");
                UIAzureManager.Instance.TextCanvas = "Anchor creada";

                AnchorData tempData = new AnchorData();

                tempData.AnchorID = currentCloudAnchor.Identifier;
                tempData.ARObjectName = aRObjectToAnchor.name;
                tempData.AnchorPosition = aRObjectToAnchor.transform.position;
                tempData.AnchorRotation = aRObjectToAnchor.transform.rotation;

                OnCreateAnchorSucceeded?.Invoke(tempData);

                Debug.Log($"Current Azure anchor ID updated to '{currentCloudAnchor.Identifier}'");
                currentAzureAnchorID = currentCloudAnchor.Identifier;
            }
            else
            {
                UIAzureManager.Instance.TextCanvas = "Error al crear Anchor";
                Debug.Log($"Failed to save cloud anchor with ID '{currentAzureAnchorID}' to Azure");

                OnCreateAnchorFailed?.Invoke();
            }
        }
        catch (Exception ex)
        {
            Debug.Log(ex.ToString());
        }
    }

    public void FindAzureAnchor(List<string> anchorsToFind) 
    {
        if (anchorsToFind.Count == 0)
        {
            return;
        }

        anchorLocateCriteria.Identifiers = anchorsToFind.ToArray();
        Debug.Log($"Anchor locate criteria configured to look for Azure anchor with ID '{currentAzureAnchorID}'");

        if ((cloudManager != null) && (cloudManager.Session != null))
        {
            currentWatcher = cloudManager.Session.CreateWatcher(anchorLocateCriteria);
            Debug.Log("Watcher created");
            Debug.Log("Looking for Azure anchor... please wait...");

        }
        else
        {
            Debug.Log("Attempt to create watcher failed, no session exists");
            currentWatcher = null;
        }
    }
    public void FindAzureAnchor(string anchorID = "")
    {
        if (anchorID != "")
        {
            currentAzureAnchorID = anchorID;
        }

        OnFindASAAnchor?.Invoke();

        List<string> anchorsToFind = new List<string>();

        if (currentAzureAnchorID != "")
        {
            anchorsToFind.Add(currentAzureAnchorID);
        }
        else
        {
            Debug.Log("Current Azure anchor ID is empty");
            return;
        }

        anchorLocateCriteria.Identifiers = anchorsToFind.ToArray();
        Debug.Log($"Anchor locate criteria configured to look for Azure anchor with ID '{currentAzureAnchorID}'");

        if ((cloudManager != null) && (cloudManager.Session != null))
        {
            currentWatcher = cloudManager.Session.CreateWatcher(anchorLocateCriteria);
            Debug.Log("Watcher created");
            Debug.Log("Looking for Azure anchor... please wait...");
            UIAzureManager.Instance.TextCanvas = "Buscando Anchor, por favor espere...";
        }
        else
        {
            Debug.Log("Attempt to create watcher failed, no session exists");
            currentWatcher = null;
        }
    }

    public async void DeleteAzureAnchor()
    {
        Debug.Log("\nAnchorModuleScript.DeleteAzureAnchor()");


        OnDeleteASAAnchor?.Invoke();

        await cloudManager.DeleteAnchorAsync(currentCloudAnchor);
        currentCloudAnchor = null;

        Debug.Log("Azure anchor deleted successfully");
    }

    public void SaveAzureAnchorIdToDisk()
    {
        string fileName = "SavedAzureAnchorID.txt";

        string folderPath = (Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer ? Application.persistentDataPath : Application.dataPath) + "/myDataFolder/";
        string filePath = folderPath + fileName;


        if (!Directory.Exists(folderPath))
        {
            Directory.CreateDirectory(folderPath);
        }

        File.WriteAllText(filePath, currentAzureAnchorID);

        Debug.Log($"Current Azure anchor ID '{currentAzureAnchorID}' successfully saved to path '{filePath}'");
    }

    public void GetAzureAnchorIdFromDisk()
    {
        string filename = "SavedAzureAnchorID.txt";
        string path = Application.persistentDataPath + "/myDataFolder/";

        string filePath = Path.Combine(path, filename);
        currentAzureAnchorID = File.ReadAllText(filePath);

        Debug.Log($"Current Azure anchor ID successfully updated with saved Azure anchor ID '{currentAzureAnchorID}' from path '{path}'");
    }
    private void CloudManager_AnchorLocated(object sender, AnchorLocatedEventArgs args)
    {
        QueueOnUpdate(new Action(() => Debug.Log($"Anchor recognized as a possible Azure anchor")));

        if (args.Status == LocateAnchorStatus.Located || args.Status == LocateAnchorStatus.AlreadyTracked)
        {
            currentCloudAnchor = args.Anchor;
            QueueOnUpdate(() =>
            {
                Debug.Log($"Azure anchor located successfully");
                UIAzureManager.Instance.TextCanvas = "Anchor Localizada";
                Pose anchorPose = Pose.identity;
                anchorPose = currentCloudAnchor.GetPose();


                Debug.Log($"Setting object to anchor pose with position '{anchorPose.position}' and rotation '{anchorPose.rotation}'");

                ObjectAnchorData objectAnchorData = new ObjectAnchorData();

                if (gameObjectToAnchor == null)
                {
                    GameObject aRContainer = Instantiate(anchorPrefab);
                    aRContainer.name = "AnchorRoot";
                    aRContainer.transform.position = anchorPose.position;
                    aRContainer.transform.rotation = anchorPose.rotation;

                    aRContainer.gameObject.CreateNativeAnchor();

                    objectAnchorData.AnchorID = args.Identifier;
                    objectAnchorData.aRObjectContainer = aRContainer;
                }
                else
                {
                    gameObjectToAnchor.transform.position = anchorPose.position;
                    gameObjectToAnchor.transform.rotation = anchorPose.rotation;

                    gameObjectToAnchor.gameObject.CreateNativeAnchor();

                    objectAnchorData.AnchorID = args.Identifier;
                    objectAnchorData.aRObjectContainer = gameObjectToAnchor;

                }

                OnAnchorPlaced?.Invoke(objectAnchorData);
                currentWatcher.Stop();
            });
        }
        else
        {
            QueueOnUpdate(new Action(() => Debug.Log($"Attempt to locate Anchor with ID '{args.Identifier}' failed, locate anchor status was not 'Located' but '{args.Status}'")));

        }
    }
    private void QueueOnUpdate(Action updateAction)
    {
        lock (dispatchQueue)
        {
            dispatchQueue.Enqueue(updateAction);
        }
    }

    public event Action OnStartASASession;
    public event Action OnEndASASession;
    public event Action OnCreateAnchorStarted;
    public event Action OnCreateLocalAnchor;
    public event Action<AnchorData> OnCreateAnchorSucceeded;
    public event Action OnCreateAnchorFailed;
    public event Action OnFindASAAnchor;
    public event Action OnDeleteASAAnchor;
    public event Action<ObjectAnchorData> OnAnchorPlaced;
}

[Serializable]
public class ObjectAnchorData
{
    public string AnchorID;
    public GameObject aRObjectContainer;
    public Vector3 AnchorPosition;
    public Quaternion AnchorRotation;
}
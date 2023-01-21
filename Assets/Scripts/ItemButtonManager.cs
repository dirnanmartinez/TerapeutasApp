using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class ItemButtonManager : MonoBehaviour
{

    private string itemName;
    public string ItemName
    {
        set
        {
            itemName = value;
        }
    }

    private string itemDescription;
    public string ItemDescription
    {
        set => itemDescription = value;  //Operador Lambda
    }

    private Sprite itemImage;
    public Sprite ItemImage { set => itemImage = value; }

    private GameObject item3DModel;
    public GameObject Item3DModel { set => item3DModel = value; }

    private ARInteractionsManager interactionsManager;

    private string urlBundleModel;
    private RawImage imageBundle;

    public string URLBundleModel { set => urlBundleModel = value; }
    public RawImage ImageBundle { get => imageBundle; set => imageBundle = value; }


// Start is called before the first frame update
void Start()
    {
        transform.GetChild(0).GetComponent<Text>().text = itemName;
        //transform.GetChild(1).GetComponent<RawImage>().texture = itemImage.texture;
        imageBundle = transform.GetChild(1).GetComponent<RawImage>();
        transform.GetChild(2).GetComponent<Text>().text = itemDescription;

        var button = GetComponent<Button>();
        //button.onClick.AddListener(GameManager.instance.ARPosition);
        button.onClick.AddListener(GameManager.Instance.ARPositionObjectMenu);
        button.onClick.AddListener(Create3DModel);

        interactionsManager = FindObjectOfType<ARInteractionsManager>();

    }

    private void Create3DModel()
    {
        //interactionsManager.Item3DModel = Instantiate(item3DModel);
        GameObject result = ApplicationAnchorsManagerServer.Instance.CheckBundle(itemName);

        if (result == null)
        {
            StartCoroutine(DownLoadAssetBundle(urlBundleModel));
        }
        else
        {
            GameObject tempGO = Instantiate(result);
            tempGO.name = itemName;
            interactionsManager.Item3DModel = tempGO;
        }

    }

    IEnumerator DownLoadAssetBundle(string urlAssetBundle)
    {
        UnityWebRequest serverRequest = UnityWebRequestAssetBundle.GetAssetBundle(urlAssetBundle);
        yield return serverRequest.SendWebRequest();
        if(serverRequest.result == UnityWebRequest.Result.Success)
        {
            AssetBundle model3D = DownloadHandlerAssetBundle.GetContent(serverRequest);
            if(model3D != null)
            {
                GameObject tempARModel = Instantiate(model3D.LoadAsset(model3D.GetAllAssetNames()[0]) as GameObject);
                AssetBundle.UnloadAllAssetBundles(false);
                tempARModel.name = itemName;
                interactionsManager.Item3DModel = tempARModel;
                ApplicationAnchorsManagerServer.Instance.AddBundle(tempARModel);

            }
            else
            {
                Debug.Log("Not a valid Assets Bundle");
            }
        }
        else
        {
            Debug.Log("Error");
        }
    }

}

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
        button.onClick.AddListener(GameManager.instance.ARPositionObjectMenu);
        button.onClick.AddListener(Create3DModel);

        interactionsManager = FindObjectOfType<ARInteractionsManager>();

    }

    private void Create3DModel()
    {
        //interactionsManager.Item3DModel = Instantiate(item3DModel);
        StartCoroutine(DownLoadAssetBundle(urlBundleModel));
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
                interactionsManager.Item3DModel = Instantiate(model3D.LoadAsset(model3D.GetAllAssetNames()[0]) as GameObject);

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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using System.Collections.Generic;
using PlayFab;
using PlayFab.ServerModels;
using TMPro;
using System.Text;
using PlayFab.ClientModels;
using ItemInstance = PlayFab.ServerModels.ItemInstance;
using GetUserInventoryRequest = PlayFab.ServerModels.GetUserInventoryRequest;
using GetUserInventoryResult = PlayFab.ServerModels.GetUserInventoryResult;
using DG.Tweening;
using System.Linq;
using UnityEngine.Networking;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

public class GameManager : MonoBehaviour
{
    #region Actions
    public event Action OnLoading;
    public event Action OnLogin;
    public event Action OnRegister;
    public event Action OnOptions;
    public event Action OnDescActivity;
    public event Action OnAsistenteInfo;
    public event Action OnAsistenteStartPaso;
    public event Action OnRegisterSpace;
    public event Action OnBoxObjetosOpen;
    public event Action OnARPositionObject;
    public event Action OnNextPaso;
    public event Action OnEndActivity;

    public event Action OnItemsMenu;
    public event Action OnARPosition;
    #endregion

    #region SerializeField
    [Header("Canvas")]
    [SerializeField] private GameObject loginCanvas;
    [SerializeField] private GameObject optionsCanvas;
    [SerializeField] private GameObject descActivityCanvas;
    [SerializeField] private GameObject endActivityCanvas;

    [Header("Login")]
    public Text messageText;
    public InputField emailInput;
    public InputField passwordInput;

    [Header("Register")]
    public Text messageInfo;
    public InputField emailInputRegister;
    public InputField password1InputRegister;
    public InputField password2InputRegister;


    [Header("Descripcion Actividad")]
    public Text nameAct;
    public Text pacienteAct;
    public Text descAct;
    public Text pasosAct;

    [Header("Asistente Info")]
    public Text nameActInfo;

    [Header("StartPaso")]
    public Text namePaso1;
    public Text bocadilloNombreAct;
    public Text bocadilloPrimerPaso;
    public Text bocadilloNombreUbicacion;
    public Text bocadilloObjetosPaso1;

    [Header("Objeto a colocar")]
    public Text objetoAColocar;

    [Header("Next Paso")]
    public Text actNamee;
    
    [Header("End Activity")]
    public Text bocadilloNombreActFin;
    public Text actFinName;


    #region azure impletation

    [SerializeField] private ServerManager serverManager;
    public ServerManager ServerManager => serverManager;
    public event Action OnLocalLoginSuccess;
    public event Action<GameObject> OnARObjectPlaced;

    public string CurrentARObjectNameOnStep;
    public string CurrentAnchorIDToFind;
    public string CurrentUserOnApp;

    public int CurrentStepForSaveAnchor;


    #endregion


    #region Texts
    [Header("Datos de la actividad para mostrar")]
    [SerializeField] private Text _tittle0;
    [SerializeField] private Text _tittle1;
    [SerializeField] private Text _tittle2;
    [SerializeField] private Text _tittle3;
    [SerializeField] private Text _tittle4;
    [SerializeField] private Text _tittle5;
    [SerializeField] private Text _tittle6;
    [SerializeField] private Text _tittle7;
    [SerializeField] private Text _tittle8;
    [SerializeField] private Text _tittle9;
    [SerializeField] private Text _tittle10;
    [SerializeField] private Text _tittle11;
    [SerializeField] private Text _tittle12;
    [SerializeField] private Text _tittle13;
    [SerializeField] private Text _tittle14;
    [SerializeField] private Text _tittle15;
    [SerializeField] private Text _tittle16;
    [SerializeField] private Text _tittle17;
    [SerializeField] private Text _tittle18;
    [SerializeField] private Text _tittle19;
    [SerializeField] private Text _tittle20;
    [SerializeField] private Text _tittle21;
    [SerializeField] private Text _tittle22;
    [SerializeField] private Text _tittle23;
    [SerializeField] private Text _tittle24;
    [SerializeField] private Text _tittle25;
    [SerializeField] private Text _tittle26;
    [SerializeField] private Text _tittle27;
    [SerializeField] private Text _tittle28;
    [SerializeField] private Text _tittle29;
    [SerializeField] private Text _tittle30;
    [SerializeField] private Text _tittle31;
    [SerializeField] private Text _tittle32;
    [SerializeField] private Text _tittle33;
    [SerializeField] private Text _tittle34;
    [SerializeField] private Text _tittle35;
    [SerializeField] private Text _tittle36;
    [SerializeField] private Text _tittle37;
    [SerializeField] private Text _tittle38;
    [SerializeField] private Text _tittle39;
    [SerializeField] private Text _tittle40;
    [SerializeField] private Text _tittle41;
    [SerializeField] private Text _tittle42;
    [SerializeField] private Text _tittle43;
    [SerializeField] private Text _tittle44;
    [SerializeField] private Text _tittle45;
    [SerializeField] private Text _tittle46;
    [SerializeField] private Text _tittle47;
    [SerializeField] private Text _tittle48;
    [SerializeField] private Text _tittle49;

    [SerializeField] private Text _description0;
    [SerializeField] private Text _description1;
    [SerializeField] private Text _description2;
    [SerializeField] private Text _description3;
    [SerializeField] private Text _description4;
    [SerializeField] private Text _description5;
    [SerializeField] private Text _description6;
    [SerializeField] private Text _description7;
    [SerializeField] private Text _description8;
    [SerializeField] private Text _description9;
    [SerializeField] private Text _description10;
    [SerializeField] private Text _description11;
    [SerializeField] private Text _description12;
    [SerializeField] private Text _description13;
    [SerializeField] private Text _description14;
    [SerializeField] private Text _description15;
    [SerializeField] private Text _description16;
    [SerializeField] private Text _description17;
    [SerializeField] private Text _description18;
    [SerializeField] private Text _description19;
    [SerializeField] private Text _description20;
    [SerializeField] private Text _description21;
    [SerializeField] private Text _description22;
    [SerializeField] private Text _description23;
    [SerializeField] private Text _description24;
    [SerializeField] private Text _description25;
    [SerializeField] private Text _description26;
    [SerializeField] private Text _description27;
    [SerializeField] private Text _description28;
    [SerializeField] private Text _description29;
    [SerializeField] private Text _description30;
    [SerializeField] private Text _description31;
    [SerializeField] private Text _description32;
    [SerializeField] private Text _description33;
    [SerializeField] private Text _description34;
    [SerializeField] private Text _description35;
    [SerializeField] private Text _description36;
    [SerializeField] private Text _description37;
    [SerializeField] private Text _description38;
    [SerializeField] private Text _description39;
    [SerializeField] private Text _description40;
    [SerializeField] private Text _description41;
    [SerializeField] private Text _description42;
    [SerializeField] private Text _description43;
    [SerializeField] private Text _description44;
    [SerializeField] private Text _description45;
    [SerializeField] private Text _description46;
    [SerializeField] private Text _description47;
    [SerializeField] private Text _description48;
    [SerializeField] private Text _description49;

    #endregion

    #endregion

    #region Control de pasos
    //VARIABLE PARA GUARDAR LAS ACTIVIDADES
    //hasta options
    List<ActivitiesByOwner> activitiesByOwner = new List<ActivitiesByOwner>();
    List<int> activitiesIds = new List<int>();
    int numActivitiesByOwner = 0;
    //hasta descripcion
    List<string> usuarios = new List<string>();
    List<Step> step = new List<Step>();
    List<int> stepsIds = new List<int>();
    int idActividadSeleccionada;
    int activity;
    //hasta StartPaso
    Step stepInformation = new Step();
    int stepAux = 0;
    //hasta registrar spacio
    string regUbicacionAntigua = null;

    //NEW
    InteractiveSpaces intSpace = new InteractiveSpaces();
    #endregion 


    //Patron Singleton
    public static GameManager Instance;
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

    //START DE LA APLICACI�N
    void Start()
    {
        //Llamo a la pantalla de loading
        LoadingMenu();
        //Llamo a collect Actividades
    }

    //MUESTRO PANTALLA DE LOADING
    #region Pantallas Login/Register
    public void LoadingMenu()
    {
        OnLoading?.Invoke();
        Debug.Log("Loading 0 menu ACTIVATED");
        StartCoroutine(Loadingg());
    }

    IEnumerator Loadingg()
    {
        yield return new WaitForSecondsRealtime(2);
        LoginMenu();
    }

    public void LoginMenu()
    {
        emailInputRegister.text = "";
        password1InputRegister.text = "";
        password2InputRegister.text = "";
        messageInfo.text = "";
        OnLogin?.Invoke();
        Debug.Log("Login menu ACTIVATED");
    }

    public void RegisterMenu()
    {
        emailInput.text = "";
        passwordInput.text = "";
        messageText.text = "";
        Debug.Log("Register menu ACTIVATED");
        OnRegister?.Invoke();
    }

    #endregion

    #region Llamadas Servidor
    IEnumerator CollectActivity2UserByActivityId()
    {
        int idActividad = idActividadSeleccionada;
        //int idActividad = 2;

        UnityWebRequest www = UnityWebRequest.Get("https://serviciotfg.azurewebsites.net/api/Activity2User/GetUsersAssigned2Activity?id=" + idActividad);
        yield return www.Send();

        if (www.isNetworkError)
        {
            Debug.Log(www.error);
        }
        var activity2user = JsonConvert.DeserializeObject < List < string >>(www.downloadHandler.text.ToString());
        usuarios = activity2user;
    }  //LISTA DE LOS PACIENTES QUE VAN A PODER JUGAR (SOLO PARA INFORMACION)
    IEnumerator CollectStepByActivtyId()
    {
        int idActividad = idActividadSeleccionada;
        //int idActividad = 6;

        UnityWebRequest www = UnityWebRequest.Get("https://serviciotfg.azurewebsites.net/api/Steps/GetSteps?idActivity=" + idActividad);
        yield return www.Send();

        if (www.isNetworkError)
        {
            Debug.Log(www.error);
        }
        var stepByActivitId = JsonConvert.DeserializeObject<List<Step>>(www.downloadHandler.text);
        step = stepByActivitId;

        foreach(var s in step)
        {
            stepsIds.Add(s.id);
        }

        InfoActivityComplete();
    }  //LISTA DE LOS PASOS QUE TIENE UN ACTIVIDAD
    IEnumerator CollectActivitiesByOwner()
    {
        string owner = emailInput.text;
        //List<int> ids = new List<int>();
        //string owner = "Terapeuta1@uclm.es";

        UnityWebRequest www = UnityWebRequest.Get("https://serviciotfg.azurewebsites.net/api/Activities/GetActivities?owner=" + owner);
        yield return www.Send();

        if (www.isNetworkError)
        {
            Debug.Log(www.error);
        }
        var actByOwner = JsonConvert.DeserializeObject<List<ActivitiesByOwner>>(www.downloadHandler.text);
        activitiesByOwner = actByOwner;

        foreach (var a in activitiesByOwner)
        {
            /*
            Debug.Log("id -->" + a.Id);
            Debug.Log("Name -->" + a.Name);
            Debug.Log("Description -->" + a.Description);
            Debug.Log("FinalMessageOK -->" + a.FinalMessageOK);
            Debug.Log("FinalMessageError -->" + a.FinalMessageError);
            Debug.Log("MaxTime -->" + a.MaxTime);
            Debug.Log("Owner -->" + a.Owner);
            */
            activitiesIds.Add(a.Id);
            numActivitiesByOwner++;
        }

        

        ActivateOptionsMenu();
    } //LISTA DE ACTIVIDADES DE UN TERAPEUTA
    IEnumerator GetStepsById()
    {
        int stepId = stepsIds[stepAux];

        //string owner = "Terapeuta1@uclm.es";

        UnityWebRequest www = UnityWebRequest.Get("https://serviciotfg.azurewebsites.net/api/Steps/GetStep?id=" + stepId);
        yield return www.Send();

        if (www.isNetworkError)
        {
            Debug.Log(www.error);
        }

        var stepInfo = JsonConvert.DeserializeObject<Step>(www.downloadHandler.text);
        stepInformation = stepInfo;

        yield return null;
        AsistenteStartPasoMenu();
       
        
    }  //INFORMACION DE UN PASO EN CONCRETO
    #endregion

    #region Metodos Azure
    public void ARObjectPlaced(GameObject aRObject) 
    {
        Debug.Log("I wil try to Anchor this Object: " + aRObject.name);
        OnARObjectPlaced?.Invoke(aRObject);
    }

    public void ActivityID(int ID) 
    {
        ApplicationAnchorsManagerServer.Instance.GetDataFromServer(ID.ToString(), GetTypeServer.Steps);

        activity = ID - 1;
        idActividadSeleccionada = activitiesIds[activity];
        StartCoroutine("CollectActivity2UserByActivityId");
        StartCoroutine("CollectStepByActivtyId");
    }

    public void DeleteObject()
    {
        string newAnchorAux = "0";
        ApplicationAnchorsManagerServer.Instance.PostAnchorData(newAnchorAux);
    }
    
    #endregion

    #region Login/Register
    public void RegisterButton()
    {
        if (password1InputRegister.text == password2InputRegister.text)
        {
            var request = new RegisterPlayFabUserRequest
            {
                Email = emailInputRegister.text,
                Password = password1InputRegister.text,
                RequireBothUsernameAndEmail = false,
            };
            PlayFabClientAPI.RegisterPlayFabUser(request, OnRegisterSuccess, OnErrorRegister);
        }
        else
        {
            messageInfo.text = "LAS CONTRASE�AS NO COINCIDEN!! VUELVA A INTERARLO";
        }
    }

    void OnRegisterSuccess(RegisterPlayFabUserResult result)
    {
        messageInfo.text = "REGISTRADO CORRECTAMENTE";
    }

    public void PressLoginButton()
    {
        DoLogin(emailInput.text, passwordInput.text);
        //StartCoroutine("CollectActivitiesByOwner");

    }

    public void DoLogin(string email, string pass)
    {
        var request = new LoginWithEmailAddressRequest
        {
            Email = email,
            Password = pass
        };
        PlayFabClientAPI.LoginWithEmailAddress(request, OnLoginSuccess, OnError);
    }

    private void OnLoginSuccess(LoginResult result)
    {
        Debug.Log("Succesful login/account create!!");

        ApplicationAnchorsManagerServer.Instance.GetDataFromServer(emailInput.text, GetTypeServer.Activities);
        OnLocalLoginSuccess?.Invoke();

        StartCoroutine("CollectActivitiesByOwner");

    }

    private void OnErrorRegister(PlayFabError error)
    {
        Debug.Log("Error while logging creating account");
        Debug.Log(error.GenerateErrorReport());
        messageInfo.text = "HA HABIDO UN ERROR, VUELVA A INTERARLO";
    }

    private void OnError(PlayFabError error)
    {
        Debug.Log("Error while logging in account");
        Debug.Log(error.GenerateErrorReport());
        messageText.text = "EMAIL / CONTRASE�A INCORRECTA!! VUELVA A INTENTARLO";
    }
    #endregion

    public void ActivateOptionsMenu()
    {
        Debug.Log("Options menu ACTIVATED");
        StopCoroutine("CollectActivitiesByOwner");

        endActivityCanvas.transform.GetChild(0).transform.DOScale(new Vector3(0, 0, 0), 0.3f);
        loginCanvas.transform.GetChild(0).transform.DOScale(new Vector3(0, 0, 0), 0.3f);

        optionsCanvas.transform.GetChild(0).transform.DOScale(new Vector3(1, 1, 1), 0.3f);  //Panel
        optionsCanvas.transform.GetChild(0).GetChild(0).transform.DOScale(new Vector3(1, 1, 1), 0.3f);  //Tittle
        optionsCanvas.transform.GetChild(0).GetChild(1).transform.DOScale(new Vector3(1, 1, 1), 0.3f); //Info

        optionsCanvas.transform.GetChild(0).GetChild(2).transform.DOScale(new Vector3(1, 1, 1), 0.3f); //Scroll
        
        optionsCanvas.transform.GetChild(0).GetChild(3).transform.DOScale(new Vector3(1, 1, 1), 0.3f); //Button
        optionsCanvas.transform.GetChild(0).GetChild(4).transform.DOScale(new Vector3(1, 1, 1), 0.3f); //Creador

        optionsCanvas.transform.GetChild(0).GetChild(2).GetChild(1).transform.DOScale(new Vector3(1, 1, 1), 0.3f); //Scroll
        optionsCanvas.transform.GetChild(0).GetChild(2).GetChild(0).transform.DOScale(new Vector3(1, 1, 1), 0.3f); //View
        optionsCanvas.transform.GetChild(0).GetChild(2).GetChild(0).GetChild(0).transform.DOScale(new Vector3(1, 1, 1), 0.3f); //content

        int i = 0;
        int numActividad = 0;

        foreach (var actividad in activitiesByOwner)
        {
            if (numActividad == 0)
            {
                optionsCanvas.transform.GetChild(0).GetChild(2).GetChild(0).GetChild(0).GetChild(i).transform.DOScale(new Vector3(1, 1, 1), 0.3f);
                _tittle0.text = actividad.Name;
                _description0.text = actividad.Description;
                i++;  
            }
            if (numActividad == 1)
            {
                optionsCanvas.transform.GetChild(0).GetChild(2).GetChild(0).GetChild(0).GetChild(i).transform.DOScale(new Vector3(1, 1, 1), 0.3f);
                _tittle1.text = actividad.Name;
                _description1.text = actividad.Description;
                i++;               
            }
            if (numActividad == 2)
            {
                optionsCanvas.transform.GetChild(0).GetChild(2).GetChild(0).GetChild(0).GetChild(i).transform.DOScale(new Vector3(1, 1, 1), 0.3f);
                _tittle2.text = actividad.Name;
                _description2.text = actividad.Description;
                i++;
            }
            if (numActividad == 3)
            {
                optionsCanvas.transform.GetChild(0).GetChild(2).GetChild(0).GetChild(0).GetChild(i).transform.DOScale(new Vector3(1, 1, 1), 0.3f);
                _tittle3.text = actividad.Name;
                _description3.text = actividad.Description;
                i++;
            }
            if (numActividad == 4)
            {
                optionsCanvas.transform.GetChild(0).GetChild(2).GetChild(0).GetChild(0).GetChild(i).transform.DOScale(new Vector3(1, 1, 1), 0.3f);
                _tittle4.text = actividad.Name;
                _description4.text = actividad.Description;
                i++;
            }
            if (numActividad == 5)
            {
                optionsCanvas.transform.GetChild(0).GetChild(2).GetChild(0).GetChild(0).GetChild(i).transform.DOScale(new Vector3(1, 1, 1), 0.3f);
                _tittle5.text = actividad.Name;
                _description5.text = actividad.Description;
                i++;
            }
            if (numActividad == 6)
            {
                optionsCanvas.transform.GetChild(0).GetChild(2).GetChild(0).GetChild(0).GetChild(i).transform.DOScale(new Vector3(1, 1, 1), 0.3f);
                _tittle6.text = actividad.Name;
                _description6.text = actividad.Description;
                i++;
            }
            if (numActividad == 7)
            {
                optionsCanvas.transform.GetChild(0).GetChild(2).GetChild(0).GetChild(0).GetChild(i).transform.DOScale(new Vector3(1, 1, 1), 0.3f);
                _tittle7.text = actividad.Name;
                _description7.text = actividad.Description;
                i++;
            }
            if (numActividad == 8)
            {
                optionsCanvas.transform.GetChild(0).GetChild(2).GetChild(0).GetChild(0).GetChild(i).transform.DOScale(new Vector3(1, 1, 1), 0.3f);
                _tittle8.text = actividad.Name;
                _description8.text = actividad.Description;
                i++;
            }
            if (numActividad == 9)
            {
                optionsCanvas.transform.GetChild(0).GetChild(2).GetChild(0).GetChild(0).GetChild(i).transform.DOScale(new Vector3(1, 1, 1), 0.3f);
                _tittle9.text = actividad.Name;
                _description9.text = actividad.Description;
                i++;
            }
            if (numActividad == 10)
            {
                optionsCanvas.transform.GetChild(0).GetChild(2).GetChild(0).GetChild(0).GetChild(i).transform.DOScale(new Vector3(1, 1, 1), 0.3f);
                _tittle10.text = actividad.Name;
                _description10.text = actividad.Description;
                i++;
            }
            if (numActividad == 11)
            {
                optionsCanvas.transform.GetChild(0).GetChild(2).GetChild(0).GetChild(0).GetChild(i).transform.DOScale(new Vector3(1, 1, 1), 0.3f);
                _tittle11.text = actividad.Name;
                _description11.text = actividad.Description;
                i++;
            }
            if (numActividad == 12)
            {
                optionsCanvas.transform.GetChild(0).GetChild(2).GetChild(0).GetChild(0).GetChild(i).transform.DOScale(new Vector3(1, 1, 1), 0.3f);
                _tittle12.text = actividad.Name;
                _description12.text = actividad.Description;
                i++;
            }
            if (numActividad == 13)
            {
                optionsCanvas.transform.GetChild(0).GetChild(2).GetChild(0).GetChild(0).GetChild(i).transform.DOScale(new Vector3(1, 1, 1), 0.3f);
                _tittle13.text = actividad.Name;
                _description13.text = actividad.Description;
                i++;
            }
            if (numActividad == 14)
            {
                optionsCanvas.transform.GetChild(0).GetChild(2).GetChild(0).GetChild(0).GetChild(i).transform.DOScale(new Vector3(1, 1, 1), 0.3f);
                _tittle14.text = actividad.Name;
                _description14.text = actividad.Description;
                i++;
            }
            if (numActividad == 15)
            {
                optionsCanvas.transform.GetChild(0).GetChild(2).GetChild(0).GetChild(0).GetChild(i).transform.DOScale(new Vector3(1, 1, 1), 0.3f);
                _tittle15.text = actividad.Name;
                _description15.text = actividad.Description;
                i++;
            }
            if (numActividad == 16)
            {
                optionsCanvas.transform.GetChild(0).GetChild(2).GetChild(0).GetChild(0).GetChild(i).transform.DOScale(new Vector3(1, 1, 1), 0.3f);
                _tittle16.text = actividad.Name;
                _description16.text = actividad.Description;
                i++;
            }
            if (numActividad == 17)
            {
                optionsCanvas.transform.GetChild(0).GetChild(2).GetChild(0).GetChild(0).GetChild(i).transform.DOScale(new Vector3(1, 1, 1), 0.3f);
                _tittle17.text = actividad.Name;
                _description17.text = actividad.Description;
                i++;
            }
            if (numActividad == 18)
            {
                optionsCanvas.transform.GetChild(0).GetChild(2).GetChild(0).GetChild(0).GetChild(i).transform.DOScale(new Vector3(1, 1, 1), 0.3f);
                _tittle18.text = actividad.Name;
                _description18.text = actividad.Description;
                i++;
            }
            if (numActividad == 19)
            {
                optionsCanvas.transform.GetChild(0).GetChild(2).GetChild(0).GetChild(0).GetChild(i).transform.DOScale(new Vector3(1, 1, 1), 0.3f);
                _tittle19.text = actividad.Name;
                _description19.text = actividad.Description;
                i++;
            }
            if (numActividad == 20)
            {
                optionsCanvas.transform.GetChild(0).GetChild(2).GetChild(0).GetChild(0).GetChild(i).transform.DOScale(new Vector3(1, 1, 1), 0.3f);
                _tittle20.text = actividad.Name;
                _description20.text = actividad.Description;
                i++;
            }
            if (numActividad == 21)
            {
                optionsCanvas.transform.GetChild(0).GetChild(2).GetChild(0).GetChild(0).GetChild(i).transform.DOScale(new Vector3(1, 1, 1), 0.3f);
                _tittle21.text = actividad.Name;
                _description21.text = actividad.Description;
                i++;
            }
            if (numActividad == 22)
            {
                optionsCanvas.transform.GetChild(0).GetChild(2).GetChild(0).GetChild(0).GetChild(i).transform.DOScale(new Vector3(1, 1, 1), 0.3f);
                _tittle22.text = actividad.Name;
                _description22.text = actividad.Description;
                i++;
            }
            if (numActividad == 23)
            {
                optionsCanvas.transform.GetChild(0).GetChild(2).GetChild(0).GetChild(0).GetChild(i).transform.DOScale(new Vector3(1, 1, 1), 0.3f);
                _tittle23.text = actividad.Name;
                _description23.text = actividad.Description;
                i++;
            }
            if (numActividad == 24)
            {
                optionsCanvas.transform.GetChild(0).GetChild(2).GetChild(0).GetChild(0).GetChild(i).transform.DOScale(new Vector3(1, 1, 1), 0.3f);
                _tittle24.text = actividad.Name;
                _description24.text = actividad.Description;
                i++;
            }
            if (numActividad == 25)
            {
                optionsCanvas.transform.GetChild(0).GetChild(2).GetChild(0).GetChild(0).GetChild(i).transform.DOScale(new Vector3(1, 1, 1), 0.3f);
                _tittle25.text = actividad.Name;
                _description25.text = actividad.Description;
                i++;
            }
            if (numActividad == 26)
            {
                optionsCanvas.transform.GetChild(0).GetChild(2).GetChild(0).GetChild(0).GetChild(i).transform.DOScale(new Vector3(1, 1, 1), 0.3f);
                _tittle26.text = actividad.Name;
                _description26.text = actividad.Description;
                i++;
            }
            if (numActividad == 27)
            {
                optionsCanvas.transform.GetChild(0).GetChild(2).GetChild(0).GetChild(0).GetChild(i).transform.DOScale(new Vector3(1, 1, 1), 0.3f);
                _tittle27.text = actividad.Name;
                _description27.text = actividad.Description;
                i++;
            }
            if (numActividad == 28)
            {
                optionsCanvas.transform.GetChild(0).GetChild(2).GetChild(0).GetChild(0).GetChild(i).transform.DOScale(new Vector3(1, 1, 1), 0.3f);
                _tittle28.text = actividad.Name;
                _description28.text = actividad.Description;
                i++;
            }
            if (numActividad == 29)
            {
                optionsCanvas.transform.GetChild(0).GetChild(2).GetChild(0).GetChild(0).GetChild(i).transform.DOScale(new Vector3(1, 1, 1), 0.3f);
                _tittle29.text = actividad.Name;
                _description29.text = actividad.Description;
                i++;
            }
            if (numActividad == 30)
            {
                optionsCanvas.transform.GetChild(0).GetChild(2).GetChild(0).GetChild(0).GetChild(i).transform.DOScale(new Vector3(1, 1, 1), 0.3f);
                _tittle30.text = actividad.Name;
                _description30.text = actividad.Description;
                i++;
            }
            if (numActividad == 31)
            {
                optionsCanvas.transform.GetChild(0).GetChild(2).GetChild(0).GetChild(0).GetChild(i).transform.DOScale(new Vector3(1, 1, 1), 0.3f);
                _tittle31.text = actividad.Name;
                _description31.text = actividad.Description;
                i++;
            }
            if (numActividad == 32)
            {
                optionsCanvas.transform.GetChild(0).GetChild(2).GetChild(0).GetChild(0).GetChild(i).transform.DOScale(new Vector3(1, 1, 1), 0.3f);
                _tittle32.text = actividad.Name;
                _description32.text = actividad.Description;
                i++;
            }
            if (numActividad == 33)
            {
                optionsCanvas.transform.GetChild(0).GetChild(2).GetChild(0).GetChild(0).GetChild(i).transform.DOScale(new Vector3(1, 1, 1), 0.3f);
                _tittle33.text = actividad.Name;
                _description33.text = actividad.Description;
                i++;
            }
            if (numActividad == 34)
            {
                optionsCanvas.transform.GetChild(0).GetChild(2).GetChild(0).GetChild(0).GetChild(i).transform.DOScale(new Vector3(1, 1, 1), 0.3f);
                _tittle34.text = actividad.Name;
                _description34.text = actividad.Description;
                i++;
            }
            if (numActividad == 35)
            {
                optionsCanvas.transform.GetChild(0).GetChild(2).GetChild(0).GetChild(0).GetChild(i).transform.DOScale(new Vector3(1, 1, 1), 0.3f);
                _tittle35.text = actividad.Name;
                _description35.text = actividad.Description;
                i++;
            }
            if (numActividad == 36)
            {
                optionsCanvas.transform.GetChild(0).GetChild(2).GetChild(0).GetChild(0).GetChild(i).transform.DOScale(new Vector3(1, 1, 1), 0.3f);
                _tittle36.text = actividad.Name;
                _description36.text = actividad.Description;
                i++;
            }
            if (numActividad == 37)
            {
                optionsCanvas.transform.GetChild(0).GetChild(2).GetChild(0).GetChild(0).GetChild(i).transform.DOScale(new Vector3(1, 1, 1), 0.3f);
                _tittle37.text = actividad.Name;
                _description37.text = actividad.Description;
                i++;
            }
            if (numActividad == 38)
            {
                optionsCanvas.transform.GetChild(0).GetChild(2).GetChild(0).GetChild(0).GetChild(i).transform.DOScale(new Vector3(1, 1, 1), 0.3f);
                _tittle38.text = actividad.Name;
                _description38.text = actividad.Description;
                i++;
            }
            if (numActividad == 39)
            {
                optionsCanvas.transform.GetChild(0).GetChild(2).GetChild(0).GetChild(0).GetChild(i).transform.DOScale(new Vector3(1, 1, 1), 0.3f);
                _tittle39.text = actividad.Name;
                _description39.text = actividad.Description;
                i++;
            }
            if (numActividad == 40)
            {
                optionsCanvas.transform.GetChild(0).GetChild(2).GetChild(0).GetChild(0).GetChild(i).transform.DOScale(new Vector3(1, 1, 1), 0.3f);
                _tittle40.text = actividad.Name;
                _description40.text = actividad.Description;
                i++;
            }
            if (numActividad == 41)
            {
                optionsCanvas.transform.GetChild(0).GetChild(2).GetChild(0).GetChild(0).GetChild(i).transform.DOScale(new Vector3(1, 1, 1), 0.3f);
                _tittle41.text = actividad.Name;
                _description41.text = actividad.Description;
                i++;
            }
            if (numActividad == 42)
            {
                optionsCanvas.transform.GetChild(0).GetChild(2).GetChild(0).GetChild(0).GetChild(i).transform.DOScale(new Vector3(1, 1, 1), 0.3f);
                _tittle42.text = actividad.Name;
                _description42.text = actividad.Description;
                i++;
            }
            if (numActividad == 43)
            {
                optionsCanvas.transform.GetChild(0).GetChild(2).GetChild(0).GetChild(0).GetChild(i).transform.DOScale(new Vector3(1, 1, 1), 0.3f);
                _tittle43.text = actividad.Name;
                _description43.text = actividad.Description;
                i++;
            }
            if (numActividad == 44)
            {
                optionsCanvas.transform.GetChild(0).GetChild(2).GetChild(0).GetChild(0).GetChild(i).transform.DOScale(new Vector3(1, 1, 1), 0.3f);
                _tittle44.text = actividad.Name;
                _description44.text = actividad.Description;
                i++;
            }
            if (numActividad == 45)
            {
                optionsCanvas.transform.GetChild(0).GetChild(2).GetChild(0).GetChild(0).GetChild(i).transform.DOScale(new Vector3(1, 1, 1), 0.3f);
                _tittle45.text = actividad.Name;
                _description45.text = actividad.Description;
                i++;
            }
            if (numActividad == 46)
            {
                optionsCanvas.transform.GetChild(0).GetChild(2).GetChild(0).GetChild(0).GetChild(i).transform.DOScale(new Vector3(1, 1, 1), 0.3f);
                _tittle46.text = actividad.Name;
                _description46.text = actividad.Description;
                i++;
            }
            if (numActividad == 47)
            {
                optionsCanvas.transform.GetChild(0).GetChild(2).GetChild(0).GetChild(0).GetChild(i).transform.DOScale(new Vector3(1, 1, 1), 0.3f);
                _tittle47.text = actividad.Name;
                _description47.text = actividad.Description;
                i++;
            }
            if (numActividad == 48)
            {
                optionsCanvas.transform.GetChild(0).GetChild(2).GetChild(0).GetChild(0).GetChild(i).transform.DOScale(new Vector3(1, 1, 1), 0.3f);
                _tittle48.text = actividad.Name;
                _description48.text = actividad.Description;
                i++;
            }
            if (numActividad == 49)
            {
                optionsCanvas.transform.GetChild(0).GetChild(2).GetChild(0).GetChild(0).GetChild(i).transform.DOScale(new Vector3(1, 1, 1), 0.3f);
                _tittle49.text = actividad.Name;
                _description49.text = actividad.Description;
                i++;
            }
            numActividad++;
        }

        descActivityCanvas.transform.GetChild(0).transform.DOScale(new Vector3(0, 0, 0), 0.3f);

    }

    public void VolverLogin()
    {
        emailInput.text = "";
        passwordInput.text = "";
        messageText.text = "";

        OnLogin?.Invoke();
        activitiesIds.Clear();
        activitiesByOwner.Clear();
        numActivitiesByOwner = 0; ///CREO
    }

    /*
    public void Act1Aux0()
    {
        activity = 0;
        idActividadSeleccionada = activitiesIds[activity];
        StartCoroutine("CollectActivity2UserByActivityId");
        StartCoroutine("CollectStepByActivtyId");
    }
    public void Act1Aux1()
    {
        activity = 1;
        idActividadSeleccionada = activitiesIds[activity];
        StartCoroutine("CollectActivity2UserByActivityId");
        StartCoroutine("CollectStepByActivtyId");
    }
    public void Act1Aux2()
    {
        activity = 2;
        idActividadSeleccionada = activitiesIds[activity];
        StartCoroutine("CollectActivity2UserByActivityId");
        StartCoroutine("CollectStepByActivtyId");
    }
    public void Act1Aux3()
    {
        activity = 3;
        idActividadSeleccionada = activitiesIds[activity];
        StartCoroutine("CollectActivity2UserByActivityId");
        StartCoroutine("CollectStepByActivtyId");
    }
    public void Act1Aux4()
    {
        activity = 4;
        idActividadSeleccionada = activitiesIds[activity];
        StartCoroutine("CollectActivity2UserByActivityId");
        StartCoroutine("CollectStepByActivtyId");
    }
    public void Act1Aux5()
    {
        activity = 5;
        idActividadSeleccionada = activitiesIds[activity];
        StartCoroutine("CollectActivity2UserByActivityId");
        StartCoroutine("CollectStepByActivtyId");
    }
    */

    public void InfoActivityComplete() {

        StopCoroutine("CollectActivity2UserByActivityId");
        StopCoroutine("CollectStepByActivtyId");

        nameAct.text = activitiesByOwner[activity].Name;
        descAct.text = activitiesByOwner[activity].Description;

        for (int x = 0; x < usuarios.Count; x++)
        {
            if (x == 0)
            {
                pacienteAct.text = usuarios[x];           
            }
            else
            {
                pacienteAct.text = pacienteAct.text + "\n" + usuarios[x];
            }
        }

        int numStepp = 0;
        int aux = 1;
        foreach (var s in step)
        {
            if (numStepp == 0)
            {   
                if(s.stepDescriptions[0].Description == "string")
                {
                    pasosAct.text = "Paso 1 --> No hay descripcion";
                }
                else
                {
                    pasosAct.text = "Paso 1 --> " + s.stepDescriptions[0].Description;
                }
            }
            else
            {
                if (s.stepDescriptions[0].Description == "string")
                {
                    pasosAct.text = pasosAct.text + "\nPaso " + aux + " --> No hay descripcion"  ;
                }
                else
                {   
                    pasosAct.text = pasosAct.text + "\nPaso " + aux +" --> " +s.stepDescriptions[0].Description;
                }
            }
            aux++;
            numStepp++;
        }

        OnDescActivity?.Invoke();
    }

    public void VolverOptions()
    {
        usuarios.Clear();
        step.Clear();
        stepsIds.Clear();
        ActivateOptionsMenu();
    }

    public void AsistenteInfoMenu()
    {
        nameActInfo.text = activitiesByOwner[activity].Name;
        OnAsistenteInfo?.Invoke();
        Debug.Log("Asistente Info menu ACTIVATED");
    }

    public void AsistenteStartPasoAux()
    {
        if(stepAux < stepsIds.Count())
        {
            StartCoroutine("GetStepsById");
            
            string anchorID = ApplicationAnchorsManagerServer.Instance.steps[stepAux].anchorId;
            Debug.Log("I wil Try to Get anchor for step " + stepAux + " Anchor ID: " + anchorID);
            if (anchorID != "0")
            {
                CurrentARObjectNameOnStep = ApplicationAnchorsManagerServer.Instance.steps[stepAux].stepDescriptions[0].entities[0].entityName;
                ApplicationAnchorsManagerServer.Instance.TryToFindAnchor(anchorID);
            }
            stepAux++ ;
        }
        else
        {
            Debug.Log("NO HAY MAS PASOS");
            EndActivityMenuControlador();
            //Llamar a no hay mas pasos
        }

    }

    public void AsistenteStartPasoMenu()
    {
        StopCoroutine("GetStepsById");

        namePaso1.text = activitiesByOwner[activity].Name;
        bocadilloNombreAct.text = activitiesByOwner[activity].Name;
        bocadilloPrimerPaso.text = stepInformation.stepDescriptions[0].Description;
        bocadilloNombreUbicacion.text = stepInformation.InteractiveSpaceName;
        bocadilloObjetosPaso1.text = stepInformation.stepDescriptions[0].entities[0].entityName;

        OnAsistenteStartPaso?.Invoke();
    }

    #region Realidad Aumentada
    public void RegisterSpaceMenu()
    {
        if (regUbicacionAntigua != stepInformation.InteractiveSpaceName)
        {
            regUbicacionAntigua = stepInformation.InteractiveSpaceName;

            OnRegisterSpace?.Invoke();
            Debug.Log("Register Space menu ACTIVATED");
        }
        else
        {
            BoxObjetosOpenMenu();
        }
    }

    public void BoxObjetosOpenMenu()
    {

        objetoAColocar.text = stepInformation.stepDescriptions[0].entities[0].entityName;

        OnBoxObjetosOpen?.Invoke();
        Debug.Log("Box Objetos Open menu ACTIVATED");
    }

    public void ARPositionObjectMenu()
    {
        OnARPositionObject?.Invoke();
        Debug.Log("AR Position Object menu ACTIVATED");
    }
    #endregion

    public void NextPasoMenuControlador()
    {
        actNamee.text = activitiesByOwner[activity].Name;
        OnNextPaso?.Invoke();
    }

    public void EndActivityMenuControlador()
    {
        bocadilloNombreActFin.text = activitiesByOwner[activity].Name;
        actFinName.text = activitiesByOwner[activity].Name;

        OnEndActivity?.Invoke();
    }

    public void SalirEditActivity()
    {

        stepAux = 0;
        regUbicacionAntigua = null;

        stepsIds.Clear();
        step.Clear();
        //InfoActivityComplete();
        
        StartCoroutine("CollectActivitiesByOwner");

        ApplicationAnchorsManagerServer.Instance.DeleteARObjects();
    }

    public void CloseApp()
    {
        Application.Quit();
    }





    public void ItemsMenu()
    {
        OnItemsMenu?.Invoke();
        Debug.Log("Item menu ACTIVATED");
    }
    public void ARPosition()
    {
        OnARPosition?.Invoke();
        Debug.Log("AR Position ACTIVATED");
    }



    

}


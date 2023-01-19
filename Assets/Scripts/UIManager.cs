using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;
using UnityEngine.UI;


public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject loadingCanvas;
    [SerializeField] private GameObject loginCanvas;
    [SerializeField] private GameObject registerCanvas;
    [SerializeField] private GameObject descActivityCanvas;
    [SerializeField] private GameObject optionsCanvas;
    [SerializeField] private GameObject asistenteInfoCanvas;
    [SerializeField] private GameObject asistenteStartPasoCanvas;
    [SerializeField] private GameObject registerSpaceCanvas;
    [SerializeField] private GameObject boxObjetosOpenCanvas;
    [SerializeField] private GameObject aRPositionObjectCanvas;
    [SerializeField] private GameObject nextPasoCanvas;
    [SerializeField] private GameObject endActivityCanvas;

    // Start is called before the first frame update
    void Start()
    {
        GameManager.instance.OnLoading += ActivateLoading;
        GameManager.instance.OnLogin += ActivateLoginMenu;
        GameManager.instance.OnRegister += ActivateRegisterMenu;
        GameManager.instance.OnDescActivity += ActivateDescActivityMenu;
        GameManager.instance.OnAsistenteInfo += ActivateAsistenteInfoMenu;
        GameManager.instance.OnAsistenteStartPaso += ActivateAsistenteStartPasoMenu;
        GameManager.instance.OnRegisterSpace += ActivateRegisterSpaceMenu;
        GameManager.instance.OnBoxObjetosOpen += ActivateBoxObjetosOpenMenu;
        GameManager.instance.OnARPositionObject += ActivateARPositionObjectMenu;
        GameManager.instance.OnNextPaso += ActivateNextPasoCanvas;
        GameManager.instance.OnEndActivity += ActivateEndActivityCanvas;
    }

    private void ActivateLoading()
    {
        loadingCanvas.transform.GetChild(0).transform.DOScale(new Vector3(1, 1, 1), 0.3f);
    }
    private void ActivateRegisterMenu()
    {
        loginCanvas.transform.GetChild(0).transform.DOScale(new Vector3(0, 0, 0), 0.3f);

        registerCanvas.transform.GetChild(0).transform.DOScale(new Vector3(1, 1, 1), 0.3f);
    }
    private void ActivateLoginMenu()
    {
        loadingCanvas.transform.GetChild(0).transform.DOScale(new Vector3(0, 0, 0), 0.3f);

        loginCanvas.transform.GetChild(0).transform.DOScale(new Vector3(1, 1, 1), 0.3f);

        registerCanvas.transform.GetChild(0).transform.DOScale(new Vector3(0, 0, 0), 0.3f);

        optionsCanvas.transform.GetChild(0).transform.DOScale(new Vector3(0, 0, 0), 0.3f);
        /*
        optionsCanvas.transform.GetChild(0).GetChild(0).transform.DOScale(new Vector3(0, 0, 0), 0.3f);
        optionsCanvas.transform.GetChild(0).GetChild(1).transform.DOScale(new Vector3(0, 0, 0), 0.3f);
        optionsCanvas.transform.GetChild(0).GetChild(2).transform.DOScale(new Vector3(0, 0, 0), 0.3f);
        optionsCanvas.transform.GetChild(0).GetChild(3).transform.DOScale(new Vector3(0, 0, 0), 0.3f);
        optionsCanvas.transform.GetChild(0).GetChild(4).transform.DOScale(new Vector3(0, 0, 0), 0.3f);
        optionsCanvas.transform.GetChild(0).GetChild(2).GetChild(0).transform.DOScale(new Vector3(0, 0, 0), 0.3f);
        optionsCanvas.transform.GetChild(0).GetChild(2).GetChild(1).transform.DOScale(new Vector3(0, 0, 0), 0.3f);
        */
        optionsCanvas.transform.GetChild(0).GetChild(2).GetChild(0).GetChild(0).GetChild(0).transform.DOScale(new Vector3(0, 0, 0), 0.3f);
        optionsCanvas.transform.GetChild(0).GetChild(2).GetChild(0).GetChild(0).GetChild(1).transform.DOScale(new Vector3(0, 0, 0), 0.3f);
        optionsCanvas.transform.GetChild(0).GetChild(2).GetChild(0).GetChild(0).GetChild(2).transform.DOScale(new Vector3(0, 0, 0), 0.3f);
        optionsCanvas.transform.GetChild(0).GetChild(2).GetChild(0).GetChild(0).GetChild(3).transform.DOScale(new Vector3(0, 0, 0), 0.3f);
        optionsCanvas.transform.GetChild(0).GetChild(2).GetChild(0).GetChild(0).GetChild(4).transform.DOScale(new Vector3(0, 0, 0), 0.3f);
        optionsCanvas.transform.GetChild(0).GetChild(2).GetChild(0).GetChild(0).GetChild(5).transform.DOScale(new Vector3(0, 0, 0), 0.3f);
        
    }
    private void ActivateDescActivityMenu()
    {
        optionsCanvas.transform.GetChild(0).transform.DOScale(new Vector3(0, 0, 0), 0.3f);
        asistenteInfoCanvas.transform.GetChild(0).transform.DOScale(new Vector3(0, 0, 0), 0.3f);
        endActivityCanvas.transform.GetChild(0).transform.DOScale(new Vector3(0, 0, 0), 0.3f);
        descActivityCanvas.transform.GetChild(0).transform.DOScale(new Vector3(1, 1, 1), 0.3f);
    }
    private void ActivateAsistenteInfoMenu()
    {
        descActivityCanvas.transform.GetChild(0).transform.DOScale(new Vector3(0, 0, 0), 0.3f);
        asistenteInfoCanvas.transform.GetChild(0).transform.DOScale(new Vector3(1, 1, 1), 0.3f);
    }
    private void ActivateAsistenteStartPasoMenu()
    {
        asistenteInfoCanvas.transform.GetChild(0).transform.DOScale(new Vector3(0, 0, 0), 0.3f);
        nextPasoCanvas.transform.GetChild(0).transform.DOScale(new Vector3(0, 0, 0), 0.3f);

        asistenteStartPasoCanvas.transform.GetChild(0).transform.DOScale(new Vector3(1, 1, 1), 0.3f);
    }
    private void ActivateRegisterSpaceMenu()
    {
        asistenteStartPasoCanvas.transform.GetChild(0).transform.DOScale(new Vector3(0, 0, 0), 0.3f);

        registerSpaceCanvas.transform.GetChild(0).transform.DOScale(new Vector3(1, 1, 1), 0.3f);
        registerSpaceCanvas.transform.GetChild(1).transform.DOScale(new Vector3(1, 1, 1), 0.3f);
    }
    private void ActivateBoxObjetosOpenMenu()
    {
        registerSpaceCanvas.transform.GetChild(0).transform.DOScale(new Vector3(0, 0, 0), 0.3f);
        registerSpaceCanvas.transform.GetChild(1).transform.DOScale(new Vector3(0, 0, 0), 0.3f);

        asistenteStartPasoCanvas.transform.GetChild(0).transform.DOScale(new Vector3(0, 0, 0), 0.3f);

        boxObjetosOpenCanvas.transform.GetChild(0).transform.DOScale(new Vector3(1, 1, 1), 0.3f);
        boxObjetosOpenCanvas.transform.GetChild(1).transform.DOScale(new Vector3(1, 1, 1), 0.3f);
        boxObjetosOpenCanvas.transform.GetChild(2).transform.DOScale(new Vector3(1, 1, 1), 0.3f);
        boxObjetosOpenCanvas.transform.GetChild(3).transform.DOScale(new Vector3(1, 1, 1), 0.3f);
        boxObjetosOpenCanvas.transform.GetChild(4).transform.DOScale(new Vector3(1, 1, 1), 0.3f);
        boxObjetosOpenCanvas.transform.GetChild(5).transform.DOScale(new Vector3(1, 1, 1), 0.3f);
        boxObjetosOpenCanvas.transform.GetChild(6).transform.DOScale(new Vector3(1, 1, 1), 0.3f);
        boxObjetosOpenCanvas.transform.GetChild(7).transform.DOScale(new Vector3(1, 1, 1), 0.3f);

        aRPositionObjectCanvas.transform.GetChild(0).transform.DOScale(new Vector3(0, 0, 0), 0.3f);
        aRPositionObjectCanvas.transform.GetChild(1).transform.DOScale(new Vector3(0, 0, 0), 0.3f);
        aRPositionObjectCanvas.transform.GetChild(2).transform.DOScale(new Vector3(0, 0, 0), 0.3f);
        aRPositionObjectCanvas.transform.GetChild(3).transform.DOScale(new Vector3(0, 0, 0), 0.3f);
    }
    private void ActivateARPositionObjectMenu()
    {
        boxObjetosOpenCanvas.transform.GetChild(0).transform.DOScale(new Vector3(0, 0, 0), 0.3f);
        boxObjetosOpenCanvas.transform.GetChild(1).transform.DOScale(new Vector3(0, 0, 0), 0.3f);
        boxObjetosOpenCanvas.transform.GetChild(2).transform.DOScale(new Vector3(0, 0, 0), 0.3f);
        boxObjetosOpenCanvas.transform.GetChild(3).transform.DOScale(new Vector3(0, 0, 0), 0.3f);
        boxObjetosOpenCanvas.transform.GetChild(4).transform.DOScale(new Vector3(0, 0, 0), 0.3f);
        boxObjetosOpenCanvas.transform.GetChild(5).transform.DOScale(new Vector3(0, 0, 0), 0.3f);
        boxObjetosOpenCanvas.transform.GetChild(6).transform.DOScale(new Vector3(0, 0, 0), 0.3f);
        boxObjetosOpenCanvas.transform.GetChild(7).transform.DOScale(new Vector3(0, 0, 0), 0.3f);

        aRPositionObjectCanvas.transform.GetChild(0).transform.DOScale(new Vector3(1, 1, 1), 0.3f);
        aRPositionObjectCanvas.transform.GetChild(1).transform.DOScale(new Vector3(1, 1, 1), 0.3f);
        aRPositionObjectCanvas.transform.GetChild(2).transform.DOScale(new Vector3(1, 1, 1), 0.3f);
        aRPositionObjectCanvas.transform.GetChild(3).transform.DOScale(new Vector3(1, 1, 1), 0.3f);
    }
    private void ActivateNextPasoCanvas()
    {
        boxObjetosOpenCanvas.transform.GetChild(0).transform.DOScale(new Vector3(0, 0, 0), 0.3f);
        boxObjetosOpenCanvas.transform.GetChild(1).transform.DOScale(new Vector3(0, 0, 0), 0.3f);
        boxObjetosOpenCanvas.transform.GetChild(2).transform.DOScale(new Vector3(0, 0, 0), 0.3f);
        boxObjetosOpenCanvas.transform.GetChild(3).transform.DOScale(new Vector3(0, 0, 0), 0.3f);
        boxObjetosOpenCanvas.transform.GetChild(4).transform.DOScale(new Vector3(0, 0, 0), 0.3f);
        boxObjetosOpenCanvas.transform.GetChild(5).transform.DOScale(new Vector3(0, 0, 0), 0.3f);
        boxObjetosOpenCanvas.transform.GetChild(6).transform.DOScale(new Vector3(0, 0, 0), 0.3f);
        boxObjetosOpenCanvas.transform.GetChild(7).transform.DOScale(new Vector3(0, 0, 0), 0.3f);

        asistenteStartPasoCanvas.transform.GetChild(0).transform.DOScale(new Vector3(0, 0, 0), 0.3f);
        nextPasoCanvas.transform.GetChild(0).transform.DOScale(new Vector3(1, 1, 1), 0.3f);
    }
    private void ActivateEndActivityCanvas()
    {
        nextPasoCanvas.transform.GetChild(0).transform.DOScale(new Vector3(0, 0, 0), 0.3f);

        endActivityCanvas.transform.GetChild(0).transform.DOScale(new Vector3(1, 1, 1), 0.3f);
    }

}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIAzureManager : MonoBehaviour
{
    public static UIAzureManager Instance;
    private string textCanvas;
    [SerializeField] private GameObject CanvasText;
    [SerializeField] private TextMeshProUGUI textAzure;

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

    public string TextCanvas 
    {
        set 
        {

            textCanvas = value;
            Debug.Log(textCanvas);
            textAzure.text = textCanvas;
            timeProgess = 0;
            shouldTurnOffCanvas = true;
            shouldRunTimer = true;
            CanvasText.SetActive(true);
        }
    }

    [SerializeField] private float timeProgess;
    [SerializeField] private float timeForCanvas = 5.0f;

    bool shouldRunTimer;
    bool shouldTurnOffCanvas;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!shouldRunTimer)
            return;

        timeProgess += Time.deltaTime;
        if (timeProgess > timeForCanvas)
        {
            if (shouldTurnOffCanvas)
            {
                shouldTurnOffCanvas = false;
                shouldRunTimer = false;
                CanvasText.SetActive(false);
            }
        }
    }
}

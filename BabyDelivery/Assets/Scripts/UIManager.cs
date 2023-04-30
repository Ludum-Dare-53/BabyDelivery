using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Events;
using Unity.VisualScripting;

public class UIManager : MonoBehaviour
{
    TMP_Text curFather;
    TMP_Text curMother;
    TMP_Text timer;
    TMP_Text remainingChildren;
    
    Slider timerSlider;
    Slider healthSlider;

    Button deliveryButton;
    Button selectionButtonA;
    Button selectionButtonB;
    Button selectionButtonC;

    private static UIManager _instance;
    public static UIManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<UIManager>();
            }
            return _instance;
        }
    }

    private void Awake()
    {
        curFather = GameObject.Find("curFather").GetComponent<TMP_Text>();
        curMother = GameObject.Find("curMother").GetComponent<TMP_Text>();
        timer = GameObject.Find("timer").GetComponent<TMP_Text>();
        remainingChildren = GameObject.Find("remainingChildren").GetComponent<TMP_Text>();

        timerSlider= GameObject.Find("timerSlider").GetComponent<Slider>();
        healthSlider = GameObject.Find("healthSlider").GetComponent<Slider>();

        deliveryButton=GameObject.Find("deliveryButton").GetComponent<Button>();
        selectionButtonA = GameObject.Find("selectionButtonA").GetComponent<Button>();
        selectionButtonB = GameObject.Find("selectionButtonB").GetComponent<Button>();
        selectionButtonC = GameObject.Find("selectionButtonC").GetComponent<Button>();
    }

    public void UISetTextCurFather(string text)
    {
        curFather.text = text;
    }
    public void UISetTextCurMother(string text)
    {
        curMother.text = text;
    }
    public void UISetTextTimer(string text)
    {
        timer.text = text;
    }
    public void UISetRemainingChildren(string text)
    {
        remainingChildren.text = "Remaining children:"+""+text;
    }

    public void UISetValueTimerSlider(float value)
    {
        timerSlider.value = value;
    }
    public void UISetValueHealthSlider(float value)
    {
        healthSlider.value = value;
    }

    public void UISetButtonDeliveryButton(UnityAction action)
    {
        deliveryButton.onClick.RemoveAllListeners();
        deliveryButton.onClick.AddListener(action);
    }
    public void UISetButtonSelectionA(UnityAction action)
    {
        selectionButtonA.onClick.RemoveAllListeners();
        selectionButtonA.onClick.AddListener(action);
    }
    public void UISetButtonSelectionB(UnityAction action)
    {
        selectionButtonB.onClick.RemoveAllListeners();
        selectionButtonB.onClick.AddListener(action);
    }
    public void UISetButtonSelectionC(UnityAction action)
    {
        selectionButtonC.onClick.RemoveAllListeners();
        selectionButtonC.onClick.AddListener(action);
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

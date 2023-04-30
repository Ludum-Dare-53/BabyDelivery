using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Events;
using Unity.VisualScripting;
using UnityEngine.SceneManagement;

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

    public delegate void SelectionEvent(Selection selection);
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

        timerSlider = GameObject.Find("timerSlider").GetComponent<Slider>();
        healthSlider = GameObject.Find("healthSlider").GetComponent<Slider>();

        deliveryButton = GameObject.Find("deliveryButton").GetComponent<Button>();
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
        timer.text = "Time:"+" "+text+"s";
    }
    public void UISetRemainingChildren(string text)
    {
        remainingChildren.text = "Remaining children:" + "" + text;
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
    public void UISetButtonSelectionA(SelectionEvent selection,Selection selectionA)
    {
        selectionButtonA.onClick.RemoveAllListeners();
        selectionButtonA.onClick.AddListener(delegate { selection(selectionA); });
    }
    public void UISetButtonSelectionB(SelectionEvent selection,Selection selectionB)
    {
        selectionButtonB.onClick.RemoveAllListeners();
        selectionButtonB.onClick.AddListener(delegate { selection(selectionB); });
    }
    public void UISetButtonSelectionC(SelectionEvent selection,Selection selectionC)
    {
        selectionButtonC.onClick.RemoveAllListeners();
        selectionButtonC.onClick.AddListener(delegate { selection(selectionC); });
    }
    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

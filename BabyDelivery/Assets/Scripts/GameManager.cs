using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using FreeDraw;
using UnityEngine.Rendering.Universal;

public class GameManager : MonoBehaviour
{
    public List<Species> species= new List<Species>();
    public int optionCount=3;
    public int totalChildren=5;
    public float totalTime = 60;
    public int totalHealth = 3;

    public Color rightColor;
    public Color wrongColor;

    [SerializeField]
    Selection curParents;

    public Drawable drawable;
    public SelectionsManager selectionsManager;
    public List<Selection> pickedSelections= new List<Selection>();

    int curHealth;
    int curRemainingChildren;
    float curRemainingTime;
    Light2D light2D;

    GameObject winPanel;
    GameObject failPanel;

    bool canChose=false;

    int shineCount = 0;
    

    void Start()
    {
        Time.timeScale = 1;
        NewParents();
        curRemainingChildren = totalChildren;
        GameObject.FindGameObjectWithTag("ParticalSystem").GetComponent<ParticleSystem>().Play();
        light2D=GameObject.FindGameObjectWithTag("GlobalLight").GetComponent<Light2D>();
        curRemainingTime = totalTime;
        curHealth = totalHealth;
        winPanel = GameObject.Find("Win");
        failPanel = GameObject.Find("Fail");
        winPanel.SetActive(false);
        failPanel.SetActive(false);

        UIManager.Instance.UISetButtonDeliveryButton(Delivery);
        UIManager.Instance.UISetRemainingChildren(curRemainingChildren.ToString());
        UIManager.Instance.UISetValueTimerSlider(curRemainingTime / totalTime);
        UIManager.Instance.UISetValueHealthSlider(curHealth/totalHealth);
    }

    
    void Update()
    {
        UIManager.Instance.UISetValueTimerSlider(curRemainingTime / totalTime);
        UIManager.Instance.UISetTextTimer(((int)curRemainingTime).ToString());
        curRemainingTime -= Time.deltaTime;

        if (curHealth <= 0||curRemainingTime<=0)
        {
            EndGame(false);
        }
        if (curRemainingChildren <= 0&&curHealth>0)
        {
            EndGame(true);
        }
    }

    public void EndGame(bool isWin)
    {
        if (isWin)
        {
            winPanel.SetActive(true);
        }
        else
        {
            failPanel.SetActive(true);
        }
        Time.timeScale = 0f;
    }
    public void NewParents()
    {
        int i=Random.Range(0,species.Count);
        int j=Random.Range(0,species.Count);
        while (i==j)
        {
            i = Random.Range(0, species.Count);
            j = Random.Range(0, species.Count);
        }
        curParents = new Selection(species[i], species[j]);
        selectionsManager.SetParentsVisible(false);
        BuildSelection();
        drawable.ResetCanvas();

        UIManager.Instance.UISetTextCurFather(curParents.father.ID.ToString());
        UIManager.Instance.UISetTextCurMother(curParents.mother.ID.ToString());
    }

    public void BuildSelection()
    {
        pickedSelections.Clear();
        for(int i=0;i<optionCount;i++)
        {
            int j = Random.Range(0, species.Count);
            int p = Random.Range(0, species.Count);
             
            while (!IsOptionAvailable(new Selection(species[j], species[p])))
            {
                j = Random.Range(0, species.Count);
                p = Random.Range(0, species.Count);
            }
            
            pickedSelections.Add(new Selection(species[j], species[p]));
        }
        int x=Random.Range(0, pickedSelections.Count);
        pickedSelections[x]=curParents;
    }

    public void Delivery()
    {
        selectionsManager.SetParentsVisible(true);
        selectionsManager.AddSelection(pickedSelections);

        UIManager.Instance.UISetButtonSelectionA(CompareParents, pickedSelections[0]);
        UIManager.Instance.UISetButtonSelectionB(CompareParents, pickedSelections[1]);
        UIManager.Instance.UISetButtonSelectionC(CompareParents, pickedSelections[2]);
        canChose = true;
    }

    public void CompareParents(Selection parents)
    {
        if (canChose)
        {
            if (parents.father == curParents.father && parents.mother == curParents.mother)
            {
                RightParents();
            }
            else
            {
                WrongParents();
            }
            canChose= false;
            curRemainingChildren -= 1;
            UIManager.Instance.UISetRemainingChildren(curRemainingChildren.ToString());
            Invoke(nameof(NewParents), 0.5f);
        }
        shineCount = 0;
    }
    public void RightParents()
    {
        StartShine(rightColor);
    }
    public void WrongParents()
    {
        StartShine(wrongColor);
        curHealth -= 1;
        UIManager.Instance.UISetValueHealthSlider((float)curHealth/totalHealth);
    }
    void StartShine(Color shineColor)
    {
        StartCoroutine(Shine(shineColor));
    }
    IEnumerator Shine(Color shineColor)
    {
        light2D.color = shineColor;
        yield return new WaitForSeconds(0.2f);
        light2D.color=Color.white;
        yield return new WaitForSeconds(0.2f);
        shineCount++;
        if (shineCount < 2)
        {
            StartShine(shineColor);
        }
    }
    public bool IsOptionAvailable(Selection cur)
    {
        for(int i=0;i<pickedSelections.Count;i++)//不会出现和之前选项完全一致的状况
        {
            if ((cur.mother == pickedSelections[i].mother && cur.father == pickedSelections[i].father) || (cur.father == pickedSelections[i].mother && cur.mother == pickedSelections[i].father))
            {
                return false;
            }
        }
        if (cur.father == curParents.father && cur.mother == curParents.mother)//不和正确选项重叠
        {
            return false;
        }
        else if(cur.father == curParents.mother && cur.mother == curParents.father)
        {
            return false;
        }
        else if (cur.father == cur.mother)//不会出现父母相同的状况
        {
            return false;
        }
        else
        {
            return true;
        }
    }
}



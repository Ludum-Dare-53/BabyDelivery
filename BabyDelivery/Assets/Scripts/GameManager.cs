using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public List<Species> species= new List<Species>();
    public int optionCount=3;
    public int totalChildren=5;
    public float totalTime = 60;

    [SerializeField]
    Selection curParents;

    public SelectionsManager selectionsManager;
    List<Selection> selections= new List<Selection>();
    int curRemainingChildren;
    float curRemainingTime;

    void Start()
    {
        NewParents();
        curRemainingChildren = totalChildren;
        curRemainingTime = totalTime;
        selectionsManager.gameObject.SetActive(false);
        UIManager.Instance.UISetButtonDeliveryButton(Delivery);
        UIManager.Instance.UISetRemainingChildren(curRemainingChildren.ToString());
        UIManager.Instance.UISetValueTimerSlider(curRemainingTime / totalTime);
    }

    
    void Update()
    {
        UIManager.Instance.UISetValueTimerSlider(curRemainingTime / totalTime);
        curRemainingTime -= Time.deltaTime;
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

        UIManager.Instance.UISetTextCurFather(curParents.father.ID.ToString());
        UIManager.Instance.UISetTextCurMother(curParents.mother.ID.ToString());
    }

    public void BuildSelection()
    {
        selections.Clear();
        for(int i=0;i<optionCount;i++)
        {
            int j = Random.Range(0, species.Count);
            int p = Random.Range(0, species.Count);
             
            while (!IsOptionAvailable(new Selection(species[j], species[p])))
            {
                j = Random.Range(0, species.Count);
                p = Random.Range(0, species.Count);
            }
            
            selections.Add(new Selection(species[j], species[p]));
        }
        int x=Random.Range(0, selections.Count);
        selections[x]=curParents;

        selectionsManager.AddSelection(selections);
    }

    public void Delivery()
    {
        selectionsManager.gameObject.SetActive(true);
        BuildSelection();
    }
    public bool IsOptionAvailable(Selection cur)
    {
        for(int i=0;i<selections.Count;i++)//������ֺ�֮ǰѡ����ȫһ�µ�״��
        {
            if ((cur.mother == selections[i].mother && cur.father == selections[i].father) || (cur.father == selections[i].mother && cur.mother == selections[i].father))
            {
                return false;
            }
        }
        if (cur.father == curParents.father && cur.mother == curParents.mother)//������ȷѡ���ص�
        {
            return false;
        }
        else if(cur.father == curParents.mother && cur.mother == curParents.father)
        {
            return false;
        }
        else if (cur.father == cur.mother)//������ָ�ĸ��ͬ��״��
        {
            return false;
        }
        else
        {
            return true;
        }
    }
}



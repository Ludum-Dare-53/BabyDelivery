using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectionsManager : MonoBehaviour
{
    public List<GameObject> selections;

    List<Selection> selectionsData=new List<Selection>();
    void Start()
    {
        
    }

    void Update()
    {
        
    }
    public void SetSlectionUI()
    {
        for(int i=0;i<selections.Count;i++)
        {
            selections[i].transform.Find("fatherImage").GetComponent<Image>().sprite = selectionsData[i].father.portrait;
            selections[i].transform.Find("motherImage").GetComponent<Image>().sprite = selectionsData[i].mother.portrait;
        }
    }

    public void AddSelection(List<Selection> options)
    {
        selectionsData.Clear();
        selectionsData = options;
        SetSlectionUI();
    }
}
[System.Serializable]
public struct Selection
{
    public Species father;
    public Species mother;

    public Selection(Species m_father, Species m_mother)
    {
        father = m_father;
        mother = m_mother;
    }
}

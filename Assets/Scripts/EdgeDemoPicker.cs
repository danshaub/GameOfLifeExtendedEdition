/*
Name:  Dan Haub
Student ID#:  2315346
Chapman Email:  haub@chapman.edu
Course Number and Section:  CPSC 236-01
Assignment:  Final (Game of Life Extended Edition)
*/

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EdgeDemoPicker : MonoBehaviour
{
    public List<GameObject> edgeDemos;
    public Dropdown edgeType;

    // Start is called before the first frame update
    public void Start()
    {
        edgeType.value = 0;
        PickDemo();
    }

    //Changes between the three edge type demos
    public void PickDemo()
    {
        short demo = (short)edgeType.value;
        for(int i = 0; i < edgeDemos.Count; i++)
        {
            if(demo == i)
            {
                edgeDemos[i].SetActive(true);
            }
            else
            {
                edgeDemos[i].SetActive(false);
            }
        }
    }
}

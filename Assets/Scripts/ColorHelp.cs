/*
Name:  Dan Haub
Student ID#:  2315346
Chapman Email:  haub@chapman.edu
Course Number and Section:  CPSC 236-01
Assignment:  Final (Game of Life Extended Edition)
*/

using UnityEngine;
using UnityEngine.UI;

public class ColorHelp : MonoBehaviour
{
    public ScrollRect colors1;
    public ScrollRect colors2;

    private Vector3 colorPos1 = new Vector3(25.97f, -46.50218f, 100);
    private Vector3 colorPos2 = new Vector3(24.15343f, -45.40844f, 100);


    //Fixes the position of the scroll rects
    void Start()
    {
        colors1.content.transform.position = colorPos1;
        Debug.Log(colors2.content.transform.position.y);
        colors2.content.transform.position = colorPos2;
        Debug.Log(colors2.content.transform.position.y);

    }

}

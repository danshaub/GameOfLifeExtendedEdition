/*
Name:  Dan Haub
Student ID#:  2315346
Chapman Email:  haub@chapman.edu
Course Number and Section:  CPSC 236-01
Assignment:  Final (Game of Life Extended Edition)
*/

using UnityEngine;

public class ColorButton : MonoBehaviour
{
    private static int nextButtonIndex = 0;

    public int selfIndex;

    private OptionMenu om;

   //Sets variables accordingly
    void Start()
    {
        om = FindObjectOfType<OptionMenu>();
        selfIndex = nextButtonIndex++;
    }

    //Calls change color on itself
    public void EditColorSelf()
    {
        om.ChangeColor(selfIndex);
    }

    //Resets the index counter
    public static void ResetIndexCounter()
    {
        nextButtonIndex = 0;
    }
    
}

/*
Name:  Dan Haub
Student ID#:  2315346
Chapman Email:  haub@chapman.edu
Course Number and Section:  CPSC 236-01
Assignment:  Final (Game of Life Extended Edition)
*/

using UnityEngine;

public class OptionButton : MonoBehaviour
{
    private static int nextButtonIndex = 0;

    public int selfIndex;

    private OptionMenu om;

    //Initializes correct variables
    private void Start()
    {
        om = FindObjectOfType<OptionMenu>();
        selfIndex = nextButtonIndex++;
    }

    //Chooses itself as the current option
    public void ChooseSelf()
    {
        om.SetHighlightedOption(selfIndex);
        OptionStorage.PickOption(selfIndex);
        om.DisplayCurrentOptions();
    }

    //Resets the index counter back to zero
    public static void ResetIndexCounter()
    {
        nextButtonIndex = 0;
    }
}

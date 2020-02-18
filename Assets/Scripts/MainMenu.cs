/*
Name:  Dan Haub
Student ID#:  2315346
Chapman Email:  haub@chapman.edu
Course Number and Section:  CPSC 236-01
Assignment:  Final (Game of Life Extended Edition)
*/

using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    //Loads either the options scene or the game scene depending on the index passed
    public void LoadScene(int indx)
    {
        SceneManager.LoadScene(indx);
    }

    public void QuitGame()
    {
        // save any game data here
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }

    //Opens a link to an informatiol webpage about Conway's game of life
    public void OpenMoreInfo()
    {
        Application.OpenURL("https://www.theguardian.com/science/alexs-adventures-in-numberland/2014/dec/15/the-game-of-life-a-beginners-guide");
    }
}

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

public class DemoBoard : MonoBehaviour
{
    public List<DemoCell2> cells;
    public List<Dropdown> rules;

    public Text aliveNeighborsText;
    public Text currentStateText;
    public Text nextStateText;

    //Calculates the state of the center cell in the next generation
    //using the rules and states of the all the cells
    public void Calculate()
    {
        bool currentState = cells[0].GetIsAlive();

        //Displays the current state of the center cell
        if (currentState)
        {
            currentStateText.text = "Current State:       Alive";
        }
        else
        {
            currentStateText.text = "Current State:       Dead";
        }

        short numNeighbors = 0;

        //Calcuates how many currounding cells are alive
        for(int i = 1; i <= 8; i++)
        {
            if (cells[i].GetIsAlive())
            {
                numNeighbors++;
            }
        }

        //Displays the number of living neighbors
        aliveNeighborsText.text = "Living Neighbors:  " + numNeighbors.ToString();

        DisplayNextState((short)rules[numNeighbors].value);
    }

    //Displays the next generation state
    private void DisplayNextState(short state)
    {
        switch (state)
        {
            case 0:
                nextStateText.text = "Next State:            Dead";
                break;
            case 1:
                nextStateText.text = currentStateText.text;
                break;
            case 2:
                nextStateText.text = "Next State:            Alive";
                break;
        }
    }

}

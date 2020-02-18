/*
Name:  Dan Haub
Student ID#:  2315346
Chapman Email:  haub@chapman.edu
Course Number and Section:  CPSC 236-01
Assignment:  Final (Game of Life Extended Edition)
*/

using UnityEngine;

public struct State
{
    public bool alive;
    public int age;

    public State(bool al, int ag)
    {
        alive = al;
        age = ag;
    }
};

public class Cell : MonoBehaviour
{
    private State currentState;
    private State nextState;
    private SpriteRenderer cellSprite;
    private GameManager gm;
    private int randomColor;
    public static bool mouseDownAlive;

    //Initializes all variables to the correct object
    void Start()
    {
        gm = FindObjectOfType<GameManager>();
        currentState = new State(false, 0);
        nextState = new State(false, 0);
        cellSprite = gameObject.GetComponent<SpriteRenderer>();
        cellSprite.color = gm.GetOptions().GetColors()[0];
        int count = gm.GetOptions().GetColors().Count;
        randomColor = Random.Range(1, count);
    }

    //Changes state of the cell when clicked
    void OnMouseDown()
    {
        if (gameObject.activeInHierarchy)
        {
            ChangeState();
        }

        mouseDownAlive = currentState.alive;
    }

    //Checks to see if the mouse button is down and the state should be changed
    private void OnMouseEnter()
    {
        if(Input.GetMouseButton(0) && currentState.alive != mouseDownAlive)
        {
            ChangeState();
        }
    }

    //Kills the cell
    public void Kill()
    {
        nextState.age = 0;
        nextState.alive = false;
    }

    //Makes the cell come alive
    void Resurrect()
    {
        if (currentState.alive)
        {
            nextState.age = currentState.age + 1;
        }
        else
        {
            nextState.age = 1;
        }

        nextState.alive = true;
    }

    //Keeps the cell in the same state in the next generation
    void RemainConstant()
    {
        if (currentState.alive)
        {
            nextState.age = currentState.age + 1;
            nextState.alive = true;
        }
        else
        {
            nextState.age = 0;
            nextState.alive = false;
        }
    }

    //Changes the state of the cell (only called on click)
    void ChangeState()
    {
        currentState.alive = !currentState.alive;
        if (currentState.alive)
        {
            currentState.age = 1;
        }
        else
        {
            currentState.age = 0;
        }

        UpdateColor();
    }

    //Updates the color of the cell
    public void UpdateColor()
    {
        //if the cell is dead then set it to the dead color
        if(!currentState.alive)
        {
            cellSprite.color = gm.GetOptions().GetColors()[0];
        }
        //else choose the correct living color
        else
        {
            //if the colors are random then the cell is set to its random color
            if (gm.GetOptions().ColorsAreRandom())
            {
                cellSprite.color = gm.GetOptions().GetColors()[randomColor];
            }
            //choose the correct color for the age of the cell
            else if(currentState.age > 0 && currentState.age < gm.GetOptions().GetColors().Count)
            {
                cellSprite.color = gm.GetOptions().GetColors()[currentState.age];
            }
            //If a cell is older than the number of colors then it will be set to the oldest color
            else
            {
                cellSprite.color = gm.GetOptions().GetColors()[gm.GetOptions().GetColors().Count-1];
            }
        }
    }

    //Returns if the cell is currently alive
    public bool IsAlive()
    {
        return currentState.alive;
    }

    //Returns if the cell will be alive
    public bool GetNextAlive()
    {
        return nextState.alive;
    }

    //Chooses the correct action given the rule for the number of neighbors
    public void NextGeneration(int neighbors)
    {

        switch (neighbors)
        {
            case 0:
                PickCase(gm.GetOptions().GetRule(0));
                break;
            case 1:
                PickCase(gm.GetOptions().GetRule(1));
                break;
            case 2:
                PickCase(gm.GetOptions().GetRule(2));
                break;
            case 3:
                PickCase(gm.GetOptions().GetRule(3));
                break;
            case 4:
                PickCase(gm.GetOptions().GetRule(4));
                break;
            case 5:
                PickCase(gm.GetOptions().GetRule(5));
                break;
            case 6:
                PickCase(gm.GetOptions().GetRule(6));
                break;
            case 7:
                PickCase(gm.GetOptions().GetRule(7));
                break;
            case 8:
                PickCase(gm.GetOptions().GetRule(8));
                break;
        }
    }

    //Calls the correct method given the case
    private void PickCase(short thisCase)
    {
        switch (thisCase)
        {
            case 0:
                this.Kill();
                break;
            case 1:
                this.RemainConstant();
                break;
            case 2:
                this.Resurrect();
                break;
        }   
    }

    //Updates the state of the cell to the next state
    public void Swap()
    {
        currentState = nextState;

        this.UpdateColor();
    }
}
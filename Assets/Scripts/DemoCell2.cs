/*
Name:  Dan Haub
Student ID#:  2315346
Chapman Email:  haub@chapman.edu
Course Number and Section:  CPSC 236-01
Assignment:  Final (Game of Life Extended Edition)
*/

using UnityEngine;

public class DemoCell2 : MonoBehaviour
{
    bool alive = false;
    private SpriteRenderer cellSprite;
    private DemoBoard demoBoard;

    //initializes all necessary variables for demo cells
    private void Start()
    {
        cellSprite = gameObject.GetComponent<SpriteRenderer>();
        demoBoard = FindObjectOfType<DemoBoard>();
        UpdateColor();
        demoBoard.Calculate();
    }

    //Changes the state of the cell when clicked
    void OnMouseDown()
    {
        if(gameObject.activeInHierarchy)
            ChangeState();
    }

    //Cell the color and board state
    void ChangeState()
    {
        alive = !alive;

        UpdateColor();

        demoBoard.Calculate();
    }

    //Changes between black and white colors
    public void UpdateColor()
    {
        if(!alive)
        {
            cellSprite.color = Color.black;
        }
        else
        {
            cellSprite.color = Color.white;
        }
    }

    //Returns the living state of the cell
    public bool GetIsAlive()
    {
        return alive;
    }
}

/*
Name:  Dan Haub
Student ID#:  2315346
Chapman Email:  haub@chapman.edu
Course Number and Section:  CPSC 236-01
Assignment:  Final (Game of Life Extended Edition)
*/

using UnityEngine;

public class DemoCell : MonoBehaviour
{
    bool alive = false;
    private SpriteRenderer cellSprite;

    private void Start()
    {
        cellSprite = gameObject.GetComponent<SpriteRenderer>();
        cellSprite.color = Color.black;
    }

    //Changes the state and color of the cell
    void OnMouseDown()
    {
        if (gameObject.activeInHierarchy)
        {
            alive = !alive;
            if (!alive)
            {
                cellSprite.color = Color.black;
            }
            else
            {
                cellSprite.color = Color.white;
            }
        }
    }
}
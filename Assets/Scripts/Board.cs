/*
Name:  Dan Haub
Student ID#:  2315346
Chapman Email:  haub@chapman.edu
Course Number and Section:  CPSC 236-01
Assignment:  Final (Game of Life Extended Edition)
*/

using UnityEngine;
using UnityEngine.UI;

public class Board : MonoBehaviour
{
    public Dropdown scaleOptions;
    private int scale;
    private Cell[][] cells;
    public Cell cellPrefab;
    public GameManager gm;

    int width;
    int height;

    // Start handles board generation calculated entirely based on the scale
    void Start()
    {
        //Find the current Options
        gm = FindObjectOfType<GameManager>();

        //Gets the current scale from the scale dropdown in game
        scale = scaleOptions.value + 1;

        //Stores the original scale of the cell preab so subsequent loads happen correctly
        float originalScale = cellPrefab.transform.localScale.x;

        //Stores the current scale of the cells in the board
        float cellScale = originalScale / scale;

        //calculates how many cells there will be in each dimension
        width = 15 * scale;
        height = 11 * scale;

        //Calculates how much space is between the cells
        float spaceBetween = 1f / scale;

        //Applies the new scale to the base cell
        cellPrefab.transform.localScale = new Vector3(cellScale, cellScale, 1f);

        //Stores x and y positions of the leftmost column and topmost row respectively
        float xOffset;
        float yOffset;

        //Calculates those values based on the scale
        if (scale != 1)
        {
            xOffset = -(7 + (0.5f / ( (float)scale / (scale - 1) )));
            yOffset = (5 + (0.5f / ( (float)scale / (scale - 1) )));
        }
        else
        {
            xOffset = -7;
            yOffset = 5;
        }

        //Finally, generates the entire cell grid
        cells = new Cell[width][];
        for(int i = 0; i < width; i++)
        {
            cells[i] = new Cell[height];
            for(int j = 0; j < height; j++)
            {
                //sets the position of the soon to be instantiated cell, then instantiates it
                Vector3 position = new Vector3((xOffset + (i * spaceBetween)), (yOffset - (j * spaceBetween)), 1f);
                cells[i][j] = (Cell) Instantiate(cellPrefab, position, new Quaternion());
                cells[i][j].transform.parent = gameObject.transform;
                    
            }
        }

        //returns the cell prefab to its original scale
        cellPrefab.transform.localScale = new Vector3(originalScale, originalScale, 1f);
    }

    //Chooses the correct neighbor calculation function based on current settings
    public void Generate()
    {
        switch (gm.GetOptions().GetEdgeType())
        {
            case 0:
                Classic();
                break;
            case 1:
                Doughnut();
                break;
            case 2:
                Mirror();
                break;
        }
    }

    //Out of bounds are treated as dead cells
    private void Classic()
    {
        //Traverse through each column
        for (int col = 0; col < width; col++)
        {
            //Traverse through each cell in that column
            for (int row = 0; row < height; row++)
            {
                //Count the number of alive neighbors for each cell
                int neighbors = 0;
                
                //Traverses the cells surrounding the current cell
                for (int i = col - 1; i <= col + 1; i++)
                {
                    for (int j = row - 1; j <= row + 1; j++)
                    {
                        //prevents counting the current cell
                        if ((col == i) && (row == j))
                        {
                            continue;
                        }

                        //If the current neighbor is off the grid, skip it
                        if ((i >= 0 && i < width) && (j >= 0 && j < height))
                        {
                            //If the current neighbor is alive, add to the neighbor count
                            if (cells[i][j].IsAlive())
                                neighbors++;
                        }
                    }
                }
                //Calculate the next state of the current cell based on the number of alive neighbors
                cells[col][row].NextGeneration(neighbors);
            }
        }

        bool isAllDead = true;

        //Updates the cells with their new state
        for (int col = 0; col < width; col++)
        {
            for (int row = 0; row < height; row++)
            {
                cells[col][row].Swap();

                if (cells[col][row].IsAlive())
                {
                    isAllDead = false;
                }
            }
        }

        if (isAllDead)
        {
            gm.Stop();
        }
    }

    //Out of bounds cells wrap around to the other side
    private void Doughnut()
    {
        //Traverse through each column
        for (int col = 0; col < width; col++)
        {
            //Traverse through each cell in that column
            for (int row = 0; row < height; row++)
            {
                //Count the number of alive neighbors for each cell
                int neighbors = 0;

                //Traverses the cells surrounding the current cell
                for (int i = col - 1; i <= col + 1; i++)
                {
                    for (int j = row - 1; j <= row + 1; j++)
                    {
                        //prevents counting the current cell
                        if ((col == i) && (row == j))
                        {
                            continue;
                        }

                        //Stores the actual location of the current neighbor
                        int x = 0, y = 0;

                        //Calculates the location of the neighbors 
                        //If i and/or j are out of bounds, they wrap around to the other side
                        x = (i == -1) ? width - 1 : i % width;
                        y = (j == -1) ? height - 1 : j % height;

                        if (cells[x][y].IsAlive())
                            neighbors++;
                    }
                }

                //Calculate the next state of the current cell based on the number of alive neighbors
                cells[col][row].NextGeneration(neighbors);
            }
        }

        bool isAllDead = true;

        //Updates the cells with their new state
        for (int col = 0; col < width; col++)
        {
            for (int row = 0; row < height; row++)
            {
                cells[col][row].Swap();

                if (cells[col][row].IsAlive())
                {
                    isAllDead = false;
                }
            }
        }

        if (isAllDead)
        {
            gm.Stop();
        }

    }

    public void UpdateColors()
    {
        for (int col = 0; col < width; col++)
        {
            for (int row = 0; row < height; row++)
            {
                cells[col][row].UpdateColor();
            }
        }
    }

    //Out of bounds cells reflect back to the closest in bounds cell
    private void Mirror()
    {
        //Traverse through each column
        for (int col = 0; col < width; col++)
        {
            //Traverse through each cell in that column
            for (int row = 0; row < height; row++)
            {
                //Count the number of alive neighbors for each cell
                int neighbors = 0;

                //Traverses the cells surrounding the current cell
                for (int i = col - 1; i <= col + 1; i++)
                {
                    for (int j = row - 1; j <= row + 1; j++)
                    {
                        //prevents counting the current cell
                        if ((col == i) && (row == j))
                        {
                            continue;
                        }

                        //Stores the actual location of the current neighbor
                        int x = 0, y = 0;

                        //Each dimension that is out of bounds is reflected back to the current cell's dimension
                        x = (i == -1 || i == width) ? col : i;
                        y = (j == -1 || j == height) ? row : j;
                        
                        //If the neighbor cell is alive, add to the neighbor count
                        if (cells[x][y].IsAlive())
                            neighbors++;
                    }
                }

                //Calculate the next state of the current cell based on the number of alive neighbors
                cells[col][row].NextGeneration(neighbors);
            }
        }

        bool isAllDead = true;

        //Updates the cells with their new state
        for (int col = 0; col < width; col++)
        {
            for (int row = 0; row < height; row++)
            {
                cells[col][row].Swap();

                if (cells[col][row].IsAlive())
                {
                    isAllDead = false;
                }
            }
        }

        if (isAllDead)
        {
            gm.Stop();
        }
    }

    //Sets all cells to dead
    public void Clear()
    {
        for (int col = 0; col < width; col++)
        {
            for (int row = 0; row < height; row++)
            {
                cells[col][row].Kill();
                cells[col][row].Swap(); 
            }
        }
    }

    //Destroys all cells and calls the start method to regenerate the board with a new scale
    public void Reset()
    {
        for (int col = width-1; col >= 0; col--)
        {
            for (int row = height-1; row >= 0; row--)
            {
                Destroy(cells[col][row].gameObject);
            }
        }

        cells = null;
        Start();
    }

    //Toggles the active state of the cells in the board
    public void ToggleInGame()
    {
        for (int col = width - 1; col >= 0; col--)
        {
            for (int row = height - 1; row >= 0; row--)
            {
                cells[col][row].gameObject.SetActive(!cells[col][row].gameObject.activeInHierarchy);
            }
        }
    }
}
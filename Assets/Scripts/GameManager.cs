/*
Name:  Dan Haub
Student ID#:  2315346
Chapman Email:  haub@chapman.edu
Course Number and Section:  CPSC 236-01
Assignment:  Final (Game of Life Extended Edition)
*/

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public Board gameBoard;
    public Slider speedSlider;
    private float speed;
    private bool speedChanged = false;
    private bool playing = false;
    private Options currentOptions;
    private OptionStorage optionStorage;

    public Dropdown[] ruleDropdowns;
    public Dropdown edgeDropdown;
    public Toggle randomColorToggle;
    public Text randomColorMessage;

    public Button playButton;
    public Button stopButton;

    public int currentEdgeType;


    // Start is called before the first frame update
    void Start()
    {
        speed = 0.2f;

        //Gets access to current game options
        optionStorage = GameObject.FindObjectOfType<OptionStorage>();
        currentOptions = OptionStorage.GetCurrent();
    }

    //Makse sure the correct edge type is always selected
    private void Update()
    {
        currentEdgeType = GetOptions().GetEdgeType();
    }

    //Returns to the main menu or reloads
    public void LoadScene(int indx)
    {
        SceneManager.LoadScene(indx);
    }

    //Generates the next generation
    public void Next()
    {
        //If the slider has been moved and the game is playing, change the speed
        if (playing && speedChanged)
        {
            CancelInvoke();
            InvokeRepeating("Next", 0f, speed);
            speedChanged = false;
        }
        //otherwise simply generate the next generation
        else
        {
            gameBoard.Generate();
        }
    }

    //Begins a repeating invoke of next
    public void Play()
    {
        playing = true;
        InvokeRepeating("Next", 0.25f, speed);
    }

    //Ends the repeating invoke
    public void Stop()
    {
        playing = false;
        playButton.gameObject.SetActive(true);
        stopButton.gameObject.SetActive(false);
        CancelInvoke();
    }

    //Clears the board
    public void Clear()
    {
        gameBoard.Clear();
    }

    //Takes value from the speed slider and updates the speed accordingly
    public void ChangeSpeed()
    {
        float newSpeed = speedSlider.value;

        speed = newSpeed / 250;

        speedChanged = true;
    }

    //Generates a new board with the new scale
    public void ChangeScale()
    {
        gameBoard.Reset();
    }

    //Takes settings from current options and displays them on the dropdown and sliders
    public void DisplayOptions()
    {
        //Displays each specific rule
        for (short i = 0; i < ruleDropdowns.Length; i++)
        {
            ruleDropdowns[i].value = currentOptions.GetRule(i);
        }

        //Displays the edge type
        edgeDropdown.value = currentOptions.GetEdgeType();

        //Displays if colors are random
        randomColorToggle.isOn = currentOptions.ColorsAreRandom();

        //If the current option list only has two colors, disable the random colors toggle
        if(currentOptions.GetColors().Count == 2)
        {
            randomColorToggle.interactable = false;
            randomColorMessage.gameObject.SetActive(true);
        }
        else
        {
            randomColorToggle.interactable = true;
            randomColorMessage.gameObject.SetActive(false);
        }
    }

    //Takes new setting changes and applies them to the game
    public void UpdateRules()
    {
        for (short i = 0; i < ruleDropdowns.Length; i++)
        {
            currentOptions.SetRule(i, (short)ruleDropdowns[i].value);
        }

        currentOptions.SetEdgeType((short)edgeDropdown.value);

        if(randomColorToggle.isOn != currentOptions.ColorsAreRandom())
        {
            currentOptions.SetColorsAreRandom(randomColorToggle.isOn);
            gameBoard.UpdateColors();
        }
        
    }

    //Lets the game board know if it is in use or not
    public void ToggleInGame()
    {
        gameBoard.ToggleInGame();
    }

    //Restores in game values to the values of the currently selected option set
    public void ResetOptions()
    {
        currentOptions = OptionStorage.GetCurrent();
        DisplayOptions();
    }

    //Returns current options
    public Options GetOptions()
    {
        return currentOptions;
    }
    
}



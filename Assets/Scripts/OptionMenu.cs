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
using UnityEngine.SceneManagement;

public class OptionMenu : MonoBehaviour
{
    public Button editButton;
    public Button renameButton;
    public Button deleteButton;

    private List<Button> optionButtons;
    private List<Options> storedOptions;

    public Button objectButtonPrefab;
    public Button colorButtonPrefab;

    public ScrollRect optionListPlane;
    public ScrollRect colorListPlane;
    
    public List<Dropdown> rulesDropdowns;
    public Dropdown edgeTypeDropdown;
    public Toggle randomColorsToggle;
    public InputField nameInputField;
    private List<Button> colorButtons;
    private List<Color> localColors;

    private GameObject[] changableButtons;
    private GameObject[] changableDropdowns;
    private GameObject[] changableColors;
    public Button addOptionButton;

    private ColorPickerTriangle colorPicker;
    private GameObject colorPickerGO;
    public GameObject colorPickerPrefab;


    public GameObject ColorPickerBackground;
    public GameObject ColorPickerContainer;
    private Color changingColor;
    private bool pickingColor = false;
    private int currentColorIndex;

    private Vector3 optionListPos = new Vector3(64, 128, 0);
    private Vector3 colorListPos = new Vector3(480, 108.8f, 0);

    

    //Loads options and displays current option
    private void Start()
    {
        //Fixes position of scroll plane content rect
        optionListPlane.content.transform.position = optionListPos;
        colorListPlane.content.transform.position = colorListPos;

        //Makes sure the color picker is inactive
        ColorPickerBackground.SetActive(false);

        //Allows changing the states of cetain UI objects
        changableButtons = GameObject.FindGameObjectsWithTag("ChangableButton");
        changableDropdowns = GameObject.FindGameObjectsWithTag("ChangableDropdown");
        changableColors = GameObject.FindGameObjectsWithTag("ColorButton");

        storedOptions = OptionStorage.GetSavedOptions();
      
        optionButtons = new List<Button>();
        DisplayOptionList();

        colorButtons = new List<Button>();
        localColors = OptionStorage.GetCurrent().GetColors();
        DisplayColors();

        EndEditing();
        DisplayCurrentOptions();
    }

    //Displays the whole list of saved options in special button objects
    private void DisplayOptionList()
    {
        //Destroys any buttons currently being displayed
        GameObject[] tempArray = GameObject.FindGameObjectsWithTag("OptionButton");
        for (int i = tempArray.Length - 1; i >= 0; i--)
        {
            Destroy(tempArray[i]);
        }

        //Lets the option button class know a new set of options will be created
        OptionButton.ResetIndexCounter();

        //Creates a new option button object for each stored option
        for (int i = 0; i < storedOptions.Count; i++)
        {
            optionListPlane.content.sizeDelta = new Vector2(182, 100 + (70 * (i + 1)));
            Button temp = Instantiate(objectButtonPrefab, optionListPlane.content);
            optionButtons.Add(temp);
            optionButtons[i].transform.position = new Vector3(0f, (-5f - (80 * i)), 0f) + optionButtons[i].transform.position;
            optionButtons[i].gameObject.GetComponentInChildren<Text>().text = storedOptions[i].GetName();
        }

        optionButtons[OptionStorage.GetCurrentOptionIndex()].image.color = Color.gray;
    }

    //Displays whole color list for the current options with special color button objects
    private void DisplayColors()
    {
        ColorButton.ResetIndexCounter();
        changableColors = GameObject.FindGameObjectsWithTag("ColorButton");
        for(int i = changableColors.Length-1; i >= 0; i--)
        {
            Destroy(changableColors[i]);
        }

        colorButtons = new List<Button>();
        for(int i = 0; i < localColors.Count; i++)
        {
            colorListPlane.content.sizeDelta = new Vector2(100 + (62.5f * (i + 1)), 80);
            Button temp = Instantiate(colorButtonPrefab, colorListPlane.content);
            temp.interactable = false;
            colorButtons.Add(temp);
            colorButtons[i].transform.position = new Vector3((5f + (80 * i)), 0f, 0f) + colorButtons[i].transform.position;
            colorButtons[i].image.color = localColors[i];
        }
    }

    //Changes the highlighted opton button
    public void SetHighlightedOption(int newIndex)
    {
        optionButtons[OptionStorage.GetCurrentOptionIndex()].image.color = Color.white;
        optionButtons[newIndex].image.color = Color.gray;
    }

    //Displays the current option set's options on the various UI elements
    public void DisplayCurrentOptions()
    {
        for (short i = 0; i < 9; i++)
        {
            rulesDropdowns[i].value = OptionStorage.GetCurrent().GetRule(i);
        }
        edgeTypeDropdown.value = OptionStorage.GetCurrent().GetEdgeType();
        nameInputField.text = OptionStorage.GetCurrent().GetName();
        randomColorsToggle.isOn = OptionStorage.GetCurrent().ColorsAreRandom();
        localColors = OptionStorage.GetCurrent().GetColors();
        DisplayColors();

        //Disallows editing of the Default options and the example options
        if(OptionStorage.GetCurrentOptionIndex() == 0 || OptionStorage.GetCurrentOptionIndex() == 1)
        {
            editButton.interactable = false;
            deleteButton.interactable = false;
            renameButton.interactable = false;
        }
        else
        {
            editButton.interactable = true;
            deleteButton.interactable = true;
            renameButton.interactable = true;

        }
    }

    //Edits the current options
    public void Edit()
    {
        //Disables the option buttons
        GameObject[] temp = GameObject.FindGameObjectsWithTag("OptionButton");
        foreach (GameObject t in temp)
        {
            t.GetComponent<Button>().interactable = false;
        }

        //Enables all UI elements that change options
        changableColors = GameObject.FindGameObjectsWithTag("ColorButton");
        changableButtons = GameObject.FindGameObjectsWithTag("ChangableButton");
        nameInputField.interactable = true;
        nameInputField.text = OptionStorage.GetCurrent().GetName();
        randomColorsToggle.interactable = true;
        for (int i = 0; i < changableColors.Length; i++)
        {
            changableColors[i].GetComponent<Button>().interactable = true;
        }
        for (int i = 0; i < changableButtons.Length; i++)
        {
            changableButtons[i].GetComponent<Button>().interactable = true;
        }
        for (int i = 0; i < changableDropdowns.Length; i++)
        {
            changableDropdowns[i].GetComponent<Dropdown>().interactable = true;
        }
        addOptionButton.interactable = false;
    }

    //Allows for renaming of the current option set
    public void Rename()
    {
        //Disables option buttons
        GameObject[] temp = GameObject.FindGameObjectsWithTag("OptionButton");
        foreach (GameObject t in temp)
        {
            t.GetComponent<Button>().interactable = false;
        }
        addOptionButton.interactable = false;
        //Enables the name input field
        nameInputField.interactable = true;
        nameInputField.text = OptionStorage.GetCurrent().GetName();
    }

    //Finishes editing
    public void EndEditing()
    {
        //Enables option buttons
        GameObject[] temp = GameObject.FindGameObjectsWithTag("OptionButton");
        foreach(GameObject t in temp)
        {
            t.GetComponent<Button>().interactable = true;
        }
        //Disables all UI option elements
        changableColors = GameObject.FindGameObjectsWithTag("ColorButton");
        changableButtons = GameObject.FindGameObjectsWithTag("ChangableButton");
        nameInputField.interactable = false;
        randomColorsToggle.interactable = false;
        for (int i = 0; i < changableColors.Length; i++)
        {
            changableColors[i].GetComponent<Button>().interactable = false;
        }
        for (int i = 0; i < changableButtons.Length; i++)
        {
            changableButtons[i].GetComponent<Button>().interactable = false;
        }
        for (int i = 0; i < changableDropdowns.Length; i++)
        {
            changableDropdowns[i].GetComponent<Dropdown>().interactable = false;
        }
        addOptionButton.interactable = true;
    }
    
    //Saves current options
    public void AcceptChanges()
    {
        Options temp = OptionStorage.GetCurrent();
        temp.SetName(nameInputField.text);
        temp.SetEdgeType((short)edgeTypeDropdown.value);
        temp.SetColorsAreRandom(randomColorsToggle.isOn);
        temp.SetColors(localColors);

        for (short i = 0; i < 9; i++)
        {
             temp.SetRule(i, (short)rulesDropdowns[i].value);
        }

        OptionStorage.SetCurrent(temp);

        optionButtons[OptionStorage.GetCurrentOptionIndex()].gameObject.GetComponentInChildren<Text>().text = temp.GetName();

        EndEditing();
    }

    //Removes a color from the color list
    public void DeleteColor()
    {
        if(localColors.Count > 2)
        {
            localColors.RemoveAt(localColors.Count - 1);
            DisplayColors();
            changableColors = GameObject.FindGameObjectsWithTag("ColorButton");
            changableButtons = GameObject.FindGameObjectsWithTag("ChangableButton");
            GameObject[] temp = GameObject.FindGameObjectsWithTag("CButton");
            nameInputField.interactable = true;
            randomColorsToggle.interactable = true;
            for (int i = 0; i < changableColors.Length; i++)
            {
                changableColors[i].GetComponent<Button>().interactable = true;
            }
            for (int i = 0; i < changableButtons.Length; i++)
            {
                changableButtons[i].GetComponent<Button>().interactable = true;
            }
            for (int i = 0; i < changableDropdowns.Length; i++)
            {
                changableDropdowns[i].GetComponent<Dropdown>().interactable = true;
            }
            for (int i = 0; i < temp.Length; i++)
            {
                temp[i].GetComponent<Button>().interactable = true;
            }
        }
    }

    //Adds a color to the color list
    public void AddColor()
    {
        localColors.Add(Color.white);
        DisplayColors();
        changableColors = GameObject.FindGameObjectsWithTag("ColorButton");
        changableButtons = GameObject.FindGameObjectsWithTag("ChangableButton");
        GameObject[] temp = GameObject.FindGameObjectsWithTag("CButton");
        nameInputField.interactable = true;
        randomColorsToggle.interactable = true;
        for (int i = 0; i < changableColors.Length; i++)
        {
            changableColors[i].GetComponent<Button>().interactable = true;
        }
        for (int i = 0; i < changableButtons.Length; i++)
        {
            changableButtons[i].GetComponent<Button>().interactable = true;
        }
        for (int i = 0; i < changableDropdowns.Length; i++)
        {
            changableDropdowns[i].GetComponent<Dropdown>().interactable = true;
        }
        for (int i = 0; i < temp.Length; i++)
        {
            temp[i].GetComponent<Button>().interactable = true;
        }
    }

    //Allows for changing the selected color
    public void ChangeColor(int colorIndex)
    {
        //displays color picker
        ColorPickerBackground.SetActive(true);
        pickingColor = true;

        changingColor = localColors[colorIndex];

        currentColorIndex = colorIndex;

        //Disables unnecessary buttons
        changableColors = GameObject.FindGameObjectsWithTag("ColorButton");
        changableButtons = GameObject.FindGameObjectsWithTag("ChangableButton");
        GameObject[] temp = GameObject.FindGameObjectsWithTag("CButton");
        nameInputField.interactable = false;
        randomColorsToggle.interactable = false;
        for (int i = 0; i < changableColors.Length; i++)
        {
            changableColors[i].GetComponent<Button>().interactable = false;
        }
        for (int i = 0; i < changableButtons.Length; i++)
        {
            changableButtons[i].GetComponent<Button>().interactable = false;
        }
        for (int i = 0; i < changableDropdowns.Length; i++)
        {
            changableDropdowns[i].GetComponent<Dropdown>().interactable = false;
        }
        for (int i = 0; i < temp.Length; i++)
        {
            temp[i].GetComponent<Button>().interactable = false;
        }

        //Instantiates a new color picker and lets you use it
        colorPickerGO = (GameObject)Instantiate(colorPickerPrefab, ColorPickerContainer.transform);
        colorPickerGO.transform.localScale = Vector3.one;
        colorPickerGO.transform.rotation = new Quaternion(0, 180, 0, 0);
        colorPickerGO.transform.position = colorPickerGO.transform.position + new Vector3(0, 0, 0);
        colorPicker = colorPickerGO.GetComponent<ColorPickerTriangle>();
        colorPicker.SetNewColor(changingColor);

    }

    //Displays the current change in the selected color button
    void Update()
    {
        if (pickingColor)
        {
            changingColor = colorPicker.TheColor;
            colorButtons[currentColorIndex].image.color = changingColor;
        }
    }

    //Saves the new color
    public void AcceptChangedColor()
    {
        localColors[currentColorIndex] = changingColor;
        EndChangingColor();
    }

    //Ends the color change mode
    public void EndChangingColor()
    {
        //Resets the screen and updates the color list
        DisplayColors();
        ColorPickerBackground.SetActive(false);
        pickingColor = false;
        Destroy(colorPickerGO);

        //Enables all necessary buttons
        changableColors = GameObject.FindGameObjectsWithTag("ColorButton");
        changableButtons = GameObject.FindGameObjectsWithTag("ChangableButton");
        GameObject[] temp = GameObject.FindGameObjectsWithTag("CButton");
        nameInputField.interactable = true;
        randomColorsToggle.interactable = true;
        for (int i = 0; i < changableColors.Length; i++)
        {
            changableColors[i].GetComponent<Button>().interactable = true;
        }
        for (int i = 0; i < changableButtons.Length; i++)
        {
            changableButtons[i].GetComponent<Button>().interactable = true;
        }
        for (int i = 0; i < changableDropdowns.Length; i++)
        {
            changableDropdowns[i].GetComponent<Dropdown>().interactable = true;
        }
        for (int i = 0; i < temp.Length; i++)
        {
            temp[i].GetComponent<Button>().interactable = true;
        }
    }

    //Deletes an option from the list
    public void DeleteOption()
    {
        OptionStorage.DeleteCurrent();

        optionButtons = new List<Button>();
        DisplayOptionList();

        colorButtons = new List<Button>();
        DisplayColors();

        EndEditing();
        DisplayCurrentOptions();
    }

    //Adds a new option to the list
    public void AddOption()
    {
        OptionStorage.AddOption();

        optionButtons = new List<Button>();
        DisplayOptionList();

        colorButtons = new List<Button>();
        DisplayColors();

        SetHighlightedOption(optionButtons.Count - 1);
        OptionStorage.PickOption(optionButtons.Count - 1);

        DisplayCurrentOptions();
    }

    //Returns to the main menu
    public void BackToMain()
    {
        SceneManager.LoadScene(0);
    }
}

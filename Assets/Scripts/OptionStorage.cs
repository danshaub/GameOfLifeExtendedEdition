/*
Name:  Dan Haub
Student ID#:  2315346
Chapman Email:  haub@chapman.edu
Course Number and Section:  CPSC 236-01
Assignment:  Final (Game of Life Extended Edition)
*/

using System.IO;
using System.Collections.Generic;
using UnityEngine;

public class OptionStorage : MonoBehaviour
{
    private static int currentOptionIndex = 0;
    private static List<Options> savedOptions;
    private static string dataPath;
    private static bool isCreated = false;


    //Loads colors from file. If no file exists or no saves are contained
    //in the file, default saves are written
    private void Awake()
    {
        //Allows each scene to have an instance of this while only
        //allowing one to exist at a time
        if (isCreated)
        {
            Destroy(this.gameObject);
        }

        else
        {
            //Lets the object persist between scenes
            DontDestroyOnLoad(this.gameObject);

            isCreated = true;

            //Gets path for data file
            dataPath = Path.Combine("SavedOptions.txt");
            
            savedOptions = new List<Options>();

            //If the data file is empty, fill it
            if (!HasOptions())
            {
                Options temp = new Options();
                temp.SetName("Default");
                SaveOptions(temp);
                SaveOptions("{\"name\":\"Example\",\"edgeType\":2,\"rules\":[1,1,2,2,2,2,2,2,2],\"colors\":[{\"r\":1.0,\"g\":1.0,\"b\":1.0,\"a\":1.0},{\"r\":1.0,\"g\":0.0,\"b\":0.0,\"a\":1.0},{\"r\":1.0,\"g\":0.25,\"b\":0.0,\"a\":1.0},{\"r\":1.0,\"g\":0.5,\"b\":0.0,\"a\":1.0},{\"r\":1.0,\"g\":0.75,\"b\":0.0,\"a\":1.0},{\"r\":1.0,\"g\":1.0,\"b\":0.0,\"a\":1.0},{\"r\":0.75,\"g\":1.0,\"b\":0.0,\"a\":1.0},{\"r\":0.5,\"g\":1.0,\"b\":0.0,\"a\":1.0},{\"r\":0.25,\"g\":1.0,\"b\":0.0,\"a\":1.0},{\"r\":0.0,\"g\":1.0,\"b\":0.0,\"a\":1.0},{\"r\":0.0,\"g\":1.0,\"b\":0.25,\"a\":1.0},{\"r\":0.0,\"g\":1.0,\"b\":0.5,\"a\":1.0},{\"r\":0.0,\"g\":1.0,\"b\":0.75,\"a\":1.0},{\"r\":0.0,\"g\":1.0,\"b\":1.0,\"a\":1.0},{\"r\":0.0,\"g\":0.75,\"b\":1.0,\"a\":1.0},{\"r\":0.0,\"g\":0.5,\"b\":1.0,\"a\":1.0},{\"r\":0.0,\"g\":0.25,\"b\":1.0,\"a\":1.0},{\"r\":0.0,\"g\":0.0,\"b\":1.0,\"a\":1.0},{\"r\":0.25,\"g\":0.0,\"b\":1.0,\"a\":1.0},{\"r\":0.5,\"g\":0.0,\"b\":1.0,\"a\":1.0},{\"r\":0.75,\"g\":0.0,\"b\":1.0,\"a\":1.0},{\"r\":1.0,\"g\":0.0,\"b\":0.75,\"a\":1.0},{\"r\":1.0,\"g\":0.0,\"b\":0.5,\"a\":1.0},{\"r\":1.0,\"g\":0.0,\"b\":0.25,\"a\":1.0},{\"r\":1.0,\"g\":0.0,\"b\":0.0,\"a\":1.0},{\"r\":1.0,\"g\":0.25,\"b\":0.0,\"a\":1.0},{\"r\":1.0,\"g\":0.5,\"b\":0.0,\"a\":1.0},{\"r\":1.0,\"g\":0.75,\"b\":0.0,\"a\":1.0},{\"r\":1.0,\"g\":1.0,\"b\":0.0,\"a\":1.0},{\"r\":0.75,\"g\":1.0,\"b\":0.0,\"a\":1.0},{\"r\":0.5,\"g\":1.0,\"b\":0.0,\"a\":1.0},{\"r\":0.25,\"g\":1.0,\"b\":0.0,\"a\":1.0},{\"r\":0.0,\"g\":1.0,\"b\":0.0,\"a\":1.0},{\"r\":0.0,\"g\":1.0,\"b\":0.25,\"a\":1.0},{\"r\":0.0,\"g\":1.0,\"b\":0.5,\"a\":1.0},{\"r\":0.0,\"g\":1.0,\"b\":0.75,\"a\":1.0},{\"r\":0.0,\"g\":1.0,\"b\":1.0,\"a\":1.0},{\"r\":0.0,\"g\":0.75,\"b\":1.0,\"a\":1.0},{\"r\":0.0,\"g\":0.5,\"b\":1.0,\"a\":1.0},{\"r\":0.0,\"g\":0.25,\"b\":1.0,\"a\":1.0},{\"r\":0.0,\"g\":0.0,\"b\":1.0,\"a\":1.0},{\"r\":0.25,\"g\":0.0,\"b\":1.0,\"a\":1.0},{\"r\":0.5,\"g\":0.0,\"b\":1.0,\"a\":1.0},{\"r\":0.75,\"g\":0.0,\"b\":1.0,\"a\":1.0},{\"r\":1.0,\"g\":0.0,\"b\":0.75,\"a\":1.0},{\"r\":1.0,\"g\":0.0,\"b\":0.5,\"a\":1.0},{\"r\":1.0,\"g\":0.0,\"b\":0.25,\"a\":1.0},{\"r\":1.0,\"g\":0.0,\"b\":0.0,\"a\":1.0},{\"r\":1.0,\"g\":0.25,\"b\":0.0,\"a\":1.0},{\"r\":1.0,\"g\":0.5,\"b\":0.0,\"a\":1.0},{\"r\":1.0,\"g\":0.75,\"b\":0.0,\"a\":1.0},{\"r\":1.0,\"g\":1.0,\"b\":0.0,\"a\":1.0},{\"r\":0.75,\"g\":1.0,\"b\":0.0,\"a\":1.0},{\"r\":0.5,\"g\":1.0,\"b\":0.0,\"a\":1.0},{\"r\":0.25,\"g\":1.0,\"b\":0.0,\"a\":1.0},{\"r\":0.0,\"g\":1.0,\"b\":0.0,\"a\":1.0},{\"r\":0.0,\"g\":1.0,\"b\":0.25,\"a\":1.0},{\"r\":0.0,\"g\":1.0,\"b\":0.5,\"a\":1.0},{\"r\":0.0,\"g\":1.0,\"b\":0.75,\"a\":1.0},{\"r\":0.0,\"g\":1.0,\"b\":1.0,\"a\":1.0},{\"r\":0.0,\"g\":0.75,\"b\":1.0,\"a\":1.0},{\"r\":0.0,\"g\":0.5,\"b\":1.0,\"a\":1.0},{\"r\":0.0,\"g\":0.25,\"b\":1.0,\"a\":1.0},{\"r\":0.0,\"g\":0.0,\"b\":1.0,\"a\":1.0},{\"r\":0.25,\"g\":0.0,\"b\":1.0,\"a\":1.0},{\"r\":0.5,\"g\":0.0,\"b\":1.0,\"a\":1.0},{\"r\":0.75,\"g\":0.0,\"b\":1.0,\"a\":1.0},{\"r\":1.0,\"g\":0.0,\"b\":0.75,\"a\":1.0},{\"r\":1.0,\"g\":0.0,\"b\":0.5,\"a\":1.0},{\"r\":1.0,\"g\":0.0,\"b\":0.25,\"a\":1.0},{\"r\":1.0,\"g\":0.0,\"b\":0.0,\"a\":1.0},{\"r\":1.0,\"g\":0.25,\"b\":0.0,\"a\":1.0},{\"r\":1.0,\"g\":0.5,\"b\":0.0,\"a\":1.0},{\"r\":1.0,\"g\":0.75,\"b\":0.0,\"a\":1.0},{\"r\":1.0,\"g\":1.0,\"b\":0.0,\"a\":1.0},{\"r\":0.75,\"g\":1.0,\"b\":0.0,\"a\":1.0},{\"r\":0.5,\"g\":1.0,\"b\":0.0,\"a\":1.0},{\"r\":0.25,\"g\":1.0,\"b\":0.0,\"a\":1.0},{\"r\":0.0,\"g\":1.0,\"b\":0.0,\"a\":1.0},{\"r\":0.0,\"g\":1.0,\"b\":0.25,\"a\":1.0},{\"r\":0.0,\"g\":1.0,\"b\":0.5,\"a\":1.0},{\"r\":0.0,\"g\":1.0,\"b\":0.75,\"a\":1.0},{\"r\":0.0,\"g\":1.0,\"b\":1.0,\"a\":1.0},{\"r\":0.0,\"g\":0.75,\"b\":1.0,\"a\":1.0},{\"r\":0.0,\"g\":0.5,\"b\":1.0,\"a\":1.0},{\"r\":0.0,\"g\":0.25,\"b\":1.0,\"a\":1.0},{\"r\":0.0,\"g\":0.0,\"b\":1.0,\"a\":1.0},{\"r\":0.25,\"g\":0.0,\"b\":1.0,\"a\":1.0},{\"r\":0.5,\"g\":0.0,\"b\":1.0,\"a\":1.0},{\"r\":0.75,\"g\":0.0,\"b\":1.0,\"a\":1.0},{\"r\":1.0,\"g\":0.0,\"b\":0.75,\"a\":1.0},{\"r\":1.0,\"g\":0.0,\"b\":0.5,\"a\":1.0},{\"r\":1.0,\"g\":0.0,\"b\":0.25,\"a\":1.0},{\"r\":1.0,\"g\":0.0,\"b\":0.0,\"a\":1.0}],\"colorsAreRandom\":false}");
            }

            //Otherwise load from the file
            if (savedOptions.Count == 0)
            {
                LoadOptions();
            }
        }
    }

    //Returns true if the data file contains saved options
    private static bool HasOptions()
    {

        using (StreamReader streamReader = File.OpenText(dataPath))
        {
            //if the reader starts at the end, it means there is nothing in the file
            return !streamReader.EndOfStream;
        }
    }

    //Gathers saved option sets from the data file and populates the list of options wiht them
    private static void LoadOptions()
    {
        using (StreamReader streamReader = File.OpenText(dataPath))
        {
            while (!streamReader.EndOfStream)
            {
                string jsonString = streamReader.ReadLine();
                savedOptions.Add(JsonUtility.FromJson<Options>(jsonString));
                
            }
            
        }
    }

    //Saves all options stored in the options list **Overwites anything previously in file**
    private static void SaveOptions()
    {
        using (StreamWriter streamWriter = new StreamWriter(dataPath))
        {
            for(int i = 0; i < savedOptions.Count; i++)
            {
                string jsonString = JsonUtility.ToJson(savedOptions[i]);
                streamWriter.Write(jsonString);
                streamWriter.Write("\n");
            }
            
        }
    }

    //Saves a single option passed as an option object
    private static void SaveOptions(Options options)
    {
        string jsonString = JsonUtility.ToJson(options);

        using (StreamWriter streamWriter = new StreamWriter(dataPath, true))
        {
            streamWriter.Write(jsonString);
            streamWriter.Write("\n");
        }
    }

    //Saves a single option passed as a JSON string
    private static void SaveOptions(string options)
    {
        using (StreamWriter streamWriter = new StreamWriter(dataPath, true))
        {
            streamWriter.Write(options);
            streamWriter.Write("\n");
        }
    }

    private void OnApplicationQuit()
    {
        SaveOptions();
    }

    //Chooses the current option index
    public static void PickOption(int optionIndex)
    {
        currentOptionIndex = optionIndex;
    }

    //Returns the current option index
    public static int GetCurrentOptionIndex()
    {
        return currentOptionIndex;
    }

    //Returns the entire list of saved options
    public static List<Options> GetSavedOptions()
    { 
        return savedOptions;
    }

    //Returns a copy of the current option
    public static Options GetCurrent()
    {
        return new Options(savedOptions[currentOptionIndex]);
    }

    //Sets the currently selected option with a passed option object
    public static void SetCurrent(Options changedOption)
    {
        savedOptions[currentOptionIndex] = changedOption;
    }

    //Removes the current option from the list
    public static void DeleteCurrent()
    {
        savedOptions.RemoveAt(currentOptionIndex);
        currentOptionIndex = 0;
    }

    //Adds an option to the end of the list
    public static void AddOption()
    {
        savedOptions.Add(new Options());
    }
}



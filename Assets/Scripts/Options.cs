/*
Name:  Dan Haub
Student ID#:  2315346
Chapman Email:  haub@chapman.edu
Course Number and Section:  CPSC 236-01
Assignment:  Final (Game of Life Extended Edition)
*/

using System.Collections.Generic;
using System;
using UnityEngine;

[System.Serializable]

public class Options
{
    [SerializeField]
    private string name;

    [SerializeField]
    private short edgeType;
    //0 -> classic
    //1 -> doughnut
    //2 -> mirror

    [SerializeField]
    private short[] rules;
    //0 -> kill
    //1 -> remainConstant
    //2 -> resurrect

    [SerializeField]
    private List<Color> colors;

    [SerializeField]
    private bool colorsAreRandom;

    public Options(string name, short edgeType, short[] rules, List<Color> colors, bool colorsAreRandom)
    {
        SetName(name);
        SetEdgeType(edgeType);
        SetRules(rules);
        SetColors(colors);
        SetColorsAreRandom(colorsAreRandom);
    }

    //Default constructor, makes a new option list with default rules
    public Options()
    {
        SetName("Untitled");
        SetEdgeType(0);
        SetRules(new short[9] { 0, 0, 1, 2, 0, 0, 0, 0, 0 });
        colors = new List<Color>();
        colors.Insert(colors.Count, Color.black);
        colors.Insert(colors.Count, Color.white);
        SetColorsAreRandom(false);

    }

    //Copy constructor
    public Options(Options old)
    {
        this.name = old.name;
        this.edgeType = old.edgeType;
        this.rules = new short[9];
        for(int i = 0; i < 9; i++)
        {
            this.rules[i] = old.rules[i];
        }
        this.colors = new List<Color>(old.colors);
        this.colorsAreRandom = old.colorsAreRandom;
    }
    
    //Returns the name of the option set
    public string GetName()
    {
        return name;
    }

    //Sets the name of the option set
    public void SetName(string name)
    {
        name = name.Trim();

        //If the name is empty, set its name to untitled
        if (name != "")
        {
            this.name = name;
        }
        else
        {
            this.name = "Untitled";
        }
    }

    //Returns the edge type of the option set
    public short GetEdgeType()
    {
        return edgeType;
    }

    //Sets the edge type of the option set
    public void SetEdgeType(short edgeType)
    {
        //Makes sure the edge type is a valid number
        if (edgeType == 0 || edgeType == 1 || edgeType == 2)
        {
            this.edgeType = edgeType;
        }

        else
        {
            this.edgeType = 0;
        }
    }

    //Returns the edge type of the option set
    public short GetRule(short rule)
    {
        return rules[rule];
    }

    //Sets the specific rule at the given index
    public void SetRule(short ruleNum, short type)
    {
        if ((type >= 0 && type <= 2) && (ruleNum >= 0 && ruleNum <= 8))
        {
            rules[ruleNum] = type;
        }
    }

    //Sets all rules with values from passed rule array
    //If the array is invalid, 
    public void SetRules(short[] rules)
    {
        bool valid = true;

        if (rules.Length != 9)
        {
            valid = false;
        }

        for (int i = 0; i < 9; i++)
        {
            if (!(rules[i] >= 0 && rules[i] <= 2))
            {
                valid = false;
            }
        }

        if (valid)
        {
            this.rules = rules;
        }

        else
        {
            rules = new short[9] { 0, 0, 1, 2, 0, 0, 0, 0, 0 };
            this.rules = rules;

        }
    }

    //Returns the list of colors for this option set
    public List<Color> GetColors()
    {
        return colors;
    }

    //Accepts a list of color objects and sets the color list to it
    public void SetColors(List<Color> colors)
    {
        if (colors.Count >= 2)
        {
            this.colors = colors;
        }
        else
        {
            colors.Clear();

            colors.Insert(colors.Count, Color.black);
            colors.Insert(colors.Count, Color.white);

            this.colors = colors;
        }
    }

    //Returns if the colors are random in this option set
    public bool ColorsAreRandom()
    {
        return colorsAreRandom;
    }

    //Sets the random colors boolean
    public void SetColorsAreRandom(bool colorsAreRandom)
    {
        this.colorsAreRandom = colorsAreRandom;
    }
}

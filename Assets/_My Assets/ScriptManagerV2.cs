using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using TMPro;

/**
 * @brief Parses, stores, and displays all lines of dialogue from an external file
 * Next line to be shown is determined by the animator
 */

public class ScriptManagerV2 : MonoBehaviour
{

    public string filename; //Filename to get lines from (txt)
    List<string> lineList; //List of lines to store lines to
    int currentLine; //Index

    [Header("")] //Unity elements
    public Animator textAnim; //Animator for text object
    public Animator fadeAnim; //Animator for fade out attached to player's field of view
    public TextMeshProUGUI text;

    // Initialize all values
    void Awake()
    {
        lineList = new List<string>();
        currentLine = 0;
        ParseLines();
    }

    /**
     * @brief Parse all lines in txt file and store them in a list
     */

    void ParseLines()
    {
        //Open file
        string contents = Resources.Load<TextAsset>(filename).text;
        //Read lines
        string[] lines = Regex.Split(contents, "\n");
        //Store lines in lineList
        for (int i = 0; i < lines.Length; i++)
        {
            lineList.Add(lines[i]);
            //Print stored line
            Debug.Log(lineList[i]);
        }
    }

    /**
 * @brief Fade out and into the next line upon being called by an Animation Event
 * @param lineNum line number to go to (if not specified, will go to the next line)
 * Useful for rewinding or skipping exercises
 */

    IEnumerator GoToNextLine(int lineNum)
    {
        //Fade out
        textAnim.SetBool("On", false);
        yield return new WaitForSeconds(0.467f);

        //Go to line number given if specified
        if (lineNum > 0)
        {
            //Loop text if outside bounds
            if (lineNum >= lineList.Count || lineNum < 0)
                currentLine = 0;
            else
                currentLine = lineNum; //Print line number specified
        }

        //Print current line if line number not specified
        else {
            //Loop text if outside bounds
            if (currentLine >= lineList.Count || currentLine < 0)
                currentLine = 0;
            //else print current line
        }

        //Replace and show text
        text.text = lineList[currentLine];
        textAnim.SetBool("On", true);

        //Play voice line
        //audio

        //Prep for next line
        currentLine++;
    }

    /**
     * @brief Fade out at the end of the game
     */
    void FadeToBlack() {
        fadeAnim.SetTrigger("Fade out");
    }

}

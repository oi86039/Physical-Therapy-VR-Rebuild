using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using TMPro;

struct Line
{ //Voice line
    public float duration;
    public string line;


    public Line(float duration, string line)
    {
        this.duration = duration;
        this.line = line;
    }
}
public class ScriptManager : MonoBehaviour
{

    public string filename; //Filename to get lines from (txt)
    List<Line> lineList;
    int currentLine;

    [Header("")]
    public Animator anim;
    public TextMeshProUGUI text;

    bool runTimer;
    public float time; //current number of seconds counted
    public float timer; //duration when to skip to next line.


    // Start is called before the first frame update
    void Start()
    {
        runTimer = false;
        lineList = new List<Line>();
        currentLine = 0;
        ParseLines();
        StartCoroutine(StartDelay());
    }

    // Update is called once per frame
    void Update()
    {
        if (runTimer)
        {
            time += Time.deltaTime;
            if (time >= timer)
            {
                StartCoroutine(GoToNextLine());
            }
        }
    }

    void ParseLines()
    {
        //Open file
        string contents = Resources.Load<TextAsset>(filename).text;
        //Read lines
        string[] lines = Regex.Split(contents, "\n");
        //Split by |
        for (int i = 0; i < lines.Length; i++)
        {
            string[] split = Regex.Split(lines[i], " ; ");

            for (int j = 0; j < split.Length; j++)
                Debug.Log(split[j]);

            //Parse float
            float duration;
            if (!float.TryParse(split[0], out duration))
            {
                Debug.Log("Error: Couldn't parse duration at line: " + (i++) + ". Using default value.");
                duration = 4;  //Default value
            }
            //Store in Line class
            Line line = new Line(duration, split[1]);
            lineList.Add(line);

            //Print stored line
            Debug.Log(lineList[i].line);
        }
    }

    IEnumerator StartDelay()
    {
        yield return new WaitForSeconds(5);
        text.text = lineList[currentLine].line;

        anim.SetBool("On", true);

        //Run with first line delay
        timer = lineList[currentLine].duration;
        runTimer = true;

        //Prep for next line
        currentLine++;
    }

    IEnumerator GoToNextLine()
    {
        //Reset timer
        runTimer = false;
        time = 0;

        //Fade out
        anim.SetBool("On", false);
        yield return new WaitForSeconds(0.467f);

        if (currentLine >= lineList.Count)
            currentLine = 0;

        //Replace and show text
        text.text = lineList[currentLine].line;
        anim.SetBool("On", true);

        //Run with new line delay
        timer = lineList[currentLine].duration;
        runTimer = true;

        //Prep for next line
        currentLine++;
    }

}

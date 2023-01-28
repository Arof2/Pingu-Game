using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TMPro;
using UnityEditor;
//using UnityEditor.Profiling.Memory.Experimental;
using UnityEngine;
using UnityEngine.UIElements;
using Object = UnityEngine.Object;

public class GostPlayer : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private GameObject gostPlayer;
    [SerializeField] private TextMeshProUGUI timerUI;
    [SerializeField] private TextMeshProUGUI BestUI;
    private bool firstdone = false;
    private int startupOffset = 25;
    private float yOffset = -0.925f;
    private float timer;
    
    private enum State
    {
        Nothing,
        Record,
        Replay,
    }

    private State states;
    private string folder = @"Antarctic_Level\";
    private string tempname = "tempRecording.txt";
    private string playname = "play.txt";
    private string ignoretag = "IGNORE";
    private string path;
    private string playpath;
    private StreamWriter streamWriter;
    private StreamReader streamReader;
    
    // Start is called before the first frame update
    private void Startup()
    {
        path = folder + tempname;
        playpath = folder + playname;
        timer = 0.0f;
        
            states = State.Record;
            Directory.CreateDirectory(folder);
            streamWriter = new StreamWriter(path);
            
        if (Input.GetKey(KeyCode.P))
        {
            if (File.Exists(playpath))
            {
                BestUI.text = "Gost: " + timerToString(float.Parse(File.ReadLines(playpath).Last()));
                states = State.Replay;
                streamReader = new StreamReader(File.OpenRead(playpath));
            }
            else
            {
                states = State.Record;
            }
        }
        else
        { 
            string quickestPath = FindQuickest();
            if (File.Exists(quickestPath))
            {
                BestUI.text = "Best: " + timerToString(float.Parse(File.ReadLines(quickestPath).Last()));
                states = State.Replay;
                    streamReader = new StreamReader(File.OpenRead(quickestPath));
                }else 
            {
                states = State.Record; 
            }
        }

        if (states == State.Nothing)
        {
            gostPlayer.SetActive(false);
        }

        firstdone = true;
    }
    

    // Update is called once per frame
    void FixedUpdate()
    {
        if (states != State.Nothing)
        {
            timer += Time.deltaTime;
            timerUI.text = timerToString(timer);
        }

        if (!firstdone)
        {
            if (startupOffset<=0)
            {
                Startup();
                startupOffset--;
            }
            else
            {
                startupOffset--;
            }
        }else{
        
        
        if (states != State.Nothing && streamWriter!=null)
        {
            string posstring = "x";
            posstring += player.position.x;
            posstring += "y";
            posstring += player.position.y; 
            posstring += "z";
            posstring += player.position.z; 
            posstring += "a";
            posstring += player.localRotation.x;
            posstring += "b";
            posstring += player.localRotation.y;
            posstring += "c";
            posstring += player.localRotation.z;
            posstring += "d";
            posstring += player.localRotation.w;
            posstring += ";";
            streamWriter.WriteLine(posstring);
        }

        if (states == State.Replay)
        {
            string currentLine;
            if ((currentLine = streamReader.ReadLine()) != null && currentLine.Contains("x"))
            {


                float xpos = float.Parse(currentLine.Substring(currentLine.IndexOf("x") + 1,
                    (currentLine.IndexOf("y") - currentLine.IndexOf("x")) - 1));
                float ypos = float.Parse(currentLine.Substring(currentLine.IndexOf("y") + 1,
                    (currentLine.IndexOf("z") - currentLine.IndexOf("y")) - 1));
                float zpos = float.Parse(currentLine.Substring(currentLine.IndexOf("z") + 1,
                    (currentLine.IndexOf("a") - currentLine.IndexOf("z")) - 1));
                ypos = ypos + yOffset;
                float xrot = float.Parse(currentLine.Substring(currentLine.IndexOf("a") + 1,
                    (currentLine.IndexOf("b") - currentLine.IndexOf("a")) - 1));
                float yrot = float.Parse(currentLine.Substring(currentLine.IndexOf("b") + 1,
                    (currentLine.IndexOf("c") - currentLine.IndexOf("b")) - 1));
                float zrot = float.Parse(currentLine.Substring(currentLine.IndexOf("c") + 1,
                    (currentLine.IndexOf("d") - currentLine.IndexOf("c")) - 1));
                float wrot = float.Parse(currentLine.Substring(currentLine.IndexOf("d") + 1,
                    (currentLine.IndexOf(";") - currentLine.IndexOf("d")) - 1));
               //yrot = yrot + Quaternion.Euler(0.0f, -90f, 0.0f).y;

                gostPlayer.transform.position = new Vector3(xpos, ypos, zpos);
                gostPlayer.transform.localRotation = (new Quaternion(xrot, yrot, zrot, wrot) * Quaternion.Euler(0.0f, 90f, 0.0f)) ;
                
            }
            else
            {
                states = State.Record;
                gostPlayer.SetActive(false);
            }
        }
        }
    }

    public void DoShowGost(Boolean shown)
    {
        gostPlayer.SetActive(shown);
    }


    public void SaveRecording()
    {
        if (states != State.Nothing)
        {
            states = State.Nothing;
            string newpath = folder;
            newpath += timerToPathString(timer) + "_";
            newpath += DateTime.Now.ToString("dd-MM-yy");
            newpath += "_";
            bool saved = false;
            int number = 1;
            streamWriter.WriteLine(timer);
            streamWriter.Close();
            streamWriter = null;
            while (!saved)
            {
                try
                {
                    System.IO.File.Move(path, (newpath+number+".txt"));
                    saved = true;
                    //got Saved
                    
                    
                }
                catch (Exception e)
                {
                    if (e.GetType() == typeof(IOException))
                    {
                        number++;
                    }

                  
                }
            }
        }
    }

    private String timerToString(float t)
    {
        int minutes = (int)t/60;
        int seconds = ((int)t) % 60;
        int milli = (int)((t - (float)((int)t)) * 100);
        //mm:ss:mm
        string m = (minutes<10)? minutes==0? "00" : "0"+minutes : minutes +"" ;
        string s = (seconds<10)? seconds==0? "00" : "0"+seconds : seconds +"" ;
        string ms = (milli<10)? milli==0? "00" : "0"+milli : milli +"";
        
        return (m + ":" + s + ":" + ms);
    }
    
    private String timerToPathString(float t)
    {
        int minutes = (int)t/60;
        int seconds = ((int)t) % 60;
        int milli = (int)((t - (float)((int)t)) * 100);
        //mm:ss:mm
        string m = (minutes<10)? minutes==0? "00" : "0"+minutes : minutes +"" ;
        string s = (seconds<10)? seconds==0? "00" : "0"+seconds : seconds +"" ;
        string ms = (milli<10)? milli==0? "00" : "0"+milli : milli +"";
        
        return (m + ";" + s + ";" + ms);
    }

    private String FindQuickest()
    {
        string[] paths = Directory.GetFiles(folder);
        int index = -1;
        float shortest = float.MaxValue;
        for (int i = 0; i < paths.Length; i++)
        {
            if (!(paths[i].Contains(ignoretag) || paths[i].Contains(tempname)))
            {
                String last = File.ReadLines(paths[i]).Last();
                if (shortest > float.Parse(last))
                {
                    index = i;
                    shortest = float.Parse(last);
                }
            }
        }
    

        if (index == -1)
        {
            return null;
        }
        else
        {
            return paths[index];
        }
            
    }


    private void OnApplicationQuit()
    {
        if (streamWriter != null){
            streamWriter.Close();
        }
        File.Delete(path);
    }
}

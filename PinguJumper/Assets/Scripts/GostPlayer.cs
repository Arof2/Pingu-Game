using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using Object = UnityEngine.Object;

public class GostPlayer : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private GameObject gostPlayer;
    private bool firstdone = false;
    private int startupOffset = 25;
    private float yOffset = -0.925f;
    
    private enum State
    {
        Nothing,
        Record,
        Replay,
    }

    private State states;
    private string folder = @"Level2_Replay\";
    private string tempname = "tempRecording.txt";
    private string playname = "play.txt";
    private string path;
    private string playpath;
    private StreamWriter streamWriter;
    private StreamReader streamReader;
    
    // Start is called before the first frame update
    private void Startup()
    {
        path = folder + tempname;
        playpath = folder + playname;
        
        
        
        states = State.Nothing;
        if (Input.GetKey(KeyCode.R))
        {
            states = State.Record;
            Directory.CreateDirectory(folder);
            streamWriter = new StreamWriter(path);
        }
        if (Input.GetKey(KeyCode.P))
        {
            
            if (File.Exists(playpath))
            {
                states = State.Replay;
                streamReader = new StreamReader(File.OpenRead(playpath));
            }
            else
            {
                states = State.Nothing;
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
        if (!firstdone)
        {
            if (startupOffset<=0)
            {
                Startup();
            }
            else
            {
                startupOffset--;
            }
        }
        
        
        if (states == State.Record && streamWriter!=null)
        {
            string posstring = "x";
            posstring += player.position.x;
            posstring += "y";
            posstring += player.position.y; 
            posstring += "z";
            posstring += player.position.z; 
            posstring += "a";
            posstring += player.rotation.x;
            posstring += "b";
            posstring += player.rotation.y;
            posstring += "c";
            posstring += player.rotation.z;
            posstring += "d";
            posstring += player.rotation.w;
            posstring += ";";
            streamWriter.WriteLine(posstring);
        }
        
        if (states == State.Replay)
        {
            string currentLine;
            if ((currentLine = streamReader.ReadLine()) != null)
            {
                
                
               float xpos = float.Parse(currentLine.Substring(currentLine.IndexOf("x")+1, (currentLine.IndexOf("y")-currentLine.IndexOf("x"))-1));
               float ypos = float.Parse(currentLine.Substring(currentLine.IndexOf("y")+1, (currentLine.IndexOf("z")-currentLine.IndexOf("y"))-1));
               float zpos = float.Parse(currentLine.Substring(currentLine.IndexOf("z")+1, (currentLine.IndexOf("a")-currentLine.IndexOf("z"))-1));
               ypos = ypos + yOffset;
               float xrot = float.Parse(currentLine.Substring(currentLine.IndexOf("a")+1, (currentLine.IndexOf("b")-currentLine.IndexOf("a"))-1));
               float yrot = float.Parse(currentLine.Substring(currentLine.IndexOf("b")+1, (currentLine.IndexOf("c")-currentLine.IndexOf("b"))-1));
               float zrot = float.Parse(currentLine.Substring(currentLine.IndexOf("c")+1, (currentLine.IndexOf("d")-currentLine.IndexOf("c"))-1));
               float wrot = float.Parse(currentLine.Substring(currentLine.IndexOf("d")+1, (currentLine.IndexOf(";")-currentLine.IndexOf("d"))-1));
               yrot = yrot + Quaternion.Euler(0.0f, -90f, 0.0f).y;
               
               gostPlayer.transform.position = new Vector3(xpos, ypos, zpos);
               gostPlayer.transform.rotation = new Quaternion(xrot, yrot, zrot, wrot);
            }
            else
            {
                states = State.Nothing;
                gostPlayer.SetActive(false);
            }
        }
    }


    public void SaveRecording()
    {
        if (states == State.Record)
        {
            states = State.Nothing;
            string newpath = folder;
            newpath += DateTime.Now.ToString("dd-MM-yy");
            newpath += "_";
            bool saved = false;
            int number = 1;
            streamWriter.Close();
            streamWriter = null;
            while (!saved)
            {
                try
                {
                    System.IO.File.Move(path, newpath+number+".txt");
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

}

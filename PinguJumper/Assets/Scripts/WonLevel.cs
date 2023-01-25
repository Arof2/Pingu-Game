using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using Object = UnityEngine.Object;

public class WonLevel : MonoBehaviour
{
    [SerializeField] private Transform playerFinalPos;
    [SerializeField] private Transform cameraFinalPos;
    [SerializeField] private Transform lostPinguPos;
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject animatedPlayer;
    [SerializeField] private GameObject playercamera;
    [SerializeField] private GameObject gostPlayer;
    [SerializeField] private float timer = 3;

    private Boolean triggered;
    // Start is called before the first frame update
    void Start()
    {
        triggered = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (triggered)
        {
            //sets Player
            Transform ptransf = player.GetComponent<Transform>();
            ptransf.LookAt(lostPinguPos.position+ new Vector3(0.0f, 0.85f, 0.0f));
            ptransf.Rotate(new Vector3(0.0f, -90.0f, 0.0f));
            ptransf.position = playerFinalPos.position;


            //sets Camera
            Transform ctransf = playercamera.GetComponent<Transform>();
            ctransf.position = cameraFinalPos.position;
            ctransf.LookAt(((playerFinalPos.position+lostPinguPos.position)/2)+new Vector3(0.0f,0.35f,0.0f));

            timer = timer - Time.deltaTime;
        }

        if (timer <= 0.0f)
        {
            //Save Playsave if currently recording
            if(gostPlayer != null) //In case there is no ghostPlayer like in the cave level
                gostPlayer.GetComponent<GostPlayer>().SaveRecording();
            //Change to HUB
            SceneManager.LoadScene("Main_HUB");
        }
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            if(gostPlayer != null) //In case there is no ghostPlayer like in the cave level
                gostPlayer.GetComponent<GostPlayer>().DoShowGost(false);
            playercamera.GetComponent<CinemachineBrain>().enabled = false;
            player.GetComponent<PlayerBehavior>().enabled = false;
            player.GetComponent<Rigidbody>().isKinematic = true;
           // player.GetComponent<Rigidbody>().useGravity = false;
            animatedPlayer.GetComponent<PlayerAnimation>().enabled = false;
            animatedPlayer.GetComponent<Animator>().SetTrigger("Trigger_Excited");
            CheckforWin win = GameObject.FindObjectOfType<CheckforWin>();
            if(win)
                win.wonLevel();
            triggered = true;
        }
    }
}

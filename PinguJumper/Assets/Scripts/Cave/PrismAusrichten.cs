using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PrismAusrichten : MonoBehaviour
{
    private bool ausrichten = false, nearEnough = false;
    [SerializeField] private GameObject text, overlay;
    private PlayerBehavior player;
    private MeshRenderer rendere;
    private GameObject cam;
    [SerializeField]private bool origin;
    [SerializeField]private LineRenderer energyStrahl;
    [SerializeField] private Transform orientation;
    public float maxDistance = 100;
    private PrismAusrichten anotherPrism;

    private void Awake()
    {
        text.SetActive(false);
        player = GameObject.FindObjectOfType<PlayerBehavior>();
        rendere = gameObject.GetComponent<MeshRenderer>();
        cam = Camera.main.gameObject;
        overlay.SetActive(false);
    }

    public void Update()
    {
        if (origin && !ausrichten)
        {
            CastRay();
        }

        if (nearEnough && Input.GetKeyDown(KeyCode.E) && !ausrichten)
        {
            AusrichtungStarten();
        }
        
        if (ausrichten && Input.GetKeyDown(KeyCode.Escape))
        {
            AusrichtungStoppen();
        }
        else if(ausrichten)
        {
            cam.transform.rotation = Quaternion.Euler(-Input.GetAxisRaw("Mouse Y") + cam.transform.eulerAngles.x, Input.GetAxisRaw("Mouse X") + cam.transform.eulerAngles.y, 0);
        }
    }

    public void CastRay()
    {
        energyStrahl.enabled = true;
        RaycastHit hit;
        if (Physics.Raycast(transform.position, orientation.position - transform.position, out hit,maxDistance))
        {
            //hits smth
            if (hit.collider.CompareTag("Prism"))
            {
                energyStrahl.SetPositions(new Vector3[]{transform.position, hit.collider.transform.position});
                anotherPrism = hit.collider.GetComponent<PrismAusrichten>();
                if(!anotherPrism.origin)
                    anotherPrism.CastRay();
            }
            else
            {
                if (anotherPrism != null)
                {
                    anotherPrism.StopRay();
                    anotherPrism = null;
                }
                anotherPrism = null;
                energyStrahl.SetPositions(new Vector3[]{transform.position, hit.point});
            }
        }
        else
        {
            if (anotherPrism != null)
            {
                anotherPrism.StopRay();
                anotherPrism = null;
            }
            anotherPrism = null;
            energyStrahl.SetPositions(new Vector3[]{transform.position, (orientation.position - transform.position).normalized * maxDistance});
        }
        
    }

    public void StopRay()
    {
        energyStrahl.enabled = false;
        if (anotherPrism != null)
        {
            anotherPrism.StopRay();
            anotherPrism = null;
        }
    }

    public void AusrichtungStarten()
    {
        StopRay();
        ausrichten = true;
        player.changePlayerControl(false);
        rendere.enabled = false;
        text.SetActive(false);
        cam.transform.position = transform.position;
        cam.transform.rotation = transform.rotation * Quaternion.Euler(0,-90,0);
        overlay.SetActive(true);
    }

    public void AusrichtungStoppen()
    {
        ausrichten = false;
        overlay.SetActive(false);
        player.changePlayerControl(true);
        text.SetActive(true);
        rendere.enabled = true;
        transform.rotation = Quaternion.Euler(0,cam.transform.eulerAngles.y + 90, cam.transform.eulerAngles.x);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            nearEnough = true;
            text.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            nearEnough = false;
            text.SetActive(false);
        }
    }
}

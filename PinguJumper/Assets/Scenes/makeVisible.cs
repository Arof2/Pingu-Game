using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class makeVisible : MonoBehaviour
{
   // [SerializeField] GameObject o1;
   public Renderer test;
    private bool visible = false;
    // Start is called before the first frame update
    void Start()
    {
        test = GetComponent<MeshRenderer>();
        test.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (collect.coins == 5)
        {
            visible = true;
        }

        if (visible)
        {
            test.enabled = true;
        }
    }
}

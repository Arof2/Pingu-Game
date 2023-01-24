using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class doorP : MonoBehaviour
{
    private MeshRenderer rend;
    public float speed = 5;
    private Collider col;
    private void Start()
    {
        rend = GetComponent<MeshRenderer>();
        col = GetComponent<Collider>();
        col.enabled = true;
    }

    public void melt()
    {
        Debug.Log(rend.material.color.a);
        Material mat = rend.material;
        rend.material.color = Color.Lerp(mat.color,
            new Color(mat.color.r, mat.color.g, mat.color.b, 0),
            Time.deltaTime * speed);

        Debug.Log(rend.material.color.a);
        if (rend.material.color.a < 0.05f)
        {
            gameObject.SetActive(false);
        }
    }
}

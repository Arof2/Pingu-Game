using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class doorP : MonoBehaviour
{
    private MeshRenderer renderer;
    public float speed = 5;
    private Collider col;
    private void Start()
    {
        renderer = GetComponent<MeshRenderer>();
        col = GetComponent<Collider>();
        col.enabled = true;
    }

    public void melt()
    {
        Material mat = renderer.material;
        renderer.material.color = Color.Lerp(mat.color,
            new Color(mat.color.r, mat.color.g, mat.color.b, 0),
            Time.deltaTime * 5);

        if (renderer.material.color.a < 5)
        {
            gameObject.SetActive(false);
        }
    }
}

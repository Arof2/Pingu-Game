using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Vector2 = UnityEngine.Vector2;

public class WaterMovement : MonoBehaviour
{
    [SerializeField] private Vector2 speed;
    private Vector2 offset;

    // Update is called once per frame
    void Update()
    {
        Vector2 newOffset = offset +(speed * Time.deltaTime);
        GetComponent<Renderer>().material.mainTextureOffset = newOffset;
       //GetComponent<Renderer>().material.SetTextureOffset("_NORMALMAP", newOffset);
        offset = newOffset;
    }
}

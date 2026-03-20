using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenWrap : MonoBehaviour
{
    private Camera cam;
    public float wrapMargin = 1.5f;

    void Start()
    {
        cam = Camera.main;
    }

    void Update()
    {
        Vector3 pos = transform.position;

        float height = cam.orthographicSize;
        float width = height * cam.aspect;

        float left = -width - wrapMargin;
        float right = width + wrapMargin;
        float top = height + wrapMargin;
        float bottom = -height - wrapMargin;

        // Horizontal
        if (pos.x > right)
            pos.x = left;
        else if (pos.x < left)
            pos.x = right;

        // Vertical
        if (pos.y > top)
            pos.y = bottom;
        else if (pos.y < bottom)
            pos.y = top;

        transform.position = pos;
    }
}

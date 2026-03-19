using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenWrap : MonoBehaviour
{
    private Camera cam;

    void Start()
    {
        cam = Camera.main;
    }

    void Update()
    {
        Vector3 pos = transform.position;

        float height = cam.orthographicSize;
        float width = height * cam.aspect;

        float buffer = 0.5f;

        // horizontal
        if (pos.x > width + buffer)
        {
            pos.x = -width - buffer;
        }
        else if (pos.x < -width - buffer)
        {
            pos.x = width + buffer;
        }

        // vertical
        if (pos.y > height + buffer)
        {
            pos.y = -height - buffer;
        }
        else if (pos.y < -height - buffer)
        {
            pos.y = height + buffer;
        }

        transform.position = pos;
    }
}

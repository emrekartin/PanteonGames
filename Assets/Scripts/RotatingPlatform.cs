using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatingPlatform : MonoBehaviour
{
    public enum RotateDirection { clockwise, counterclockwise }
    public RotateDirection rotateDirection;
    public bool clockwise, counterclockwise;
    Transform _tr;
    private void Awake()
    {
        _tr = transform;
    }
    void Start()
    {
        chooseRotateDirection(rotateDirection);
    }
    void Update()
    {
        if (clockwise)
        {
            rotateClockwise();
        }
        else if (counterclockwise)
        {
            rotateCounterclockwise();
        }
        
    }
    void rotateClockwise()
    {
        _tr.Rotate(new Vector3(0, 0, -1) * Time.deltaTime * 100f);
    }
    void rotateCounterclockwise()
    {
        _tr.Rotate(new Vector3(0, 0, 1) * Time.deltaTime * 100f);
    }
    void chooseRotateDirection(RotateDirection rotateDirection)
    {
        switch (rotateDirection)
        {
            case RotateDirection.clockwise:
                clockwise = true;
                break;
            case RotateDirection.counterclockwise:
                counterclockwise = true;
                break;
            default:
                break;
        }
    }
}

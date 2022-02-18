using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HalfDonutObstacle : MonoBehaviour
{
    public enum Direction {lefttoright,righttoleft}
    public Direction direction;
    [SerializeField]
    Transform _movingPart;
    Vector3 _target;
    Vector3 _source;
    bool lefttoright, righttoleft;
    bool run = false;
    void Start()
    {
        ChooseDirection(direction);
        if (lefttoright)
        {
            _target = new Vector3(_movingPart.position.x + 3f, _movingPart.position.y, _movingPart.position.z);
        }
        else if (righttoleft)
        {
            _target = new Vector3(_movingPart.position.x - 3f, _movingPart.position.y, _movingPart.position.z);
        }
        _source = _movingPart.position;
    }
    void Update()
    {
        
        if (!run)
        {
            _movingPart.position = Vector3.Lerp(_movingPart.position, _target, Time.deltaTime * 4f);
            StartCoroutine(Run());
        }
        else
        {
            _movingPart.position = Vector3.Lerp(_movingPart.position, _source, Time.deltaTime * 4f);
        }
        
    }
    IEnumerator Run()
    {
        yield return new WaitForSeconds(1);
        run = true;
        StartCoroutine(SetRun());
    }
    IEnumerator SetRun()
    {
        yield return new WaitForSeconds(3);
        run = false;
    }
    void ChooseDirection(Direction direction)
    {
        switch (direction)
        {
            case Direction.lefttoright:
                lefttoright = true;
                break;
            case Direction.righttoleft:
                righttoleft = true;
                break;
            default:
                break;
        }
    }
}

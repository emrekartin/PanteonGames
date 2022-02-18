using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacles : MonoBehaviour
{
    public enum MoveType{notMove,rightleft,leftright}
    [SerializeField]
    float _movingDistance;
    [SerializeField]
    float _movingSpeed;
    public MoveType move;
    bool rightleft;
    bool leftright;
    bool notMove;
    Vector3 holdPos;
    Transform _tr;
    
    void Start()
    {
        ChooseDirection(move);
        _tr = transform;
        holdPos = _tr.position;
    }
    void Update()
    {
        Move();
        Rotate();
    }
    void ChooseDirection(MoveType move)
    {
        switch (move)
        {
            case MoveType.rightleft:
                rightleft = true;
                break;
            case MoveType.leftright:
                leftright = true;
                break;
            case MoveType.notMove:
                notMove = true;
                break;
            default:
                break;
        }
    }
    void Move()
    {
        if (rightleft)
        {
            float yPos = Mathf.PingPong(Time.time * _movingSpeed, 1) * _movingDistance;
            transform.position = new Vector3(holdPos.x+ yPos, transform.position.y, transform.position.z);
        }
        else if(leftright)
        {
            float yPos = Mathf.PingPong(Time.time * _movingSpeed, 1) * _movingDistance;
            transform.position = new Vector3(holdPos.x + -yPos, transform.position.y, transform.position.z);
        }
        else if(notMove)
        {
        
        }
    }
    void Rotate()
    {
        transform.Rotate(new Vector3(0,1,0)* Time.deltaTime*200f);
    }
}

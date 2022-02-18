using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public static CameraController cameraController;
    Space offsetPositionSpace = Space.Self;
    [SerializeField]
    Vector3 _offsetPosition;
    [SerializeField]
    Vector3 _offsetRotation;
    [SerializeField]
    Vector3 _offsetPositionforEndGame;
    [SerializeField]
    Vector3 _offsetRotationforEndGame;
    GameObject _target;
    Vector3 _vec;
    Transform _tr;
    public bool endGame;
    private void Awake()
    {
        cameraController = this;
    }
    void Start()
    {
        _tr = transform;
        _target = Player.player.gameObject;
    }
    void Update()
    {
        if(!endGame)
        Move();
        else
        {
            EndGame();
        }
    }
    void Move()
    {
        _tr.rotation = Quaternion.Lerp(_tr.rotation, Quaternion.Euler(_offsetRotation.x, 0, 0), Time.deltaTime);
        _vec = new Vector3(_target.transform.position.x, 0, _target.transform.position.z);
        _tr.position = Vector3.Lerp(transform.position, _vec + _offsetPosition, 8 * Time.deltaTime);
    }
    void EndGame()
    {
        _tr.rotation = Quaternion.Lerp(_tr.rotation, Quaternion.Euler(_offsetRotationforEndGame.x, 0, 0), Time.deltaTime);
        _vec = new Vector3(_target.transform.position.x, 0, _target.transform.position.z);
        _tr.position = Vector3.Lerp(transform.position, _vec + _offsetPositionforEndGame, 8 * Time.deltaTime);
    }
}

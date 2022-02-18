using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorizontalMovement : MonoBehaviour
{
    public static HorizontalMovement horizontalMovement;
    [SerializeField]
    Animator _playerAnimator;
    public float limitX;
    public float runSpeed; 
    private float multx;
    bool OnlyOne;
    private float speedTouch = 0.2f; float newX = 0;
    float newZ = 0;
    float axesX; public float xSpeed;
    public GameObject TapToPlay;
    public bool finishGame;
    void Start()
    {
        multx = 720f / Screen.width;
    }
    private void Awake()
    {
        horizontalMovement = this;
    }
    void Update()
    {
        TouchMovement();
    }
    void TouchMovement()
    {
        if (!finishGame)
        {
            
            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);
                if (touch.phase == TouchPhase.Moved)
                {
                    axesX = touch.deltaPosition.x * multx * speedTouch;
                }
            }
            else
            {
                axesX = 0;
            }

            newX = transform.position.x + xSpeed * axesX * Time.deltaTime;
            newX = Mathf.Clamp(newX, -limitX, limitX);
            newZ = transform.position.z + runSpeed * Time.deltaTime;
            if (Input.GetMouseButton(0))
            {
                if (!OnlyOne)
                {
                    OnlyOne = true;
                    GameManager.gameManager.PlayGame = true;
                    TapToPlay.SetActive(false);
                }
                transform.position = new Vector3(newX, transform.position.y, newZ);
                _playerAnimator.SetBool("isRunning", true);
            }
            else
            {
                _playerAnimator.SetBool("isRunning", false);

            }

        }
        else
        {
            transform.position = Vector3.Lerp(transform.position, LevelManagement.levelManagement.finishForRun.GetComponent<Collider>().ClosestPoint(transform.position)+new Vector3(0,0,1f), Time.deltaTime * 8f);
            if (Vector3.Distance(transform.position, LevelManagement.levelManagement.finishForRun.GetComponent<Collider>().ClosestPoint(transform.position) + new Vector3(0, 0, 1f)) < 0.14f)
            {
                _playerAnimator.SetBool("isRunning", false);
                CameraController.cameraController.endGame = true;
            }

        }
    }

}

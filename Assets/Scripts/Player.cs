using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player player;
    Transform _tr;
    Vector3 _target1Pos;
    Vector3 _targetPos;
    [SerializeField]
    Animator _playerAnimator;
    Quaternion _targetRot;
    public bool move;
    bool OnlyOne1;
    // Start is called before the first frame update
    void Awake()
    {
        move = true;
        player = this;
        _tr = transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (move)
        {
            Move();
            Rotation();
        }
        else
        {
            //AfterFinish();
        }
    }
    void Move()
    {
        _target1Pos = new Vector3(HorizontalMovement.horizontalMovement.gameObject.transform.position.x, transform.position.y, HorizontalMovement.horizontalMovement.gameObject.transform.position.z - 2f);
        _tr.position = Vector3.Lerp(_tr.position, _target1Pos, Time.deltaTime * 8f);
        //GetComponent<Rigidbody>().velocity = ((_target1Pos-transform.position).normalized  * 7f);
    }
    void Rotation()
    {
        _targetPos = HorizontalMovement.horizontalMovement.gameObject.transform.position - _tr.transform.position;
        _targetPos.y = 0;
        _targetRot = Quaternion.LookRotation(_targetPos);
        _tr.rotation = Quaternion.Lerp(_tr.rotation, _targetRot, Time.deltaTime * 8f);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Obs")
        {
            _playerAnimator.SetBool("isFalling", true);
            LevelManagement.levelManagement.GameOver.SetActive(true);
            GameManager.gameManager.RetryPrefab.SetActive(true);
            move = false;
        }
    }
    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "RotatingPlatform")
        {
            if (collision.gameObject.GetComponent<RotatingPlatform>().counterclockwise)
            {
                HorizontalMovement.horizontalMovement.transform.position = Vector3.Lerp(HorizontalMovement.horizontalMovement.transform.position, (HorizontalMovement.horizontalMovement.transform.position + (-HorizontalMovement.horizontalMovement.transform.right)), Time.deltaTime * 5f);
                if(HorizontalMovement.horizontalMovement.transform.position.x < -3.3f)
                {
                    _playerAnimator.SetBool("isFalling", true);
                    LevelManagement.levelManagement.GameOver.SetActive(true);
                    GameManager.gameManager.RetryPrefab.SetActive(true);
                    gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
                    move = false;
                }
            }
            else if (collision.gameObject.GetComponent<RotatingPlatform>().clockwise)
            {
                HorizontalMovement.horizontalMovement.transform.position = Vector3.Lerp(HorizontalMovement.horizontalMovement.transform.position, (HorizontalMovement.horizontalMovement.transform.position + (HorizontalMovement.horizontalMovement.transform.right)), Time.deltaTime * 5f);
                if (HorizontalMovement.horizontalMovement.transform.position.x > 3f)
                {
                    _playerAnimator.SetBool("isFalling", true);
                    LevelManagement.levelManagement.GameOver.SetActive(true);
                    GameManager.gameManager.RetryPrefab.SetActive(true);
                    move = false;
                    gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
                }
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "FinishLine")
        {
            if (!OnlyOne1)
            {
                OnlyOne1 = true;
                RankSystem.rankSystem.t++;
                if (!RankSystem.rankSystem.LosePlayer)
                {
                    Debug.Log("Win"); LevelManagement.levelManagement.Paintable1.SetActive(true);
                    LevelManagement.levelManagement.Paintable2.SetActive(true);
                    LevelManagement.levelManagement.ScoreForPaint.SetActive(true);
                    RankSystem.rankSystem.textView.SetActive(false);
                    HorizontalMovement.horizontalMovement.finishGame = true;
                    _playerAnimator.SetBool("Victory", true);
                }
                else
                {
                    Debug.Log("Lose");
                    LevelManagement.levelManagement.GameOver.SetActive(true);
                    GameManager.gameManager.RetryPrefab.SetActive(true);
                    HorizontalMovement.horizontalMovement.finishGame = true;
                }
            }
            
        }
    }
    
}

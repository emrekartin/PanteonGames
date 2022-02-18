using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Opponent : MonoBehaviour
{
    bool OnlyOne1;
    NavMeshAgent navMeshAgent;
    bool move;
    Transform _tr;
    Vector3 _target;
    int i = 0;
    bool OnlyOne;
    bool _toTarget;
    Vector3 _startingPoint;
    public Vector3 Target1;
    public Vector3 Target2;
    public Vector3 Target3;
    int RandomINT;
    [SerializeField]
    Animator _OpponentAnimator;
    void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        _tr = transform;
    }
    void Start()
    {
        _startingPoint = _tr.position;
    }
    void Update()
    {
        if (GameManager.gameManager.PlayGame)
        {
            if (!move)
            {
                if (!_toTarget)
                {
                    _OpponentAnimator.SetBool("isRunning", true);
                    GetTargetMove();
                }
                if (Vector3.Distance(_tr.position, _target) < 1f)
                {
                    if (i + 1 != LevelManagement.levelManagement.stage[0].obs.Count)
                    {
                        i++;
                    }
                    else
                    {
                        _toTarget = true;
                        _target = LevelManagement.levelManagement.finishForRun.GetComponent<Collider>().ClosestPoint(_tr.position);
                    }
                    OnlyOne = false;
                }
                else
                {
                    navMeshAgent.SetDestination(_target);
                }
            }
        }
        else
        {

        }
    }
    void GetTargetMove()
    {
        if (!OnlyOne)
        {
            RandomINT = Random.Range(0, 2);
            OnlyOne = true;
            Target1 = new Vector3(-3.5f, 0, LevelManagement.levelManagement.stage[0].obs[i].transform.position.z);
            Target2 = new Vector3(0, 0, LevelManagement.levelManagement.stage[0].obs[i].transform.position.z);
            Target3 = new Vector3(+3.5f, 0, LevelManagement.levelManagement.stage[0].obs[i].transform.position.z);
            if (Vector3.Distance(LevelManagement.levelManagement.stage[0].obs[i].transform.position, Target1) < Vector3.Distance(LevelManagement.levelManagement.stage[0].obs[i].transform.position, Target2))
            {
                if (Vector3.Distance(LevelManagement.levelManagement.stage[0].obs[i].transform.position, Target1) < Vector3.Distance(LevelManagement.levelManagement.stage[0].obs[i].transform.position, Target3))
                {
                    if (RandomINT == 0)
                    {
                        _target = Target2;
                    }
                    else if (RandomINT == 1)
                    {
                        _target = Target3;
                    }
                }
                else
                {
                    if (RandomINT == 0)
                    {
                        _target = Target1;
                    }
                    else if (RandomINT == 1)
                    {
                        _target = Target2;
                    }
                }
            }
            else
            {
                if (Vector3.Distance(LevelManagement.levelManagement.stage[0].obs[i].transform.position, Target2) < Vector3.Distance(LevelManagement.levelManagement.stage[0].obs[i].transform.position, Target3))
                {
                    if (RandomINT == 0)
                    {
                        _target = Target1;
                    }
                    else if (RandomINT == 1)
                    {
                        _target = Target3;
                    }
                }
                else
                {

                    if (RandomINT == 0)
                    {
                        _target = Target2;
                    }
                    else if (RandomINT == 1)
                    {
                        _target = Target1;
                    }
                }
            }

        }
        
    }
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Obs")
        {
            navMeshAgent.enabled = false;
            GetComponent<BoxCollider>().enabled = false;
            GetComponent<Rigidbody>().isKinematic = true;
            _OpponentAnimator.SetBool("isFalling", true);
            move = true;
            StartCoroutine(WaitFallDown());
        }
    }
    IEnumerator WaitFallDown()
    {
        yield return new WaitForSeconds(2);
        _OpponentAnimator.SetBool("isFalling", false);
        navMeshAgent.enabled = true;
        GetComponent<BoxCollider>().enabled = true;
        GetComponent<Rigidbody>().isKinematic = false;
        OnlyOne = false;
        move = false;
        _toTarget = false;
        i = 0;
        _tr.position = _startingPoint;

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "FinishLine")
        {
            if (!OnlyOne1)
            {
                StartCoroutine(WaitAfterFinish());
                OnlyOne1 = true;
                RankSystem.rankSystem.t++; RankSystem.rankSystem.LosePlayer = true;
            }

        }
    }
    IEnumerator WaitAfterFinish()
    {
        yield return new WaitForSeconds(1);
        
        if (RankSystem.rankSystem.rankForPlayer + 1 != 1)
        {
            Destroy(gameObject);
            _OpponentAnimator.SetBool("isRunning", false);

            navMeshAgent.enabled = false;
            move = true;
        }
    }
}

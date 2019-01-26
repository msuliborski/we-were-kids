using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FatherController : MonoBehaviour
{
    [SerializeField]
    private Vector3 _currentTarget;
    private NavMeshAgent _agent;
    [SerializeField]
    private bool _isHunting = false;
    [SerializeField]
    private bool _isWaiting = false;
    [SerializeField]
    private List<Vector3> _patrolPositions;
    private float _isWaitingTimer = 1.5f;

    
    public void SetIsHunting(bool isHunting, Vector3 target)
    {
        if (isHunting)
        {
            _isHunting = true;
            _currentTarget = target;
            _agent.speed = 9f;
            _agent.SetDestination(_currentTarget);
        }
        else
        {
            _isHunting = false;
            _currentTarget = RandomDest();
            _agent.SetDestination(_currentTarget);
            _agent.speed = 2f;
        }
    }
    
    
    void Start()
    {
        _patrolPositions = new List<Vector3>();
        GameObject points = GameObject.Find("PatrolPoints");
        for (int i = 0; i < points.transform.childCount; i++)
        {
            Vector3 patroPoint = points.transform.GetChild(i).position;
            _patrolPositions.Add(new Vector3(patroPoint.x, 5.165674f, patroPoint.z));
        }
        Debug.Log("Count: " + _patrolPositions.Count);
        _currentTarget = _patrolPositions[0];
        _agent = GetComponent<NavMeshAgent>();
        _agent.SetDestination(_currentTarget);
        _agent.speed = 30f;
        Debug.Log("start");
        
    }
    
    

    // Update is called once per frame
    void Update()
    {
       // if (!_isHunting)
       // {
           if (!_isWaiting)
          
            {
                if (transform.position == _currentTarget)
                {
                    //_agent.Stop();
                    _isWaiting = true;
                }
            }
            else
            {
                Debug.Log(_isWaitingTimer);
                _isWaitingTimer -= Time.deltaTime;
                if (_isWaitingTimer <= 0f)
                {
                    Debug.Log(_isWaitingTimer);
                   _isWaiting = false;
                   _isWaitingTimer = 1.5f;
                   _currentTarget = RandomDest();
                   _agent.SetDestination(_currentTarget);
                   
                }
            }
       // }
       // else
       // {
           /* if (Mathf.Approximately(transform.position.x, _currentTarget.x) && Mathf.Approximately(transform.position.z, _currentTarget.z))
                         {
                             Debug.Log("kureaaaa");
                             SetIsHunting(false, Vector3.zero);
                             
                             _isWaiting = false;
                         }*/
       // }
    }
    
    
    Vector3 RandomDest()
    {
        _patrolPositions.Remove(_currentTarget);
        int index = Random.Range(0, _patrolPositions.Count);
        Vector3 random = _patrolPositions[index];
        _patrolPositions.Add(_currentTarget);
        return random;
    }

    private void OnTriggerEnter(Collider col)
    {

        if (col.gameObject.tag == "children")
        {
            // WSTAW BACHORA DO KATA
        }
    }
}

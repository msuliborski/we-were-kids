using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FatherController : MonoBehaviour
{
    private PlayerHandler[] player;
    
    
    [SerializeField]
    private Vector3 _currentTarget;
    private NavMeshAgent _agent;
    [SerializeField]
    private bool _isHunting = false;
    [SerializeField]
    private bool _isWaiting = false;
    [SerializeField] double isHuntingTimer = 10f;
   
    private List<Vector3> _patrolPositions;
    private float _isWaitingTimer = 1.5f;

    private Animator anim;
    private GameObject model;
    
    public static int player0Score = 0;
    public static int player1Score = 0;

    [SerializeField] private AudioClip slap;
    [SerializeField] private AudioClip kurla;
    private AudioSource source;

    
    public void SetIsHunting(bool isHunting, Vector3 target)
    {
        if (isHunting)
        {
            _isHunting = true;
            _currentTarget = target;
            _agent.speed = 40f;
            _agent.SetDestination(_currentTarget);
            anim.SetBool("walking", false);
            anim.SetBool("running", true);
        }
        else
        {
            _isHunting = false;
            _currentTarget = RandomDest();
            _agent.SetDestination(_currentTarget);
            _agent.speed = 20f;
            anim.SetBool("running", false);
            anim.SetBool("walking", true);
        }
    }
    
    
    void Start()
    {
        player = new[] {GameObject.Find("Player0").GetComponent<PlayerHandler>(), GameObject.Find("Player1").GetComponent<PlayerHandler>()};
        model = transform.GetChild(0).gameObject;
        anim = GetComponentInChildren<Animator>();
        _patrolPositions = new List<Vector3>();
        GameObject points = GameObject.Find("PatrolPoints");
        for (int i = 0; i < points.transform.childCount; i++)
        {
            Vector3 patroPoint = points.transform.GetChild(i).position;
            _patrolPositions.Add(new Vector3(patroPoint.x, 5.143333f, patroPoint.z));
        }
        Debug.Log("Count: " + _patrolPositions.Count);
        _currentTarget = _patrolPositions[0];
        _agent = GetComponent<NavMeshAgent>();
        _agent.SetDestination(_currentTarget);
        _agent.speed = 20f;
        Debug.Log("start");
        //anim.SetBool("walking", true);
        source = GetComponent<AudioSource>();
        source.clip = slap;
        SetIsHunting(true, player[0].transform.position);
        
    }
    
    

    // Update is called once per frame
    void Update()
    {
       if (!_isHunting)
       {
           if (!_isWaiting)
          
            {
                if (Mathf.Approximately(transform.position.x, _currentTarget.x) && Mathf.Approximately(transform.position.z, _currentTarget.z))
                {
                    //_agent.Stop();
                    _isWaiting = true;
                    anim.SetBool("walking", false);
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
                   anim.SetBool("walking", true);
                   _isWaitingTimer = 1.5f;
                   _currentTarget = RandomDest();
                   _agent.SetDestination(_currentTarget);
                   
                }
            }
       }
       else
       {
           isHuntingTimer -= Time.deltaTime;
           if (isHuntingTimer <= 0)
           {
               SetIsHunting(false, Vector3.zero);
               isHuntingTimer = 10f;
           }
       }
       
       
       model.transform.position = new Vector3(transform.position.x, -0.36f, transform.position.z);
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
    
        if (col.gameObject.tag == "Player" && _isHunting)
        {Debug.Log("chuj");
            source.PlayOneShot(source.clip);
            anim.SetBool("running", false);
            anim.SetBool("walking", false);
            anim.SetBool("slap", true);
            isHuntingTimer = 10f;
            SetIsHunting(false, Vector3.zero);
        }
    }
    
   
}

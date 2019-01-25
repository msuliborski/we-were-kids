using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using TMPro;

public class Farmer : MonoBehaviour {

    public GameObject Control1;
    public GameObject Control2;
    public GameObject WinScreen;
    public TextMeshProUGUI WinScreenText;

    public enum WindowState { Control1, Control2, Game, WinScreen};
    public WindowState CurrState = WindowState.Control1;

    
    public TextMeshProUGUI Timer;
    public float TimerValue = 120f;
    //public int CultistsCounter = 2;
    List<Vector3> _patrolPositions;
    public List<Cultist> Cultists = new List<Cultist>();
    public Vector3 _currentTarget;
    public NavMeshAgent _agent;
    private bool _isHunting = false;
    private AudioSource source;
    [SerializeField] private AudioClip clip;
    public void SetIsHunting(bool isHunting, Vector3 target)
    {
        if (isHunting)
        {
            if (!source.isPlaying) source.PlayOneShot(source.clip);
            _isHunting = true;
            _currentTarget = target;
            _agent.speed = 9f;
            //_agent.speed = 0;
            _agent.SetDestination(_currentTarget);
            Animator.enabled = true;
        }
        else
        {
            _isHunting = false;
            _currentTarget = RandomDest();
            _agent.SetDestination(_currentTarget);
            _agent.speed = 2f;
        }
    }

    public bool IsWaiting = false;
   
    public float IsWaitingTimer = 1.5f;

    public Animator Animator;


    void Start()
    {
        Timer = GameObject.Find("Timer").GetComponent<TextMeshProUGUI>();
        source = gameObject.GetComponent<AudioSource>();
        source.clip = clip;
        _patrolPositions = new List<Vector3>();
        GameObject points = GameObject.Find("PatrolPoints");
        for(int i = 0; i < transform.childCount; i++)
            _patrolPositions.Add(points.transform.GetChild(i).position);
        Cultists.Add(GameObject.Find("Cultist 1").GetComponent<Cultist>());
        Cultists.Add(GameObject.Find("Cultist 2").GetComponent<Cultist>());
        Animator = GetComponentInChildren<Animator>();
        
        _currentTarget = _patrolPositions[0];
        _agent = GetComponent<NavMeshAgent>();
        _agent.SetDestination(_currentTarget);
        //_agent.speed = 0;
        _agent.speed = 3f;

    }

    void Update()
    {
        if (CurrState == WindowState.Game)
        {
            TimerValue -= Time.deltaTime;
            Timer.text = Mathf.RoundToInt(TimerValue).ToString();
            if (TimerValue <= 0)
            {
                CurrState = WindowState.WinScreen;
                Cultists[0].Blocked = true;
                Cultists[1].Blocked = true;
                WinScreen.SetActive(true);
                if (Cultists[0].Killed > Cultists[1].Killed)
                    WinScreenText.text = "Left player has won by\nkilling " + Cultists[0].Killed.ToString() + " pig(s) in the name of Satan!\nSatan is satisfied with\nyour bloody sacrifice.";
                else if (Cultists[0].Killed < Cultists[1].Killed)
                    WinScreenText.text = "Right player has won\nby killing " + Cultists[1].Killed.ToString() + " pig(s) in the name of Satan!\nSatan is satisfied with\nyour bloody sacrifice.";
                else
                {
                    if (Cultists[0].Killed == 0 && Cultists[1].Killed == 0)
                        WinScreenText.text = "There is a draw!\nSatan is not satisfied with\nyour sacrifice.";
                    else
                        WinScreenText.text = "There is a draw!\nSatan is satisfied with\nyour bloody sacrifice.";
                }
            }


            if (Input.GetKeyDown(KeyCode.Escape))
            {
                SceneManager.LoadScene("menu");
            }
            if (!_isHunting)
            {
                if (!IsWaiting)
                {

                    if (transform.position.x == _currentTarget.x && transform.position.z == _currentTarget.z)
                    {
                        IsWaiting = true;
                        AnimatorIdle();
                    }
                }
                else
                {
                    IsWaitingTimer -= Time.deltaTime;
                    if (IsWaitingTimer <= 0f)
                    {
                        IsWaiting = false;
                        IsWaitingTimer = 1.5f;
                        _currentTarget = RandomDest();
                        _agent.SetDestination(_currentTarget);
                        Animator.enabled = true;

                    }
                }
            }
            else
            {
                if (transform.position.x == _currentTarget.x && transform.position.z == _currentTarget.z)
                {
                    SetIsHunting(false, Vector3.zero);
                    IsWaiting = false;
                }
            }
        }
    }

    void LateUpdate()
    {
        if (CurrState == WindowState.Control1)
        {
            if (Input.anyKeyDown)
            {
                foreach (Animator anim in Cultists[0].ButtonAnimators)
                    anim.SetBool("Active", true);
                foreach (Animator anim in Cultists[1].ButtonAnimators)
                    anim.SetBool("Active", true);
                CurrState = WindowState.Control2;
                Control1.SetActive(false);
                Control2.SetActive(true);
            }
        }
        else if (CurrState == WindowState.Control2)
        {
            if (Input.anyKeyDown)
            {
                foreach (Animator anim in Cultists[0].ButtonAnimators)
                    anim.SetBool("Active", false);
                foreach (Animator anim in Cultists[1].ButtonAnimators)
                    anim.SetBool("Active", false);
                CurrState = WindowState.Game;
                Control2.SetActive(false);
                Cultists[0].Blocked = false;
                Cultists[1].Blocked = false;
            }
        }
        else if (CurrState == WindowState.WinScreen)
        {
            if (Input.anyKeyDown)
            {
                SceneManager.LoadScene("menu");
            }


        }
    }

    Vector3 RandomDest()
    {
        Vector3 random = _currentTarget;
        while(random == _currentTarget)
        {
            random = _patrolPositions[Random.Range(0, _patrolPositions.Count)];
        }
        return random;
    }

    private void OnTriggerEnter(Collider col)
    {
        
        if (col.gameObject.tag == "cultist")
        {
           Cultist cultist = col.gameObject.GetComponent<Cultist>();
            if (cultist.IsBeast)
            {
                //_agent.speed = 11;

                
                //Couroutine To Play Death Animation
                
                    cultist.IsDying = true;
                    cultist._rigidbody.velocity = Vector3.zero;
                    cultist.IsBeast = false;
                    //cultist.Marker.sprite = null;
                    if (cultist.IsKilling)
                    {
                        Piglet piglet = cultist._touchedPiglet;
                        piglet.Rb.velocity =
                        new Vector3(Random.Range(-1f, 1f), 0, Random.Range(-1f, 1f)).normalized * piglet._vel;
                        piglet.Rb.drag = 0;
                        foreach (TextMeshProUGUI txt in cultist.Text)
                            txt.text = "";
                    
                        foreach (Animator animator in cultist.ButtonAnimators)
                            animator.SetBool("Active", false);
                    }
                    StartCoroutine(cultist.PlayDeathAnimation());
                
            }
            //  }
            // --
            //else
            // Load Win screen scene

        }
    }

    void AnimatorIdle()
    {
        Animator.enabled = false;
    }

    #region OldCode
    /*   public int Width = 18, Height = 10;
       public Queue<Field> Frontier { get; set; }
       public Dictionary<Field, Field> CameFrom { get; set; }
       public Dictionary<Field, int> FromDirection { get; set; }
       public List<Field> Path { get; set; }
       [SerializeField]
       private Field[] _fieldsToVisit;
       public Field CurrentDestination { get; set; }
       [SerializeField]
       private Field _currentField;
       public Field CurrentField
       {
           get
           {
               return _currentField;
           }
           set
           {
               _currentField = value;
           }
       }
       public Field CurrentTarget { get; set; }

       private bool _achievedFirstField = false;


       [SerializeField]
       private Direction _currentRotation = Direction.N;

       [SerializeField]
       private int _movementSpeed;
       [SerializeField]
       private int _rotatingSpeed;

       public enum MovementState { Moving, Rotating }
       [SerializeField]
       private MovementState _currentState = MovementState.Moving;
       public MovementState CurrentState { get { return _currentState; } set { _currentState = value; } }

       private delegate void currentState();
       private currentState _currentStateDelegate;

       private Quaternion _destinationRotation;

       public Field[,] Fields { get; set; }
       private Cultist[] _cultists;

       void ActivateFields()
       {
           Fields = new Field[Width, Height];
           int y = 0, x = 0;
           GameObject 
           for (int y = -Height / 2; y <= Height / 2 - 1; y++)
               for (int x = -Width / 2; x <= Width / 2 - 1; x++)
               {
                   GameObject field = new GameObject { name = "Field " + (x + Width / 2).ToString() + " " + (y + Height / 2).ToString() };
                   field.transform.SetParent(transform);
                   Field fieldScript = field.AddComponent<Field>();
                   fieldScript.X = x + Width / 2;
                   fieldScript.Y = y + Height / 2;
                   fieldScript.Width = Width;
                   fieldScript.Height = Height;
                   Fields[fieldScript.X, fieldScript.Y] = fieldScript;
                   fieldScript.Width = Width;
                   fieldScript.Height = Height;
                   field.transform.position = new Vector2(2 * x + 1, 2 * y + 1);
               }

           foreach (Field field in Fields)
           {
               int x = field.X;
               int y = field.Y;
               if (y + 1 < Height)
               {
                   field.Neighbors[0] = Fields[x, y + 1];
                   if (x + 1 < Width) field.Neighbors[1] = Fields[x + 1, y + 1];
                   if (x - 1 >= 0) field.Neighbors[7] = Fields[x - 1, y + 1];
               }
               if (y - 1 >= 0)
               {
                   field.Neighbors[4] = Fields[x, y - 1];
                   if (x + 1 < Width) field.Neighbors[3] = Fields[x + 1, y - 1];
                   if (x - 1 >= 0) field.Neighbors[5] = Fields[x - 1, y - 1];
               }
               if (x + 1 < Width) field.Neighbors[2] = Fields[x + 1, y];
               if (x - 1 >= 0) field.Neighbors[6] = Fields[x - 1, y];
           }
       }

       void Awake()
       {
           ActivateFields();

           Frontier = new Queue<Field>();
           CameFrom = new Dictionary<Field, Field>();
           FromDirection = new Dictionary<Field, int>();
           Path = new List<Field>();
           _currentStateDelegate = Moving;
           transform.position = _currentField.transform.position;
           //Debug.Log(_currentField.XOffset + 1 + " " + _currentField.YOffset + 1);
           if (Fields == null) Debug.Log("AAA");
           FindPath(Fields[_currentField.X, _currentField.Y + 1]);
       }

       void Start ()
       {
           //_cultists[0] = GameObject.Find("Cultist 1").GetComponent<Cultist>();
       }


       void Update ()
       {
           _currentStateDelegate();
       }

       void Moving()
       {
           transform.position = Vector3.MoveTowards(transform.position, Path[0].transform.position, _movementSpeed * Time.deltaTime);
           if (transform.position == Path[0].transform.position) OnArrivalToField(Path[0]);
       }

       void Rotating()
       {
           transform.rotation = Quaternion.RotateTowards(transform.rotation, _destinationRotation, _rotatingSpeed * Time.deltaTime);
           if (transform.rotation == _destinationRotation)
           {
               if (Path.Count != 0)
               {
                   _currentStateDelegate = Moving;
                   _currentState = MovementState.Moving;
               }
               else
               {
                   Field random = FindRandomField();
                   FindPath(random);
                   if (_currentField.GetNeighbor(_currentRotation) == Path[0])
                   {
                       _currentState = MovementState.Rotating;
                       _currentStateDelegate = Rotating;
                   }
                   else
                   {
                       _currentState = MovementState.Moving;
                       _currentStateDelegate = Moving;
                   }
               }
           }
       }

       void OnArrivalToField(Field field)
       {
           if (_achievedFirstField)
           {
               _currentField = field;
               Path.RemoveAt(0);
           }
           else _achievedFirstField = true;

           if (Path.Count == 0)
           {
               //Animator.SetBool("Walking", false);
               Field random = FindRandomField();
               FindPath(random);
               if (_currentField.GetNeighbor(_currentRotation) == Path[0])
               {
                   _currentState = MovementState.Rotating;
                   _currentStateDelegate = Rotating;
               }
               else
               {
                   _currentState = MovementState.Moving;
                   _currentStateDelegate = Moving;
               }
           }
           else
           {
               if (_currentField.GetNeighbor(_currentRotation) == Path[0])
               {
                   _currentState = MovementState.Moving;
                   _currentStateDelegate = Moving;
               }
               else Rotate(Path[0]);
           }
       }

       void Rotate(Field field)
       {
           _currentState = MovementState.Rotating;
           _currentStateDelegate = Rotating;
           _destinationRotation = GetRotation(_currentRotation, _currentField, field);
       }

       Quaternion GetRotation(Direction currentRotation, Field currentField, Field neighbor)
       {
           Direction nextDirection = _currentField.GetDirection(neighbor);
           return GetRotation(_currentRotation, nextDirection);
       }


       Quaternion GetRotation(Direction currentDirection, Direction nextDirection)
       {
           return Quaternion.Euler(0, (int)_currentRotation * 45 + currentDirection.SmallestDifference(nextDirection) * 45, 0);
       }


       void FindPath(Field field)
       {
           _achievedFirstField = false;
           Frontier.Clear();
           CameFrom.Clear();
           Path.Clear();
           FromDirection.Clear();
           Frontier.Enqueue(_currentField);
           CameFrom.Add(_currentField, null);
           FromDirection.Add(_currentField, (int)_currentRotation);

           while (Frontier.Count != 0)
           {
               Field current = Frontier.Dequeue();
               if (current == field) { Debug.Log("chuj"); break; }
               for (Direction direction = Direction.N; direction < Direction.NW; direction++)
               {
                   Field neighbor = current.Neighbors[(int)direction];
                   if (neighbor && !CameFrom.ContainsKey(neighbor))
                   {
                       Debug.Log(neighbor.X + " " + neighbor.Y);
                       if (neighbor == field) Debug.Log("Tu cie mam");
                       FromDirection[neighbor] = (int)direction;
                       Frontier.Enqueue(neighbor);
                       CameFrom[neighbor] = current;
                   }
               }
           }

           Field curr = field;
           while (curr != _currentField)
           {
               Path.Add(curr);
               curr = CameFrom[curr];
           }
           Path.Reverse();
       }

       Field FindRandomField()
       {
           Field randomField = _currentField;
           while (randomField == _currentField)
               randomField = _fieldsToVisit[Random.Range(0, _fieldsToVisit.Length)];
           return randomField;
       }*/
    #endregion
}

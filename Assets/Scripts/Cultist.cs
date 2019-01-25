using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Cultist : MonoBehaviour {

    public bool Blocked = true;
    public GameObject PigletPrefab;
    public float respawnTimer = 9f;
    public Sprite[] Markers;
    public int Killed = 0;
    public TextMeshProUGUI Counter;
    public bool IsDying = false;
    public bool IsKilling = false;
    public bool Died = false;
    public MusicManager Music;
    public int Number = 1;
    public Farmer Farmer;
	[SerializeField] private Sprite piglet;
	[SerializeField] private Sprite cultist;
	
	[SerializeField] private bool _isBeast = false;
	[SerializeField] private float vel;
	
	[SerializeField] public List<TextMeshProUGUI> Text = new List<TextMeshProUGUI>();
	
	[SerializeField] private AudioClip SatanLaugh;
    [SerializeField] private AudioClip kill;
	
	[SerializeField] private AudioSource source;
    [SerializeField] private AudioSource source2;

    public Animator IcoAnimator;
    public List<Animator> ButtonAnimators = new List<Animator>();
	private Animator animator;
	
	public static bool active = false;

	// left side of keyboard
	private string letters = "wasdqertfzxcvb";

	private KeyCode[] _keys = new KeyCode[5];
	private int _currKey;
	public Piglet _touchedPiglet;
	
	private bool sound_flag = false;

    public SpriteRenderer Marker;

    public AudioSource KnifeSource;

    public Rigidbody _rigidbody;
    
	public bool IsBeast
	{
		get { return _isBeast; }
		set
		{
			_isBeast = value;
            if (value)
            {
                gameObject.GetComponent<SpriteRenderer>().sprite = cultist;
                vel = 7;
                _currKey = 0;
                Music.SetMusic(true, Number);
                Text[0].text = "";
                Text[1].text = "";
                Text[2].text = "";
                Marker.sprite = Markers[1];
                IcoAnimator.SetBool("Satan", true);
            }
            else
            {
                vel = 5;
                gameObject.GetComponent<SpriteRenderer>().sprite = piglet;
                Music.SetMusic(false, Number);
                if (!IsDying) Marker.sprite = Markers[0];
                IcoAnimator.SetBool("Satan", false);
            }
		}
	}


    void Start() {
        Counter = GameObject.Find("Counter" + Number).GetComponent<TextMeshProUGUI>();
        Music = GameObject.Find("Main Camera").GetComponent<MusicManager>();
        Farmer = GameObject.Find("Farmer").GetComponent<Farmer>();
        KnifeSource = transform.GetChild(0).GetComponent<AudioSource>();
        Marker = transform.GetChild(1).GetComponent<SpriteRenderer>();
        
        source.clip = SatanLaugh;
        source2.clip = kill;

        _rigidbody = GetComponent<Rigidbody>();
        if (Number == 2)
        {
            Text.Add(GameObject.Find("QTE2").GetComponent<TextMeshProUGUI>());
            Text.Add(GameObject.Find("QTE2 (1)").GetComponent<TextMeshProUGUI>());
            Text.Add(GameObject.Find("QTE2 (2)").GetComponent<TextMeshProUGUI>());
            letters = "ghyuiopjklbnm";
            IcoAnimator = GameObject.Find("Ico2").GetComponent<Animator>();
            ButtonAnimators.Add(GameObject.Find("Key1_p2").GetComponent<Animator>());
            ButtonAnimators.Add(GameObject.Find("Key2_p2").GetComponent<Animator>());
            ButtonAnimators.Add(GameObject.Find("Key3_p2").GetComponent<Animator>());
        }
        else
        {
            Text.Add(GameObject.Find("QTE1").GetComponent<TextMeshProUGUI>());
            Text.Add(GameObject.Find("QTE1 (1)").GetComponent<TextMeshProUGUI>());
            Text.Add(GameObject.Find("QTE1 (2)").GetComponent<TextMeshProUGUI>());
            ButtonAnimators.Add(GameObject.Find("Key1_p1").GetComponent<Animator>());
            ButtonAnimators.Add(GameObject.Find("Key2_p1").GetComponent<Animator>());
            ButtonAnimators.Add(GameObject.Find("Key3_p1").GetComponent<Animator>());
        
            IcoAnimator = GameObject.Find("Ico1").GetComponent<Animator>();
        }

		animator = gameObject.GetComponent<Animator>();
	}
	
	void Update ()
	{
        if (!Blocked)
        {
            if (!Died)
            {
                if (!IsDying && !IsKilling)
                {
                    //trun on off satan mode
                    if (Number == 1)
                    {
                        if (Input.GetKeyDown(KeyCode.Space))
                        {
                            IsBeast = !IsBeast;
                            sound_flag = true;

                            if (IsBeast && sound_flag)
                            {
                                source.Stop();
                                source.PlayOneShot(source.clip);
                                sound_flag = false;
                            }
                        }
                    }
                    else
                    {
                        if (Input.GetKeyDown(KeyCode.Return))
                        {
                            
                            IsBeast = !IsBeast;

                            sound_flag = true;

                            if (IsBeast && sound_flag)
                            {
                                source.Stop();
                                source.PlayOneShot(source.clip);
                                sound_flag = false;
                            }
                        }
                    }


                    if (Number == 1)
                    {
                        int xDelta = 0, yDelta = 0;

                        if (Input.GetKey(KeyCode.W)) yDelta++;
                        if (Input.GetKey(KeyCode.S)) yDelta--;
                        if (Input.GetKey(KeyCode.D)) xDelta++;
                        if (Input.GetKey(KeyCode.A)) xDelta--;


                        AnimatorIdle();



                        if (yDelta == 1) animator.SetBool("Up", true);
                        else if (yDelta == -1) animator.SetBool("Down", true);

                        if (xDelta == 1)
                            animator.SetBool("Right", true);
                        else if (xDelta == -1) animator.SetBool("Left", true);

                        _rigidbody.velocity = new Vector3(xDelta, 0, yDelta).normalized * vel;

                        #region OldCOde
                        /*if (Input.GetKey(KeyCode.W))
                        {
                            _rigidbody.velocity = new Vector3(_rigidbody.velocity.x, 0, vel);
                            animator.SetBool("Up", true);
                        }

                        if (Input.GetKeyUp(KeyCode.W))
                        {
                            _rigidbody.velocity = new Vector3(_rigidbody.velocity.x, 0, 0);
                            animator.SetBool("Up", false);
                        }

                        if (Input.GetKey(KeyCode.S))
                        {
                            _rigidbody.velocity = new Vector3(_rigidbody.velocity.x, 0, -vel);
                            animator.SetBool("Down", true);
                        }

                        if (Input.GetKeyUp(KeyCode.S))
                        {
                            _rigidbody.velocity = new Vector3(_rigidbody.velocity.x, 0, 0);
                            animator.SetBool("Down", false);
                        }

                        if (Input.GetKey(KeyCode.D))
                        {
                            _rigidbody.velocity = new Vector3(vel, 0, _rigidbody.velocity.z);
                            animator.SetBool("Right", true);
                        }

                        if (Input.GetKeyUp(KeyCode.D))
                        {
                            _rigidbody.velocity = new Vector3(0, 0, _rigidbody.velocity.z);
                            animator.SetBool("Right", false);
                        }

                        if (Input.GetKey(KeyCode.A))
                        {
                            _rigidbody.velocity = new Vector3(-vel, 0, _rigidbody.velocity.z);
                            animator.SetBool("Left", true);
                        }

                        if (Input.GetKeyUp(KeyCode.A))
                        {
                            _rigidbody.velocity = new Vector3(0, 0, _rigidbody.velocity.z);
                            animator.SetBool("Left", false);
                        }*/
                        #endregion
                    }
                    else
                    {
                        int xDelta = 0, yDelta = 0;

                        if (Input.GetKey(KeyCode.UpArrow)) yDelta++;
                        if (Input.GetKey(KeyCode.DownArrow)) yDelta--;
                        if (Input.GetKey(KeyCode.RightArrow)) xDelta++;
                        if (Input.GetKey(KeyCode.LeftArrow)) xDelta--;
                        AnimatorIdle();
                        if (yDelta == 1) animator.SetBool("Up", true);
                        else if (yDelta == -1) animator.SetBool("Down", true);

                        if (xDelta == 1)
                            animator.SetBool("Right", true);
                        else if (xDelta == -1) animator.SetBool("Left", true);

                        _rigidbody.velocity = new Vector3(xDelta, 0, yDelta).normalized * vel;
                        #region OldCode
                        /*
                        if (Input.GetKey(KeyCode.UpArrow))
                        {
                            _rigidbody.velocity = new Vector3(_rigidbody.velocity.x, 0, vel);
                            animator.SetBool("Up", true);
                        }

                        if (Input.GetKeyUp(KeyCode.UpArrow))
                        {
                            _rigidbody.velocity = new Vector3(_rigidbody.velocity.x, 0, 0);
                            animator.SetBool("Up", false);
                        }

                        if (Input.GetKey(KeyCode.DownArrow))
                        {
                            _rigidbody.velocity = new Vector3(_rigidbody.velocity.x, 0, -vel);
                            animator.SetBool("Down", true);
                        }

                        if (Input.GetKeyUp(KeyCode.DownArrow))
                        {
                            _rigidbody.velocity = new Vector3(_rigidbody.velocity.x, 0, 0);
                            animator.SetBool("Down", false);
                        }

                        if (Input.GetKey(KeyCode.RightArrow))
                        {
                            _rigidbody.velocity = new Vector3(vel, 0, _rigidbody.velocity.z);
                            animator.SetBool("Right", true);
                        }

                        if (Input.GetKeyUp(KeyCode.RightArrow))
                        {
                            _rigidbody.velocity = new Vector3(0, 0, _rigidbody.velocity.z);
                            animator.SetBool("Right", false);
                        }

                        if (Input.GetKey(KeyCode.LeftArrow))
                        {
                            _rigidbody.velocity = new Vector3(-vel, 0, _rigidbody.velocity.z);
                            animator.SetBool("Left", true);
                        }

                        if (Input.GetKeyUp(KeyCode.LeftArrow))
                        {
                            _rigidbody.velocity = new Vector3(0, 0, _rigidbody.velocity.z);
                            animator.SetBool("Left", false);
                        }
                    }*/
                        #endregion
                    }

                }
            }
            else
            {
                respawnTimer -= Time.deltaTime;
                if (Number == 2) Text[2].text = Mathf.RoundToInt(respawnTimer).ToString();
                else Text[0].text = Mathf.RoundToInt(respawnTimer).ToString();
                if (respawnTimer <= 0)
                {
                    Respawn();
                }
            }
            if (IsBeast)
            {
                if (_currKey <= 2)
                {
                    if (Input.GetKeyDown(_keys[_currKey]))
                    {
                        if (_currKey == 2)
                        {
                            Instantiate(PigletPrefab, new Vector3(3.09f, _touchedPiglet.transform.position.y, 12.06f), _touchedPiglet.transform.rotation, GameObject.Find("Piglets").transform);
                            _touchedPiglet.gameObject.SetActive(false);

                            source2.PlayOneShot(source2.clip);
                            Killed++;
                            Counter.text = Killed.ToString();
                            AnimatorIdle();
                            ButtonAnimators[_currKey].SetBool("Active", false);
                            animator.SetBool("Kill", true);
                            StartCoroutine(Cooldown());
                            Text[0].text = "";
                            Text[1].text = "";
                            Text[2].text = "";
                            _currKey = 0;
                        }
                        else
                        {
                            ButtonAnimators[_currKey].SetBool("Active", false);
                            _currKey++;
                            ButtonAnimators[_currKey].SetBool("Active", true);
                        }
                    }
                }
            }
        }

      


    }
	

	private void OnCollisionEnter(Collision collision)
	{
		if (collision.gameObject.CompareTag("piglet"))
		{
            if (IsBeast && !IsKilling)
			{
                _touchedPiglet = collision.gameObject.GetComponent<Piglet>();
                _touchedPiglet.Rb.velocity = Vector3.zero;
                _touchedPiglet.Rb.drag = Mathf.Infinity;
                _rigidbody.drag = Mathf.Infinity;
                AnimatorIdle();
                purge();
			}
		}
	}

	void purge()
	{
        source2.PlayOneShot(source2.clip);

        Farmer.SetIsHunting(true, transform.position);
        IsKilling = true;
		for (int i = 0; i < 3;i++)
		{
			string key = Rand();
            
            _keys[i] = (KeyCode) System.Enum.Parse(typeof(KeyCode), key.ToUpper());
		}
        Text[0].text = _keys[0].ToString();
        Text[1].text = _keys[1].ToString();
        Text[2].text = _keys[2].ToString();
        ButtonAnimators[0].SetBool("Active", true);
    }

	string Rand()
	{
		return letters[Random.Range(0, letters.Length)].ToString();
	}




	IEnumerator Cooldown()
	{
        yield return new WaitForSeconds(0.5f);
        KnifeSource.Play();
        yield return new WaitForSeconds(1.5f);
		animator.SetBool("Kill", false);
        IsKilling = false;
        IsBeast = false;
        _rigidbody.drag = 0f;
        
        
    }

    public IEnumerator PlayDeathAnimation()
    {
        //SET DEATH BOOL
        foreach (Animator butt in ButtonAnimators)
            butt.SetBool("Active", false);
        yield return new WaitForSeconds(1f);
        IcoAnimator.SetBool("Die", true);
        //Farmer.CultistsCounter--;
        //if (Farmer.CultistsCounter == 0)
        //{
            //WinScreenData wns = GameObject.Find("Win Screen Data").GetComponent<WinScreenData>();
            //wns.FillData(Farmer.Cultists[0].Killed, Farmer.Cultists[1].Killed);
            
        //}
        Died = true;
        GetComponent<Collider>().enabled = false;
        GetComponent<SpriteRenderer>().enabled = false;
        Marker.sprite = null;
        if (Number == 2) Text[2].text = Mathf.RoundToInt(respawnTimer).ToString();
        else Text[0].text = Mathf.RoundToInt(respawnTimer).ToString();
    }


    void Respawn()
    {
        if (Number == 1) transform.position = new Vector3(-3.51f, transform.position.y, 1);
        else transform.position = new Vector3(1.06f, transform.position.y, -2.67f);
        Died = false;
        IsDying = false;
        IsKilling = false;
        AnimatorIdle();
        Marker.sprite = Markers[0];
        GetComponent<Collider>().enabled = true;
        GetComponent<SpriteRenderer>().enabled = true;
        respawnTimer = 9f;
        IcoAnimator.SetBool("Die", false);
        Text[0].text = "";
        Text[1].text = "";
        Text[2].text = "";
        _rigidbody.drag = 0;
        
    }

    void AnimatorIdle()
    {
        animator.SetBool("Left", false);
        animator.SetBool("Right", false);
        animator.SetBool("Up", false);
        animator.SetBool("Down", false);
    }

}
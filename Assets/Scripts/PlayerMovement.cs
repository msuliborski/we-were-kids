using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float vel;
    [SerializeField] private float rot;
    [SerializeField] private GameObject projectile;
    private GameObject inst;

    [SerializeField] public int id;

    private Rigidbody _rigidbody;

	private SortedDictionary<int, KeyCode> goKey = new SortedDictionary<int, KeyCode>();
	private SortedDictionary<int, KeyCode> leftKey = new SortedDictionary<int, KeyCode>();
	private SortedDictionary<int, KeyCode> rightKey = new SortedDictionary<int, KeyCode>();
	private SortedDictionary<int, KeyCode> actionKey = new SortedDictionary<int, KeyCode>();


    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
		inst = transform.GetChild(0).gameObject;

		goKey.Add(0, KeyCode.W);
		goKey.Add(1, KeyCode.I);
		leftKey.Add(0, KeyCode.A);
		leftKey.Add(1, KeyCode.J);
		rightKey.Add(0, KeyCode.D);
		rightKey.Add(1, KeyCode.L);
		actionKey.Add(0, KeyCode.Space);
		actionKey.Add(1, KeyCode.Period);
		Debug.Log(id);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(goKey[id]))
        {
            _rigidbody.velocity = transform.forward * vel;
        }

        if (Input.GetKeyUp(goKey[id]))
        {
            _rigidbody.velocity = Vector3.zero;
        }

        if (Input.GetKey(leftKey[id]))
        {
			Debug.Log("kurwa down " + id);
            transform.Rotate(new Vector3(0, -1, 0) * Time.deltaTime * rot, Space.World);
        }

        if (Input.GetKeyUp(leftKey[id])){
			Debug.Log("kurwa up " + id);
            _rigidbody.angularVelocity = Vector3.zero;
        }

        if (Input.GetKey(rightKey[id]))
        {
            transform.Rotate(new Vector3(0, 1, 0) * Time.deltaTime * rot, Space.World);
        }

        if (Input.GetKeyUp(rightKey[id]))
        {
            _rigidbody.angularVelocity = Vector3.zero;
        }
        if (Input.GetKeyDown(actionKey[id]))
        {
            Quaternion parent = transform.rotation;
            Instantiate(projectile, inst.transform.position, parent);
            //projectile.transform.rotation = inst.transform.rotation;
        }
    }
}
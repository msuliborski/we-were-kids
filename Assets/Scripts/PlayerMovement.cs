using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float vel;
    [SerializeField] private float rot;

    [SerializeField] public int id;

    private Rigidbody _rigidbody;



    List<KeyCode> integers = new List<KeyCode>()

	var wKey = new Dictionary<int, KeyCode>();

    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();

		wKey[0] = Input.GetKey(KeyCode.W);
		wKey[0] = Input.GetKey(KeyCode.W);
		wKey[0] = Input.GetKey(KeyCode.W);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.W))
        {
            
            _rigidbody.velocity = transform.forward * vel;
        }

        if (Input.GetKeyUp(KeyCode.W))
        {
            _rigidbody.velocity = Vector3.zero;
        }

        if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(new Vector3(0, -1, 0) * Time.deltaTime * rot, Space.World);
        }

        if (Input.GetKeyUp(KeyCode.A))
        {
            _rigidbody.angularVelocity = Vector3.zero;
        }

        if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(new Vector3(0, 1, 0) * Time.deltaTime * rot, Space.World);
        }

        if (Input.GetKeyUp(KeyCode.D))
        {
            _rigidbody.angularVelocity = Vector3.zero;
        }
    }
}
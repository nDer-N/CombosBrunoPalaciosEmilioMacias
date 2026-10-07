using TMPro;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class CubeController : MonoBehaviour
{
    private string[] _combos = {"8822qa", "8882qa", "888"};
    private string _input;
    private Rigidbody _rb;
    public float timer =5f;
    private float _currentTimer;
    private bool _isCombo = false;
    private int _comboKey;

    public AudioSource audioSource;
    public AudioClip combo1;
    public AudioClip combo2;
    public AudioClip combo3;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _input = "";
        _currentTimer = 0f;
        _comboKey = 10;
        _rb=GetComponent<Rigidbody>();
        audioSource = gameObject.GetComponent<AudioSource>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (_isCombo && _currentTimer>0)
        {
            _currentTimer-=Time.deltaTime;
        } else
        { 
          _currentTimer = timer; 
          _isCombo = false; 
          _input = "";
        }

        captureInput();
        CompareCombo(_input);
        //Debug.Log(_input);


        if(_currentTimer<=0 && _comboKey != 10)
        {
            Debug.Log(_comboKey);
            ComboPerform(_comboKey);
        }
       

    }

    void captureInput()
    {

        if (Input.GetKeyDown("up"))
        {
            _input += "8";
            _isCombo = true;
        } else if (Input.GetKeyDown("down")){
            _input +="2";
            _isCombo = true;
        } else if (Input.GetKeyDown("q"))
        {
            _input += "q";
            _isCombo = true;
        } else if (Input.GetKeyDown("a"))
        {
            _input += "a";
            _isCombo = true;
        }
    }

    void CompareCombo(string combo)
    {
        if (_isCombo)
        {
            for (int i = 0; i<_combos.Length; i++)
            {
                if (_combos[i].Equals(combo))
                {
                    _comboKey = i;
                }
            }
        }
    }

    void ComboPerform(int key)
    {
        Vector3 torque = Vector3.zero;
        Vector3 jump = Vector3.zero;
        switch (key)
        {
            case 0:
                torque.x = 100f;
                jump.z = 100f;
                jump.y = 300f;
                _rb.AddTorque(torque);
                _rb.AddForce(jump);
                audioSource.PlayOneShot(combo2, 1);
                break;
            case 1:
                torque.y = 100f;
                jump.y = 300f;
                _rb.AddTorque(torque);
                _rb.AddForce(jump);
                audioSource.PlayOneShot(combo1, 1);
                break;
            case 2:
                torque.x = -100f;
                jump.z = -100f;
                jump.y = 300f;
                _rb.AddTorque(torque);
                _rb.AddForce(jump);
                audioSource.PlayOneShot(combo3, 1);
                break;
        }
    }

}

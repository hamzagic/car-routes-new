using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Car : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private int _turnSpeed;

    [SerializeField] private int _lives = 3;

    [SerializeField] private TMP_Text _livesText;
    private Vector3 initialPosition = new Vector3(-16f, 6.52f, -26f);

    private int _steerValue;

    private bool _isMoving = false;

    private float _speedAccelerationOverTime = 0.05f;

    private int _laps = 0;

    private ScoreSystem _scoreSystem;

    // let "Paused" message blinking on the screen if user presses pause after game starts
    // add gifts on the track that can restore lives or get temporary immunity

    private void Start() 
    {
        GameObject obj = GameObject.Find("Score");
        _scoreSystem = obj.GetComponent<ScoreSystem>();
        _livesText.text = $"Lives: {_lives}";
    }

    void Update()
    {
        Movement();
    }

    private void Movement()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _isMoving = !_isMoving;
        }
        if (_isMoving)
        {
            _speed += _speedAccelerationOverTime * Time.deltaTime;
            _isMoving = true;
            transform.Rotate(0f, _steerValue * _turnSpeed * Time.deltaTime, 0f);
            transform.Translate(Vector3.forward * _speed * Time.deltaTime);
            TurningWheel();
        }
    }

    private void TurningWheel()
    {
        if (Input.GetAxis("Horizontal") > 0)
        {
            _steerValue = 1;
        }
        else if(Input.GetAxis("Horizontal") < 0)
        {
            _steerValue = -1;
        }
        else if(!Input.GetButton("Horizontal"))
        {
            _steerValue = 0;
        }
    }

    private void OnTriggerEnter(Collider other) 
    {
        if (other.gameObject.tag == "Obstacle" || other.gameObject.tag == "Enemy")
        {
            _lives--;
            _livesText.text = $"Lives: {_lives}";
            if (_lives > 0)
            {
                _isMoving = false;
                transform.position = initialPosition;
                transform.eulerAngles = new Vector3(0,0,0);
            }
            else
            {
                _scoreSystem.ResetScore();
                SceneManager.LoadScene(0);
            }
        }

        // if (other.gameObject.tag == "Enemy")
        // {
        //     _isMoving = false;
        //     transform.position = initialPosition;
        //     transform.eulerAngles = new Vector3(0,0,0);
        // }

        if (other.gameObject.tag == "Gift")
        {
            GetGift();
            Destroy(other.gameObject);
        }

        if (other.gameObject.tag == "Point")
        {
            _scoreSystem.AddBonusPoints();
            Destroy(other.gameObject);
        }
    }

    private void GetGift()
    {
        if (_lives < 3 && _lives > 0)
        {
            _lives++;
        }
    }

    private void AddLap()
    {
        _laps++;
        if (_laps == 3)
        {
            // todo: go to next phase
        }
    }

    public bool GetIsMoving()
    {
        return _isMoving;
    }
}

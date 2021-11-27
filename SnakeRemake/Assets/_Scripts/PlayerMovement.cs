using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    private bool movingLeft, movingRight, movingUp, movingDown;

    [SerializeField] private Transform snakeBodyPrefab;
    [SerializeField] private List<Transform> snakeBodySegment;

    // Start is called before the first frame update
    void Start()
    {
        snakeBodySegment = new List<Transform>();
        snakeBodySegment.Add(transform);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        for (int i = snakeBodySegment.Count -1; i > 0; i--)
        {
            snakeBodySegment[i].position = (snakeBodySegment[i - 1].position);
        }

        print("working");
        transform.Translate(Vector3.forward - new Vector3(0,0,0.5f));
    }

    private void Update()
    {
        PlayerRotation();
    }


    private void PlayerRotation()
    {
        if(Time.timeScale > 0)
        {
            if (Input.GetKeyDown(KeyCode.LeftArrow) && !movingRight)
            {
                transform.rotation = Quaternion.LookRotation(Vector3.left);

                movingDown = false;
                movingLeft = true;
                movingRight = false;
                movingUp = false;
            }
            if (Input.GetKeyDown(KeyCode.RightArrow) && !movingLeft)
            {
                transform.rotation = Quaternion.LookRotation(Vector3.right);

                movingDown = false;
                movingLeft = false;
                movingRight = true;
                movingUp = false;
            }
            if (Input.GetKeyDown(KeyCode.DownArrow) && !movingUp)
            {
                transform.rotation = Quaternion.LookRotation(Vector3.back);

                movingDown = true;
                movingLeft = false;
                movingRight = false;
                movingUp = false;
            }
            if (Input.GetKeyDown(KeyCode.UpArrow) && !movingDown)
            {
                transform.rotation = Quaternion.LookRotation(Vector3.forward);

                movingDown = false;
                movingLeft = false;
                movingRight = false;
                movingUp = true;
            }
        }
        
    }



    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Fruit") || other.CompareTag("SpecialFruit"))
        {
            Grow();
        }
    }

    private void Grow()
    {
        Transform _segment = Instantiate(snakeBodyPrefab);
        _segment.position = (snakeBodySegment[snakeBodySegment.Count - 1].position);

        snakeBodySegment.Add(_segment);
    }

}

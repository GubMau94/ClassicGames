using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeController : MonoBehaviour
{
    [SerializeField] private float speed;
    bool movingLeft, movingRight, movingUp, movingDown;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);

        RotationManager();

    }

    /// <summary>
    /// Gestisce la rotazione del serpente
    /// </summary>
    private void RotationManager()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow) && !movingLeft)
        {
            transform.rotation = Quaternion.LookRotation(Vector3.right);
            movingDown = false;
            movingUp = false;
            movingLeft = false;
            movingRight = true;
        } 
        if (Input.GetKeyDown(KeyCode.LeftArrow) && !movingRight)
        {
            transform.rotation = Quaternion.LookRotation(Vector3.left);
            movingDown = false;
            movingUp = false;
            movingLeft = true;
            movingRight = false;
        }
        if (Input.GetKeyDown(KeyCode.UpArrow) && !movingDown)
        {
            transform.rotation = Quaternion.LookRotation(Vector3.forward);
            movingDown = false;
            movingUp = true;
            movingLeft = false;
            movingRight = false;
        }
        if (Input.GetKeyDown(KeyCode.DownArrow) && !movingUp)
        {
            transform.rotation = Quaternion.LookRotation(Vector3.back);
            movingDown = true;
            movingUp = false;
            movingLeft = false;
            movingRight = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        print("cazzooooo");

        if (other.CompareTag("Wall"))
        {
            print("sto sbattendoooo");
            speed = 0;
        }
    }
}

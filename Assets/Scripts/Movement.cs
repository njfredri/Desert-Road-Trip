using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    // Start is called before the first frame update
    public static Vector3 uMovement;
    public static float uEulerDirection;
    public float speedupAcc;
    public float brakeSlowDown;
    public float coastSlowDown;
    public float uVelocity; //velocity since it can be negative
    public float maxVelocity = 100;
    public float minVelocity = 0;
    
    public PlayerLook driver;


    void Start()
    {
        uMovement = new Vector3(0, 0, 0);
        uEulerDirection = 0;
    }

    // Update is called once per frame
    void Update()
    {
        

        if (driver == null) return;
        float acceleration = 0;
        if (driver.useKeyboard)
        { 
            if(Keyboard.current.upArrowKey.isPressed) { acceleration = speedupAcc; }
            else if (Keyboard.current.downArrowKey.isPressed) { acceleration = brakeSlowDown; }
            else { acceleration = coastSlowDown; }
        }  
        else
        {
            float steer = driver.gamepad.leftStick.y.ReadValue();
            acceleration = steer >= 0 ? speedupAcc: brakeSlowDown;
        }

        // Decide which sound to play
        if (AudioManager.instance != null)
        {
            if (acceleration == speedupAcc)
            {
                AudioManager.instance.PlaySound("Engine");
                AudioManager.instance.StopSound("Coast");
            }
            else if (acceleration == coastSlowDown)
            {
                AudioManager.instance.StopSound("Engine");
                if (uMovement.magnitude > 1) AudioManager.instance.PlaySound("Coast");

            }
            else if (acceleration == brakeSlowDown)
            {
                AudioManager.instance.StopSound("Engine");
                AudioManager.instance.StopSound("Coast");
                //AudioManager.instance.PlaySound("Brake");
            }
            if (uMovement.magnitude < 1) AudioManager.instance.StopSound("Coast");
        }

        //do the speen
        if (SteerCar.isStuck)
        {
            acceleration = -50;
            //Debug.Log("pls stop");
            uEulerDirection += 15;
            //add sound here for spinning
        }

        uVelocity += acceleration*Time.deltaTime;
        if (uVelocity > maxVelocity) { uVelocity = maxVelocity; }
        if (uVelocity < minVelocity) { uVelocity = minVelocity; }

        //hard stop the car if offroad
        

        uMovement = new Vector3(uVelocity * Mathf.Sin(Mathf.Deg2Rad * uEulerDirection), 0, Mathf.Abs(uVelocity * Mathf.Cos(Mathf.Deg2Rad * uEulerDirection)));

        steer();
        messUpSteering();
        //Debug.Log("dir: " + uEulerDirection + " sin: " + Mathf.Sin(Mathf.Deg2Rad*uEulerDirection) + " cos" + Mathf.Cos(Mathf.Deg2Rad*uEulerDirection));
    }

   

    void steer()
    {
        if (driver.useKeyboard)
        {
            if (Keyboard.current.leftArrowKey.isPressed) { uEulerDirection -= 10.0f * Time.deltaTime; }
            if (Keyboard.current.rightArrowKey.isPressed) { uEulerDirection += 10.0f * Time.deltaTime; }
        }
        else
        {
            uEulerDirection+= driver.gamepad.leftStick.x.ReadValue() * 10.0f * Time.deltaTime;
        }
    }

    void messUpSteering()
    {
        //normal offset for driving on road
        if(Random.Range(0,100) < 10) uEulerDirection += Random.Range(-3.0f, 3.0f)*Time.deltaTime;
        if(PlayerLook.timeSinceLastKick < 1.0f) { uEulerDirection += Random.Range(-100.0f, 100.0f) * Time.deltaTime; }
    }
}

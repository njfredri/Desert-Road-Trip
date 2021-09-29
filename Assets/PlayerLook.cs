using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerLook : MonoBehaviour
{
    public static float timeSinceLastKick = 0;
    static int padsInUse = 0;
    public bool useKeyboard;
    public int id = 1;

    private int gamepadIndex = -1;
    public Gamepad gamepad;
    public float lookSpeed;
    private Vector3 lookAngles = Vector3.zero;
    public static PlayerLook kicker;

    // Start is called before the first frame update
    void Start()
    {
        //gamepad = Gamepad.current;
    }

    // Update is called once per frame
    void Update()
    {

        timeSinceLastKick += Time.deltaTime;
        if (!useKeyboard)
        {
            // Look for an available gamepad
            if(gamepadIndex == -1)
            {
                if (Gamepad.all.Count > padsInUse)
                {
                    gamepadIndex = padsInUse;
                    padsInUse+= 1;
                    gamepad = Gamepad.all[gamepadIndex];
                }
            }


            if (gamepad != null)
            {

                Vector2 lookInput = gamepad.rightStick.ReadValue();

                lookAngles.x = (lookAngles.x - lookInput.y * lookSpeed * Time.deltaTime) % 360;
                lookAngles.y = (lookAngles.y + lookInput.x * lookSpeed * Time.deltaTime) % 360;

                transform.localEulerAngles = lookAngles;

                
            }
        }
        else
        {
            
            Vector2 lookInput = new Vector2((Keyboard.current.aKey.isPressed ? -1 : 0) + (Keyboard.current.dKey.isPressed ? 1 : 0),
                                            (Keyboard.current.wKey.isPressed ? 1 : 0) + (Keyboard.current.sKey.isPressed ? -1 : 0));
            lookInput.Normalize();

            lookAngles.x = (lookAngles.x - lookInput.y * lookSpeed * Time.deltaTime) % 360;
            lookAngles.y = (lookAngles.y + lookInput.x * lookSpeed * Time.deltaTime) % 360;

            transform.localEulerAngles = lookAngles;
            if (kicker.useKeyboard)
            {
                if (Keyboard.current.yKey.wasPressedThisFrame)//gamepad.aButton.wasPressedThisFrame && this.id == 3) 
                {
                    //Debug.Log("this kick");
                    timeSinceLastKick = 0;
                    AudioManager.instance.ForcePlay("vine-boom");

                    //AudioManager.instance.PlaySound
                }
            }
            else if (kicker.gamepad != null)
            {
                if (kicker.gamepad.aButton.wasPressedThisFrame)//gamepad.aButton.wasPressedThisFrame && this.id == 3) 
                {
                    //Debug.Log("this kick");
                    timeSinceLastKick = 0;
                    AudioManager.instance.ForcePlay("vine-boom");

                    //AudioManager.instance.PlaySound
                }
            }
        }
    }
}

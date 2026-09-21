using UnityEngine;

public class Flashlight : MonoBehaviour
{
    public Light flashlight;
    public Light floodlight;
    public AudioSource SwitchOn;


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            flashlight.enabled = !flashlight.enabled;
            floodlight.enabled = !floodlight.enabled;
            SwitchOn.Play();
        }
    }
}
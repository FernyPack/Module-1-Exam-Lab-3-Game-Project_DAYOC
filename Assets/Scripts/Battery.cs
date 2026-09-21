using UnityEngine;
using TMPro;

public class Battery : MonoBehaviour
{
    public float battery = 100f;

    public float walkDrain = 0.5f;
    public float sprintDrain = 2f;
    public float flashlightDrain = 1.5f;

    public TMP_Text batteryText;
    public Light flashlight;

    private bool batteryDepleted = false;

    void Update()
    {
        if (batteryDepleted)
            return;

        bool moving = Input.GetAxisRaw("Horizontal") != 0 ||
                      Input.GetAxisRaw("Vertical") != 0;

        bool sprinting = Input.GetKey(KeyCode.LeftShift) && moving;

        float drain = 0f;

        if (moving)
            drain += walkDrain;

        if (sprinting)
            drain += sprintDrain;

        if (flashlight != null && flashlight.enabled)
            drain += flashlightDrain;

        battery -= drain * Time.deltaTime;
        battery = Mathf.Clamp(battery, 0f, 100f);

        if (battery <= 5f && flashlight != null && flashlight.enabled)
        {
            flashlight.enabled = false;
        }

        if (battery <= 0f)
        {
            battery = 0f;
            batteryDepleted = true;

            GameManager.Instance.GameOver();
        }

        if (batteryText != null)
        {
            batteryText.text = "Battery: " + Mathf.RoundToInt(battery) + "%";
        }
    }
}
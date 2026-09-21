using UnityEngine;

public class FlickerLight : MonoBehaviour
{
    public Light pointLight;

    public float minIntensity = 0.2f;
    public float maxIntensity = 1.2f;
    public float flickerSpeed = 0.08f;

    private float timer;

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= flickerSpeed)
        {
            timer = 0f;
            pointLight.intensity = Random.Range(minIntensity, maxIntensity);
        }
    }
}
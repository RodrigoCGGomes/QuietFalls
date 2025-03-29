using UnityEngine;

public class SimpleSunRotate : MonoBehaviour
{
    [SerializeField] private float speed = 10f;
    public float DayNightRatio;

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.right * speed * Time.deltaTime);

        // Assuming sun's forward points from sun to ground at noon.
        DayNightRatio = Mathf.Clamp01(Vector3.Dot(transform.forward, Vector3.down) * 0.5f + 0.5f);
    }
}

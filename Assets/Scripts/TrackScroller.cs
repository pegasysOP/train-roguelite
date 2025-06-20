using UnityEngine;

public class TrackScroller : MonoBehaviour
{
    public float speed;

    private void Update()
    {
        transform.position -= new Vector3(speed * Time.deltaTime, 0, 0);

        // reset
        if (transform.position.x < -10)
            transform.position += new Vector3(10, 0, 0);
    }
}
 
using UnityEngine;
using System.Collections;

public class HorizontalRailsMovement : MonoBehaviour {

    public Vector3[] waypoints;
    public float minimumDistLeft = 0.1f;
    public float speed = 1.0f;
    public GameObject player;

    private bool direction = true;
    private int position = 0;
    private int nextPos = 1;
    private float timer = 0.0f;
	// Use this for initialization
	void Start ()
    {
	    
	}
	
	// Update is called once per frame
	void Update ()
    {
        timer += Time.deltaTime * speed;
        var dist = Vector3.Distance(this.gameObject.transform.position, waypoints[nextPos]);

        if (dist <= minimumDistLeft)
        {
            timer = 0.0f;
            if (nextPos == waypoints.Length - 1 || nextPos == 0)
                direction = !direction;

            var tempnextPosition = nextPos;
            nextPos += (direction ? 1 : -1);
            position = tempnextPosition;
        }

        var pos = Vector3.Lerp(waypoints[position], waypoints[nextPos], timer);
        this.gameObject.transform.position = pos;
        this.gameObject.transform.LookAt(player.transform);
	}
}

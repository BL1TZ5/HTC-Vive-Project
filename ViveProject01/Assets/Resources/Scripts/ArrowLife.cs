using UnityEngine;
using System.Collections;

public class ArrowLife : MonoBehaviour {

    public float lifeTime = 120.0f;
    private float timer;
	// Use this for initialization
	void Start ()
    {
	
	}
	
	// Update is called once per frame
	void Update ()
    {
        timer += Time.deltaTime;
        if (this.transform.position.y <= -2.0f || timer >= lifeTime)
        {
            timer = 0.0f;
            Destroy(this);
        }
	}
}

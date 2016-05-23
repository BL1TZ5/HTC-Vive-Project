using UnityEngine;
using System.Collections;

public class ArcheryTarget : MonoBehaviour {

    public ScoreScript scoreScript;
    public Transform midPoint;
    public float radius;
    public float maximumScore;

    private Collider collider;

	// Use this for initialization
	void Start ()
    {
        collider = GetComponent<Collider>();
	}
	
	// Update is called once per frame
	void Update ()
    {
	
	}

    void OnCollisionEnter(Collision collision)
    {
        var center = collider.bounds.center;
        var hit = collision.contacts[0].point;

        float dist = Vector3.Distance(center, hit);
        var factor = 1 - dist / radius;

        if (factor < 0.0f)
            factor = 0.0f;
        if (factor > 1.0f)
            factor = 1.0f;
        
        float score = (float)System.Math.Round(factor, 1);
        if (score <= 0.1f)
            score = 0.1f;

        score *= maximumScore;
        scoreScript.Score += score;

        // Attach arrow to target
        collision.collider.gameObject.transform.SetParent(this.transform);
        collision.collider.attachedRigidbody.useGravity = false;
        collision.collider.attachedRigidbody.isKinematic = true;
    }
}

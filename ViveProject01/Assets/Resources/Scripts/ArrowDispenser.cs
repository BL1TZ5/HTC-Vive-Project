using UnityEngine;
using System.Collections;

public class ArrowDispenser : MonoBehaviour {

    public GameObject arrowPrefab;
    public float scaleY = 1.0f;
    public float arrowRespawnTime = 5.0f;

    private float timer = 0.0f;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update ()
    {
        timer += Time.deltaTime;

        if(timer >= arrowRespawnTime)
        {
            timer = 0.0f;
            GameObject arrow = (GameObject)GameObject.Instantiate(arrowPrefab, this.transform.position, this.transform.rotation);
            var scale = arrow.transform.localScale;
            scale.y *= scaleY;
            arrow.transform.localScale = scale;
        }
	}
}

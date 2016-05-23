using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SteamVRBorderSpeedup : MonoBehaviour {

    public float speedMultiplier = 2.0f;
    public float width = 1.0f;
    public float height = 1.0f;
    public GameObject rig;
    public Vector3 currentCenter;
    public Vector3 prevPos;
    public GameObject area;

    public GameObject canvas;
    public Text txtCurrentPosition;

    public bool enableJumping = false; // You sadist!
    public float jumpModifier = 5.0f;
    public float playerHeight = 1.6f;
	// Use this for initialization
	void Start ()
    {
        currentCenter = this.gameObject.transform.position;
        currentCenter.y = 0.0f;
        var scale = area.transform.localScale;
        scale.x *= width;
        scale.y *= height;
        area.transform.localScale = scale;
        prevPos = this.gameObject.transform.position;
        canvas.transform.parent = this.gameObject.transform;
	}
	
	// Update is called once per frame
	void Update ()
    {
        var rigPos = rig.transform.position;
        var areaPos = area.transform.position;
        if (Mathf.Abs(prevPos.x - currentCenter.x) > height / 2 || Mathf.Abs(prevPos.z - currentCenter.z) > width / 2)
        {
            if (this.gameObject.transform.localPosition.x - currentCenter.x > height / 2)
            {
                rigPos.x = this.gameObject.transform.localPosition.x - height / 2;
                areaPos.x = this.gameObject.transform.position.x - height / 2;
                currentCenter.x = rigPos.x;
            }
            else if (this.gameObject.transform.localPosition.x - currentCenter.x < -height / 2)
            {
                rigPos.x = this.gameObject.transform.localPosition.x + height / 2;
                areaPos.x = this.gameObject.transform.position.x + height / 2;
                currentCenter.x = rigPos.x;
            }

            if (this.gameObject.transform.localPosition.z - currentCenter.z > width / 2)
            {
                rigPos.z = this.gameObject.transform.localPosition.z - width / 2;
                areaPos.z = this.gameObject.transform.position.z - width / 2;
                currentCenter.z = rigPos.z;
            }
            else if (this.gameObject.transform.localPosition.z - currentCenter.z < -width / 2)
            {
                rigPos.z = this.gameObject.transform.localPosition.z + width / 2;
                areaPos.z = this.gameObject.transform.position.z + width / 2;
                currentCenter.z = rigPos.z;
            }
        }

        // Activate this if you hate the person who's testing your stuff
        if (enableJumping)
        {
            if (this.gameObject.transform.localPosition.y > playerHeight)
            {
                rigPos.y = (this.gameObject.transform.localPosition.y - playerHeight) * jumpModifier;
            }
            else if (rigPos.y > 0)
                rigPos.y = 0;
        }

        prevPos = this.gameObject.transform.localPosition;
        txtCurrentPosition.text = rigPos.ToString();

        rig.transform.position = rigPos;
        area.transform.position = areaPos;
    }
}

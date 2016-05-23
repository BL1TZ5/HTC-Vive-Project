using UnityEngine;
using System.Collections;

public class BowstringScript : MonoBehaviour {

    public bool resettingString = false;
    public bool arrowAttached = false;
    public bool isHold = false;
    public bool isStringReleased = false;

    public float maximumForce = 100.0f;
    public float maximumStretchDistance = 0.005f;
    public GameObject stringBone;
    public NewtonVR.NVRInteractableItem interactableString;
    public NewtonVR.NVRHand hand;
    public GameObject currentArrow;
    public float timeForReset = 0.5f;

    private Vector3 stringOrigin;
    private Collider stringCollider;
    private float resetTimer = 0.0f;

    // Use this for initialization
    void Start ()
    {
        stringOrigin = stringBone.transform.localPosition;
        stringCollider = stringBone.GetComponent<Collider>();
        interactableString = stringBone.GetComponent<NewtonVR.NVRInteractableItem>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (isHold && hand.HoldButtonUp && currentArrow != null)
        {
            resettingString = true;
            arrowAttached = false;

            var arrowCollider = currentArrow.GetComponent<Collider>();
            currentArrow.transform.SetParent(null);
            arrowCollider.enabled = true;
            arrowCollider.attachedRigidbody.isKinematic = false;
            arrowCollider.attachedRigidbody.useGravity = true;

            float dist = stringBone.transform.localPosition.z - stringOrigin.z;
            arrowCollider.attachedRigidbody.AddRelativeForce(new Vector3(0, (dist / maximumStretchDistance) * maximumForce), 0);
            
            currentArrow = null;
        }

        isHold = interactableString.IsAttached;

        if (stringBone.transform.localPosition.z < stringOrigin.z)
            stringBone.transform.localPosition = stringOrigin;

        if (currentArrow == null && resettingString && resetTimer <= timeForReset)
        {
            resetTimer += Time.deltaTime;
            stringBone.transform.localPosition = Vector3.Lerp(stringBone.transform.localPosition, stringOrigin, resetTimer / timeForReset);
        }
        else if(resettingString)
        {
            resettingString = false;
            resetTimer = 0.0f;
            stringBone.transform.localPosition = stringOrigin;
        }
        
	    if(stringBone.transform.localPosition.x != 0 
            || stringBone.transform.localPosition.y != 0 
            || stringBone.transform.localRotation.eulerAngles.x != 0.0f 
            || stringBone.transform.localRotation.eulerAngles.y != 0.0f 
            || stringBone.transform.localRotation.eulerAngles.z != 0.0f)
        {
            var pos = stringBone.transform.localPosition;
            pos.x = pos.y = 0.0f;
            stringBone.transform.localPosition = pos;
            stringBone.transform.localRotation = Quaternion.Euler(0, 0, 0);
        }
	}

    void OnCollisionEnter(Collision coll)
    {
        var other = coll.gameObject;
        if (other.gameObject.CompareTag("arrow") && resetTimer == 0.0f)
        {
            if (currentArrow != null)
                Destroy(currentArrow);

            currentArrow = other;

            other.transform.SetParent(stringBone.transform);
            other.transform.localPosition = Vector3.zero;
            other.transform.localRotation = Quaternion.Euler(-90, 0, 0);

            var interactable = currentArrow.GetComponent<NewtonVR.NVRInteractableItem>();
            interactable.EndInteraction();
            interactable.enabled = false;

            coll.collider.attachedRigidbody.useGravity = false;
            coll.collider.attachedRigidbody.isKinematic = true;
            coll.collider.enabled = false;
            arrowAttached = true;
        }
    }
}

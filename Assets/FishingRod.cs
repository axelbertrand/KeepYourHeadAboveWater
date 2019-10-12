using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishingRod : MonoBehaviour
{
    [Header("Own Properties")]
    [SerializeField]
    private float launchImpulse = 50f;

    [SerializeField]
    private float rodMaxLength = 50f;

    [Header("Own Game Objects")]
    [SerializeField]
    private GameObject fishingDirection;
    [SerializeField]
    private GameObject throwGameObject;

    [Space]

    [Header("Extern Objects")]
    [SerializeField]
    private GameObject hookPrefab;


    private bool isLookingForThrow = false;
    private Vector3 throwDirection = Vector3.zero;

    private HookBehavior hookBehavior;

    private bool IsLookingForThrow
    {
        get
        {
            return isLookingForThrow;
        }

        set
        {
            fishingDirection.SetActive(value);
            isLookingForThrow = value;
        }
    }

    private void Awake()
    {
    }

    // Start is called before the first frame update
    void Start()
    {
        IsLookingForThrow = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("FishingRod") && !isLookingForThrow && hookBehavior == null)
        {
            // Player looking to throw the rod
            IsLookingForThrow = true;

        }else if (Input.GetButtonDown("FishingRod") && !isLookingForThrow && hookBehavior != null)
        {

            if (hookBehavior.IsHookAnchored)
            {
                // Player looking to go to hook
                SpringJoint2D springJoint = gameObject.AddComponent<SpringJoint2D>();
                springJoint.connectedAnchor = hookBehavior.transform.position;

                springJoint.autoConfigureDistance = false;
                springJoint.distance = 2f;
            }
            else
            {
                hookBehavior.GoBackToPlayer();
            }

            
        }
        else if (Input.GetButtonDown("FishingRod") && isLookingForThrow)
        {
            // Throw the fishing rod in the choosen direction

            GameObject hookGameObject = Instantiate(hookPrefab, throwGameObject.transform.position, new Quaternion());
            hookBehavior = hookGameObject.GetComponent<HookBehavior>();
            hookBehavior.LaunchHook(throwDirection, launchImpulse, this, rodMaxLength);

            IsLookingForThrow = false;
        }
        else if (isLookingForThrow)
        {
            Vector3 wantedDirection = new Vector3(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"), 0);

            if(wantedDirection.magnitude > 0.35f)
            {
                throwDirection = wantedDirection;
            }

            wantedDirection = SetVector8Directions(wantedDirection);

            float angle = Mathf.Atan2(throwDirection.y, throwDirection.x) * Mathf.Rad2Deg;
            Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);

            fishingDirection.transform.rotation = q;
        }
    }


    public void HookDestroyed()
    {

        SpringJoint2D springJoint2D = GetComponent<SpringJoint2D>();
        if(springJoint2D != null)
        {
            Destroy(springJoint2D);
        }

        hookBehavior = null;
    }


    /**
     * Transform the Vector to be in the 8 directions
    **/
    private Vector3 SetVector8Directions(Vector3 value)
    {
        return value;
    }
}

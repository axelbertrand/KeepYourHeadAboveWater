using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpringJointRenderer : MonoBehaviour {

    public float startWidth = 0.05f;
    public float endWidth = 0.05f;
    public Material aMaterial;


    // these are set in start
    private LineRenderer line;

    public Transform parent;
    public Transform pointOfAttached;

    void Start()
    {
        line = this.gameObject.GetComponent<LineRenderer>();
        line.startWidth = startWidth;
        line.endWidth = endWidth;

        line.positionCount = 2;

        line.material = aMaterial;

        line.GetComponent<Renderer>().enabled = true;
    }

    void Update()
    {
        line.SetPosition(0, pointOfAttached.position);
        line.SetPosition(1, parent.position);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class waterBehavior : MonoBehaviour
{
    public float speed = 1.25f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(new Vector3(0f, Time.deltaTime * speed));
    }
}

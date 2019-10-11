using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {
    public Transform target;
    private Camera cam;
    public float speed = 0.01f;
    public Vector2 size;
    public Vector3 offset;

    void Start() {
        cam = GetComponent<Camera>();
    }
    
    void FixedUpdate() {
        Rect bounds = new Rect(-size*.5f, size);
        Vector2 pos = cam.WorldToScreenPoint(target.position);
        pos.x = pos.x/Screen.width*2-1;
        pos.y = pos.y/Screen.height*2-1;
        if(!bounds.Contains(pos)) {
            transform.position = Vector3.Lerp(transform.position, target.position-offset+Vector3.back*10, speed);
        }
    }
}

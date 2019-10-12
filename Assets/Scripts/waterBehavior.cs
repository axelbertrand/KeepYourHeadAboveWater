using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class waterBehavior : MonoBehaviour
{
    
    private int speed_ = 3;

    [SerializeField]
    public int deltaWater_ = 0;
    private int currentPos_;
    // Start is called before the first frame update
    void Start()
    {
        currentPos_ = 0;
    }

    // Update is called once per frame
    void Update()
    {
        ChangeLevel();
    }

    void ChangeLevel()
    {
        if(deltaWater_ != currentPos_)
        {
            int direction = (deltaWater_ - currentPos_ > 0) ? 1 : -1; 
            transform.Translate(new Vector3(0,direction * speed_ * Time.deltaTime,0));
            currentPos_+= direction;
        }
    }

    public void setDelta(int delta)
    {
        deltaWater_ += delta;
    }
}

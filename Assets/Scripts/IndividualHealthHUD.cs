using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IndividualHealthHUD : MonoBehaviour
{

    public CharacterLife characterLife;
    public GameObject deadObject;

    private bool isDead = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        float lifePerc = 0;

        if(characterLife != null)
        {
            lifePerc = Mathf.Clamp((float)characterLife.life_ / CharacterLife.MAX_LIFE, 0f, CharacterLife.MAX_LIFE);  
        }else if (!isDead)
        {
            isDead = true;
            deadObject.SetActive(true);
        }

        Transform mask = transform.Find("Mask");
        ((RectTransform)mask).anchoredPosition = new Vector3(0, -(1 - lifePerc) * 130, 0);
        ((RectTransform)mask.Find("Circle")).anchoredPosition = new Vector3(0, (1 - lifePerc) * 130, 0);
    }


}

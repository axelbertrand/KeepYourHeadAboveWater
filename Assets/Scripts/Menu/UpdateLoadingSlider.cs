using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpdateLoadingSlider : MonoBehaviour
{

    public Slider slider;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        slider.value = Mathf.Lerp(slider.value, SceneLoader.Instance.LoadingProgress / 100, 10 * Time.deltaTime);
    }
}

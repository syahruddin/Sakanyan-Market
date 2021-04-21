using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BarSabar : MonoBehaviour
{
    public Slider Slider;
    public Text text;
    public Color low,high;
    public Vector3 offset;
    // Start is called before the first frame update
    public void set(float max, float kesabaran)
    {
      Slider.value = kesabaran;
      Slider.maxValue = max;
      Slider.fillRect.GetComponentInChildren<Image>().color = Color.Lerp(low,high,Slider.normalizedValue);
    }

    // Update is called once per frame
    void Update()
    {
      Slider.transform.position = Camera.main.WorldToScreenPoint(transform.parent.position + offset);
      text.transform.position = Camera.main.WorldToScreenPoint(transform.parent.position + offset + new Vector3(0,.3f,0));
    }
}

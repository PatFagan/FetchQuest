using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterCustomization : MonoBehaviour
{
    public GameObject characterModel;
    public Slider redSlider, greenSlider, blueSlider;

    Color newColor;

    // Update is called once per frame
    void Update()
    {
        newColor = new Color(redSlider.value, greenSlider.value, 
        blueSlider.value, 0f);
        SkinnedMeshRenderer renderer = characterModel.GetComponent<SkinnedMeshRenderer>();
        renderer.materials[1].color = newColor;
    }

    public void ChangeShirtColor()
    {
        //renderer.materials[2] = renderer.materials[5];
    }
}

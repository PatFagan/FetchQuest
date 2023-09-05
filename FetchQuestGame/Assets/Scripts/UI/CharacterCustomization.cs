using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterCustomization : MonoBehaviour
{
    public GameObject characterModel;
    public Slider redSlider, greenSlider, blueSlider;
    public Slider redPantsSlider, greenPantsSlider, bluePantsSlider;
    public Slider redBackpackSlider, greenBackpackPantsSlider, blueBackpackPantsSlider;

    Color newColor;

    // Update is called once per frame
    void Update()
    {
        ChangeShirtColor();
        ChangePantsColor();
        ChangeBackpackColor();
    }

    public void ChangeShirtColor()
    {
        newColor = new Color(redSlider.value, greenSlider.value, 
        blueSlider.value, 0f);
        SkinnedMeshRenderer renderer = characterModel.GetComponent<SkinnedMeshRenderer>();
        renderer.materials[1].color = newColor;
    }

    public void ChangePantsColor()
    {
        newColor = new Color(redPantsSlider.value, greenPantsSlider.value, 
        bluePantsSlider.value, 0f);
        SkinnedMeshRenderer renderer = characterModel.GetComponent<SkinnedMeshRenderer>();
        renderer.materials[3].color = newColor;
    }

    public void ChangeBackpackColor()
    {
        newColor = new Color(redBackpackSlider.value, greenBackpackPantsSlider.value, 
        blueBackpackPantsSlider.value, 0f);
        SkinnedMeshRenderer renderer = characterModel.GetComponent<SkinnedMeshRenderer>();
        renderer.materials[2].color = newColor;
    }
}

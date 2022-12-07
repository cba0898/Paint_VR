using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColorPicker : MonoBehaviour
{
    private static ColorPicker instance = null;
    public static ColorPicker Instance
    {
        get
        {
            if (null == instance) instance = FindObjectOfType<ColorPicker>();
            return instance;
        }
    }

    private void Awake()
    {
        if (null == instance) instance = this;
        SetColor(0);
    }
 
    public Color selectedColor { get; private set; }
    [SerializeField] private List<Image> colorImages;
    [SerializeField] private Material objectMaterial;
    [SerializeField] private Material previewMaterial;
    [SerializeField] private Material[] materials;
    public void SetColor(int buttonIndex)
    {
        selectedColor = colorImages[buttonIndex].color;
        previewMaterial.color = colorImages[buttonIndex].color;
        SetMaterial(buttonIndex);
    }
    public Material GetMaterial()
    {
        return objectMaterial;
    }
    private void SetMaterial(int materialIndex)
    {
        objectMaterial = materials[materialIndex];
    }
}

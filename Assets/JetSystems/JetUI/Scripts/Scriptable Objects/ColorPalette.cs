using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Color Palette", menuName = "Scriptable Objects/Color Palette", order = 1)]
public class ColorPalette : ScriptableObject
{
    public List<Color> colors;
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Quality { Common, Uncommon, Rare, Epic, Legendary }
public static class QualityColor
{
    private static Dictionary<Quality, string> colors = new Dictionary<Quality, string>()
    {
        {Quality.Common, "#d6d6d6" },
        {Quality.Uncommon, "#00ff00ff" },
        {Quality.Rare, "#0000ffff" },
        {Quality.Epic, "#800080ff" },
        {Quality.Legendary, "#ff8a00" },
    };

    public static Dictionary<Quality, string> MyColors { get => colors; }
}

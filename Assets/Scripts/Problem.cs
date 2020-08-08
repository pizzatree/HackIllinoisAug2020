using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Problem : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI overheadText = null;

    [SerializeField]
    private string tempText = "x = 1 + 1";

    private void OnEnable()
    {
        overheadText.text = tempText;
    }
}

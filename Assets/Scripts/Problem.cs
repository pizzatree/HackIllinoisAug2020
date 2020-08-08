using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;

public class Problem : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI overheadText = null;

    [SerializeField]
    private TMP_InputField input;

    [SerializeField]
    private string tempText = " = 1 + 1";

    [SerializeField]
    private SpriteRenderer target;

    private char problemChar;
    private bool listening;

	public bool Listening { get => listening; set => listening = value; }

	public void AssignVariable(char variable)
    {
        problemChar = variable;
        overheadText.text = problemChar + tempText;
    }

    private void Update()
    {
        if (Listening)
        {
            input.Select();
            target.gameObject.SetActive(true);
        }
        else target.gameObject.SetActive(false);
    }

    public void Deselect() { input.text = ""; listening = false; Debug.Log("deselect"); }
}

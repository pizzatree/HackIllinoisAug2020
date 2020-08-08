using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Problem : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI equationTF = null, variableTF = null;

    [SerializeField]
    private TMP_InputField input;

    [SerializeField]
    private SpriteRenderer targetGraphic;

    private iQuestion question;

    private char problemChar;

    public void AssignProperties(char variable, iQuestion question, int difficulty)
    {
        problemChar = variable;
        this.question = question;
        variableTF.text = variable + " =";
        equationTF.text = question.Question(difficulty);

        var eqTextSize = new Vector2(equationTF.text.Length, equationTF.rectTransform.rect.y);
        equationTF.rectTransform.sizeDelta = eqTextSize;
    }

    public void Select()
    {
        input.Select();
        targetGraphic.enabled = true;
    }

    public void Deselect()
    {
        input.text = "";
        targetGraphic.enabled = false;
    }
}

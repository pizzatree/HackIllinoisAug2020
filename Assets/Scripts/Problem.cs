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

    public void AssignProperties(char variable, iQuestion question, int difficulty)
    {
        this.question = question;
        variableTF.text = variable + " =";
        equationTF.text = question.Question(difficulty);

        var eqTextSize = new Vector2(equationTF.text.Length, equationTF.rectTransform.rect.y);
        equationTF.rectTransform.sizeDelta = eqTextSize;

        SanitizeInput(question);
    }

    private void SanitizeInput(iQuestion question)
    {
        switch(question)
        {
            case Add a:
            case Subtract b:
            case Multiply c:
            case Divide d:
                input.contentType = TMP_InputField.ContentType.IntegerNumber;
                break;
            default:
                break;
        }
    }

    public void Answer()
    {
        Debug.Log(question.Answer(input.text.ToString()));
    }

    public void Select()
    {
        input.Select();
        targetGraphic.enabled = true;
    }

    public void Deselect()
    {
        Answer();
        input.text = "";
        targetGraphic.enabled = false;
    }
}

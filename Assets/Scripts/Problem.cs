using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Problem : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI equationTF = null, variableTF = null;

    private iQuestion question;

    private char problemChar;
    private bool active;

    public void AssignProperties(char variable, iQuestion question, int difficulty)
    {
        problemChar = variable;
        this.question = question;
        variableTF.text = variable + " =";
        equationTF.text = question.Question(difficulty);

        var eqTextSize = new Vector2(equationTF.text.Length, equationTF.rectTransform.rect.y);
        equationTF.rectTransform.sizeDelta = eqTextSize;
    }



    private void Update()
    {
        if(active)
        {
            ; // check answer
        }
    }
}

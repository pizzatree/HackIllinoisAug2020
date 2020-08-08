using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Problem : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI overheadText = null;

    [SerializeField]
    private string tempText = " = 1 + 1";

    private iQuestion question;

    private char problemChar;
    private bool active;

    public void AssignProperties(char variable, iQuestion question, int difficulty)
    {
        problemChar = variable;
        this.question = question;
        overheadText.text = problemChar + " =" + question.Question(difficulty);
    }



    private void Update()
    {
        if(active)
        {
            ; // check answer
        }
    }
}

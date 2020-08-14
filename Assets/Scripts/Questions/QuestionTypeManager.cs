using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestionTypeManager : MonoBehaviour
{
    [SerializeField] Toggle additionToggle = null;
    [SerializeField] Toggle subtractionToggle = null;
    [SerializeField] Toggle multiplicationToggle = null;
    [SerializeField] Toggle divisionToggle = null;

    public static Toggle AdditionToggle = null;
    public static Toggle SubtractionToggle = null;
    public static Toggle MultiplicationToggle = null;
    public static Toggle DivisionToggle = null;

	public void Start()
	{
        AdditionToggle = additionToggle;
        SubtractionToggle = subtractionToggle;
        MultiplicationToggle = multiplicationToggle;
        DivisionToggle = divisionToggle;
        DontDestroyOnLoad(this);
	}

}

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
    [SerializeField] InputField speedMultiplierInput;
    [SerializeField] InputField maxValueInput;

    public static Toggle AdditionToggle = null;
    public static Toggle SubtractionToggle = null;
    public static Toggle MultiplicationToggle = null;
    public static Toggle DivisionToggle = null;
    public static InputField SpeedMultiplierInput = null;
    public static InputField MaxValueInput = null;

	public void Start()
	{
        AdditionToggle = additionToggle;
        SubtractionToggle = subtractionToggle;
        MultiplicationToggle = multiplicationToggle;
        DivisionToggle = divisionToggle;
        SpeedMultiplierInput = speedMultiplierInput;
        MaxValueInput = maxValueInput;
        DontDestroyOnLoad(this);
	}

	private void Update()
	{
        if (StartGame.GameStarted)
        {
            if (SpeedMultiplierInput.text != "") { if (float.Parse(SpeedMultiplierInput.text) <= 0) SpeedMultiplierInput.text = 0.1f.ToString(); }
            else { SpeedMultiplierInput.text = 1f.ToString(); Debug.Log(""); }
            if (MaxValueInput.text != "") { if (int.Parse(MaxValueInput.text) < 9) maxValueInput.text = 9.ToString(); }
            else maxValueInput.text = 9.ToString();
        }
    }


}

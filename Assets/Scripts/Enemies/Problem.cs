using System.Text;
using Questions;
using Questions.Arithmetic;
using TMPro;
using UnityEngine;
using Utilities;

namespace Enemies
{
    public class Problem : MonoBehaviour
    {
        public char Variable { get; private set; }

        [SerializeField] private TextMeshProUGUI equationTF = null,
                                                 variableTF = null,
                                                 inputTF    = null;

        [SerializeField] private SpriteRenderer targetGraphic = null;
        
        private          bool          listening   = false;
        private readonly StringBuilder inputString = new StringBuilder();
        private          IQuestion     question;

        public void AssignProperties(char variable, IQuestion question, int difficulty)
        {
            this.question   = question;
            this.Variable   = variable;
            variableTF.text = char.ToUpper(variable) + " =";
            equationTF.text = question.Question(difficulty);

            // If letters are accepted into input, change that here
        }

        public void LoseTarget()
        {
            Answer();
            CleanInputField(false);
        }

        public  void Target()          => CleanInputField(true);
        private void Update()          => ReadInput();
        private void UpdateInputText() => inputTF.text = inputString.ToString();

        private void ReadInput()
        {
            if(!listening)
                return;

            var nextChar = KeyInput.GetKey();
            if(!nextChar.HasValue)
                return;

            if(nextChar.Value == (char) 0 && inputString.Length > 0)
                inputString.Length -= 1;
            else
                inputString.Append(nextChar.Value);

            UpdateInputText();
        }

        private void CleanInputField(bool isTarget)
        {
            listening    = targetGraphic.enabled = isTarget;
            inputTF.text = "";
            inputString.Clear();
        }

        private void Answer()
        {
            var accepted = question.Answer(inputTF.text);
            if(!accepted)
                return;

            targetGraphic.enabled = false;
            EnemyManager.Inst.SolnAccepted(Variable);
            equationTF.text = variableTF.text = "";
            SendMessageUpwards("SolutionAccepted", SendMessageOptions.RequireReceiver);
            Destroy(this);
        }

        private void OnMouseDown()
        {
            EnemyManager.Inst.ForceSelect(Variable);
            Target();
        }
    }
}
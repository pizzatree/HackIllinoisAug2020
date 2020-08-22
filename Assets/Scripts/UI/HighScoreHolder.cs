using UnityEngine;

namespace UI
{
    public class HighScoreHolder : MonoBehaviour
    {
        private void Start()
        {
            GetComponent<TMPro.TextMeshProUGUI>().text = "BEST: " + ScoreManager.Inst.GetHighScore();
        }
    }
}

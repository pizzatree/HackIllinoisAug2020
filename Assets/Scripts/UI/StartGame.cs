using UnityEngine;

namespace UI
{
    public class StartGame : MonoBehaviour
    {
        public void OnWithTheShow()
        {
            ScoreManager.Inst.ResetScore();
            UnityEngine.SceneManagement.SceneManager.LoadScene("Game");
        }
    }
}

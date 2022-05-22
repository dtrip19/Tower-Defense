using UnityEngine;
using UnityEngine.SceneManagement;

namespace Battle.UI
{
    public class LossScreen : MonoBehaviour
    {
        private CanvasGroup group;

        private void Awake()
        {
            group = GetComponent<CanvasGroup>();
            LifeTracker.OnPlayerLose += Appear;
        }

        private void Appear()
        {
            group.alpha = 1;
            group.interactable = true;
            group.blocksRaycasts = true;
        }

        public void RetryMap()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        public void ReturnToMainMenu()
        {
            SceneManager.LoadScene(0);
        }

        private void OnDestroy()
        {
            LifeTracker.OnPlayerLose -= Appear;
        }
    }
}
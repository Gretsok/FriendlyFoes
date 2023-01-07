using UnityEngine;

namespace FriendlyFoes.MainMenu
{
    public class MainMenuCanvasController : MonoBehaviour
    {
        public virtual void Show()
        {
            gameObject.SetActive(true);
        }

        public virtual void Hide()
        {
            gameObject.SetActive(false);
        }
    }
}
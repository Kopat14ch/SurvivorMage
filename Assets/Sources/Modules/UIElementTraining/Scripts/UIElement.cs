using UnityEngine;

namespace Sources.Modules.UIElementTraining.Scripts
{
    public class UIElement : MonoBehaviour
    {
        public void Enable(UIElement disableElement = null)
        {
            if (disableElement != null)
                disableElement.Disable();

            gameObject.SetActive(true);
        }

        public void Disable()
        {
            gameObject.SetActive(false);
        }
    }
}
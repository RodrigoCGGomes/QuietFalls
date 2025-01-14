using QuietFallsGameManaging;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace QF_Tools {
    public class QF_SelectableHandler : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, ISelectHandler, IDeselectHandler
    {
        #region Variables
        public enum SelectableType {NotAssesed, Unknown, NoSelectableComponent, Button, Slider}
        [Tooltip("Is it a button? A slider?")] public SelectableType type;

        #endregion
        #region MonoBehaviour Calls
        private void Awake()
        {
            type = DetermineSelectableType(this.gameObject);
        }
        #endregion

        private SelectableType DetermineSelectableType(GameObject subjectGO) //Returns the type of the Selectable component.
        {
            SelectableType t = SelectableType.NotAssesed;
            if (TryGetComponent<Selectable>(out var selectableComponent))
            {
                switch (selectableComponent.GetType().Name)
                {
                    case nameof(Button):
                        t = SelectableType.Button;
                        return t;
                    case nameof(Slider):
                        t = SelectableType.Slider;
                        return t;
                    default:
                        type = SelectableType.Unknown;
                    break;
                }
            }
            else
            { 
                t = SelectableType.NoSelectableComponent;
                return t;
            }
            return t;
        }

        #region Event System Handlers
        public void OnSelect(BaseEventData eventData)
        {

        }
        public void OnDeselect(BaseEventData eventData)
        {

        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            GameInputManager.RequestToBeSelected(this.gameObject);
        }

        public void OnPointerExit(PointerEventData eventData)
        {

        }
        #endregion
    }
}

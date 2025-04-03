using QuietFallsGameManaging;
using System.Collections;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace QF_Tools {
    public class QF_SelectableHandler : MonoBehaviour,
        IPointerEnterHandler, IPointerExitHandler, ISelectHandler, IDeselectHandler
    {
        #region Variables

        //Dinamically Set at start
        private enum SelectableType {NotAssesed, Unknown, NoSelectableComponent, Button, Slider}

        [Tooltip("Is it a button? A slider?")] 
        private SelectableType type;
        private Vector3 initialScale;

        private CancellationTokenSource _zoomCancellationTokenSource;
        
        //Inspector Editing
        public enum AnimationType { ZoomIn, NoAnimation }
        public AnimationType animationType;
        public Image optionalHighlightImage;
        #endregion

        #region MonoBehaviour Calls

        private void Awake()
        {
            type = DetermineSelectableType(this.gameObject);
            initialScale = transform.localScale;

            if (optionalHighlightImage != null)
            {
                optionalHighlightImage.enabled = false;
            }
        }
        private void OnEnable()
        {
            
        }
        private void OnDisable()
        {
            //Debug.LogWarning($"Disabled Selectable {this.gameObject.name}");
            transform.localScale = initialScale;

            if (optionalHighlightImage != null)
            {
                optionalHighlightImage.enabled = false;
            }
        }

        #endregion

        #region Private Instructions

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
        private async Task ZoomIn(CancellationToken token)
        {
            Vector3 desiredScale = new Vector3(1, 1, 1) * 1.1f;
            float zoomSpeed = 50;

            while (!token.IsCancellationRequested && (transform.localScale - desiredScale).magnitude > 0.01f)
            {
                transform.localScale = Vector3.Lerp(transform.localScale, desiredScale, zoomSpeed * Time.deltaTime);
                await Task.Yield();
            }

            transform.localScale = desiredScale;
        }
        private async Task ZoomOut(CancellationToken token)
        {
            Vector3 desiredScale = initialScale;
            float zoomSpeed = 5;

            while (!token.IsCancellationRequested && (transform.localScale - desiredScale).magnitude > 0.01f)
            {
                transform.localScale = Vector3.Lerp(transform.localScale, desiredScale, zoomSpeed * Time.deltaTime);
                await Task.Yield();
            }

            transform.localScale = desiredScale;
        }

        #endregion

        #region Event System Handlers

        public void OnSelect(BaseEventData eventData)
        {
            if (animationType == AnimationType.ZoomIn)
            {
                _zoomCancellationTokenSource?.Cancel();
                _zoomCancellationTokenSource = new CancellationTokenSource();
                _ = ZoomIn(_zoomCancellationTokenSource.Token);
            }
            if (optionalHighlightImage != null)
            {
                optionalHighlightImage.enabled = true;
            }
        }
        public void OnDeselect(BaseEventData eventData)
        {
            if (animationType == AnimationType.ZoomIn)
            {
                _zoomCancellationTokenSource?.Cancel();
                _zoomCancellationTokenSource = new CancellationTokenSource();
                _ = ZoomOut(_zoomCancellationTokenSource.Token);
            }
            if (optionalHighlightImage != null)
            {
                optionalHighlightImage.enabled = false;
            }
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

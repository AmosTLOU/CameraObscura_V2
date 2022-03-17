using UnityEngine;
using UnityEngine.EventSystems;

namespace UI {
    [RequireComponent(typeof(RectTransform))]
    public class Draggable: MonoBehaviour, IDragHandler {
        private RectTransform _rectTransform;
        
        private void Start(){
            _rectTransform = GetComponent<RectTransform>();
        }

        public void OnDrag(PointerEventData eventData){
            _rectTransform.anchoredPosition += eventData.delta;
        }
    }
}
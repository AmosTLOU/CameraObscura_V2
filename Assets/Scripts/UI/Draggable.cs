using UnityEngine;
using UnityEngine.EventSystems;
using Core;

namespace UI {
    [RequireComponent(typeof(RectTransform))]
    public class Draggable: MonoBehaviour, IDragHandler, IPointerDownHandler, IPointerUpHandler {
        private RectTransform _rectTransform;
        
        private void Start(){
            _rectTransform = GetComponent<RectTransform>();
        }

        public void OnDrag(PointerEventData eventData){
            _rectTransform.anchoredPosition += eventData.delta;
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            Log.I("pointer up");
            EvidenceBoard.Instance.ImageDrop(GetComponent<RectTransform>());
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            Log.I("pointer down");
        }
    }
}
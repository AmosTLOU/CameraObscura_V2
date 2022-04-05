using UnityEngine;
using UnityEngine.EventSystems;
using Core;

namespace UI {

    [RequireComponent(typeof(RectTransform))]
    public class Draggable: MonoBehaviour, IDragHandler, IPointerDownHandler, IPointerUpHandler {
        private RectTransform _rectTransform;
        public bool isSuspect;
        GameObject line;

        private void Start(){
            _rectTransform = GetComponent<RectTransform>();
        }

        public void OnDrag(PointerEventData eventData){
            _rectTransform.anchoredPosition += eventData.delta;

            if (line != null)
                UpdateLine();
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            //Log.I("pointer up");
            //EvidenceBoard.Instance.ImageDrop(GetComponent<RectTransform>());
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            //Log.I("pointer down");
        }

        public void AddLine()
        {
            if(!line) 
                line = Instantiate(transform.parent.GetComponentInChildren<UIColllider>().linePrefab, transform.parent.GetComponentInChildren<UIColllider>().lines.transform);
            UpdateLine();
        }

        public void DeleteLine()
        {

            if (line)
                Destroy(line);
        }

        public void UpdateLine()
        {
            RectTransform rect = transform.GetComponent<RectTransform>();
            RectTransform lineRect = line.GetComponent<RectTransform>();

            float x = rect.localPosition.x / 2;
            float y = rect.localPosition.y / 2;
            lineRect.localPosition = new Vector3(x, y, 0.0f);

            float angle = Vector3.Angle(Vector3.right, transform.localPosition);
            if (y < 0) angle = -angle;

            lineRect.localRotation = Quaternion.Euler(0, 0, angle);

            float length = Mathf.Pow((x * x + y * y), 0.5f); 
            lineRect.sizeDelta = new Vector2(length, lineRect.sizeDelta.y);

        }
    }
}
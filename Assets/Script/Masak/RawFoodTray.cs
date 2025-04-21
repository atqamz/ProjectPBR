using UnityEngine;

namespace Script.Masak
{
    public class RawFoodTray : MonoBehaviour
    {
        [SerializeField] private GameObject rawFoodPrefab;
        
        private UnityEngine.Camera _mainCamera; 

        private bool _isDraggingRawFood;
        
        private void Awake()
        {
            _mainCamera = UnityEngine.Camera.main;
        }
        
        private void GetRawFood()
        {
            if (_isDraggingRawFood) return;

            var mousePosition = Input.mousePosition;
            mousePosition.z = 10f;
            var worldPosition = _mainCamera.ScreenToWorldPoint(mousePosition);
            var rawFood = Instantiate(rawFoodPrefab, worldPosition, Quaternion.identity); 
            rawFood.TryGetComponent(out Food food);
            food.InitFood();
        }

        #region Events

        public void OnMouseDrag()
        {
            GetRawFood();
            _isDraggingRawFood = true;
        }
        
        public void OnEndDrag()
        {
            _isDraggingRawFood = false;
        }

        #endregion
    }
}

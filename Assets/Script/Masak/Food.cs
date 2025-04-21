using System;
using System.Collections.Generic;
using UnityEngine;
using MEC;

namespace Script.Masak
{
    public class Food : MonoBehaviour
    {
        #region enums

        private enum FoodState
        {
            Raw,
            Cooked,
            Burnt
        }
        
        private enum FoodLocation
        {
            None,
            OnWok,
            OnFinishTray
        }

        #endregion
        
        private UnityEngine.Camera _mainCamera; 

        private bool _isDragged;
        private bool _canBeDragged;
        private const float ZOffset = 10f;
        
        private FoodLocation _foodLocation;
        private FoodState _foodState;
        private CookingWok _cookingWok;
        private FinishFoodTray _finishFoodTray;
        private CoroutineHandle _cookingCoroutine;
        
        [SerializeField] private SpriteRenderer foodSpriteRenderer;

        private void Awake()
        {
            _mainCamera = UnityEngine.Camera.main;
        }
        

        private void Update()
        {
            if (!_isDragged) return;
            FollowMouse();
                
            if (Input.GetMouseButtonUp(0))
            {
                StopDragging();
            }
        }

        private void FollowMouse()
        {
            var mousePosition = Input.mousePosition;
            mousePosition.z = ZOffset;
            var worldPosition = _mainCamera.ScreenToWorldPoint(mousePosition);
            
            transform.position = new Vector3(worldPosition.x, worldPosition.y, transform.position.z);
        }

        public void InitFood()
        {
            _isDragged = true;
            _canBeDragged = false;
            _foodLocation = FoodLocation.None;
            _foodState = FoodState.Raw;
        }

        public void StartDragging()
        {
            if (!_canBeDragged) return;
            if (_cookingCoroutine.IsValid)
            {
                Timing.KillCoroutines(_cookingCoroutine);
            }
            
            _isDragged = true;
            
            if(_foodLocation == FoodLocation.OnWok)
            {
                _cookingWok.RemoveFoodFromWok(transform);
            }
        }
        
        private void StopDragging()
        {
            _isDragged = false;

            switch (_foodLocation)
            {
                case FoodLocation.OnWok:
                    if(!_cookingWok) return;
                    _cookingWok.AddFoodToWok(transform);
                    Debug.Log($"start couroutine cooking food");
                    _cookingCoroutine = Timing.RunCoroutine(StartCooking().CancelWith(gameObject));
                    break;
        
                case FoodLocation.OnFinishTray:
                    if (_foodState == FoodState.Raw)
                    {
                        Destroy(gameObject);
                    }
                    
                    if (!_finishFoodTray) return;
                    _finishFoodTray.AddFoodToFinishTray(transform);
            
                    // Ensure food can be dragged after being placed in the tray
                    _canBeDragged = true;

                    if(_cookingCoroutine.IsValid) 
                        Timing.KillCoroutines(_cookingCoroutine);
            
                    break;
        
                case FoodLocation.None:
                    Destroy(gameObject);
                    break;
            }
        }

        private IEnumerator<float> StartCooking()
        {
            yield return Timing.WaitForSeconds(5f);
            foodSpriteRenderer.color = Color.red;
            _foodState = FoodState.Cooked;
            _canBeDragged = true; // Allow dragging immediately after cooking

            yield return Timing.WaitForSeconds(5f);
            foodSpriteRenderer.color = Color.gray;
            _foodState = FoodState.Burnt;
            _canBeDragged = true; // Ensure burnt food is still draggable
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Wok"))
            {
                _foodLocation = FoodLocation.OnWok;
                other.TryGetComponent(out _cookingWok);
            }

            if (other.CompareTag("FinishFoodTray"))
            {
                _foodLocation = FoodLocation.OnFinishTray;
                other.TryGetComponent(out _finishFoodTray);
            }
        }
        
        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.CompareTag("Wok"))
            {
                _foodLocation = FoodLocation.None;
            }
            
            if (other.CompareTag("FinishFoodTray"))
            {
                _foodLocation = FoodLocation.None;
            }
            
        }
    }
}

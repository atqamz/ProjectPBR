using System;
using PrimeTween;
using UnityEngine;

namespace Script.Camera
{
    public class CameraMovement : MonoBehaviour
    {
        public enum PositionState { Left, Middle, Right }
    
        [Header("Positions")]
        [SerializeField] private Transform position1; // Far left
        [SerializeField] private Transform position2; // Middle
        [SerializeField] private Transform position3; // Far right
    
        [Header("Settings")]
        [SerializeField] private float moveDuration = 1f;
        [SerializeField] private Ease easeType = Ease.OutQuad;
    
        private PositionState _currentPositionState = PositionState.Left;
        private bool _isMoving;
        private Tween _moveTween;

        private void Start()
        {
            transform.position = position1.position;
        }

        private void Update()
        {
            if (_isMoving) return;

            if (Input.GetKeyDown(KeyCode.D))
            {
                MoveRight();
            }
            else if (Input.GetKeyDown(KeyCode.A))
            {
                MoveLeft();
            }
        }

        private void OnDisable()
        {
            if (!_moveTween.isAlive) return;
            
            _moveTween.Stop();
            _isMoving = false;
        }

        private void MoveRight()
        {
            switch (_currentPositionState)
            {
                case PositionState.Left:
                    MoveToPosition(position2.position, PositionState.Middle);
                    break;
                case PositionState.Middle:
                    MoveToPosition(position3.position, PositionState.Right);
                    break;
            }
        }

        private void MoveLeft()
        {
            switch (_currentPositionState)
            {
                case PositionState.Right:
                    MoveToPosition(position2.position, PositionState.Middle);
                    break;
                case PositionState.Middle:
                    MoveToPosition(position1.position, PositionState.Left);
                    break;
            }
        }

        private void MoveToPosition(Vector3 targetPosition, PositionState newState)
        {
            _isMoving = true;
            _currentPositionState = newState;

            _moveTween = Tween.Position(
                target: transform,
                endValue: targetPosition,
                duration: moveDuration,
                ease: easeType
            ).OnComplete(() => _isMoving = false);
        }
    }
}
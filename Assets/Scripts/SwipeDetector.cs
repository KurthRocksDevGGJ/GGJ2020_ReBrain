using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Photon.Pun.Demo.PunBasics 
{

    public class SwipeDetector : MonoBehaviourPunCallbacks
    {
        private Vector2 fingerDownPosition;
        private Vector2 fingerUpPosition;

        [SerializeField]
        private float minDistanceForSwipe = 20f;

        [SerializeField]
        private bool detectSwipeOnlyAfterRelease = false;
        public static event Action<SwipeData> OnSwipe = delegate { };

        // Update is called once per frame
        void Update()
        {
            foreach (Touch touch in Input.touches)
            {
                if(touch.phase == TouchPhase.Began)
                {
                    fingerUpPosition = touch.position;
                    fingerDownPosition = touch.position;
                }

                if(!detectSwipeOnlyAfterRelease && touch.phase == TouchPhase.Moved)
                {
                    fingerDownPosition = touch.position;
                    DetectSwipe();
                }

                if(touch.phase == TouchPhase.Ended)
                {
                    fingerDownPosition = touch.position;
                    DetectSwipe();
                }
            }
        }

        private void DetectSwipe()
        {
            if(SwipeDistanceCheckMet())
            {
                if(IsVerticalSwipe())
                {
                    var direction = fingerDownPosition.y - fingerUpPosition.y > 0 ? SwipeDirection.Up : SwipeDirection.Down;
                    SendSwipe(direction);
                }
                else
                {
                    var direction = fingerDownPosition.x - fingerUpPosition.x > 0 ? SwipeDirection.Right : SwipeDirection.Left;
                    SendSwipe(direction);
                }
            }
            else 
            {
                var direction = SwipeDirection.None;
                SendSwipe(direction);
            }
            fingerUpPosition = fingerDownPosition;
        }

        private bool SwipeDistanceCheckMet()
        {
            return VerticalMoveDistance() > minDistanceForSwipe || HorizontalMoveDistance() > minDistanceForSwipe;
        }

        private float VerticalMoveDistance()
        {
            return Mathf.Abs(fingerDownPosition.y - fingerUpPosition.y);
        }

        private float HorizontalMoveDistance()
        {
            return Mathf.Abs(fingerDownPosition.x - fingerUpPosition.x);
        }

        private bool IsVerticalSwipe()
        {
            return VerticalMoveDistance() > HorizontalMoveDistance();
        }
        
        private void SendSwipe(SwipeDirection direction)
        {
            SwipeData swipeData = new SwipeData()
            {
                Direction = direction,
                StartPosition = fingerDownPosition,
                EndPosition = fingerUpPosition
            };
            OnSwipe(swipeData);
        }
    }

    public struct SwipeData
    {
        public Vector2 StartPosition;
        public Vector2 EndPosition;
        public SwipeDirection Direction;
        public bool Tabbed;
    }

    public enum SwipeDirection 
    {
        None,
        Up,
        Down,
        Left,
        Right
    }
}

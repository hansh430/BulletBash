using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public interface IAgentInput 
{
     UnityEvent<Vector2> OnMovementKeyPressed { get; set; }
   
     UnityEvent<Vector2> OnPointerPositionChange { get; set; }

     UnityEvent OnFireButtonPressed { get; set; }

     UnityEvent OnFireButtonReleased { get; set; }
}

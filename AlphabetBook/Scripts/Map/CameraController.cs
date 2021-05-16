using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Lean.Touch;

namespace AlphabetBook
{
    /// <summary>This component allows you to move the current GameObject (e.g. Camera) based on finger drags and the specified ScreenDepth.</summary>
	[HelpURL(LeanTouch.HelpUrlPrefix + "CameraController")]
    [AddComponentMenu(LeanTouch.ComponentPathPrefix + "Drag Camera")]
    public class CameraController : MonoBehaviour
    {
		/// <summary>The method used to find fingers to use with this component. See LeanFingerFilter documentation for more information.</summary>
		public LeanFingerFilter Use = new LeanFingerFilter(true);

		/// <summary>The method used to find world coordinates from a finger. See LeanScreenDepth documentation for more information.</summary>
		public LeanScreenDepth ScreenDepth = new LeanScreenDepth(LeanScreenDepth.ConversionType.DepthIntercept);

		/// <summary>The movement speed will be multiplied by this.
		/// -1 = Inverted Controls.</summary>
		[Tooltip("The movement speed will be multiplied by this.\n\n-1 = Inverted Controls.")]
		public float Sensitivity = 1.0f;

		/// <summary>If you want this component to change smoothly over time, then this allows you to control how quick the changes reach their target value.
		/// -1 = Instantly change.
		/// 1 = Slowly change.
		/// 10 = Quickly change.</summary>
		[Tooltip("If you want this component to change smoothly over time, then this allows you to control how quick the changes reach their target value.\n\n-1 = Instantly change.\n\n1 = Slowly change.\n\n10 = Quickly change.")]
		public float Dampening = -1.0f;

		/// <summary>This allows you to control how much momenum is retained when the dragging fingers are all released.
		/// NOTE: This requires <b>Dampening</b> to be above 0.</summary>
		[Tooltip("This allows you to control how much momenum is retained when the dragging fingers are all released.\n\nNOTE: This requires <b>Dampening</b> to be above 0.")]
		[Range(0.0f, 1.0f)]
		public float Inertia;

		[HideInInspector]
		[SerializeField]
		private Vector3 remainingDelta;

		/// <summary>This method moves the current GameObject to the center point of all selected objects.</summary>
		[ContextMenu("Move To Selection")]
		public virtual void MoveToSelection()
		{
			var center = default(Vector3);
			var count = 0;

			foreach (var selectable in LeanSelectable.Instances)
			{
				if (selectable.IsSelected == true)
				{
					center += selectable.transform.position;
					count += 1;
				}
			}

			if (count > 0)
			{
				var oldPosition = transform.localPosition;

				transform.position = center / count;

				remainingDelta += transform.localPosition - oldPosition;

				transform.localPosition = oldPosition;
			}
		}

		/// <summary>If you've set Use to ManuallyAddedFingers, then you can call this method to manually add a finger.</summary>
		public void AddFinger(LeanFinger finger)
		{
			Use.AddFinger(finger);
		}

		/// <summary>If you've set Use to ManuallyAddedFingers, then you can call this method to manually remove a finger.</summary>
		public void RemoveFinger(LeanFinger finger)
		{
			Use.RemoveFinger(finger);
		}

		/// <summary>If you've set Use to ManuallyAddedFingers, then you can call this method to manually remove all fingers.</summary>
		public void RemoveAllFingers()
		{
			Use.RemoveAllFingers();
		}

#if UNITY_EDITOR
		protected virtual void Reset()
		{
			Use.UpdateRequiredSelectable(gameObject);
		}
#endif


		public float widthLenght = 10;

		private const float WIDTH = 9.6f;

		private float delta = 0f;

		protected virtual void Awake()
		{
			Use.UpdateRequiredSelectable(gameObject);

			Vector3 screenSize = GetComponent<Camera>().ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0f));

			delta = -(WIDTH - screenSize.x);

		//	Debug.Log("delta = " + delta);

			transform.DOMoveX(delta, 1f);
		}

		public void SetCameraPos(float x)
		{
			transform.DOMoveX(x, 0.01f);
		}

		public void SetCameraDefault()
        {
			transform.DOMoveX(delta, 0.01f);
		}

		protected virtual void LateUpdate()
		{
			// Get the fingers we want to use
			var fingers = Use.GetFingers();

			// Get the last and current screen point of all fingers
			var lastScreenPoint = LeanGesture.GetLastScreenCenter(fingers);
			var screenPoint = LeanGesture.GetScreenCenter(fingers);

			// Get the world delta of them after conversion
			var worldDelta = ScreenDepth.ConvertDelta(lastScreenPoint, screenPoint, gameObject);

			// Store the current position
			var oldPosition = transform.localPosition;

			worldDelta.y = 0;

			// Pan the camera based on the world delta
			transform.position -= worldDelta * Sensitivity;


            Vector3 temp = transform.position;

            temp.x = Mathf.Clamp(temp.x, delta, widthLenght - delta);
            // temp.y = Mathf.Clamp(temp.y, -0.1f, 0.1f);

            transform.position = temp;


            // Add to remainingDelta
            remainingDelta += transform.localPosition - oldPosition;

			// Get t value
			var factor = LeanTouch.GetDampenFactor(Dampening, Time.deltaTime);

			// Dampen remainingDelta
			var newRemainingDelta = Vector3.Lerp(remainingDelta, Vector3.zero, factor);


			oldPosition.y = 0;
			remainingDelta.y = 0;
			newRemainingDelta.y = 0;

			// Shift this position by the change in delta
			transform.localPosition = oldPosition + remainingDelta - newRemainingDelta;





			if (fingers.Count == 0 && Inertia > 0.0f && Dampening > 0.0f)
			{
				newRemainingDelta = Vector3.Lerp(newRemainingDelta, remainingDelta, Inertia);
			}

			// Update remainingDelta with the dampened value
			remainingDelta = newRemainingDelta;



			
		}

	}
}




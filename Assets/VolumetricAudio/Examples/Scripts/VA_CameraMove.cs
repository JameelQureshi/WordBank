using UnityEngine;

namespace VolumetricAudio
{
	/// <summary>This component allows you to move the current GameObject based on WASD/mouse/finger drags.</summary>
	[HelpURL(VA_Helper.HelpUrlPrefix + "VA_CameraMove")]
	[AddComponentMenu("Volumetric Audio/VA Camera Move")]
	public class VA_CameraMove : MonoBehaviour
	{
		/// <summary>Is this component currently listening for inputs?</summary>
		public bool Listen { set { listen = value; } get { return listen; } } [SerializeField] private bool listen = true;

		/// <summary>How quickly the position goes to the target value (-1 = instant).</summary>
		public float Damping { set { damping = value; } get { return damping; } } [SerializeField] private float damping = 10.0f;

		/// <summary>The movement speed will be multiplied by this.</summary>
		public float Speed { set { speed = value; } get { return speed; } } [SerializeField] private float speed = 1.0f;

		/// <summary>The keys/fingers required to move left/right.</summary>
		public VA_InputManager.Axis HorizontalControls { set { horizontalControls = value; } get { return horizontalControls; } } [SerializeField] private VA_InputManager.Axis horizontalControls = new VA_InputManager.Axis(2, false, VA_InputManager.AxisGesture.HorizontalDrag, 1.0f, KeyCode.A, KeyCode.D, KeyCode.LeftArrow, KeyCode.RightArrow, 100.0f);

		/// <summary>The keys/fingers required to move backward/forward.</summary>
		public VA_InputManager.Axis DepthControls { set { depthControls = value; } get { return depthControls; } } [SerializeField] private VA_InputManager.Axis depthControls = new VA_InputManager.Axis(2, false, VA_InputManager.AxisGesture.HorizontalDrag, 1.0f, KeyCode.S, KeyCode.W, KeyCode.DownArrow, KeyCode.UpArrow, 100.0f);

		/// <summary>The keys/fingers required to move down/up.</summary>
		public VA_InputManager.Axis VerticalControls { set { verticalControls = value; } get { return verticalControls; } } [SerializeField] private VA_InputManager.Axis verticalControls = new VA_InputManager.Axis(3, false, VA_InputManager.AxisGesture.HorizontalDrag, 1.0f, KeyCode.F, KeyCode.R, KeyCode.None, KeyCode.None, 100.0f);

		[System.NonSerialized]
		private Vector3 remainingDelta;

		protected virtual void OnEnable()
		{
			VA_InputManager.EnsureThisComponentExists();
		}

		protected virtual void Update()
		{
			if (listen == true)
			{
				AddToDelta();
				DampenDelta();
			}
		}

		private void AddToDelta()
		{
			// Get delta from binds
			var delta = default(Vector3);

			delta.x = horizontalControls.GetValue(Time.deltaTime);
			delta.y = verticalControls  .GetValue(Time.deltaTime);
			delta.z = depthControls     .GetValue(Time.deltaTime);

			// Store old position
			var oldPosition = transform.position;

			// Translate
			transform.Translate(delta * speed * Time.deltaTime, Space.Self);

			// Add to remaining
			var acceleration = transform.position - oldPosition;

			remainingDelta += acceleration;

			// Revert position
			transform.position = oldPosition;
		}

		private void DampenDelta()
		{
			// Dampen remaining delta
			var factor   = VA_Helper.DampenFactor(damping, Time.deltaTime);
			var newDelta = Vector3.Lerp(remainingDelta, Vector3.zero, factor);

			// Translate by difference
			transform.position += remainingDelta - newDelta;

			// Update remaining
			remainingDelta = newDelta;
		}
	}
}

#if UNITY_EDITOR
namespace VolumetricAudio
{
	using UnityEditor;

	[CanEditMultipleObjects]
	[CustomEditor(typeof(VA_CameraMove))]
	public class SgtCameraMove_Editor : VA_Editor<VA_CameraMove>
	{
		protected override void OnInspector()
		{
			Draw("listen", "Is this component currently listening for inputs?");
			Draw("damping", "How quickly the position goes to the target value (-1 = instant).");

			EditorGUILayout.Separator();

			Draw("speed", "The movement speed will be multiplied by this.");

			EditorGUILayout.Separator();

			Draw("horizontalControls", "The keys/fingers required to move right/left.");
			Draw("depthControls", "The keys/fingers required to move backward/forward.");
			Draw("verticalControls", "The keys/fingers required to move down/up.");
		}
	}
}
#endif
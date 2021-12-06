using UnityEngine;

namespace VolumetricAudio
{
	/// <summary>This component allows you to rotate the current GameObject based on mouse/finger drags.</summary>
	[HelpURL(VA_Helper.HelpUrlPrefix + "VA_CameraPivot")]
	[AddComponentMenu("Volumetric Audio/VA Camera Pivot")]
	public class VA_CameraPivot : MonoBehaviour
	{
		/// <summary>Is this component currently listening for inputs?</summary>
		public bool Listen { set { listen = value; } get { return listen; } } [SerializeField] private bool listen = true;

		/// <summary>How quickly the rotation transitions from the current to the target value (-1 = instant).</summary>
		public float Damping { set { damping = value; } get { return damping; } } [SerializeField] private float damping = 10.0f;

		/// <summary>The keys/fingers required to pitch down/up.</summary>
		public VA_InputManager.Axis PitchControls { set { pitchControls = value; } get { return pitchControls; } } [SerializeField] private VA_InputManager.Axis pitchControls = new VA_InputManager.Axis(1, true, VA_InputManager.AxisGesture.VerticalDrag, -0.1f, KeyCode.None, KeyCode.None, KeyCode.None, KeyCode.None, 45.0f);

		/// <summary>The keys/fingers required to yaw left/right.</summary>
		public VA_InputManager.Axis YawControls { set { yawControls = value; } get { return yawControls; } } [SerializeField] private VA_InputManager.Axis yawControls = new VA_InputManager.Axis(1, true, VA_InputManager.AxisGesture.HorizontalDrag, 0.1f, KeyCode.None, KeyCode.None, KeyCode.None, KeyCode.None, 45.0f);

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
			}

			DampenDelta();
		}

		private void AddToDelta()
		{
			remainingDelta.x += pitchControls.GetValue(Time.deltaTime);
			remainingDelta.y += yawControls  .GetValue(Time.deltaTime);
		}

		private void DampenDelta()
		{
			// Dampen remaining delta
			var factor   = VA_Helper.DampenFactor(damping, Time.deltaTime);
			var newDelta = Vector3.Lerp(remainingDelta, Vector3.zero, factor);

			// Rotate by difference
			var euler = transform.localEulerAngles;

			euler.x = -Mathf.DeltaAngle(euler.x, 0.0f);

			euler += remainingDelta - newDelta;

			euler.x = Mathf.Clamp(euler.x, -89.0f, 89.0f);

			transform.localEulerAngles = euler;

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
	[CustomEditor(typeof(VA_CameraPivot))]
	public class VA_CameraPivot_Editor : VA_Editor<VA_CameraPivot>
	{
		protected override void OnInspector()
		{
			Draw("listen", "Is this component currently listening for inputs?");
			Draw("damping", "How quickly the rotation transitions from the current to the target value (-1 = instant).");

			EditorGUILayout.Separator();

			Draw("pitchControls", "The keys/fingers required to pitch down/up.");
			Draw("yawControls", "The keys/fingers required to yaw left/right.");
		}
	}
}
#endif
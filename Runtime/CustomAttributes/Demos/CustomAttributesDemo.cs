using UnityEngine;

namespace MuffinDev.Core.Demos
{

	[AddComponentMenu("Muffin Dev/Demos/Custom Attributes Demo")]
	public class CustomAttributesDemo : MonoBehaviour
	{

        [Header("Min and Max attributes")]

        [Min(0f)]
        [Tooltip("This value can't be less than 0")]
        public float min = 1f;

        [Min(0f)]
        [Tooltip("You can't use string values using Min attributes")]
        public string wrongMin = string.Empty;

        [Max(10)]
        [Tooltip("Each veactor values can't be more than 10")]
        public Vector3 max = new Vector3(10f, 10f, 10f);

        [Header("IfChecked attribute")]

        [Tooltip("Check/Uncheck this property in order to enable/disable the following value property.")]
        public bool enableFollowingProperty = true;

        [IfChecked("enableFollowingProperty")]
        public int value = 5;

        [Header("AnimCurve attribute")]

        [AnimCurve]
        [Tooltip("Time and value of this curve can only be between 0 and 1")]
        public AnimationCurve curve1 = AnimationCurves.Linear;

        [AnimCurve(0f, 1f, 0f, 10f, EColor.Orange)]
        [Tooltip("Time of this curve is clamped between 0 and 1, its value clamped between 0 and 10. This curve is also coloured in blue")]
        public AnimationCurve curve2 = AnimationCurves.Linear;

        [Header("RangeMinMax attribute")]

        [RangeMinMax(0f, 10f)]
        [Tooltip("You can define a minimum (X) and a maximum (Y) values between 0 and 10")]
        public Vector2 rangeMinMax = new Vector2(3f, 6f);

        [RangeMinMax(0f, 100f, true)]
        [Tooltip("Minimum and maximum are clamped between 0 and 100, but only integers are allowed")]
        public Vector2 integerRangeMinMax = new Vector2(60f, 80f);

        [Header("Indent attribute")]

        [Indent(1)]
        [Tooltip("Property indented at level 1")]
        public string indent1 = string.Empty;

        [Indent(2)]
        [Tooltip("Property indented at level 2")]
        public string indent2 = string.Empty;

        [Header("Readonly attribute")]

        [Readonly]
        [Tooltip("This value can't be edited")]
        public string readonlyValue = string.Empty;

        [Header("ComponentRef attribute")]

        [ComponentRef]
        [Tooltip("The Transform instance is set automatically using GetComponent(). You won't be able to set this value to none.")]
        public Transform thisTransform = null;

        [ComponentRef(true, true)]
        [Tooltip("The Light object is set automatically using GameObject.FindObjectOfType(). You won't be able to set this value to none, unless you remove all lights in the scene.")]
        public Light sceneLight = null;

    }

}
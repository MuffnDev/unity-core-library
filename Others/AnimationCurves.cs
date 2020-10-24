using UnityEngine;

namespace MuffinDev
{

	///<summary>
	/// Bundle of predefined AnimationCurves for Editor tools.
	///</summary>
	public static class AnimationCurves
	{

        public static AnimationCurve Linear
        {
            get
            {
                return new AnimationCurve
                (
                    new Keyframe(0.0f, 0.0f, 0.0f, 1.0f),
                    new Keyframe(1.0f, 1.0f, 1.0f, 0.0f)
                );
            }
        }

        public static AnimationCurve ReverseLinear
        {
            get
            {
                return new AnimationCurve
                (
                    new Keyframe(0.0f, 1.0f, 0.0f, -1.0f),
                    new Keyframe(1.0f, 0.0f, -1.0f, 0.0f)
                );
            }
        }

        public static AnimationCurve EaseInOut
        {
            get
            {
                return new AnimationCurve
                (
                    new Keyframe(0.0f, 0.0f, 0.0f, 0.0f),
                    new Keyframe(1.0f, 1.0f, 0.0f, 0.0f)
                );
            }
        }

        public static AnimationCurve One
        {
            get
            {
                return new AnimationCurve
                (
                    new Keyframe(0.0f, 1.0f, 0.0f, 0.0f),
                    new Keyframe(1.0f, 1.0f, 0.0f, 0.0f)
                );
            }
        }

        public static AnimationCurve Zero
        {
            get
            {
                return new AnimationCurve
                (
                    new Keyframe(0.0f, 0.0f, 0.0f, 0.0f),
                    new Keyframe(1.0f, 0.0f, 0.0f, 0.0f)
                );
            }
        }

    }

}
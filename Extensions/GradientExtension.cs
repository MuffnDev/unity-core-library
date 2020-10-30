using UnityEngine;

namespace MuffinDev
{

    /// <summary>
    /// Extensions for Gradient class.
    /// </summary>
    public static class GradientExtension
    {

        /// <summary>
        /// Clones the gradient.
        /// </summary>
        /// <param name="_Gradient">The gradient to clone.</param>
        /// <returns>Returns the cloned Gradient instance.</returns>
        public static Gradient Clone(this Gradient _Gradient)
        {
            Gradient gradient = new Gradient();
            gradient.alphaKeys = (GradientAlphaKey[])_Gradient.alphaKeys.Clone();
            gradient.colorKeys = (GradientColorKey[])_Gradient.colorKeys.Clone();
            gradient.mode = _Gradient.mode;
            return gradient;
        }

        /// <summary>
        /// Reverses the gradient color and alpha keys.
        /// </summary>
        /// <param name="_Gradient">The original gradient you want to reverse..</param>
        /// <returns>Returns a new Gradient instance with the reversed keys of the input Gradient.</returns>
        public static Gradient Reverse(this Gradient _Gradient)
        {
            Gradient gradient = _Gradient.Clone();

            gradient.colorKeys = new GradientColorKey[_Gradient.colorKeys.Length];
            for (int i = 0; i < _Gradient.colorKeys.Length; i++)
            {
                gradient.colorKeys[i] = new GradientColorKey(_Gradient.colorKeys[i].color, 1f - _Gradient.colorKeys[i].time);
            }

            gradient.alphaKeys = new GradientAlphaKey[_Gradient.alphaKeys.Length];
            for (int i = 0; i < _Gradient.alphaKeys.Length; i++)
            {
                gradient.alphaKeys[i] = new GradientAlphaKey(_Gradient.alphaKeys[i].alpha, 1f - _Gradient.alphaKeys[i].time);
            }

            return gradient;
        }

    }

}
using UnityEngine;

namespace Assets.Source
{
    public class HealthBar : MonoBehaviour
    {
        public Texture2D Frame;
        public Rect FramePosition;
        private float _frameAspect;
        public Texture2D Bar;
        public Rect BarPosition;
        private float _barAspect;
        public float Scale;

        public void Awake()
        {
            _frameAspect = Frame.height / (float) Frame.width;
            _barAspect = Bar.height / (float) Bar.width;
        }

        public void OnGUI()
        {
            FramePosition.x = (Screen.width - FramePosition.width) / 2;
            FramePosition.width = Screen.width * _frameAspect * Scale;
            FramePosition.height = FramePosition.width * _frameAspect;
            GUI.DrawTexture(FramePosition, Frame);
        }
    }
}

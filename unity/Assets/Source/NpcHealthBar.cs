using UnityEngine;

namespace Assets.Source
{
    public class NpcHealthBar : MonoBehaviour
    {
        public Texture2D Frame;
        public Rect FramePosition;
        private float _frameAspect;
        public Texture2D Bar;
        public Rect BarPosition;
        private float _barAspect;
        public Player Player;

        public float Scale;
        private bool _hasTarget;

        [Range(0, 1)]
        public float HealthPercentage;

        public void Awake()
        {
            _frameAspect = Frame.height / (float) Frame.width;
            _barAspect = Bar.height / (float) Bar.width;
        }
        
        public void Update()
        {
            if (_hasTarget = (Player.Target != null))
            {
                var npc = Player.Target.GetComponent<Npc>();
                HealthPercentage = Mathf.Clamp01(npc.CurrentHealth/npc.MaxHealth);
            }
        }

        public void OnGUI()
        {
            if(_hasTarget) DrawFrame();
        }

        public void DrawFrame()
        {
            // frame
            FramePosition.x = (Screen.width - FramePosition.width) / 2;
            FramePosition.width = Screen.width * _frameAspect * Scale;
            FramePosition.height = FramePosition.width * _frameAspect;
            GUI.DrawTexture(FramePosition, Frame);
            DrawBar();
        }

        public void DrawBar()
        {
            // bar
            BarPosition.x = FramePosition.x + FramePosition.width * 0.105f;
            BarPosition.width = (Screen.width * _barAspect * Scale * 0.79f);
            BarPosition.height = BarPosition.width * _barAspect * 0.5f;
            BarPosition.y = FramePosition.y + FramePosition.height * 0.3f;
            BarPosition.width *= HealthPercentage;
            GUI.DrawTexture(BarPosition, Bar);
        }
    }
}

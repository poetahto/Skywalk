
namespace Game.Scripts
{
    public class RopeEffectNoJump : RopeEffect
    {
        private InputJumpingController _jumpingController;

        private void Start()
        {
            _jumpingController = FindObjectOfType<InputJumpingController>();
        }

        protected override void OnRopeEnter()
        {
            base.OnRopeEnter();
            _jumpingController.enabled = false;

        }

        protected override void OnRopeExit()
        {
            base.OnRopeExit();
            _jumpingController.enabled = true;
        }
    }
}
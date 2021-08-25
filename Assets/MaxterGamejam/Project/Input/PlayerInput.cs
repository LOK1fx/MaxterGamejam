namespace com.LOK1game.recode
{
    public static class PlayerInput
    {
        public static bool initialized;

        private static ControlsAction _input;

        public static void Init()
        {
            if(initialized) { return; }

            _input = new ControlsAction();

            _input.Enable();

            initialized = true;
        }

        public static ControlsAction GetInput()
        {
            if(_input == null)
            {
                throw new System.Exception("Input is not initialized yet");
            }

            return _input;
        }
    }
}
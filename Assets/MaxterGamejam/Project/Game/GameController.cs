using UnityEngine;
using Photon.Pun;
using com.LOK1game.MaxterGamejam;

namespace com.LOK1game.recode
{
    /// <summary>
    /// Тестовый контроллер с режимом бой насмерть
    /// </summary>
    public class GameController : GameControllerBase
    {
        [SerializeField] private GamejamGameModeData _gamejamModeData;

        private void Start()
        {
            var activity = new Discord.Activity
            {
                State = "Играет",
                Details = "В игре",

                Assets =
                {
                    LargeImage = "mgj_logo",
                },

                Type = Discord.ActivityType.Playing,

                Instance = true,
            };

            SetGameMode<GamejamGameMode>(_gamejamModeData);
        }

        protected override GameOptions InitializeGameOptions()
        {
            var newOptions = new GamejamGameOptions();

            _currentGameOptions = newOptions;

            TryGetGameOptions<GamejamGameOptions>(out var options);
            options.SetBunnyhopping(true, this);

            return _currentGameOptions = options;
        }
    }
}
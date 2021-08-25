using UnityEngine;
using System.Collections;
using com.LOK1game.recode.Player;

namespace com.LOK1game.recode.Architecture
{
    public class LocalPlayerInteractor : Interactor
    {
        public Player.Player player { get; private set; }

        private LocalPlayerRepository _repository;

        public int health => _repository.health;

        public override void OnCreate()
        {
            base.OnCreate();

            _repository = Game.GetRepository<LocalPlayerRepository>();
        }

        public override void Initialize()
        {
            base.Initialize();

            LocalPlayer.Initialize(this);

            //player = (Player.Player)Resources.Load("Prefabs/Player");
        }

        public void AddHealth(object sender, int value)
        {
            _repository.health += value;
        }

        public void Damage(object sender, int value)
        {
            _repository.health -= value;
        }
    }
}

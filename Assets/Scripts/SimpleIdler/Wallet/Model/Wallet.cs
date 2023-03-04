using Leopotam.Ecs;
using UnityEngine;

namespace SimpleIdler.Wallet.Model
{
    public class Wallet
    {
        public int ID { get; }

        private const string WALLET_SAVE_KEY = "simple_idler_wallet_";
        private readonly EcsWorld _world;

        private int _amount;

        public int Amount
        {
            get => _amount;
            set
            {
                _amount = value;
                _world?.NewEntity().Get<Components.WalletChangedSignal>();
                Save();
            }
        }

        public Wallet(EcsWorld world, int id)
        {
            ID = id;
            _world = world;
        }

        public void Save()
        {
            PlayerPrefs.SetInt(WALLET_SAVE_KEY + ID, _amount);
            PlayerPrefs.Save();
        }

        public void Load()
        {
            _amount = PlayerPrefs.GetInt(WALLET_SAVE_KEY + ID, 0);
        }
    }
}
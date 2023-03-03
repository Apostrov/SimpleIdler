using Leopotam.Ecs;

namespace SimpleIdler.Wallet.Model
{
    public class WalletsFacade
    {
        public readonly Wallet[] Wallets;
        public Wallet Player => Wallets[0];

        public WalletsFacade(int amount, EcsWorld world)
        {
            Wallets = new Wallet[amount];
            for (int i = 0; i < Wallets.Length; i++)
            {
                Wallets[i] = new Wallet(world, i);
                Wallets[i].Load();
            }
        }

        public void Save()
        {
            foreach (var wallet in Wallets)
            {
                wallet.Save();
            }
        }

        public void Load()
        {
            foreach (var wallet in Wallets)
            {
                wallet.Load();
            }
        }
    }
}
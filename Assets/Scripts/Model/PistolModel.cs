namespace Ig.Model
{
    public class PistolModel : WeaponModel
    {
        public override void Fire()
        {
            if (!CanFire) return;
            if (!Ammunition) return;
            var ammunition = Instantiate(Ammunition, Gun.position, Ammunition.Rotation);
            ammunition.AddForce(Gun.forward * Force);
            CanFire = false;
            Invoke(nameof(ReadyForShoot), RechargeTime);
        }
    }
}
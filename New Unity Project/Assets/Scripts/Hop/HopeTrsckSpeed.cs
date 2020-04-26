namespace Hop
{
    public class HopeTrsckSpeed : HopeTrackFade

    {
        public override void EffectPlayer(HopPlayer player)
        {
            player.m_BallSpeed = 2f;
        }
    }
}
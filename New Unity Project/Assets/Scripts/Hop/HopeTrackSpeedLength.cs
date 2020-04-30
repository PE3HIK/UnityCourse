namespace Hop
{
    public class HopeTrackSpeedLength:HopeTrackSpeed
    {
        public override void EffectPlayer(HopPlayer player)
        {
            player.m_BallSpeed = 2f;
            player.m_JumpDistance = 4f;
        }
    }
}
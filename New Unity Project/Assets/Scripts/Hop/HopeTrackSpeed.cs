namespace Hop
{
    public class HopeTrackSpeed : HopeTrackFade

    {
        public override void EffectPlayer(HopPlayer player)
        {
            player.m_BallSpeed = 1.5f;
            player.m_JumpDistance = 2f; 
        }
    }
}
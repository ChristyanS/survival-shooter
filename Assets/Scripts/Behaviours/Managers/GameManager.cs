namespace Behaviours.Managers
{
    public class GameManager : Singleton<GameManager>
    {
        public bool InstaKillEnable { get; set; }
        public bool DoublePointsEnable { get; set; }
    }
}
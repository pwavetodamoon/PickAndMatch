namespace PickandMatch.Scripts.Defines
{
    public enum CardType
    {
        unknow = 0,
        animal01 = 1,
        animal02 = 2,
        animal03 = 3,
        animal04 = 4,
        animal05 = 5,
        animal06 = 6,
        animal07 = 7,
        animal08 = 8,
        animal09 = 9,
        animal10 = 10,
        animal11 = 11,
        animal12 = 12,
        animal13 = 13,
        animal14 = 14,
        animal15 = 15,
        animal16 = 16,
        animal17 = 17,
        animal18 = 18,
        animal19 = 19,
        animal20 = 20,
    }
    public static class TimeConfig
    {
        public const float TimeDuration = 0.25f;
    }

    public enum CardStatus
    {
        Ready = 0,
        NotReady = 1,
    }
    public enum GameStatus
    {
        Lose = 0,
        Win = 1,
        Play = 2,
        Pause = 3,
    }

}
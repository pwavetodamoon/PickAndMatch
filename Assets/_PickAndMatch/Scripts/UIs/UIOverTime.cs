using deVoid.UIFramework;
using deVoid.Utils;
using PickandMatch.Scripts.Defines;

namespace PickandMatch.Scripts.UIs
{
    public class UIOverTime : APanelController
    {
        public void OnRestartButton()
        {
            Signals.Get<OnRestartLevel>().Dispatch();
        }

        public void OnBackButton()
        {
            Signals.Get<BackToLevelProgess>().Dispatch();
            Signals.Get<StatusGame>().Dispatch(GameStatus.Lose);
        }
    }
}
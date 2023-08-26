using deVoid.UIFramework;
using PickandMatch.Scripts.Defines;
using deVoid.Utils;

namespace Packages.Scripts.UIs
{
    public class UIPanelPauseGameController : APanelController
    {
        public void OnClosePausePanelButton()
        {
            Signals.Get<HidePausePanel>().Dispatch();
            Signals.Get<StatusGame>().Dispatch(GameStatus.Play);
            TimeController.SetResumeGame();
        }
        public void OnBackToLevelProgessButton()
        {
            Signals.Get<BackToLevelProgess>().Dispatch();
        }

    }

}

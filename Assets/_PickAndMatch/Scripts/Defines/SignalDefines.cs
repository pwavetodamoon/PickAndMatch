using deVoid.Utils;
using PickandMatch.Scripts.Core;
namespace PickandMatch.Scripts.Defines
{

    public class HidePausePanel : ASignal
    {

    }
    public class OpenPausePanel : ASignal
    {

    }

    public class NextLevel : ASignal
    {

    }

    public class BackToLevelProgess : ASignal
    {

    }
    public class SelectCard : ASignal<CardController>
    {

    }
    public class UILevelData : ASignal
    {

    }
    public class UIMenuData : ASignal

    {

    }   
    public class OnRestartLevel : ASignal
    {

    }   
    public class StartTimeLevel : ASignal<float>
    {

    }
    public class StatusGame : ASignal<GameStatus>
    {

    }    

    public class UIPanelNotificationLockedEvent : ASignal
    {

    }
    public class NodeLevelsSignals : ASignal
    {

    }
    public class CheckCount : ASignal<int>
    {

    }
    public class StarWinGame : ASignal
    {

    }

    public class ShowLevel : ASignal
    {

    }
    public class ShowHowTo : ASignal
    {

    }
    public class HideHowto : ASignal
    {

    }
    public class FetchAuto : ASignal<bool>
    {

    }
    public class LevelSelect : ASignal<uint> { }
}



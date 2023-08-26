using deVoid.UIFramework;
using UnityEngine;

namespace PickandMatch.Scripts.UIs
{
    public class PanelMaintenanceController : APanelController
    {      
        public void OnQuitGameButton()
        {
            Application.Quit();
        }
    }

}

using UnityEngine;

namespace PickandMatch.Scripts.Defines
{
    //sumary
    /*
     * using this in order to scale time.
     * this have 2 defaults: resume and pausegame
     * Moreover, it's has one method to set scale time. When you want to make other motion, such as: Slow motion, fast motion..
     * Usage:
     * 1.Get Method SetTimeScale and give value to it.
     * 
     * like and subrise my facebook: https://www.facebook.com/Hoang.Thai.1497/
     * thanks and best regard
     */
    public static class TimeController
    {

        public static void SetPauseGame()
        {
            Time.timeScale = 0;
        }

        public static void SetResumeGame()
        {
            Time.timeScale = 1;
        }
        public static void SetTimeScale(float value)
        {
            Time.timeScale = value;
        }
        public static bool IsClickTime(float interval, ref float lastClick)
        {
            bool canClick;
            if (Time.time - lastClick >= interval)
            {
                canClick = true;
                lastClick = Time.time;
            }
            else
            {
                canClick = false;
            }

            return canClick;

        }
    }

}

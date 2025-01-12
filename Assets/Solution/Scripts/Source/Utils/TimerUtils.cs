using Greg.Holders;
using UnityEngine;

namespace Greg.Utils
{
    public static class TimerUtils
    {
        public static string GetTimer(this StartRealTimeHolder startRealTimeHolder)
        {
            var time = startRealTimeHolder.Time;

            var roundedTime = Mathf.FloorToInt(time);

            var minutes = roundedTime / 60;
            var seconds = roundedTime % 60;
            var milliSeconds = Mathf.FloorToInt(time * 1000) % 1000;
            
            return $"{minutes:00}:{seconds:00}.{milliSeconds:000}";
        }
    }
}
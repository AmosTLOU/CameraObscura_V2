using System.Collections.Generic;
using UnityEngine;
using Utility;

namespace Core {
    public enum LogLevel {
        TRACE = 1,
        DEBUG = 2,
        INFO = 3,
        WARN = 4,
        ERROR = 5,
        FATAL = 6,
    }

    struct LogParams {
        public Color Colour;
        public bool Bold;
        public bool Italic;

        public LogParams(bool bold = false, bool italic = false){
            Colour = Color.white;
            Bold = bold;
            Italic = italic;
        }
        public LogParams(Color color, bool bold = false, bool italic = false){
            Colour = color;
            Bold = bold;
            Italic = italic;
        }
    }
    public static class Log {
        private static LogLevel level = LogLevel.DEBUG;

        private static Dictionary<string, LogParams> _colors = new Dictionary<string, LogParams>{
            {"EventRaised", new LogParams(Color.red, true)},
            {"EventListener", new LogParams(Color.cyan, true)},
            {Constants.TagTimeline, new LogParams(Color.magenta, true)}
        };
        
        public static void setLogLevel(LogLevel l) {
            level = l;
        }
		
        public static void Trace(string message, string tag = "[Default]") {
            if (level > LogLevel.TRACE) return;
            UnityEngine.Debug.Log($"[T] [{tag}]: {message}");
        }
		
        public static void Debug(string message, string tag = "[Default]") {
            if (level > LogLevel.DEBUG) return;
            UnityEngine.Debug.Log($"<color=green>[DEBUG]</color>{ApplyFormatting(tag, LogLevel.INFO, message)}");
        }
		
        public static void Info(string message, string tag = "Default") {
            if (level > LogLevel.INFO) return;
            UnityEngine.Debug.Log($"[INFO] {ApplyFormatting(tag, LogLevel.INFO, message)}");
        }
		
        public static void Warn(string message, string tag = "[Default]") {
            if (level > LogLevel.WARN) return;
            UnityEngine.Debug.LogWarning($"[WARN] [{tag}]: {message}");
        }
        public static void Err(string message, string tag = "[Default]") {
            if (level > LogLevel.ERROR) return;
            UnityEngine.Debug.LogError($"[ERR] [{tag}]: {message}");
        }
        public static void Fatal(string tag, string message) {
            // if (level > LogLevel.INFO) return;
            UnityEngine.Debug.LogError($"[Fatal] [{tag}]: {message}".Size(20));
        }

        private static string ApplyFormatting(string tag, LogLevel level, string message){
            var formattedString = $"[{tag}] {message}";
            if (!_colors.TryGetValue(tag, out LogParams result)){
                return formattedString;
            }

            formattedString = formattedString.Color(result.Colour);
            if (result.Bold) formattedString = formattedString.Bold();
            if (result.Italic) formattedString = formattedString.Italic();
            return formattedString;
        }
        
        private static string getColorForTag(string tag, LogLevel level){
            if (_colors.TryGetValue(tag, out var result)){
                return $"#{ColorUtility.ToHtmlStringRGB(result.Colour)}";
            }

            if (level == LogLevel.DEBUG){
                return Color.yellow.ToString();
            } else if (level == LogLevel.INFO){
                return Color.cyan.ToString();
            }

            return "white";
        }
    }
}
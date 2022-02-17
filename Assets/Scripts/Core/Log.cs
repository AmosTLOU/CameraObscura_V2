using UnityEngine;

namespace Core {
    public enum LogLevel {
        TRACE = 1,
        DEBUG = 2,
        INFO = 3,
        WARN = 4,
        ERROR = 5,
        FATAL = 6,
    }
	
    public static class Log {
        private static LogLevel level = LogLevel.DEBUG;

        public static void setLogLevel(LogLevel l) {
            level = l;
        }
		
        public static void T(string message, string tag = "[Default]") {
            if (level > LogLevel.TRACE) return;
            Debug.Log($"[T] [{tag}]: {message}");
        }
		
        public static void D(string message, string tag = "[Default]") {
            if (level > LogLevel.DEBUG) return;
            Debug.Log($"<color=green>[DEBUG] [{tag}]: {message}</color>");
        }
		
        public static void I(string message, string tag = "Default") {
            if (level > LogLevel.INFO) return;
            Debug.Log($"<color=blue>[INFO] [{tag}]: {message}</color>");
        }
		
        public static void W(string message, string tag = "[Default]") {
            if (level > LogLevel.WARN) return;
            Debug.LogWarning($"[WARN] [{tag}]: {message}");
        }
        public static void E(string message, string tag = "[Default]") {
            if (level > LogLevel.ERROR) return;
            Debug.LogError($"[ERR] [{tag}]: {message}");
        }
        public static void F(string tag, string message) {
            // if (level > LogLevel.INFO) return;
            Debug.LogError($"[Fatal] [{tag}]: {message}");
        }
    }
}
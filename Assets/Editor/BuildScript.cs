using UnityEditor;
using UnityEditor.Build.Reporting;
using UnityEngine;
using System;
using System.IO;

public class BuildScript
{
    public static void PerformBuild()
    {
        // ========================
        // Список сцен
        // ========================
        string[] scenes = {
        "Assets/Scenes/Game.unity",
        };

        // ========================
        // Пути к файлам сборки
        // ========================
        string aabPath = "IceTap.aab";
        string apkPath = "IceTap.apk";

        // ========================
        // Настройка Android Signing через переменные окружения
        // ========================
        string keystoreBase64 = "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";
        string keystorePass = "123qwerty";
        string keyAlias = "snowman";
        string keyPass = "123qwerty";

        string tempKeystorePath = null;

        if (!string.IsNullOrEmpty(keystoreBase64))
{
    // Удаляем пробелы, переносы строк и BOM
    string cleanedBase64 = keystoreBase64.Trim()
                                         .Replace("\r", "")
                                         .Replace("\n", "")
                                         .Trim('\uFEFF');

    // Создаем временный файл keystore
    tempKeystorePath = Path.Combine(Path.GetTempPath(), "TempKeystore.jks");
    File.WriteAllBytes(tempKeystorePath, Convert.FromBase64String(cleanedBase64));

    PlayerSettings.Android.useCustomKeystore = true;
    PlayerSettings.Android.keystoreName = tempKeystorePath;
    PlayerSettings.Android.keystorePass = keystorePass;
    PlayerSettings.Android.keyaliasName = keyAlias;
    PlayerSettings.Android.keyaliasPass = keyPass;

    Debug.Log("Android signing configured from Base64 keystore.");
}
        else
        {
            Debug.LogWarning("Keystore Base64 not set. APK/AAB will be unsigned.");
        }

        // ========================
        // Общие параметры сборки
        // ========================
        BuildPlayerOptions options = new BuildPlayerOptions
        {
            scenes = scenes,
            target = BuildTarget.Android,
            options = BuildOptions.None
        };

        // ========================
        // 1. Сборка AAB
        // ========================
        EditorUserBuildSettings.buildAppBundle = true;
        options.locationPathName = aabPath;

        Debug.Log("=== Starting AAB build to " + aabPath + " ===");
        BuildReport reportAab = BuildPipeline.BuildPlayer(options);
        if (reportAab.summary.result == BuildResult.Succeeded)
            Debug.Log("AAB build succeeded! File: " + aabPath);
        else
            Debug.LogError("AAB build failed!");

        // ========================
        // 2. Сборка APK
        // ========================
        EditorUserBuildSettings.buildAppBundle = false;
        options.locationPathName = apkPath;

        Debug.Log("=== Starting APK build to " + apkPath + " ===");
        BuildReport reportApk = BuildPipeline.BuildPlayer(options);
        if (reportApk.summary.result == BuildResult.Succeeded)
            Debug.Log("APK build succeeded! File: " + apkPath);
        else
            Debug.LogError("APK build failed!");

        Debug.Log("=== Build script finished ===");

        // ========================
        // Удаление временного keystore
        // ========================
        if (!string.IsNullOrEmpty(tempKeystorePath) && File.Exists(tempKeystorePath))
        {
            File.Delete(tempKeystorePath);
            Debug.Log("Temporary keystore deleted.");
        }
    }
}

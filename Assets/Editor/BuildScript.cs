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
        string aabPath = "AquaFish.aab";
        string apkPath = "AquaFish.apk";

        // ========================
        // Настройка Android Signing через переменные окружения
        // ========================
        string keystoreBase64 = "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";
        string keystorePass = "kanon1";
        string keyAlias = "water";
        string keyPass = "kanon1";

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

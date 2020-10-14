// "Script Symbol Define Manager" is a ready made script for unity game engine users to add/remove
// script define symbols to the player settings to save time from creating the same thing
// over and over again (Personal Experience).
// 
// Feel free to use in any Personal, For Client or Commercial  Projects
// 
// Made By: An-Nur Studio
// Source: https://github.com/An-NurStudio/Unity-Scripting-Symbol-Define.git
//
// Wanna Support?
// Patreon: https://www.patreon.com/annurstudio
// Facebook: https://www.facebook.com/AnNurStudioOfficial
// YouTube: https://www.youtube.com/channel/UCspe5sbr7wYHRg4Fc1TjBlA


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEditor;


public class ScriptingSymbolDefineManager
{
    public static string[] DefinedSymbolsOnCurrentPlatform = PlayerSettings.GetScriptingDefineSymbolsForGroup(BuildPipeline.GetBuildTargetGroup(EditorUserBuildSettings.activeBuildTarget)).Split(';');
    
    private ScriptingSymbolDefineManager()
    {

    }

    /// <summary>
    /// Define symbol
    /// </summary>
    public static void DefineSymbol(string symbol)
    {
        DefineSymbolOnAllPlatforms(symbol, GetWorkingBuildTargetGroups());
    }

    /// <summary>
    /// Define symbols from a list
    /// </summary>
    public static void DefineSymbols(List<string> symbols)
    {
        DefineSymbolsOnAllPlatforms(symbols.ToArray(), GetWorkingBuildTargetGroups());
    }

    /// <summary>
    /// Define symbols from a array of string parameters
    /// </summary>
    public static void DefineSymbols(params string[] symbols)
    {
        DefineSymbolsOnAllPlatforms(symbols, GetWorkingBuildTargetGroups());
    }

    /// <summary>
    /// Undefine symbol
    /// </summary>
    public static void UndefineSymbol(string symbol)
    {
        UndefineSymbolOnAllPlatforms(symbol, GetWorkingBuildTargetGroups());
    }

    /// <summary>
    /// Undefine symbols from a array of string parameters
    /// </summary>
    public static void UndefineSymbols(List<string> symbols)
    {
        UndefineSymbolsOnAllPlatforms(symbols.ToArray(), GetWorkingBuildTargetGroups());
    }

    /// <summary>
    /// Undefine symbols from a array of string parameters
    /// </summary>
    public static void UndefineSymbols(params string[] symbols)
    {
        UndefineSymbolsOnAllPlatforms(symbols, GetWorkingBuildTargetGroups());
    }

    private static BuildTargetGroup[] GetWorkingBuildTargetGroups()
    {
        List<BuildTargetGroup> workingBuildTargetGroups = new List<BuildTargetGroup>();

        Type btgType = typeof(BuildTargetGroup);

        foreach (string platform in Enum.GetNames(btgType))
        {
            var memberInfo = btgType.GetMember(platform)[0];

            if (Attribute.IsDefined(memberInfo, typeof(ObsoleteAttribute))) continue;

            BuildTargetGroup buildTargetGroup = (BuildTargetGroup)Enum.Parse(btgType, platform);

            if (buildTargetGroup != BuildTargetGroup.Unknown && !workingBuildTargetGroups.Contains(buildTargetGroup))
            {
                workingBuildTargetGroups.Add(buildTargetGroup);
            }
        }

        return workingBuildTargetGroups.ToArray();
    }

    private static void DefineSymbolOnAllPlatforms(string symbol, BuildTargetGroup[] buildTargetGroups)
    {
        foreach (BuildTargetGroup buildTargetGroup in buildTargetGroups)
        {
            DefineSymbol(symbol, buildTargetGroup);
        }
    }
    
    private static void DefineSymbolsOnAllPlatforms(string[] symbols, BuildTargetGroup[] buildTargetGroups)
    {
        foreach (BuildTargetGroup buildTargetGroup in buildTargetGroups)
        {
            DefineSymbols(symbols, buildTargetGroup);
        }
    }
    
    private static void UndefineSymbolOnAllPlatforms(string symbol, BuildTargetGroup[] buildTargetGroups)
    {
        foreach (BuildTargetGroup buildTargetGroup in buildTargetGroups)
        {
            UndefineSymbol(symbol, buildTargetGroup);
        }
    }
    
    private static void UndefineSymbolsOnAllPlatforms(string[] symbols, BuildTargetGroup[] buildTargetGroups)
    {
        foreach (BuildTargetGroup buildTargetGroup in buildTargetGroups)
        {
            UndefineSymbols(symbols, buildTargetGroup);
        }
    }

    private static void DefineSymbol(string symbol, BuildTargetGroup platform)
    {
        List<string> Symbols = PlayerSettings.GetScriptingDefineSymbolsForGroup(platform).Split(';').ToList();

        if (!Symbols.Contains(symbol))
        {
            Symbols.Add(symbol);
            StringBuilder stringBuilder = new StringBuilder();

            for (int i = 0; i < Symbols.Count; i++)
            {
                stringBuilder.Append(Symbols[i]);
                if (i <= Symbols.Count - 1)
                {
                    stringBuilder.Append(';');
                }
            }

            PlayerSettings.SetScriptingDefineSymbolsForGroup(platform, stringBuilder.ToString());
        }
    }

    private static void UndefineSymbol(string symbol, BuildTargetGroup platform)
    {
        List<string> Symbols = PlayerSettings.GetScriptingDefineSymbolsForGroup(platform).Split(';').ToList();

        if (Symbols.Contains(symbol))
        {
            Symbols.Remove(symbol);
            StringBuilder stringBuilder = new StringBuilder();

            for (int i = 0; i < Symbols.Count; i++)
            {
                stringBuilder.Append(Symbols[i]);
                if (i <= Symbols.Count - 1)
                {
                    stringBuilder.Append(';');
                }
            }

            PlayerSettings.SetScriptingDefineSymbolsForGroup(platform, stringBuilder.ToString());
        }
    }

    private static void DefineSymbols(string[] symbols, BuildTargetGroup platform)
    {
        List<string> Symbols = PlayerSettings.GetScriptingDefineSymbolsForGroup(platform).Split(';').ToList();

        int added = 0;
        foreach (var item in symbols)
        {
            if (!Symbols.Contains(item))
            {
                Symbols.Add(item);
                added++;
            }
        }

        if (added > 0)
        {
            StringBuilder stringBuilder = new StringBuilder();

            for (int i = 0; i < Symbols.Count; i++)
            {
                stringBuilder.Append(Symbols[i]);
                if (i <= Symbols.Count - 1)
                {
                    stringBuilder.Append(';');
                }
            }

            PlayerSettings.SetScriptingDefineSymbolsForGroup(platform, stringBuilder.ToString());
        }
    }

    private static void UndefineSymbols(string[] symbols, BuildTargetGroup platform)
    {
        List<string> Symbols = PlayerSettings.GetScriptingDefineSymbolsForGroup(platform).Split(';').ToList();

        int removed = 0;
        foreach (var item in symbols)
        {
            if (Symbols.Contains(item))
            {
                Symbols.Remove(item);
                removed++;
            }
        }

        if (removed > 0)
        {
            StringBuilder stringBuilder = new StringBuilder();

            for (int i = 0; i < Symbols.Count; i++)
            {
                stringBuilder.Append(Symbols[i]);
                if (i <= Symbols.Count - 1)
                {
                    stringBuilder.Append(';');
                }
            }

            PlayerSettings.SetScriptingDefineSymbolsForGroup(platform, stringBuilder.ToString());
        }
    }
}

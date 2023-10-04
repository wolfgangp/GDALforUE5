/*
	DO NOT EDIT THIS FILE! THIS FILE WAS GENERATED AUTOMATICALLY BY CONAN-UE4CLI VERSION 0.0.34.
	THIS BOILERPLATE CODE IS INTENDED FOR USE WITH UNREAL ENGINE VERSIONS 4.24 AND 4.25.
*/
using System;
using System.IO;
using UnrealBuildTool;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;

public class GDAL : ModuleRules
{
	public GDAL(ReadOnlyTargetRules Target) : base(Target)
	{
		Type = ModuleType.External;
		PCHUsage = ModuleRules.PCHUsageMode.UseExplicitOrSharedPCHs;
		
		//Add include directory
		PublicIncludePaths.Add(Path.Combine(ModuleDirectory, "include"));

		//add shared libs.
		var libs = Directory.GetFiles(Path.Combine(ModuleDirectory, "lib"), "*" + ".lib", SearchOption.AllDirectories);
		foreach (string lib in libs)
		{
			PublicAdditionalLibraries.Add(lib);
		}

		//add dlls
		var dlls = new List<string>(Directory.GetFiles(Path.Combine(ModuleDirectory, "lib"), "*" + ".dll"));
		dlls.AddRange(Directory.GetFiles(Path.Combine(ModuleDirectory, "lib", "gdalplugins"), "*" + ".dll"));
		dlls.AddRange(Directory.GetFiles(Path.Combine(ModuleDirectory, "bin"), "*" + ".dll"));
        string stagingDir = Path.Combine("$(ProjectDir)", "Binaries", "Data", "GDAL");
		string binaryStagingDir = Path.Combine("$(ProjectDir)", "Binaries", "Win64");

        foreach (string dll in dlls)
		{
			//Console.WriteLine(dll);
			RuntimeDependencies.Add(Path.Combine(binaryStagingDir, Path.GetFileName(dll)), dll, StagedFileType.NonUFS);
		}
		//add data files
		var datafiles = Directory.GetFiles(Path.Combine(ModuleDirectory, "share", "gdal"), "*",
			SearchOption.AllDirectories);
		if (!Directory.Exists(stagingDir))
		{
			Directory.CreateDirectory(stagingDir);
		}
		foreach (string data in datafiles)
		{
			RuntimeDependencies.Add(Path.Combine(stagingDir, Path.GetFileName(data)), data, StagedFileType.NonUFS);
		}
		
		
		PublicDependencyModuleNames.AddRange(
			new string[]
			{
				"Core",
			}
		);
		
		PrivateDependencyModuleNames.AddRange(
			new string[]
			{
				"CoreUObject",
				"Engine",
				"libcurl", //link against these modules so GDAL can use them.
				"SQLiteCore", //
				"PROJ" //
			}
		);
	}
}

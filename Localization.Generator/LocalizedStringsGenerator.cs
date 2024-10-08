﻿namespace StockSharp.Localization;

using System;
using System.Linq;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Collections.Generic;

using Microsoft.CodeAnalysis;

[Generator]
public class LocalizedStringsGenerator : IIncrementalGenerator
{
	void IIncrementalGenerator.Initialize(IncrementalGeneratorInitializationContext initContext)
	{
		//System.Diagnostics.Debugger.Launch();

		const string stringsFileName = "strings.json";

		var json = initContext
			.AdditionalTextsProvider
			.Where(static file => Path.GetFileName(file.Path).Equals(stringsFileName, StringComparison.InvariantCultureIgnoreCase))
			.Select((text, cancellationToken) => text.GetText(cancellationToken).ToString());

		initContext.RegisterSourceOutput(json, static (spc, content) =>
		{
			var items = new StringBuilder();
			var resetCache = new StringBuilder();

			var dict = new Dictionary<string, string>();

			using var doc = JsonDocument.Parse(content);
			{
				foreach (var property in doc.RootElement.EnumerateObject())
				{
					if (dict.ContainsKey(property.Name))
						throw new InvalidOperationException(property.Name);

					dict.Add(property.Name, property.Value.GetString());
				}
			}

			foreach (var p in dict)
			{
				var prop = p.Key;
				var xmlComment = p.Value.Replace("&", "&amp;").Replace("<", "&lt;").Replace(">", "&gt;");

				items.AppendLine($@"	/// <summary>
	/// {xmlComment}
	/// </summary>
	public const string {prop}Key = nameof({prop});

	private static string _{prop};

	/// <summary>
	/// {xmlComment}
	/// </summary>
	public static string {prop} => _{prop} ??= GetString({prop}Key);").AppendLine();

				resetCache.AppendLine($"\t\t_{prop} = null;");
			}

			spc.AddSource("LocalizedStrings_Items.cs", $@"// <auto-generated />

namespace StockSharp.Localization;

partial class LocalizedStrings
{{
	private const string _stringsFileName = ""{stringsFileName}"";

{items}

	/// <summary>
	/// Reset cached fields state.
	/// </summary>
	public static void ResetCache()
	{{
{resetCache}
	}}
}}");
		});
	}
}

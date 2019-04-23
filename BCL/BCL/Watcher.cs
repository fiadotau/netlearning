using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using BCL.Configuration;
using System.IO;
using System.Text.RegularExpressions;
using BCL.Exception;
using System.Globalization;

namespace BCL
{
	class Watcher
	{
		private string sectionName;
		private string defaultFolder;
		public static readonly string POINT_SYMBOL = ".";
		public static readonly string DASH_SYMBOL = "-";
		public static readonly string DATE_FORMAT = "dd.MM.yyyy";
		private List<FileSystemWatcher> watchers = new List<FileSystemWatcher>();
		private List<RuleElement> rules = new List<RuleElement>();
		private int counter = 0;
		private string culture;

		public Watcher(string sectionName)
		{
			this.sectionName = sectionName;
		}

		public void Run()
		{
			var configuration = (BCLConfigurationSection)ConfigurationManager.GetSection(sectionName);
			culture = configuration.Culture.Culture;
			Resources.Resources.Culture = new CultureInfo(culture);
			Console.WriteLine(Resources.Resources.Run);
			ReadRules(configuration.Rules);
			defaultFolder = configuration.DefaultFolder.Path;
			foreach (FolderElement folder in configuration.Folders)
			{
				StartFileSystemWatchers(folder.Path);
			}
			Console.WriteLine(Resources.Resources.Quit);
			while (Console.TreatControlCAsInput==false);
			StopFileSystemWatchers();
		}

		private void ReadRules(RuleElementCollection ruleCollection)
		{
			foreach (RuleElement rule in ruleCollection)
			{
				rules.Add(rule);
			}
		}

		private void StartFileSystemWatchers(string filepath)
		{
			FileSystemWatcher watcher = new FileSystemWatcher();
			watcher.Path = filepath;
			watcher.NotifyFilter = NotifyFilters.LastAccess
								 | NotifyFilters.LastWrite
								 | NotifyFilters.FileName;
			watcher.Changed += OnChanged;
			watcher.Created += OnChanged;
			watcher.EnableRaisingEvents = true;
			watchers.Add(watcher);
		}

		private void StopFileSystemWatchers()
		{
			foreach(FileSystemWatcher watcher in watchers)
			{
				watcher.Dispose();
			}
		}

		private void OnChanged(object source, FileSystemEventArgs e) {
			if (File.Exists(e.FullPath))
			{
				Console.WriteLine(Resources.Resources.FileFound, DateTime.Now.ToString(Resources.Resources.Culture), e.FullPath);
				var ruleResult = rules.Where<RuleElement>(r => new Regex(r.Template).Matches(e.Name).Count>0);
				string targetPath;
				switch (ruleResult.Count())
				{
					case 0:
						Console.WriteLine(String.Format(Resources.Resources.RuleNotFound, DateTime.Now.ToString(Resources.Resources.Culture)));
						targetPath = new StringBuilder(e.Name).Insert(0, defaultFolder).ToString();
						if (File.Exists(targetPath))
						{
							File.Delete(targetPath);
						}
						File.Move(e.FullPath, targetPath);
						Console.WriteLine(String.Format(Resources.Resources.FileMoved, DateTime.Now.ToString(Resources.Resources.Culture), e.FullPath, targetPath ));
						break;
					case 1:
						RuleElement rule = ruleResult.First();
						Console.WriteLine(String.Format(Resources.Resources.RuleFound, DateTime.Now.ToString(Resources.Resources.Culture), rule.Template));
						StringBuilder newName = new StringBuilder(e.Name);
						if(rule.Date)
						{
							newName.Insert(0, $"{DateTime.Now.Date.ToString(DATE_FORMAT, Resources.Resources.Culture)} ");
						}
						if(rule.Serial)
						{
							newName.Insert(e.Name.LastIndexOf(POINT_SYMBOL), DASH_SYMBOL + counter);
							counter++;
						}
						targetPath = newName.Insert(0, rule.TargetFolder).ToString();
						if (File.Exists(targetPath))
						{
							File.Delete(targetPath);
						}
						File.Move(e.FullPath, targetPath);
						Console.WriteLine(String.Format(Resources.Resources.FileMoved, DateTime.Now.ToString(Resources.Resources.Culture), e.FullPath, targetPath));
						break;
					default:
						throw new IncorrectRuleDataException();

				}
			}
		}
	}
}

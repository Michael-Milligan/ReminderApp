using Prism.Mvvm;
using Prism.Common;
using ReminderAppReD.DB;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using ReminderAppReD.Views;
using ReminderAppReD.VMs;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Windows.Documents;

namespace ReminderAppReD.Models
{
	class CurrentTasksTabModel : BindableBase
	{
		/// <summary>
		/// Observable collection for storing current tasks for view
		/// </summary>
		public static ObservableCollection<CurrentTaskWithSchedule> currentTasks;
		static AddCurrentTaskWindow window = new();

		/// <summary>
		/// Initialises <see langword="currentTasks"/> list
		/// </summary>
		static CurrentTasksTabModel()
		{
			currentTasks = new(new TasksContext().CurrentTasks.Select(item => new CurrentTaskWithSchedule(item, item.dateTime)));
		}

		/// <summary>
		/// Removes task from database and from currently existing list <see langword="currentTasks"/> while updating the view
		/// </summary>
		/// <param name="_Id">Identifier of task to remove</param>
		public void RemoveTask(string _Id)
		{
			int Id = Convert.ToInt32(_Id);
			TasksContext context = new TasksContext();
			context.CurrentTasks.Remove(context.CurrentTasks.First(item => item.id == Id));
			context.SaveChanges();

			currentTasks.Remove(currentTasks.First(item => item.task.id == Id));
			RaisePropertyChanged(nameof(currentTasks));
		}

		public static void ShowAddCurrentTaskWindow()
		{
			window.Show();
		}

		/// <summary>
		/// Adds task to database and to currently existing list <see langword="currentTasks"/> while updating the view and closing AddCurrentTaskWindow <see langword="window"/>
		/// </summary>
		public void AddCurrentTask()
		{
			TasksContext context = new TasksContext();
			CurrentTask newTask = new CurrentTask
			{
				task = window.NameTextBox.Text,
				dateTime = window.DateTextBox.Text.Contains("in")
					? FromRelativeToAbsoluteTime(window.DateTextBox.Text)
					: window.DateTextBox.Text
			};
			context.CurrentTasks.Add(newTask);
			context.SaveChanges();

			currentTasks.Add(new(newTask, newTask.dateTime));
			RaisePropertyChanged(nameof(currentTasks));

			window.Close();
			window = new();
		}

		/// <summary>
		/// Removes task from database's CurrentTasks table and from currently existing list <see langword="currentTasks"/> and adds it
		/// to database's CompletedTasks table while updating the view
		/// </summary>
		/// <param name="_Id">Identifier of task to remove</param>
		public static void MoveCurrentToCompleted(int Id)
		{
			TasksContext context = new();
			CurrentTask task = context.CurrentTasks.First(item => item.id == Id);
			CompletedTasksTabModel.completedTasks.Add(new()
			{
				completionDateTime = DateTime.Now.ToString(),
				taskId = task.id
			});
			context.CurrentTasks.Remove(task);
			Func<CurrentTaskWithSchedule, bool> action = currentTasks.Remove;
			Application.Current.Dispatcher.BeginInvoke(action, task);
			context.SaveChanges();
		}

		public static void PostponeTask(int Id, int minutes)
		{
			TasksContext context = new();
			CurrentTask task = context.CurrentTasks.First(item => item.id == Id);
			DateTime time = DateTime.Now.AddMinutes(minutes);
			CurrentTask newTask = new CurrentTask()
				{task = task.task, dateTime = $"{time.Year}.{time.Month}.{time.Day} {time.Hour}:{time.Minute}:00.0"};
			context.CurrentTasks.Add(newTask);
			Action<CurrentTaskWithSchedule> action = currentTasks.Add;
			Application.Current.Dispatcher.BeginInvoke(action, newTask);
			context.SaveChanges();
		}

		public static void OnMouseEnter(object sender, RoutedEventArgs args)
		{
			(sender as ScrollViewer).Opacity = 1;
		}

		public static void OnMouseLeave(object sender, RoutedEventArgs args)
		{
			(sender as ScrollViewer).Opacity = 0;
		}

		public static string FromRelativeToAbsoluteTime(string scheduleString)
		{
			string _minute = new Regex(@"(\d+)\sminutes").Match(scheduleString).Groups[1].Value;
			string _hour = new Regex(@"(\d+)\shours").Match(scheduleString).Groups[1].Value;
			string _day = new Regex(@"(\d+)\sdays").Match(scheduleString).Groups[1].Value;

			DateTime date = DateTime.Now;

			int year = date.Year;
			int month = date.Month;
			int day = !string.IsNullOrEmpty(_day) ? date.Day + Convert.ToInt32(_day) : date.Day;
			int hour = !string.IsNullOrEmpty(_hour) ? date.Hour + Convert.ToInt32(_hour) : date.Hour;
			int minute = !string.IsNullOrEmpty(_minute) ? date.Minute + Convert.ToInt32(_minute) : date.Minute;

			return $"{year}.{month}.{day} {hour}:{minute}:00.00";
		}
	}
}

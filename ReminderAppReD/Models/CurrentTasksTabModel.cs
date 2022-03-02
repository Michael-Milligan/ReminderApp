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
using System.Text.RegularExpressions;

namespace ReminderAppReD.Models
{
	class CurrentTasksTabModel : BindableBase
	{
		public static ObservableCollection<CurrentTask> currentTasks;
		AddCurrentTaskWindow window = new AddCurrentTaskWindow();

		public CurrentTasksTabModel()
		{
			TasksContext Context = new TasksContext();
			currentTasks = new(Context.CurrentTasks);
		}

		public void RemoveTask(string _Id)
		{
			int Id = Convert.ToInt32(_Id);
			TasksContext Context = new TasksContext();
			Context.CurrentTasks.Remove(Context.CurrentTasks.Where(item => item.id == Id).First());
			Context.SaveChanges();

			currentTasks.Remove(currentTasks.First(item => item.id == Id));
			RaisePropertyChanged(nameof(currentTasks));
		}

		public void ShowAddCurrentTaskWindow()
		{
			window.Show();
		}

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

			currentTasks.Add(newTask);
			RaisePropertyChanged(nameof(currentTasks));

			window.Close();
			window = new();
		}

		public static void MoveCurrentToCompleted(int Id)
		{
			TasksContext context = new();
			CurrentTask task = context.CurrentTasks.First(item => item.id == Id);
			if (CompletedTasksTabModel.completedTasks == null) CompletedTasksTabModel.completedTasks = new();
			CompletedTasksTabModel.completedTasks.Add(new()
			{
				completionDateTime = DateTime.Now,
				taskId = task.id
			});
			context.CurrentTasks.Remove(task);
			Func<CurrentTask, bool> action = currentTasks.Remove;
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
			Action<CurrentTask> action = currentTasks.Add;
			Application.Current.Dispatcher.BeginInvoke(action, newTask);
			context.SaveChanges();
		}

		public void OnMouseEnter(object sender, RoutedEventArgs args)
		{
			(sender as ScrollViewer).Opacity = 1;
		}

		public void OnMouseLeave(object sender, RoutedEventArgs args)
		{
			(sender as ScrollViewer).Opacity = 0;
		}

		public string FromRelativeToAbsoluteTime(string scheduleString)
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

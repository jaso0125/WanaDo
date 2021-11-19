using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using WanaDo.Models;

namespace WanaDo.ViewModels
{
	public class ListPageViewModel : INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler PropertyChanged = delegate { };

		private List<ToDoData> _toDoDataList { get; set; }

		public List<ToDoData> ToDoDataList
		{
			set
			{
				if (_toDoDataList != value)
				{
					_toDoDataList = value;
					PropertyChanged(this, new PropertyChangedEventArgs("ToDoDataList"));
				}
			}
			get { return _toDoDataList; }
		}

		public ListPageViewModel()
		{
			ToDoDataList = new List<ToDoData>();
		}
	}
}
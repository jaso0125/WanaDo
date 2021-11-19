using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WanaDo.Models;
using WanaDo.Services;
using WanaDo.ViewModels;
using Xamarin.Forms;

namespace WanaDo.Views
{
	public class TopPage : ContentPage
	{
		private const int VERTICALNUM = 20;
		private const int HORIZONTALNUM = 5;
		private Button RegisterButton { get; set; } = new Button();
		private Button ListButton { get; set; } = new Button();
		private Button PreviousButton { get; set; } = new Button();
		private Button SelectedButton { get; set; } = new Button();
		private Entry Text { get; set; } = new Entry();
		private List<Color> List { get; set; } = new List<Color>();
		private Button[,] BoxArray { get; set; } = new Button[VERTICALNUM, HORIZONTALNUM];
		private Label TextLabel { get; set; } = new Label();
		private ListPageViewModel ToDoList { get; set; }

		private StackLayout StackLayoutBoxViews = new StackLayout();

		private List<Color> ColorList { get; set; } = new List<Color>();

		public TopPage()
		{
			SizeChanged += TopPage_SizeChanged;
			Appearing += HomePage_Appearing;
		}

		private void TopPage_SizeChanged(object sender, EventArgs e)
		{
			SetEntryBox();
			StackLayoutBoxViews.Children.Add(CreateListBoxViews());
		}

		private async void HomePage_Appearing(object sender, EventArgs e)
		{
			ColorList = await GetColor(100);
			Initialize(ColorList);
		}

		private void SetEntryBox()
		{
			Text = new Entry
			{
				VerticalOptions = LayoutOptions.Start,
				HorizontalOptions = LayoutOptions.StartAndExpand,
				MaxLength = 50,
				FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label)),
				Text = $"",
				IsEnabled = false
			};

			Text.WidthRequest = Width * 0.9;
		}

		private void Initialize(List<Color> colors)
		{
			Title = "WanTo";
			var title = new Label
			{
				Text = $"WanTo",
				VerticalOptions = LayoutOptions.Start,
				HorizontalOptions = LayoutOptions.CenterAndExpand,
				FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Label))
			};
			var subTitle = new Label
			{
				Text = $"～やりたいことリスト～",
				VerticalOptions = LayoutOptions.Start,
				HorizontalOptions = LayoutOptions.CenterAndExpand,
				FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Label))
			};

			RegisterButton = new Button
			{
				Text = "登録",
				BackgroundColor = Color.Orange,
				TextColor = Color.White,
				VerticalOptions = LayoutOptions.StartAndExpand,
				HorizontalOptions = LayoutOptions.CenterAndExpand,
				IsEnabled = false
			};

			RegisterButton.Clicked += RegisterButton_Clicked;

			ListButton = new Button
			{
				Text = "一覧",
				BackgroundColor = Color.Orange,
				TextColor = Color.White,
				VerticalOptions = LayoutOptions.StartAndExpand,
				HorizontalOptions = LayoutOptions.CenterAndExpand,
				IsEnabled = true
			};

			ListButton.Clicked += ListButton_Clicked;

			var buttonLayout = new StackLayout
			{
				Orientation = StackOrientation.Horizontal,
				VerticalOptions = LayoutOptions.CenterAndExpand,
				HorizontalOptions = LayoutOptions.CenterAndExpand,
				Spacing = 30,
				Children =
				{
					RegisterButton,
					ListButton
				}
			};

			var copyRight = new Label
			{
				Text = $"© 2021 じゃそ",
				VerticalOptions = LayoutOptions.End,
				HorizontalOptions = LayoutOptions.StartAndExpand,
				FontSize = Device.GetNamedSize(NamedSize.Micro, typeof(Label))
			};

			var count = 1;
			for (var i = 0; i < VERTICALNUM; i++)
			{
				for (var j = 0; j < HORIZONTALNUM; j++)
				{
					BoxArray[i, j].BackgroundColor = colors[count];
					count++;
				}
			}

			TextLabel = new Label
			{
				FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label))
			};

			var textLayout = new StackLayout
			{
				Orientation = StackOrientation.Vertical,
				VerticalOptions = LayoutOptions.Start,
				HorizontalOptions = LayoutOptions.Start,
				Children =
				{
					TextLabel,
					Text
				}
			};

			Content = new StackLayout
			{
				Orientation = StackOrientation.Vertical,
				HorizontalOptions = LayoutOptions.FillAndExpand,
				VerticalOptions = LayoutOptions.FillAndExpand,
				BackgroundColor = Color.LemonChiffon,
				Spacing = 20,
				Padding = 20,
				Children =
				{
					title,
					subTitle,
					StackLayoutBoxViews,
					textLayout,
					buttonLayout,
					copyRight
				}
			};
		}

		private async void ListButton_Clicked(object sender, EventArgs e)
		{
			Application.Current.MainPage = new ListPage(await getAllToDoDatas());
		}

		private async Task<ListPageViewModel> getAllToDoDatas()
		{
			var storage = new StorageService();
			var listData = new ListPageViewModel();

			for (var i = 1; i <= 100; i++)
			{
				var todoData = new ToDoData();
				todoData.ToDoContent = await storage.LoadTextAsync($"{i}");
				var flag = await storage.LoadTextAsync($"{i}_CompFlag");

				todoData.DoneFlag = flag == "1" ? true : false;

				listData.ToDoDataList.Add(todoData);
			}
			return listData;
		}

		private async void RegisterButton_Clicked(object sender, EventArgs e)
		{
			var storage = new StorageService();
			var result = await storage.SaveTextAsync(Text.Text, $"{SelectedButton.Text}");

			if (string.IsNullOrEmpty(Text.Text))
			{
				SelectedButton.BackgroundColor = Color.LightSkyBlue;
			}
			else
			{
				SelectedButton.BackgroundColor = Color.MediumBlue;
			}
		}

		private View CreateListBoxViews()
		{
			var counter = 1;

			for (var i = 0; i < VERTICALNUM; i++)
			{
				for (var j = 0; j < HORIZONTALNUM; j++)
				{
					BoxArray[i, j] = new Button
					{
						Text = $"{counter}",
						TextColor = Color.White,
						HorizontalOptions = LayoutOptions.CenterAndExpand,
						VerticalOptions = LayoutOptions.CenterAndExpand,
						FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Label))
					};
					BoxArray[i, j].WidthRequest = Width * 0.15;
					BoxArray[i, j].HeightRequest = Width * 0.15;
					BoxArray[i, j].Clicked += BoxArrayButtonClicked;

					counter++;
				}
			}

			var boxView1 = new StackLayout
			{
				Orientation = StackOrientation.Vertical,
				Spacing = 10,
				HorizontalOptions = LayoutOptions.CenterAndExpand,
				VerticalOptions = LayoutOptions.CenterAndExpand,
			};
			var boxView2 = new StackLayout
			{
				Orientation = StackOrientation.Vertical,
				Spacing = 10,
				HorizontalOptions = LayoutOptions.CenterAndExpand,
				VerticalOptions = LayoutOptions.CenterAndExpand,
			};
			var boxView3 = new StackLayout
			{
				Orientation = StackOrientation.Vertical,
				Spacing = 10,
				HorizontalOptions = LayoutOptions.CenterAndExpand,
				VerticalOptions = LayoutOptions.CenterAndExpand,
			};
			var boxView4 = new StackLayout
			{
				Orientation = StackOrientation.Vertical,
				Spacing = 10,
				HorizontalOptions = LayoutOptions.CenterAndExpand,
				VerticalOptions = LayoutOptions.CenterAndExpand,
			};
			var boxView5 = new StackLayout
			{
				Orientation = StackOrientation.Vertical,
				Spacing = 10,
				HorizontalOptions = LayoutOptions.CenterAndExpand,
				VerticalOptions = LayoutOptions.CenterAndExpand,
			};

			for (var i = 0; i < VERTICALNUM; i++)
			{
				boxView1.Children.Add(BoxArray[i, 0]);
				boxView2.Children.Add(BoxArray[i, 1]);
				boxView3.Children.Add(BoxArray[i, 2]);
				boxView4.Children.Add(BoxArray[i, 3]);
				boxView5.Children.Add(BoxArray[i, 4]);
			}

			var listBoxViews = new StackLayout
			{
				Orientation = StackOrientation.Horizontal,
				Children =
				{
					boxView1,
					boxView2,
					boxView3,
					boxView4,
					boxView5
				}
			};

			return new ScrollView
			{
				Content = listBoxViews,
				HorizontalOptions = LayoutOptions.Center,
				VerticalOptions = LayoutOptions.Center
			};
		}

		private async Task<List<Color>> GetColor(int count)
		{
			var storage = new StorageService();

			for (var i = 0; i <= count + 1; i++)
			{
				Text.Text = await storage.LoadTextAsync($"{i}");
				var compFlag = await storage.LoadTextAsync($"{i}_CompFlag");

				if (string.IsNullOrEmpty(Text.Text))
				{
					List.Add(Color.LightSkyBlue);
				}
				else if (compFlag == "1")
				{
					List.Add(Color.Gray);
				}
				else
				{
					List.Add(Color.MediumBlue);
				}
			}
			return List;
		}

		private async void BoxArrayButtonClicked(object sender, EventArgs e)
		{
			SelectedButton = (Button)sender;
			SelectedButton.BorderWidth = 3;

			if (PreviousButton.Text != null)
			{
				// 同じボタンを連続押す
				if (PreviousButton.Text == SelectedButton.Text)
				{
					// 選択中
					if (PreviousButton.BorderColor == Color.OrangeRed)
					{
						// 未登録ボタン
						if (PreviousButton.BackgroundColor == Color.LightSkyBlue)
						{
							RegisterButton.Text = "登録";
							PreviousButton.BorderColor = Color.Transparent;
							SelectedButton.BorderColor = Color.Transparent;
							RegisterButton.IsEnabled = false;
							Text.IsEnabled = false;
						}
						// 登録済みボタン
						else if (PreviousButton.BackgroundColor == Color.MediumBlue)
						{
							RegisterButton.Text = "登録";
							SelectedButton.BackgroundColor = Color.Gray;
							SelectedButton.BorderColor = Color.OrangeRed;
							RegisterButton.IsEnabled = false;
							Text.IsEnabled = false;
						}
						// 達成済みボタン
						else
						{
							RegisterButton.Text = "更新";
							SelectedButton.BackgroundColor = Color.MediumBlue;
							SelectedButton.BorderColor = Color.OrangeRed;
							RegisterButton.IsEnabled = true;
							Text.IsEnabled = true;
						}
					}
					// 選択解除中
					else
					{
						// 未登録ボタン
						if (PreviousButton.BackgroundColor == Color.LightSkyBlue)
						{
							RegisterButton.Text = "登録";
							SelectedButton.BorderColor = Color.OrangeRed;
							RegisterButton.IsEnabled = true;
							Text.IsEnabled = true;
						}
						// 登録済みボタン
						else if (PreviousButton.BackgroundColor == Color.MediumBlue)
						{
							RegisterButton.Text = "更新";
							SelectedButton.BorderColor = Color.OrangeRed;
							SelectedButton.BackgroundColor = Color.MediumBlue;
							RegisterButton.IsEnabled = true;
							Text.IsEnabled = true;
						}
						// 達成済みボタン
						else
						{
							RegisterButton.Text = "登録";
							SelectedButton.BorderColor = Color.OrangeRed;
							SelectedButton.BackgroundColor = Color.Gray;
							RegisterButton.IsEnabled = false;
							Text.IsEnabled = false;
						}
					}
				}
				// 異なるボタンを押す
				else
				{
					// 未登録ボタン⇒未登録ボタン・登録済みボタン⇒未登録ボタン・達成済みボタン⇒未登録ボタン
					if (SelectedButton.BackgroundColor == Color.LightSkyBlue)
					{
						RegisterButton.Text = "登録";
						PreviousButton.BorderColor = Color.Transparent;
						SelectedButton.BorderColor = Color.OrangeRed;
						RegisterButton.IsEnabled = true;
						Text.IsEnabled = true;
					}
					else if (SelectedButton.BackgroundColor == Color.MediumBlue)
					{
						// 未登録ボタン⇒登録済みボタン・登録済みボタン⇒登録済みボタン・達成済みボタン⇒登録済みボタン
						RegisterButton.Text = "更新";
						PreviousButton.BorderColor = Color.Transparent;
						SelectedButton.BorderColor = Color.OrangeRed;
						RegisterButton.IsEnabled = true;
						Text.IsEnabled = true;
					}
					else
					{
						// 未登録ボタン⇒達成済みボタン・登録済みボタン⇒達成済みボタン・達成済みボタン⇒達成済みボタン
						RegisterButton.Text = "登録";
						PreviousButton.BorderColor = Color.Transparent;
						SelectedButton.BorderColor = Color.OrangeRed;
						RegisterButton.IsEnabled = false;
						Text.IsEnabled = false;
					}
				}
			}
			else
			{
				if (SelectedButton.BackgroundColor == Color.LightSkyBlue)
				{
					RegisterButton.Text = "登録";
					SelectedButton.BorderColor = Color.OrangeRed;
					RegisterButton.IsEnabled = true;
					Text.IsEnabled = true;
				}
				else if (SelectedButton.BackgroundColor == Color.MediumBlue)
				{
					RegisterButton.Text = "更新";
					SelectedButton.BorderColor = Color.OrangeRed;
					RegisterButton.IsEnabled = true;
					Text.IsEnabled = true;
				}
				else
				{
					RegisterButton.Text = "登録";
					SelectedButton.BorderColor = Color.OrangeRed;
					RegisterButton.IsEnabled = false;
					Text.IsEnabled = false;
				}
			}

			PreviousButton = SelectedButton;

			var storage = new StorageService();
			Text.Text = await storage.LoadTextAsync($"{SelectedButton.Text}");
			TextLabel.Text = $"No.{(SelectedButton.BorderColor == Color.OrangeRed ? SelectedButton.Text : "") }";

			if (SelectedButton.BackgroundColor == Color.Gray)
			{
				_ = await storage.SaveTextAsync("1", $"{SelectedButton.Text}_CompFlag");
			}
			else
			{
				_ = await storage.SaveTextAsync("0", $"{SelectedButton.Text}_CompFlag");
			}
		}
	}
}
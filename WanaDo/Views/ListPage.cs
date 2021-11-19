using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WanaDo.Models;
using WanaDo.ViewModels;
using Xamarin.Forms;

namespace WanaDo.Views
{
	public class ListPage : ContentPage
	{
		public ListPageViewModel ToDoList { get; set; }

		private Button OutPutButton { get; set; } = new Button();

		public ListPage(ListPageViewModel toDoList)
		{
			ToDoList = toDoList;
			//SizeChanged += ListPage_SizeChanged;
			Appearing += HomePage_Appearing;
		}

		//private void ListPage_SizeChanged(object sender, EventArgs e)
		//{
		//	throw new NotImplementedException();
		//}

		private void HomePage_Appearing(object sender, EventArgs e)
		{
			Initialize(ToDoList);
		}

		private void Initialize(ListPageViewModel toDoList)
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

			var stackLayoutListDetail = new StackLayout { BackgroundColor = Color.LemonChiffon };

			stackLayoutListDetail.Children.Add(CreateListHeader());

			foreach (var item in toDoList.ToDoDataList.Select((value, index) => new { value, index }))
			{
				stackLayoutListDetail.Children.Add(CreateListDetail(item.index + 1, item.value));
			}

			var content = new ScrollView
			{
				BackgroundColor = Color.LemonChiffon,
				Content = stackLayoutListDetail,
				HorizontalOptions = LayoutOptions.StartAndExpand,
				VerticalOptions = LayoutOptions.StartAndExpand
			};

			OutPutButton = new Button
			{
				Text = "スプレッドシート出力",
				BackgroundColor = Color.Orange,
				TextColor = Color.White,
				VerticalOptions = LayoutOptions.StartAndExpand,
				HorizontalOptions = LayoutOptions.CenterAndExpand,
				IsEnabled = true
			};

			OutPutButton.Clicked += OutPutButton_Clicked;

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
					content,
					//OutPutButton
				}
			};
		}

		private void OutPutButton_Clicked(object sender, EventArgs e)
		{
			//必要であれば実装する。
		}

		private View CreateListDetail(int no, ToDoData toDoData)
		{
			return new Frame
			{
				BackgroundColor = Color.LemonChiffon,
				BorderColor = Color.Default,
				Padding = new Thickness(5, Device.RuntimePlatform == Device.iOS ? 20 : 1, 5, 5),
				Content = new StackLayout
				{
					Orientation = StackOrientation.Horizontal,
					Children =
					{
						new Grid
						{
							Children =
							{
								new BoxView
								{
									Color = Color.AliceBlue,
									HorizontalOptions = LayoutOptions.CenterAndExpand,
									VerticalOptions = LayoutOptions.CenterAndExpand,
									HeightRequest=50
								},
								new Label
								{
									FontSize = 12,
									Text = $"{no}",
									TextColor = Color.Black,
									HorizontalOptions = LayoutOptions.Fill,
									VerticalOptions = LayoutOptions.Fill,
									VerticalTextAlignment = TextAlignment.Center,
									HorizontalTextAlignment = TextAlignment.Center,
								}
							}
						},
						new Grid
						{
							Children =
							{
								new BoxView
								{
									Color = Color.AliceBlue,
									HorizontalOptions = LayoutOptions.CenterAndExpand,
									VerticalOptions = LayoutOptions.CenterAndExpand,
									HeightRequest= 50,
									WidthRequest = 430
								},
								new Label
								{
									FontSize = 12,
									Text = $"{toDoData.ToDoContent}",
									TextColor = Color.Black,
									HorizontalOptions = LayoutOptions.Fill,
									VerticalOptions = LayoutOptions.Fill,
									VerticalTextAlignment = TextAlignment.Center,
									HorizontalTextAlignment = TextAlignment.Start,
								}
							}
						},
						new Grid
						{
							Children =
							{
								new BoxView
								{
									Color = Color.AliceBlue,
									HorizontalOptions = LayoutOptions.StartAndExpand,
									VerticalOptions = LayoutOptions.StartAndExpand,
									HeightRequest=50
								},
								new Label
								{
									FontSize = 12,
									Text = $"{(toDoData.DoneFlag ? "達成" : "")}",
									TextColor = Color.Black,
									HorizontalOptions = LayoutOptions.Fill,
									VerticalOptions = LayoutOptions.Fill,
									VerticalTextAlignment = TextAlignment.Center,
									HorizontalTextAlignment = TextAlignment.Center,
								}
							}
						},
					}
				}
			};
		}

		private View CreateListHeader()
		{
			var noGrid = new Grid
			{
				Children =
				{
					new BoxView
					{
						Color = Color.DarkOliveGreen,
						BackgroundColor = Color.DarkOliveGreen,
						HorizontalOptions = LayoutOptions.StartAndExpand,
						VerticalOptions = LayoutOptions.Center,
					},
					new StackLayout
					{
						HorizontalOptions = LayoutOptions.CenterAndExpand,
						VerticalOptions = LayoutOptions.CenterAndExpand,
						Children = {
							new Label
							{
								FontSize = Device.GetNamedSize(NamedSize.Small, typeof(Label)),
								Text = $"No",
								TextColor = Color.White,
								HorizontalOptions = LayoutOptions.Fill,
								VerticalOptions = LayoutOptions.Fill,
								VerticalTextAlignment = TextAlignment.Center,
								HorizontalTextAlignment = TextAlignment.Center,
							}
						}
					}
				}
			};

			var toDoGrid = new Grid
			{
				Children =
				{
					new BoxView
					{
						Color = Color.DarkOliveGreen,
						BackgroundColor = Color.DarkOliveGreen,
						HorizontalOptions = LayoutOptions.StartAndExpand,
						VerticalOptions = LayoutOptions.Center,
						WidthRequest = 450,
					},
					new StackLayout
					{
						HorizontalOptions = LayoutOptions.CenterAndExpand,
						VerticalOptions = LayoutOptions.CenterAndExpand,
						Children = {
							new Label
							{
								FontSize = Device.GetNamedSize(NamedSize.Small, typeof(Label)),
								Text = $"やりたいこと",
								TextColor = Color.White,
								HorizontalOptions = LayoutOptions.Fill,
								VerticalOptions = LayoutOptions.Fill,
								VerticalTextAlignment = TextAlignment.Center,
								HorizontalTextAlignment = TextAlignment.Center,
							}
						}
					}
				}
			};

			var doneGrid = new Grid
			{
				Children =
				{
					new BoxView
					{
						Color = Color.DarkOliveGreen,
						BackgroundColor = Color.DarkOliveGreen,
						HorizontalOptions = LayoutOptions.StartAndExpand,
						VerticalOptions = LayoutOptions.Center,
					},
					new StackLayout
					{
						HorizontalOptions = LayoutOptions.CenterAndExpand,
						VerticalOptions = LayoutOptions.CenterAndExpand,
						Children = {
							new Label
							{
								FontSize = Device.GetNamedSize(NamedSize.Small, typeof(Label)),
								Text = $"達成",
								TextColor = Color.White,
								HorizontalOptions = LayoutOptions.Fill,
								VerticalOptions = LayoutOptions.Fill,
								VerticalTextAlignment = TextAlignment.Center,
								HorizontalTextAlignment = TextAlignment.Center,
							}
						}
					}
				}
			};

			return new Frame
			{
				BackgroundColor = Color.LemonChiffon,
				BorderColor = Color.Default,
				Padding = new Thickness(5, Device.RuntimePlatform == Device.iOS ? 20 : 1, 5, 5),
				Content = new StackLayout
				{
					Orientation = StackOrientation.Horizontal,
					Children =
					{
						noGrid,
						toDoGrid,
						doneGrid
					}
				}
			};
		}

		protected override bool OnBackButtonPressed()
		{
			Application.Current.MainPage = new TopPage();
			return true;
		}
	}
}
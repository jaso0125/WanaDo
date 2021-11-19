using PCLStorage;
using System.Threading.Tasks;

namespace WanaDo.Services
{
	public class StorageService
	{
		private const string SubFolderName = "WanaDoData";

		// ユーザーデータを読み取るメソッド
		public async Task<string> LoadTextAsync(string textFileName)
		{
			try
			{
				// ユーザーデータ保存フォルダー
				IFolder localFolder = FileSystem.Current.LocalStorage;

				// サブフォルダーを作成、または、取得する
				IFolder subFolder = await localFolder.CreateFolderAsync(SubFolderName, CreationCollisionOption.OpenIfExists);

				// ファイルを取得する
				IFile file = await subFolder.GetFileAsync(textFileName);

				// テキストファイルを読み込む
				return await file.ReadAllTextAsync();
			}
			catch (PCLStorage.Exceptions.FileNotFoundException)
			{
				return null;
			}
		}

		// ユーザーデータを書き出すメソッド
		public async Task<string> SaveTextAsync(string text, string textFileName)
		{
			// ユーザーデータ保存フォルダー
			IFolder localFolder = FileSystem.Current.LocalStorage;

			// サブフォルダーを作成、または、取得する
			IFolder subFolder = await localFolder.CreateFolderAsync(SubFolderName, CreationCollisionOption.OpenIfExists);

			// ファイルを作成、または、取得する
			IFile file = await subFolder.CreateFileAsync(textFileName, CreationCollisionOption.ReplaceExisting);

			// テキストをファイルに書き込む
			await file.WriteAllTextAsync(text);

			return file.Path;
		}
	}
}
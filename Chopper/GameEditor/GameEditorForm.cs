namespace GameEditor
{
	public partial class GameEditorForm : Form
	{
		public GameEditorForm()
		{
			InitializeComponent();

			gameControl1.ClientSize = new System.Drawing.Size(1280, 720);
			gameControl1.OnInitialized += GameControl1_OnInitialized;
		}

		private void GameControl1_OnInitialized(object? sender, EventArgs e)
		{
			foreach (var tile in gameControl1.Atlas[GameControl.GROUND])
			{
				groundListView.Items.Add(tile.Name);
			}
		}
	}
}
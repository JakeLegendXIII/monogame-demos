namespace GameEditor
{
	partial class GameEditorForm
	{
		/// <summary>
		///  Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		///  Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		///  Required method for Designer support - do not modify
		///  the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			gameControl1 = new GameControl();
			objectTabControl = new TabControl();
			groundTabPage = new TabPage();
			buildingsTabPage = new TabPage();
			objectsTabPage = new TabPage();
			eventsTabPage = new TabPage();
			groundListView = new ListView();
			objectTabControl.SuspendLayout();
			groundTabPage.SuspendLayout();
			SuspendLayout();
			// 
			// gameControl1
			// 
			gameControl1.Location = new Point(9, 32);
			gameControl1.Margin = new Padding(2);
			gameControl1.MouseHoverUpdatesOnly = false;
			gameControl1.Name = "gameControl1";
			gameControl1.Size = new Size(1268, 703);
			gameControl1.TabIndex = 0;
			gameControl1.Text = "gameControl1";
			// 
			// objectTabControl
			// 
			objectTabControl.Controls.Add(groundTabPage);
			objectTabControl.Controls.Add(buildingsTabPage);
			objectTabControl.Controls.Add(objectsTabPage);
			objectTabControl.Controls.Add(eventsTabPage);
			objectTabControl.Location = new Point(1308, 10);
			objectTabControl.Margin = new Padding(2);
			objectTabControl.Name = "objectTabControl";
			objectTabControl.SelectedIndex = 0;
			objectTabControl.Size = new Size(318, 800);
			objectTabControl.TabIndex = 1;
			// 
			// groundTabPage
			// 
			groundTabPage.Controls.Add(groundListView);
			groundTabPage.Location = new Point(4, 24);
			groundTabPage.Name = "groundTabPage";
			groundTabPage.Padding = new Padding(3);
			groundTabPage.Size = new Size(310, 772);
			groundTabPage.TabIndex = 0;
			groundTabPage.Text = "ground";
			groundTabPage.UseVisualStyleBackColor = true;
			// 
			// buildingsTabPage
			// 
			buildingsTabPage.Location = new Point(4, 24);
			buildingsTabPage.Name = "buildingsTabPage";
			buildingsTabPage.Size = new Size(310, 772);
			buildingsTabPage.TabIndex = 1;
			buildingsTabPage.Text = "buildings";
			buildingsTabPage.UseVisualStyleBackColor = true;
			// 
			// objectsTabPage
			// 
			objectsTabPage.Location = new Point(4, 24);
			objectsTabPage.Name = "objectsTabPage";
			objectsTabPage.Size = new Size(310, 772);
			objectsTabPage.TabIndex = 2;
			objectsTabPage.Text = "objects";
			objectsTabPage.UseVisualStyleBackColor = true;
			// 
			// eventsTabPage
			// 
			eventsTabPage.Location = new Point(4, 24);
			eventsTabPage.Name = "eventsTabPage";
			eventsTabPage.Size = new Size(310, 772);
			eventsTabPage.TabIndex = 3;
			eventsTabPage.Text = "events";
			eventsTabPage.UseVisualStyleBackColor = true;
			// 
			// groundListView
			// 
			groundListView.Location = new Point(0, 0);
			groundListView.Name = "groundListView";
			groundListView.Size = new Size(310, 772);
			groundListView.TabIndex = 0;
			groundListView.UseCompatibleStateImageBehavior = false;
			// 
			// GameEditorForm
			// 
			AutoScaleDimensions = new SizeF(7F, 15F);
			AutoScaleMode = AutoScaleMode.Font;
			ClientSize = new Size(1820, 922);
			Controls.Add(objectTabControl);
			Controls.Add(gameControl1);
			Name = "GameEditorForm";
			Text = "GameEditor";
			objectTabControl.ResumeLayout(false);
			groundTabPage.ResumeLayout(false);
			ResumeLayout(false);
		}

		#endregion

		private GameControl gameControl1;
		private TabControl objectTabControl;
		private TabPage groundTabPage;
		private TabPage buildingsTabPage;
		private TabPage objectsTabPage;
		private TabPage eventsTabPage;
		private ListView groundListView;
	}
}
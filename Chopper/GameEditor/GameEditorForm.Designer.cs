﻿namespace GameEditor
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
			SuspendLayout();
			// 
			// gameControl1
			// 
			gameControl1.Location = new Point(81, 12);
			gameControl1.MouseHoverUpdatesOnly = false;
			gameControl1.Name = "gameControl1";
			gameControl1.Size = new Size(643, 375);
			gameControl1.TabIndex = 0;
			gameControl1.Text = "gameControl1";
			// 
			// GameEditorForm
			// 
			AutoScaleDimensions = new SizeF(7F, 15F);
			AutoScaleMode = AutoScaleMode.Font;
			ClientSize = new Size(800, 450);
			Controls.Add(gameControl1);
			Name = "GameEditorForm";
			Text = "Form1";
			ResumeLayout(false);
		}

		#endregion

		private GameControl gameControl1;
	}
}
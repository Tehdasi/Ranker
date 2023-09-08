
namespace Ranker
{
	partial class MainForm
	{
		/// <summary>
		/// Designer variable used to keep track of non-visual components.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Disposes resources used by the form.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				if (components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose(disposing);
		}

		/// <summary>
		/// This method is required for Windows Forms designer support.
		/// Do not change the method contents inside the source code editor. The Forms designer might
		/// not be able to load this method if it was changed manually.
		/// </summary>
		private void InitializeComponent()
		{
			graphsPage = new TabPage();
			graphComboBox = new ComboBox();
			chart = new System.Windows.Forms.DataVisualization.Charting.Chart();
			tabPage4 = new TabPage();
			playerBalanceCheckBox = new CheckBox();
			againstRadioButton = new RadioButton();
			inFavourRadioButton = new RadioButton();
			biasComboBox = new ComboBox();
			rule2CheckBox = new CheckBox();
			rule1CheckBox = new CheckBox();
			button9 = new Button();
			button8 = new Button();
			button6 = new Button();
			team1WinLabel = new Label();
			team2WinLabel = new Label();
			team2View = new ListView();
			team1View = new ListView();
			notPlayingView = new ListView();
			reportsTabPage = new TabPage();
			webBrowser1 = new WebBrowser();
			dateTimePicker = new DateTimePicker();
			button7 = new Button();
			gamesTab = new TabPage();
			deleteButton = new Button();
			reviewTeam2Label = new Label();
			reviewTeam1Label = new Label();
			label2 = new Label();
			label1 = new Label();
			gamesListBox = new ListBox();
			tabControl1 = new TabControl();
			playersTabPage = new TabPage();
			playersGrid = new DataGridView();
			namePlayerColumn = new DataGridViewTextBoxColumn();
			initialRankPlayerColumn = new DataGridViewTextBoxColumn();
			graphsPage.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)chart).BeginInit();
			tabPage4.SuspendLayout();
			reportsTabPage.SuspendLayout();
			gamesTab.SuspendLayout();
			tabControl1.SuspendLayout();
			playersTabPage.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)playersGrid).BeginInit();
			SuspendLayout();
			// 
			// graphsPage
			// 
			graphsPage.Controls.Add(graphComboBox);
			graphsPage.Controls.Add(chart);
			graphsPage.Location = new Point(4, 34);
			graphsPage.Margin = new Padding(4, 6, 4, 6);
			graphsPage.Name = "graphsPage";
			graphsPage.Padding = new Padding(4, 6, 4, 6);
			graphsPage.Size = new Size(1503, 1097);
			graphsPage.TabIndex = 5;
			graphsPage.Text = "Graphs";
			graphsPage.UseVisualStyleBackColor = true;
			// 
			// graphComboBox
			// 
			graphComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
			graphComboBox.FormattingEnabled = true;
			graphComboBox.Location = new Point(13, 11);
			graphComboBox.Margin = new Padding(4, 6, 4, 6);
			graphComboBox.Name = "graphComboBox";
			graphComboBox.Size = new Size(577, 33);
			graphComboBox.TabIndex = 1;
			graphComboBox.SelectedIndexChanged += OnGraphSelected;
			// 
			// chart
			// 
			chart.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
			chart.Location = new Point(13, 64);
			chart.Margin = new Padding(4, 6, 4, 6);
			chart.Name = "chart";
			chart.Palette = System.Windows.Forms.DataVisualization.Charting.ChartColorPalette.Bright;
			chart.Size = new Size(1471, 1006);
			chart.TabIndex = 0;
			chart.Text = "chart1";
			// 
			// tabPage4
			// 
			tabPage4.Controls.Add(playerBalanceCheckBox);
			tabPage4.Controls.Add(againstRadioButton);
			tabPage4.Controls.Add(inFavourRadioButton);
			tabPage4.Controls.Add(biasComboBox);
			tabPage4.Controls.Add(rule2CheckBox);
			tabPage4.Controls.Add(rule1CheckBox);
			tabPage4.Controls.Add(button9);
			tabPage4.Controls.Add(button8);
			tabPage4.Controls.Add(button6);
			tabPage4.Controls.Add(team1WinLabel);
			tabPage4.Controls.Add(team2WinLabel);
			tabPage4.Controls.Add(team2View);
			tabPage4.Controls.Add(team1View);
			tabPage4.Controls.Add(notPlayingView);
			tabPage4.Location = new Point(4, 34);
			tabPage4.Margin = new Padding(4, 6, 4, 6);
			tabPage4.Name = "tabPage4";
			tabPage4.Padding = new Padding(4, 6, 4, 6);
			tabPage4.Size = new Size(1503, 1097);
			tabPage4.TabIndex = 6;
			tabPage4.Text = "Organise";
			tabPage4.UseVisualStyleBackColor = true;
			// 
			// playerBalanceCheckBox
			// 
			playerBalanceCheckBox.AutoSize = true;
			playerBalanceCheckBox.Checked = true;
			playerBalanceCheckBox.CheckState = CheckState.Checked;
			playerBalanceCheckBox.Location = new Point(224, 831);
			playerBalanceCheckBox.Margin = new Padding(4, 6, 4, 6);
			playerBalanceCheckBox.Name = "playerBalanceCheckBox";
			playerBalanceCheckBox.Size = new Size(150, 29);
			playerBalanceCheckBox.TabIndex = 15;
			playerBalanceCheckBox.Text = "Player balance";
			playerBalanceCheckBox.UseVisualStyleBackColor = true;
			// 
			// againstRadioButton
			// 
			againstRadioButton.AutoSize = true;
			againstRadioButton.Location = new Point(360, 731);
			againstRadioButton.Margin = new Padding(4, 6, 4, 6);
			againstRadioButton.Name = "againstRadioButton";
			againstRadioButton.Size = new Size(94, 29);
			againstRadioButton.TabIndex = 14;
			againstRadioButton.Text = "against";
			againstRadioButton.UseVisualStyleBackColor = true;
			// 
			// inFavourRadioButton
			// 
			inFavourRadioButton.AutoSize = true;
			inFavourRadioButton.Checked = true;
			inFavourRadioButton.Location = new Point(360, 686);
			inFavourRadioButton.Margin = new Padding(4, 6, 4, 6);
			inFavourRadioButton.Name = "inFavourRadioButton";
			inFavourRadioButton.Size = new Size(129, 29);
			inFavourRadioButton.TabIndex = 13;
			inFavourRadioButton.TabStop = true;
			inFavourRadioButton.Text = "in favour of";
			inFavourRadioButton.UseVisualStyleBackColor = true;
			// 
			// biasComboBox
			// 
			biasComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
			biasComboBox.FormattingEnabled = true;
			biasComboBox.Location = new Point(510, 685);
			biasComboBox.Margin = new Padding(4, 6, 4, 6);
			biasComboBox.Name = "biasComboBox";
			biasComboBox.Size = new Size(200, 33);
			biasComboBox.TabIndex = 12;
			// 
			// rule2CheckBox
			// 
			rule2CheckBox.AutoSize = true;
			rule2CheckBox.Location = new Point(224, 689);
			rule2CheckBox.Margin = new Padding(4, 6, 4, 6);
			rule2CheckBox.Name = "rule2CheckBox";
			rule2CheckBox.Size = new Size(119, 29);
			rule2CheckBox.TabIndex = 11;
			rule2CheckBox.Text = "Bias game";
			rule2CheckBox.UseVisualStyleBackColor = true;
			// 
			// rule1CheckBox
			// 
			rule1CheckBox.AutoSize = true;
			rule1CheckBox.Checked = true;
			rule1CheckBox.CheckState = CheckState.Checked;
			rule1CheckBox.Location = new Point(224, 789);
			rule1CheckBox.Margin = new Padding(4, 6, 4, 6);
			rule1CheckBox.Name = "rule1CheckBox";
			rule1CheckBox.Size = new Size(384, 29);
			rule1CheckBox.TabIndex = 10;
			rule1CheckBox.Text = "1st and 2nd best players on different teams";
			rule1CheckBox.UseVisualStyleBackColor = true;
			// 
			// button9
			// 
			button9.Location = new Point(437, 578);
			button9.Margin = new Padding(4, 6, 4, 6);
			button9.Name = "button9";
			button9.Size = new Size(202, 44);
			button9.TabIndex = 7;
			button9.Text = "Won";
			button9.UseVisualStyleBackColor = true;
			button9.Click += OnTeam2Win;
			// 
			// button8
			// 
			button8.Location = new Point(224, 578);
			button8.Margin = new Padding(4, 6, 4, 6);
			button8.Name = "button8";
			button8.Size = new Size(202, 44);
			button8.TabIndex = 6;
			button8.Text = "Won";
			button8.UseVisualStyleBackColor = true;
			button8.Click += OnTeam1Win;
			// 
			// button6
			// 
			button6.Location = new Point(224, 632);
			button6.Margin = new Padding(4, 6, 4, 6);
			button6.Name = "button6";
			button6.Size = new Size(413, 44);
			button6.TabIndex = 5;
			button6.Text = "Autobalance";
			button6.UseVisualStyleBackColor = true;
			button6.Click += OnAutobalance;
			// 
			// team1WinLabel
			// 
			team1WinLabel.Location = new Point(224, 11);
			team1WinLabel.Margin = new Padding(4, 0, 4, 0);
			team1WinLabel.Name = "team1WinLabel";
			team1WinLabel.Size = new Size(202, 29);
			team1WinLabel.TabIndex = 4;
			team1WinLabel.Text = "Team 1";
			// 
			// team2WinLabel
			// 
			team2WinLabel.Location = new Point(437, 11);
			team2WinLabel.Margin = new Padding(4, 0, 4, 0);
			team2WinLabel.Name = "team2WinLabel";
			team2WinLabel.Size = new Size(202, 29);
			team2WinLabel.TabIndex = 3;
			team2WinLabel.Text = "Team 2";
			// 
			// team2View
			// 
			team2View.AllowDrop = true;
			team2View.Location = new Point(437, 46);
			team2View.Margin = new Padding(4, 6, 4, 6);
			team2View.Name = "team2View";
			team2View.Size = new Size(200, 515);
			team2View.TabIndex = 2;
			team2View.UseCompatibleStateImageBehavior = false;
			team2View.View = View.List;
			team2View.ItemDrag += OnStartDrag;
			team2View.DragDrop += OnDragDrop;
			team2View.DragEnter += OnDragEnter;
			// 
			// team1View
			// 
			team1View.AllowDrop = true;
			team1View.Location = new Point(224, 46);
			team1View.Margin = new Padding(4, 6, 4, 6);
			team1View.Name = "team1View";
			team1View.Size = new Size(200, 515);
			team1View.TabIndex = 1;
			team1View.UseCompatibleStateImageBehavior = false;
			team1View.View = View.List;
			team1View.ItemDrag += OnStartDrag;
			team1View.DragDrop += OnDragDrop;
			team1View.DragEnter += OnDragEnter;
			// 
			// notPlayingView
			// 
			notPlayingView.AllowDrop = true;
			notPlayingView.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
			notPlayingView.Location = new Point(13, 11);
			notPlayingView.Margin = new Padding(4, 6, 4, 6);
			notPlayingView.Name = "notPlayingView";
			notPlayingView.Size = new Size(200, 1054);
			notPlayingView.TabIndex = 0;
			notPlayingView.UseCompatibleStateImageBehavior = false;
			notPlayingView.View = View.List;
			notPlayingView.ItemDrag += OnStartDrag;
			notPlayingView.DragDrop += OnDragDrop;
			notPlayingView.DragEnter += OnDragEnter;
			// 
			// reportsTabPage
			// 
			reportsTabPage.Controls.Add(webBrowser1);
			reportsTabPage.Controls.Add(dateTimePicker);
			reportsTabPage.Controls.Add(button7);
			reportsTabPage.Location = new Point(4, 34);
			reportsTabPage.Margin = new Padding(4, 6, 4, 6);
			reportsTabPage.Name = "reportsTabPage";
			reportsTabPage.Padding = new Padding(4, 6, 4, 6);
			reportsTabPage.Size = new Size(1503, 1097);
			reportsTabPage.TabIndex = 3;
			reportsTabPage.Text = "Reports";
			reportsTabPage.UseVisualStyleBackColor = true;
			// 
			// webBrowser1
			// 
			webBrowser1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
			webBrowser1.Location = new Point(13, 68);
			webBrowser1.Margin = new Padding(4, 6, 4, 6);
			webBrowser1.MinimumSize = new Size(33, 39);
			webBrowser1.Name = "webBrowser1";
			webBrowser1.Size = new Size(1471, 1011);
			webBrowser1.TabIndex = 5;
			// 
			// dateTimePicker
			// 
			dateTimePicker.Location = new Point(247, 18);
			dateTimePicker.Margin = new Padding(4, 6, 4, 6);
			dateTimePicker.Name = "dateTimePicker";
			dateTimePicker.Size = new Size(331, 31);
			dateTimePicker.TabIndex = 4;
			dateTimePicker.ValueChanged += OnStartDateChanged;
			// 
			// button7
			// 
			button7.Location = new Point(13, 11);
			button7.Margin = new Padding(4, 6, 4, 6);
			button7.Name = "button7";
			button7.Size = new Size(223, 44);
			button7.TabIndex = 3;
			button7.Text = "Rankings";
			button7.UseVisualStyleBackColor = true;
			button7.Click += OnReportRankings;
			// 
			// gamesTab
			// 
			gamesTab.Controls.Add(deleteButton);
			gamesTab.Controls.Add(reviewTeam2Label);
			gamesTab.Controls.Add(reviewTeam1Label);
			gamesTab.Controls.Add(label2);
			gamesTab.Controls.Add(label1);
			gamesTab.Controls.Add(gamesListBox);
			gamesTab.Location = new Point(4, 34);
			gamesTab.Margin = new Padding(4, 6, 4, 6);
			gamesTab.Name = "gamesTab";
			gamesTab.Padding = new Padding(4, 6, 4, 6);
			gamesTab.Size = new Size(1503, 1097);
			gamesTab.TabIndex = 1;
			gamesTab.Text = "Games";
			gamesTab.UseVisualStyleBackColor = true;
			// 
			// deleteButton
			// 
			deleteButton.Location = new Point(962, 52);
			deleteButton.Margin = new Padding(4, 6, 4, 6);
			deleteButton.Name = "deleteButton";
			deleteButton.Size = new Size(124, 44);
			deleteButton.TabIndex = 8;
			deleteButton.Text = "Delete";
			deleteButton.UseVisualStyleBackColor = true;
			deleteButton.Click += OnDeleteGame;
			// 
			// reviewTeam2Label
			// 
			reviewTeam2Label.Location = new Point(603, 140);
			reviewTeam2Label.Margin = new Padding(4, 0, 4, 0);
			reviewTeam2Label.Name = "reviewTeam2Label";
			reviewTeam2Label.Size = new Size(178, 286);
			reviewTeam2Label.TabIndex = 7;
			reviewTeam2Label.Text = "reviewTeam2Label";
			// 
			// reviewTeam1Label
			// 
			reviewTeam1Label.Location = new Point(358, 140);
			reviewTeam1Label.Margin = new Padding(4, 0, 4, 0);
			reviewTeam1Label.Name = "reviewTeam1Label";
			reviewTeam1Label.Size = new Size(236, 286);
			reviewTeam1Label.TabIndex = 6;
			reviewTeam1Label.Text = "reviewTeam1Label";
			// 
			// label2
			// 
			label2.Location = new Point(603, 61);
			label2.Margin = new Padding(4, 0, 4, 0);
			label2.Name = "label2";
			label2.Size = new Size(349, 79);
			label2.TabIndex = 3;
			label2.Text = "label2";
			// 
			// label1
			// 
			label1.Location = new Point(358, 61);
			label1.Margin = new Padding(4, 0, 4, 0);
			label1.Name = "label1";
			label1.Size = new Size(236, 79);
			label1.TabIndex = 2;
			label1.Text = "label1";
			// 
			// gamesListBox
			// 
			gamesListBox.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
			gamesListBox.FormattingEnabled = true;
			gamesListBox.ItemHeight = 25;
			gamesListBox.Location = new Point(13, 11);
			gamesListBox.Margin = new Padding(4, 6, 4, 6);
			gamesListBox.Name = "gamesListBox";
			gamesListBox.Size = new Size(333, 1054);
			gamesListBox.TabIndex = 0;
			gamesListBox.SelectedIndexChanged += OnGameSelected;
			// 
			// tabControl1
			// 
			tabControl1.Controls.Add(gamesTab);
			tabControl1.Controls.Add(playersTabPage);
			tabControl1.Controls.Add(tabPage4);
			tabControl1.Controls.Add(reportsTabPage);
			tabControl1.Controls.Add(graphsPage);
			tabControl1.Dock = DockStyle.Fill;
			tabControl1.Location = new Point(0, 0);
			tabControl1.Margin = new Padding(4, 6, 4, 6);
			tabControl1.Name = "tabControl1";
			tabControl1.SelectedIndex = 0;
			tabControl1.Size = new Size(1511, 1135);
			tabControl1.TabIndex = 0;
			// 
			// playersTabPage
			// 
			playersTabPage.Controls.Add(playersGrid);
			playersTabPage.Location = new Point(4, 34);
			playersTabPage.Margin = new Padding(3, 4, 3, 4);
			playersTabPage.Name = "playersTabPage";
			playersTabPage.Padding = new Padding(3, 4, 3, 4);
			playersTabPage.Size = new Size(1503, 1097);
			playersTabPage.TabIndex = 7;
			playersTabPage.Text = "Players";
			playersTabPage.UseVisualStyleBackColor = true;
			// 
			// playersGrid
			// 
			playersGrid.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
			playersGrid.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			playersGrid.Columns.AddRange(new DataGridViewColumn[] { namePlayerColumn, initialRankPlayerColumn });
			playersGrid.Location = new Point(7, 8);
			playersGrid.Margin = new Padding(3, 4, 3, 4);
			playersGrid.Name = "playersGrid";
			playersGrid.RowHeadersWidth = 62;
			playersGrid.RowTemplate.Height = 28;
			playersGrid.Size = new Size(1492, 1076);
			playersGrid.TabIndex = 0;
			playersGrid.CellEndEdit += OnPlayerCellEndEdit;
			// 
			// namePlayerColumn
			// 
			namePlayerColumn.HeaderText = "Name";
			namePlayerColumn.MinimumWidth = 8;
			namePlayerColumn.Name = "namePlayerColumn";
			namePlayerColumn.Width = 150;
			// 
			// initialRankPlayerColumn
			// 
			initialRankPlayerColumn.HeaderText = "Initial Rank";
			initialRankPlayerColumn.MinimumWidth = 8;
			initialRankPlayerColumn.Name = "initialRankPlayerColumn";
			initialRankPlayerColumn.Width = 150;
			// 
			// MainForm
			// 
			AutoScaleDimensions = new SizeF(10F, 25F);
			AutoScaleMode = AutoScaleMode.Font;
			ClientSize = new Size(1511, 1135);
			Controls.Add(tabControl1);
			Margin = new Padding(4, 6, 4, 6);
			Name = "MainForm";
			Text = "Ranker";
			Load += OnLoad;
			graphsPage.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)chart).EndInit();
			tabPage4.ResumeLayout(false);
			tabPage4.PerformLayout();
			reportsTabPage.ResumeLayout(false);
			gamesTab.ResumeLayout(false);
			tabControl1.ResumeLayout(false);
			playersTabPage.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)playersGrid).EndInit();
			ResumeLayout(false);
		}

		private System.Windows.Forms.TabPage graphsPage;
		private System.Windows.Forms.ComboBox graphComboBox;
		private System.Windows.Forms.DataVisualization.Charting.Chart chart;
		private System.Windows.Forms.TabPage tabPage4;
		private System.Windows.Forms.CheckBox playerBalanceCheckBox;
		private System.Windows.Forms.RadioButton againstRadioButton;
		private System.Windows.Forms.RadioButton inFavourRadioButton;
		private System.Windows.Forms.ComboBox biasComboBox;
		private System.Windows.Forms.CheckBox rule2CheckBox;
		private System.Windows.Forms.CheckBox rule1CheckBox;
		private System.Windows.Forms.Button button9;
		private System.Windows.Forms.Button button8;
		private System.Windows.Forms.Button button6;
		private System.Windows.Forms.Label team1WinLabel;
		private System.Windows.Forms.Label team2WinLabel;
		private System.Windows.Forms.ListView team2View;
		private System.Windows.Forms.ListView team1View;
		private System.Windows.Forms.ListView notPlayingView;
		private System.Windows.Forms.TabPage reportsTabPage;
		private System.Windows.Forms.WebBrowser webBrowser1;
		private System.Windows.Forms.DateTimePicker dateTimePicker;
		private System.Windows.Forms.Button button7;
		private System.Windows.Forms.TabPage gamesTab;
		private System.Windows.Forms.Button deleteButton;
		private System.Windows.Forms.Label reviewTeam2Label;
		private System.Windows.Forms.Label reviewTeam1Label;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.ListBox gamesListBox;
		private System.Windows.Forms.TabControl tabControl1;
		private System.Windows.Forms.TabPage playersTabPage;
		private System.Windows.Forms.DataGridView playersGrid;
		private System.Windows.Forms.DataGridViewTextBoxColumn namePlayerColumn;
		private System.Windows.Forms.DataGridViewTextBoxColumn initialRankPlayerColumn;
	}
}

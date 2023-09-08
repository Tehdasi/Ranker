
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
			if (disposing) {
				if (components != null) {
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
			this.graphsPage = new System.Windows.Forms.TabPage();
			this.graphComboBox = new System.Windows.Forms.ComboBox();
			this.chart = new System.Windows.Forms.DataVisualization.Charting.Chart();
			this.tabPage4 = new System.Windows.Forms.TabPage();
			this.playerBalanceCheckBox = new System.Windows.Forms.CheckBox();
			this.againstRadioButton = new System.Windows.Forms.RadioButton();
			this.inFavourRadioButton = new System.Windows.Forms.RadioButton();
			this.biasComboBox = new System.Windows.Forms.ComboBox();
			this.rule2CheckBox = new System.Windows.Forms.CheckBox();
			this.rule1CheckBox = new System.Windows.Forms.CheckBox();
			this.button9 = new System.Windows.Forms.Button();
			this.button8 = new System.Windows.Forms.Button();
			this.button6 = new System.Windows.Forms.Button();
			this.team1WinLabel = new System.Windows.Forms.Label();
			this.team2WinLabel = new System.Windows.Forms.Label();
			this.team2View = new System.Windows.Forms.ListView();
			this.team1View = new System.Windows.Forms.ListView();
			this.notPlayingView = new System.Windows.Forms.ListView();
			this.reportsTabPage = new System.Windows.Forms.TabPage();
			this.webBrowser1 = new System.Windows.Forms.WebBrowser();
			this.dateTimePicker = new System.Windows.Forms.DateTimePicker();
			this.button7 = new System.Windows.Forms.Button();
			this.gamesTab = new System.Windows.Forms.TabPage();
			this.deleteButton = new System.Windows.Forms.Button();
			this.reviewTeam2Label = new System.Windows.Forms.Label();
			this.reviewTeam1Label = new System.Windows.Forms.Label();
			this.gameFilterComboBox = new System.Windows.Forms.ComboBox();
			this.label2 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.gamesListBox = new System.Windows.Forms.ListBox();
			this.tabControl1 = new System.Windows.Forms.TabControl();
			this.playersTabPage = new System.Windows.Forms.TabPage();
			this.playersGrid = new System.Windows.Forms.DataGridView();
			this.namePlayerColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.initialRankPlayerColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.graphsPage.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.chart)).BeginInit();
			this.tabPage4.SuspendLayout();
			this.reportsTabPage.SuspendLayout();
			this.gamesTab.SuspendLayout();
			this.tabControl1.SuspendLayout();
			this.playersTabPage.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.playersGrid)).BeginInit();
			this.SuspendLayout();
			// 
			// graphsPage
			// 
			this.graphsPage.Controls.Add(this.graphComboBox);
			this.graphsPage.Controls.Add(this.chart);
			this.graphsPage.Location = new System.Drawing.Point(4, 29);
			this.graphsPage.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.graphsPage.Name = "graphsPage";
			this.graphsPage.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.graphsPage.Size = new System.Drawing.Size(1352, 875);
			this.graphsPage.TabIndex = 5;
			this.graphsPage.Text = "Graphs";
			this.graphsPage.UseVisualStyleBackColor = true;
			// 
			// graphComboBox
			// 
			this.graphComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.graphComboBox.FormattingEnabled = true;
			this.graphComboBox.Location = new System.Drawing.Point(12, 9);
			this.graphComboBox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.graphComboBox.Name = "graphComboBox";
			this.graphComboBox.Size = new System.Drawing.Size(520, 28);
			this.graphComboBox.TabIndex = 1;
			this.graphComboBox.SelectedIndexChanged += new System.EventHandler(this.OnGraphSelected);
			// 
			// chart
			// 
			this.chart.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.chart.Location = new System.Drawing.Point(12, 51);
			this.chart.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.chart.Name = "chart";
			this.chart.Palette = System.Windows.Forms.DataVisualization.Charting.ChartColorPalette.Bright;
			this.chart.Size = new System.Drawing.Size(1324, 805);
			this.chart.TabIndex = 0;
			this.chart.Text = "chart1";
			// 
			// tabPage4
			// 
			this.tabPage4.Controls.Add(this.playerBalanceCheckBox);
			this.tabPage4.Controls.Add(this.againstRadioButton);
			this.tabPage4.Controls.Add(this.inFavourRadioButton);
			this.tabPage4.Controls.Add(this.biasComboBox);
			this.tabPage4.Controls.Add(this.rule2CheckBox);
			this.tabPage4.Controls.Add(this.rule1CheckBox);
			this.tabPage4.Controls.Add(this.button9);
			this.tabPage4.Controls.Add(this.button8);
			this.tabPage4.Controls.Add(this.button6);
			this.tabPage4.Controls.Add(this.team1WinLabel);
			this.tabPage4.Controls.Add(this.team2WinLabel);
			this.tabPage4.Controls.Add(this.team2View);
			this.tabPage4.Controls.Add(this.team1View);
			this.tabPage4.Controls.Add(this.notPlayingView);
			this.tabPage4.Location = new System.Drawing.Point(4, 29);
			this.tabPage4.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.tabPage4.Name = "tabPage4";
			this.tabPage4.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.tabPage4.Size = new System.Drawing.Size(1352, 875);
			this.tabPage4.TabIndex = 6;
			this.tabPage4.Text = "Organise";
			this.tabPage4.UseVisualStyleBackColor = true;
			// 
			// playerBalanceCheckBox
			// 
			this.playerBalanceCheckBox.AutoSize = true;
			this.playerBalanceCheckBox.Checked = true;
			this.playerBalanceCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
			this.playerBalanceCheckBox.Location = new System.Drawing.Point(202, 665);
			this.playerBalanceCheckBox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.playerBalanceCheckBox.Name = "playerBalanceCheckBox";
			this.playerBalanceCheckBox.Size = new System.Drawing.Size(138, 24);
			this.playerBalanceCheckBox.TabIndex = 15;
			this.playerBalanceCheckBox.Text = "Player balance";
			this.playerBalanceCheckBox.UseVisualStyleBackColor = true;
			// 
			// againstRadioButton
			// 
			this.againstRadioButton.AutoSize = true;
			this.againstRadioButton.Location = new System.Drawing.Point(324, 585);
			this.againstRadioButton.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.againstRadioButton.Name = "againstRadioButton";
			this.againstRadioButton.Size = new System.Drawing.Size(86, 24);
			this.againstRadioButton.TabIndex = 14;
			this.againstRadioButton.Text = "against";
			this.againstRadioButton.UseVisualStyleBackColor = true;
			// 
			// inFavourRadioButton
			// 
			this.inFavourRadioButton.AutoSize = true;
			this.inFavourRadioButton.Checked = true;
			this.inFavourRadioButton.Location = new System.Drawing.Point(324, 549);
			this.inFavourRadioButton.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.inFavourRadioButton.Name = "inFavourRadioButton";
			this.inFavourRadioButton.Size = new System.Drawing.Size(112, 24);
			this.inFavourRadioButton.TabIndex = 13;
			this.inFavourRadioButton.TabStop = true;
			this.inFavourRadioButton.Text = "in favour of";
			this.inFavourRadioButton.UseVisualStyleBackColor = true;
			// 
			// biasComboBox
			// 
			this.biasComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.biasComboBox.FormattingEnabled = true;
			this.biasComboBox.Location = new System.Drawing.Point(459, 548);
			this.biasComboBox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.biasComboBox.Name = "biasComboBox";
			this.biasComboBox.Size = new System.Drawing.Size(180, 28);
			this.biasComboBox.TabIndex = 12;
			// 
			// rule2CheckBox
			// 
			this.rule2CheckBox.AutoSize = true;
			this.rule2CheckBox.Location = new System.Drawing.Point(202, 551);
			this.rule2CheckBox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.rule2CheckBox.Name = "rule2CheckBox";
			this.rule2CheckBox.Size = new System.Drawing.Size(110, 24);
			this.rule2CheckBox.TabIndex = 11;
			this.rule2CheckBox.Text = "Bias game";
			this.rule2CheckBox.UseVisualStyleBackColor = true;
			// 
			// rule1CheckBox
			// 
			this.rule1CheckBox.AutoSize = true;
			this.rule1CheckBox.Checked = true;
			this.rule1CheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
			this.rule1CheckBox.Location = new System.Drawing.Point(202, 631);
			this.rule1CheckBox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.rule1CheckBox.Name = "rule1CheckBox";
			this.rule1CheckBox.Size = new System.Drawing.Size(341, 24);
			this.rule1CheckBox.TabIndex = 10;
			this.rule1CheckBox.Text = "1st and 2nd best players on different teams";
			this.rule1CheckBox.UseVisualStyleBackColor = true;
			// 
			// button9
			// 
			this.button9.Location = new System.Drawing.Point(393, 462);
			this.button9.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.button9.Name = "button9";
			this.button9.Size = new System.Drawing.Size(182, 35);
			this.button9.TabIndex = 7;
			this.button9.Text = "Won";
			this.button9.UseVisualStyleBackColor = true;
			this.button9.Click += new System.EventHandler(this.OnTeam2Win);
			// 
			// button8
			// 
			this.button8.Location = new System.Drawing.Point(202, 462);
			this.button8.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.button8.Name = "button8";
			this.button8.Size = new System.Drawing.Size(182, 35);
			this.button8.TabIndex = 6;
			this.button8.Text = "Won";
			this.button8.UseVisualStyleBackColor = true;
			this.button8.Click += new System.EventHandler(this.OnTeam1Win);
			// 
			// button6
			// 
			this.button6.Location = new System.Drawing.Point(202, 506);
			this.button6.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.button6.Name = "button6";
			this.button6.Size = new System.Drawing.Size(372, 35);
			this.button6.TabIndex = 5;
			this.button6.Text = "Autobalance";
			this.button6.UseVisualStyleBackColor = true;
			this.button6.Click += new System.EventHandler(this.OnAutobalance);
			// 
			// team1WinLabel
			// 
			this.team1WinLabel.Location = new System.Drawing.Point(202, 9);
			this.team1WinLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.team1WinLabel.Name = "team1WinLabel";
			this.team1WinLabel.Size = new System.Drawing.Size(182, 23);
			this.team1WinLabel.TabIndex = 4;
			this.team1WinLabel.Text = "Team 1";
			// 
			// team2WinLabel
			// 
			this.team2WinLabel.Location = new System.Drawing.Point(393, 9);
			this.team2WinLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.team2WinLabel.Name = "team2WinLabel";
			this.team2WinLabel.Size = new System.Drawing.Size(182, 23);
			this.team2WinLabel.TabIndex = 3;
			this.team2WinLabel.Text = "Team 2";
			// 
			// team2View
			// 
			this.team2View.AllowDrop = true;
			this.team2View.HideSelection = false;
			this.team2View.Location = new System.Drawing.Point(393, 37);
			this.team2View.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.team2View.Name = "team2View";
			this.team2View.Size = new System.Drawing.Size(180, 413);
			this.team2View.TabIndex = 2;
			this.team2View.UseCompatibleStateImageBehavior = false;
			this.team2View.View = System.Windows.Forms.View.List;
			this.team2View.ItemDrag += new System.Windows.Forms.ItemDragEventHandler(this.OnStartDrag);
			this.team2View.DragDrop += new System.Windows.Forms.DragEventHandler(this.OnDragDrop);
			this.team2View.DragEnter += new System.Windows.Forms.DragEventHandler(this.OnDragEnter);
			// 
			// team1View
			// 
			this.team1View.AllowDrop = true;
			this.team1View.HideSelection = false;
			this.team1View.Location = new System.Drawing.Point(202, 37);
			this.team1View.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.team1View.Name = "team1View";
			this.team1View.Size = new System.Drawing.Size(180, 413);
			this.team1View.TabIndex = 1;
			this.team1View.UseCompatibleStateImageBehavior = false;
			this.team1View.View = System.Windows.Forms.View.List;
			this.team1View.ItemDrag += new System.Windows.Forms.ItemDragEventHandler(this.OnStartDrag);
			this.team1View.DragDrop += new System.Windows.Forms.DragEventHandler(this.OnDragDrop);
			this.team1View.DragEnter += new System.Windows.Forms.DragEventHandler(this.OnDragEnter);
			// 
			// notPlayingView
			// 
			this.notPlayingView.AllowDrop = true;
			this.notPlayingView.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
			this.notPlayingView.HideSelection = false;
			this.notPlayingView.Location = new System.Drawing.Point(12, 9);
			this.notPlayingView.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.notPlayingView.Name = "notPlayingView";
			this.notPlayingView.Size = new System.Drawing.Size(180, 844);
			this.notPlayingView.TabIndex = 0;
			this.notPlayingView.UseCompatibleStateImageBehavior = false;
			this.notPlayingView.View = System.Windows.Forms.View.List;
			this.notPlayingView.ItemDrag += new System.Windows.Forms.ItemDragEventHandler(this.OnStartDrag);
			this.notPlayingView.DragDrop += new System.Windows.Forms.DragEventHandler(this.OnDragDrop);
			this.notPlayingView.DragEnter += new System.Windows.Forms.DragEventHandler(this.OnDragEnter);
			// 
			// reportsTabPage
			// 
			this.reportsTabPage.Controls.Add(this.webBrowser1);
			this.reportsTabPage.Controls.Add(this.dateTimePicker);
			this.reportsTabPage.Controls.Add(this.button7);
			this.reportsTabPage.Location = new System.Drawing.Point(4, 29);
			this.reportsTabPage.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.reportsTabPage.Name = "reportsTabPage";
			this.reportsTabPage.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.reportsTabPage.Size = new System.Drawing.Size(1352, 875);
			this.reportsTabPage.TabIndex = 3;
			this.reportsTabPage.Text = "Reports";
			this.reportsTabPage.UseVisualStyleBackColor = true;
			// 
			// webBrowser1
			// 
			this.webBrowser1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.webBrowser1.Location = new System.Drawing.Point(12, 54);
			this.webBrowser1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.webBrowser1.MinimumSize = new System.Drawing.Size(30, 31);
			this.webBrowser1.Name = "webBrowser1";
			this.webBrowser1.Size = new System.Drawing.Size(1324, 809);
			this.webBrowser1.TabIndex = 5;
			// 
			// dateTimePicker
			// 
			this.dateTimePicker.Location = new System.Drawing.Point(222, 14);
			this.dateTimePicker.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.dateTimePicker.Name = "dateTimePicker";
			this.dateTimePicker.Size = new System.Drawing.Size(298, 26);
			this.dateTimePicker.TabIndex = 4;
			this.dateTimePicker.ValueChanged += new System.EventHandler(this.OnStartDateChanged);
			// 
			// button7
			// 
			this.button7.Location = new System.Drawing.Point(12, 9);
			this.button7.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.button7.Name = "button7";
			this.button7.Size = new System.Drawing.Size(201, 35);
			this.button7.TabIndex = 3;
			this.button7.Text = "Rankings";
			this.button7.UseVisualStyleBackColor = true;
			this.button7.Click += new System.EventHandler(this.OnReportRankings);
			// 
			// gamesTab
			// 
			this.gamesTab.Controls.Add(this.deleteButton);
			this.gamesTab.Controls.Add(this.reviewTeam2Label);
			this.gamesTab.Controls.Add(this.reviewTeam1Label);
			this.gamesTab.Controls.Add(this.gameFilterComboBox);
			this.gamesTab.Controls.Add(this.label2);
			this.gamesTab.Controls.Add(this.label1);
			this.gamesTab.Controls.Add(this.gamesListBox);
			this.gamesTab.Location = new System.Drawing.Point(4, 29);
			this.gamesTab.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.gamesTab.Name = "gamesTab";
			this.gamesTab.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.gamesTab.Size = new System.Drawing.Size(1352, 875);
			this.gamesTab.TabIndex = 1;
			this.gamesTab.Text = "Games";
			this.gamesTab.UseVisualStyleBackColor = true;
			// 
			// deleteButton
			// 
			this.deleteButton.Location = new System.Drawing.Point(866, 42);
			this.deleteButton.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.deleteButton.Name = "deleteButton";
			this.deleteButton.Size = new System.Drawing.Size(112, 35);
			this.deleteButton.TabIndex = 8;
			this.deleteButton.Text = "Delete";
			this.deleteButton.UseVisualStyleBackColor = true;
			this.deleteButton.Click += new System.EventHandler(this.OnDeleteGame);
			// 
			// reviewTeam2Label
			// 
			this.reviewTeam2Label.Location = new System.Drawing.Point(543, 112);
			this.reviewTeam2Label.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.reviewTeam2Label.Name = "reviewTeam2Label";
			this.reviewTeam2Label.Size = new System.Drawing.Size(160, 229);
			this.reviewTeam2Label.TabIndex = 7;
			this.reviewTeam2Label.Text = "reviewTeam2Label";
			// 
			// reviewTeam1Label
			// 
			this.reviewTeam1Label.Location = new System.Drawing.Point(322, 112);
			this.reviewTeam1Label.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.reviewTeam1Label.Name = "reviewTeam1Label";
			this.reviewTeam1Label.Size = new System.Drawing.Size(212, 229);
			this.reviewTeam1Label.TabIndex = 6;
			this.reviewTeam1Label.Text = "reviewTeam1Label";
			// 
			// gameFilterComboBox
			// 
			this.gameFilterComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.gameFilterComboBox.FormattingEnabled = true;
			this.gameFilterComboBox.Items.AddRange(new object[] {
            "All",
            "Ranked",
            "WithUnknowns"});
			this.gameFilterComboBox.Location = new System.Drawing.Point(12, 9);
			this.gameFilterComboBox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.gameFilterComboBox.Name = "gameFilterComboBox";
			this.gameFilterComboBox.Size = new System.Drawing.Size(300, 28);
			this.gameFilterComboBox.TabIndex = 4;
			this.gameFilterComboBox.SelectedIndexChanged += new System.EventHandler(this.OnFilterSelected);
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(543, 49);
			this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(314, 63);
			this.label2.TabIndex = 3;
			this.label2.Text = "label2";
			this.label2.Click += new System.EventHandler(this.Label2Click);
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(322, 49);
			this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(212, 63);
			this.label1.TabIndex = 2;
			this.label1.Text = "label1";
			// 
			// gamesListBox
			// 
			this.gamesListBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
			this.gamesListBox.FormattingEnabled = true;
			this.gamesListBox.ItemHeight = 20;
			this.gamesListBox.Location = new System.Drawing.Point(12, 49);
			this.gamesListBox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.gamesListBox.Name = "gamesListBox";
			this.gamesListBox.Size = new System.Drawing.Size(300, 804);
			this.gamesListBox.TabIndex = 0;
			this.gamesListBox.SelectedIndexChanged += new System.EventHandler(this.OnGameSelected);
			// 
			// tabControl1
			// 
			this.tabControl1.Controls.Add(this.gamesTab);
			this.tabControl1.Controls.Add(this.playersTabPage);
			this.tabControl1.Controls.Add(this.tabPage4);
			this.tabControl1.Controls.Add(this.reportsTabPage);
			this.tabControl1.Controls.Add(this.graphsPage);
			this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tabControl1.Location = new System.Drawing.Point(0, 0);
			this.tabControl1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.tabControl1.Name = "tabControl1";
			this.tabControl1.SelectedIndex = 0;
			this.tabControl1.Size = new System.Drawing.Size(1360, 908);
			this.tabControl1.TabIndex = 0;
			// 
			// playersTabPage
			// 
			this.playersTabPage.Controls.Add(this.playersGrid);
			this.playersTabPage.Location = new System.Drawing.Point(4, 29);
			this.playersTabPage.Name = "playersTabPage";
			this.playersTabPage.Padding = new System.Windows.Forms.Padding(3);
			this.playersTabPage.Size = new System.Drawing.Size(1352, 875);
			this.playersTabPage.TabIndex = 7;
			this.playersTabPage.Text = "Players";
			this.playersTabPage.UseVisualStyleBackColor = true;
			// 
			// playersGrid
			// 
			this.playersGrid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.playersGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.playersGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.namePlayerColumn,
            this.initialRankPlayerColumn});
			this.playersGrid.Location = new System.Drawing.Point(6, 6);
			this.playersGrid.Name = "playersGrid";
			this.playersGrid.RowHeadersWidth = 62;
			this.playersGrid.RowTemplate.Height = 28;
			this.playersGrid.Size = new System.Drawing.Size(1343, 861);
			this.playersGrid.TabIndex = 0;
			this.playersGrid.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.OnPlayerCellEndEdit);
			// 
			// namePlayerColumn
			// 
			this.namePlayerColumn.HeaderText = "Name";
			this.namePlayerColumn.MinimumWidth = 8;
			this.namePlayerColumn.Name = "namePlayerColumn";
			this.namePlayerColumn.Width = 150;
			// 
			// initialRankPlayerColumn
			// 
			this.initialRankPlayerColumn.HeaderText = "Initial Rank";
			this.initialRankPlayerColumn.MinimumWidth = 8;
			this.initialRankPlayerColumn.Name = "initialRankPlayerColumn";
			this.initialRankPlayerColumn.Width = 150;
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1360, 908);
			this.Controls.Add(this.tabControl1);
			this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
			this.Name = "MainForm";
			this.Text = "Ranker";
			this.Load += new System.EventHandler(this.OnLoad);
			this.graphsPage.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.chart)).EndInit();
			this.tabPage4.ResumeLayout(false);
			this.tabPage4.PerformLayout();
			this.reportsTabPage.ResumeLayout(false);
			this.gamesTab.ResumeLayout(false);
			this.tabControl1.ResumeLayout(false);
			this.playersTabPage.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.playersGrid)).EndInit();
			this.ResumeLayout(false);

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
		private System.Windows.Forms.ComboBox gameFilterComboBox;
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

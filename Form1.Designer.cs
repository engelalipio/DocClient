namespace DoceboClient
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>


        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            cmdGo = new Button();
            txtURL = new TextBox();
            label1 = new Label();
            label2 = new Label();
            txtClientID = new TextBox();
            label3 = new Label();
            txtClientSecret = new TextBox();
            txtResponse = new RichTextBox();
            button1 = new Button();
            label4 = new Label();
            txtToken = new TextBox();
            label5 = new Label();
            cboMethod = new ComboBox();
            cboEnvironment = new ComboBox();
            label6 = new Label();
            txtBaseURL = new TextBox();
            label7 = new Label();
            SuspendLayout();
            // 
            // cmdGo
            // 
            cmdGo.Location = new Point(448, 46);
            cmdGo.Name = "cmdGo";
            cmdGo.Size = new Size(75, 23);
            cmdGo.TabIndex = 0;
            cmdGo.Text = "Get Token";
            cmdGo.UseVisualStyleBackColor = true;
            cmdGo.Click += cmdGo_Click;
            // 
            // txtURL
            // 
            txtURL.Location = new Point(106, 47);
            txtURL.Name = "txtURL";
            txtURL.Size = new Size(336, 23);
            txtURL.TabIndex = 1;
            txtURL.Text = "oauth2/token";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(23, 50);
            label1.Name = "label1";
            label1.Size = new Size(52, 15);
            label1.TabIndex = 2;
            label1.Text = "API URL:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(23, 79);
            label2.Name = "label2";
            label2.Size = new Size(55, 15);
            label2.TabIndex = 4;
            label2.Text = "Client ID:";
            // 
            // txtClientID
            // 
            txtClientID.Location = new Point(106, 76);
            txtClientID.Name = "txtClientID";
            txtClientID.Size = new Size(417, 23);
            txtClientID.TabIndex = 3;
            txtClientID.Text = "LIR_DEV_Client";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(23, 108);
            label3.Name = "label3";
            label3.Size = new Size(76, 15);
            label3.TabIndex = 6;
            label3.Text = "Client Secret:";
            // 
            // txtClientSecret
            // 
            txtClientSecret.Location = new Point(106, 105);
            txtClientSecret.Name = "txtClientSecret";
            txtClientSecret.Size = new Size(417, 23);
            txtClientSecret.TabIndex = 5;
            txtClientSecret.Text = "26cc0bc7e71a3c245ad7a289c70829e20b7e69e03442d8849b32abd2424d6d90";
            // 
            // txtResponse
            // 
            txtResponse.Location = new Point(23, 202);
            txtResponse.Name = "txtResponse";
            txtResponse.ScrollBars = RichTextBoxScrollBars.Vertical;
            txtResponse.Size = new Size(490, 272);
            txtResponse.TabIndex = 7;
            txtResponse.Text = "";
            // 
            // button1
            // 
            button1.Location = new Point(426, 162);
            button1.Name = "button1";
            button1.Size = new Size(75, 23);
            button1.TabIndex = 8;
            button1.Text = "Invoke API";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(23, 136);
            label4.Name = "label4";
            label4.Size = new Size(41, 15);
            label4.TabIndex = 10;
            label4.Text = "Token:";
            // 
            // txtToken
            // 
            txtToken.Location = new Point(106, 133);
            txtToken.Name = "txtToken";
            txtToken.Size = new Size(417, 23);
            txtToken.TabIndex = 9;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(23, 166);
            label5.Name = "label5";
            label5.Size = new Size(52, 15);
            label5.TabIndex = 11;
            label5.Text = "Method:";
            // 
            // cboMethod
            // 
            cboMethod.FormattingEnabled = true;
            cboMethod.Items.AddRange(new object[] { "GET", "POST", "PUT", "DELETE" });
            cboMethod.Location = new Point(106, 163);
            cboMethod.Name = "cboMethod";
            cboMethod.Size = new Size(82, 23);
            cboMethod.TabIndex = 12;
            cboMethod.Text = "GET";
            // 
            // cboEnvironment
            // 
            cboEnvironment.FormattingEnabled = true;
            cboEnvironment.Items.AddRange(new object[] { "DEV", "UAT", "PROD" });
            cboEnvironment.Location = new Point(106, 6);
            cboEnvironment.Name = "cboEnvironment";
            cboEnvironment.Size = new Size(60, 23);
            cboEnvironment.TabIndex = 14;
            cboEnvironment.Text = "UAT";
            cboEnvironment.SelectedIndexChanged += cboEnvironment_SelectedIndexChanged;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(23, 9);
            label6.Name = "label6";
            label6.Size = new Size(78, 15);
            label6.TabIndex = 13;
            label6.Text = "Environment:";
            // 
            // txtBaseURL
            // 
            txtBaseURL.Location = new Point(236, 6);
            txtBaseURL.Name = "txtBaseURL";
            txtBaseURL.Size = new Size(206, 23);
            txtBaseURL.TabIndex = 15;
            txtBaseURL.Text = "https://citiuat.docebosaas.com/";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(172, 9);
            label7.Name = "label7";
            label7.Size = new Size(58, 15);
            label7.TabIndex = 16;
            label7.Text = "Base URL:";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(540, 486);
            Controls.Add(label7);
            Controls.Add(txtBaseURL);
            Controls.Add(cboEnvironment);
            Controls.Add(label6);
            Controls.Add(cboMethod);
            Controls.Add(label5);
            Controls.Add(label4);
            Controls.Add(txtToken);
            Controls.Add(button1);
            Controls.Add(txtResponse);
            Controls.Add(label3);
            Controls.Add(txtClientSecret);
            Controls.Add(label2);
            Controls.Add(txtClientID);
            Controls.Add(label1);
            Controls.Add(txtURL);
            Controls.Add(cmdGo);
            Name = "Form1";
            Text = "Docebo Client";
            Load += Form1_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button cmdGo;
        private TextBox txtURL;
        private Label label1;
        private Label label2;
        private TextBox txtClientID;
        private Label label3;
        private TextBox txtClientSecret;
        private RichTextBox txtResponse;
        private Button button1;
        private Label label4;
        private TextBox txtToken;
        private Label label5;
        private ComboBox cboMethod;
        private ComboBox cboEnvironment;
        private Label label6;
        private TextBox txtBaseURL;
        private Label label7;
    }
}

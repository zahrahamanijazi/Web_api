
namespace Assignment2
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.alice = new System.Windows.Forms.Label();
            this.bob = new System.Windows.Forms.Label();
            this.cyclic_group = new System.Windows.Forms.Label();
            this.generator = new System.Windows.Forms.Label();
            this.privateAlice = new System.Windows.Forms.Label();
            this.publickey = new System.Windows.Forms.Label();
            this.privateBob = new System.Windows.Forms.Label();
            this.publicBob = new System.Windows.Forms.Label();
            this.aliceLable = new System.Windows.Forms.Label();
            this.lableBob = new System.Windows.Forms.Label();
            this.alicePrivate = new System.Windows.Forms.TextBox();
            this.alicePublic = new System.Windows.Forms.TextBox();
            this.Alicesharedkey = new System.Windows.Forms.TextBox();
            this.bobPrivate = new System.Windows.Forms.TextBox();
            this.bobPublic = new System.Windows.Forms.TextBox();
            this.bobShared = new System.Windows.Forms.TextBox();
            this.txtgenerator = new System.Windows.Forms.TextBox();
            this.Encrypt = new System.Windows.Forms.Button();
            this.txtEncrypted = new System.Windows.Forms.TextBox();
            this.richTextBox3 = new System.Windows.Forms.TextBox();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.richCipherText = new System.Windows.Forms.RichTextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.prime = new System.Windows.Forms.Label();
            this.Primetxt = new System.Windows.Forms.TextBox();
            this.qTxt = new System.Windows.Forms.TextBox();
            this.btn_keys = new System.Windows.Forms.Button();
            this.DiffieHellman = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.secretKeyAlice = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.secretkeyBob = new System.Windows.Forms.TextBox();
            this.BBS = new System.Windows.Forms.Button();
            this.btnDecrypt = new System.Windows.Forms.Button();
            this.CyclicGruopNum = new System.Windows.Forms.RichTextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // alice
            // 
            this.alice.AutoSize = true;
            this.alice.Location = new System.Drawing.Point(344, 239);
            this.alice.Name = "alice";
            this.alice.Size = new System.Drawing.Size(38, 17);
            this.alice.TabIndex = 2;
            this.alice.Text = "Alice";
            // 
            // bob
            // 
            this.bob.AutoSize = true;
            this.bob.Location = new System.Drawing.Point(740, 249);
            this.bob.Name = "bob";
            this.bob.Size = new System.Drawing.Size(33, 17);
            this.bob.TabIndex = 3;
            this.bob.Text = "Bob";
            // 
            // cyclic_group
            // 
            this.cyclic_group.AutoSize = true;
            this.cyclic_group.Location = new System.Drawing.Point(162, 82);
            this.cyclic_group.Name = "cyclic_group";
            this.cyclic_group.Size = new System.Drawing.Size(88, 17);
            this.cyclic_group.TabIndex = 4;
            this.cyclic_group.Text = "Cyclic Group";
            // 
            // generator
            // 
            this.generator.AutoSize = true;
            this.generator.Location = new System.Drawing.Point(266, 164);
            this.generator.Name = "generator";
            this.generator.Size = new System.Drawing.Size(73, 17);
            this.generator.TabIndex = 6;
            this.generator.Text = "Generator";
            // 
            // privateAlice
            // 
            this.privateAlice.AutoSize = true;
            this.privateAlice.Location = new System.Drawing.Point(262, 271);
            this.privateAlice.Name = "privateAlice";
            this.privateAlice.Size = new System.Drawing.Size(76, 17);
            this.privateAlice.TabIndex = 8;
            this.privateAlice.Text = "PrivateKey";
            // 
            // publickey
            // 
            this.publickey.AutoSize = true;
            this.publickey.Location = new System.Drawing.Point(262, 314);
            this.publickey.Name = "publickey";
            this.publickey.Size = new System.Drawing.Size(70, 17);
            this.publickey.TabIndex = 9;
            this.publickey.Text = "PublicKey";
            // 
            // privateBob
            // 
            this.privateBob.AutoSize = true;
            this.privateBob.Location = new System.Drawing.Point(665, 279);
            this.privateBob.Name = "privateBob";
            this.privateBob.Size = new System.Drawing.Size(80, 17);
            this.privateBob.TabIndex = 10;
            this.privateBob.Text = "Private Key";
            // 
            // publicBob
            // 
            this.publicBob.AutoSize = true;
            this.publicBob.Location = new System.Drawing.Point(671, 322);
            this.publicBob.Name = "publicBob";
            this.publicBob.Size = new System.Drawing.Size(74, 17);
            this.publicBob.TabIndex = 11;
            this.publicBob.Text = "Public Key";
            // 
            // aliceLable
            // 
            this.aliceLable.AutoSize = true;
            this.aliceLable.Location = new System.Drawing.Point(126, 415);
            this.aliceLable.Name = "aliceLable";
            this.aliceLable.Size = new System.Drawing.Size(116, 17);
            this.aliceLable.TabIndex = 12;
            this.aliceLable.Text = "Alice Shared Key";
            // 
            // lableBob
            // 
            this.lableBob.AutoSize = true;
            this.lableBob.Location = new System.Drawing.Point(620, 415);
            this.lableBob.Name = "lableBob";
            this.lableBob.Size = new System.Drawing.Size(111, 17);
            this.lableBob.TabIndex = 13;
            this.lableBob.Text = "Bob Shared Key";
            // 
            // alicePrivate
            // 
            this.alicePrivate.Location = new System.Drawing.Point(376, 271);
            this.alicePrivate.Name = "alicePrivate";
            this.alicePrivate.Size = new System.Drawing.Size(100, 22);
            this.alicePrivate.TabIndex = 14;
            // 
            // alicePublic
            // 
            this.alicePublic.Location = new System.Drawing.Point(376, 314);
            this.alicePublic.Name = "alicePublic";
            this.alicePublic.Size = new System.Drawing.Size(100, 22);
            this.alicePublic.TabIndex = 15;
            // 
            // Alicesharedkey
            // 
            this.Alicesharedkey.Location = new System.Drawing.Point(214, 412);
            this.Alicesharedkey.Name = "Alicesharedkey";
            this.Alicesharedkey.Size = new System.Drawing.Size(199, 22);
            this.Alicesharedkey.TabIndex = 16;
            // 
            // bobPrivate
            // 
            this.bobPrivate.Location = new System.Drawing.Point(764, 279);
            this.bobPrivate.Name = "bobPrivate";
            this.bobPrivate.Size = new System.Drawing.Size(100, 22);
            this.bobPrivate.TabIndex = 17;
            // 
            // bobPublic
            // 
            this.bobPublic.Location = new System.Drawing.Point(764, 317);
            this.bobPublic.Name = "bobPublic";
            this.bobPublic.Size = new System.Drawing.Size(100, 22);
            this.bobPublic.TabIndex = 18;
            // 
            // bobShared
            // 
            this.bobShared.Location = new System.Drawing.Point(737, 412);
            this.bobShared.Name = "bobShared";
            this.bobShared.Size = new System.Drawing.Size(260, 22);
            this.bobShared.TabIndex = 19;
            // 
            // txtgenerator
            // 
            this.txtgenerator.Location = new System.Drawing.Point(348, 161);
            this.txtgenerator.Name = "txtgenerator";
            this.txtgenerator.Size = new System.Drawing.Size(145, 22);
            this.txtgenerator.TabIndex = 7;
            // 
            // Encrypt
            // 
            this.Encrypt.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.Encrypt.Location = new System.Drawing.Point(257, 697);
            this.Encrypt.Name = "Encrypt";
            this.Encrypt.Size = new System.Drawing.Size(75, 38);
            this.Encrypt.TabIndex = 21;
            this.Encrypt.Text = "Encrypt";
            this.Encrypt.UseVisualStyleBackColor = false;
            this.Encrypt.Click += new System.EventHandler(this.Encrypt_Click_1);
            // 
            // txtEncrypted
            // 
            this.txtEncrypted.Location = new System.Drawing.Point(58, 794);
            this.txtEncrypted.Multiline = true;
            this.txtEncrypted.Name = "txtEncrypted";
            this.txtEncrypted.Size = new System.Drawing.Size(486, 245);
            this.txtEncrypted.TabIndex = 23;
            // 
            // richTextBox3
            // 
            this.richTextBox3.Location = new System.Drawing.Point(623, 794);
            this.richTextBox3.Multiline = true;
            this.richTextBox3.Name = "richTextBox3";
            this.richTextBox3.Size = new System.Drawing.Size(506, 245);
            this.richTextBox3.TabIndex = 24;
            // 
            // richTextBox1
            // 
            this.richTextBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.richTextBox1.Location = new System.Drawing.Point(58, 595);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(486, 96);
            this.richTextBox1.TabIndex = 25;
            this.richTextBox1.Text = "";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(253, 575);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(73, 17);
            this.label1.TabIndex = 26;
            this.label1.Text = "Alice Input";
            // 
            // richCipherText
            // 
            this.richCipherText.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.richCipherText.Location = new System.Drawing.Point(623, 595);
            this.richCipherText.Name = "richCipherText";
            this.richCipherText.Size = new System.Drawing.Size(506, 96);
            this.richCipherText.TabIndex = 27;
            this.richCipherText.Text = "";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(814, 575);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(72, 17);
            this.label2.TabIndex = 28;
            this.label2.Text = "BOB input";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(459, 12);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(149, 32);
            this.button1.TabIndex = 29;
            this.button1.Text = "Start";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(734, 161);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(16, 17);
            this.label3.TabIndex = 30;
            this.label3.Text = "q";
            // 
            // prime
            // 
            this.prime.AutoSize = true;
            this.prime.Location = new System.Drawing.Point(567, 164);
            this.prime.Name = "prime";
            this.prime.Size = new System.Drawing.Size(17, 17);
            this.prime.TabIndex = 31;
            this.prime.Text = "P";
            // 
            // Primetxt
            // 
            this.Primetxt.Location = new System.Drawing.Point(582, 161);
            this.Primetxt.Name = "Primetxt";
            this.Primetxt.Size = new System.Drawing.Size(57, 22);
            this.Primetxt.TabIndex = 32;
            // 
            // qTxt
            // 
            this.qTxt.Location = new System.Drawing.Point(747, 161);
            this.qTxt.Name = "qTxt";
            this.qTxt.Size = new System.Drawing.Size(77, 22);
            this.qTxt.TabIndex = 33;
            // 
            // btn_keys
            // 
            this.btn_keys.Location = new System.Drawing.Point(376, 189);
            this.btn_keys.Name = "btn_keys";
            this.btn_keys.Size = new System.Drawing.Size(344, 35);
            this.btn_keys.TabIndex = 34;
            this.btn_keys.Text = "Generate Keys for Alice and BOB";
            this.btn_keys.UseVisualStyleBackColor = true;
            this.btn_keys.Click += new System.EventHandler(this.btn_keys_Click);
            // 
            // DiffieHellman
            // 
            this.DiffieHellman.Location = new System.Drawing.Point(376, 362);
            this.DiffieHellman.Name = "DiffieHellman";
            this.DiffieHellman.Size = new System.Drawing.Size(344, 36);
            this.DiffieHellman.TabIndex = 36;
            this.DiffieHellman.Text = "Generate Shared Keys by Diffie-Hellman";
            this.DiffieHellman.UseVisualStyleBackColor = true;
            this.DiffieHellman.Click += new System.EventHandler(this.DiffieHellman_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(23, 531);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(107, 17);
            this.label4.TabIndex = 38;
            this.label4.Text = "Alice SecretKey";
            // 
            // secretKeyAlice
            // 
            this.secretKeyAlice.Location = new System.Drawing.Point(136, 531);
            this.secretKeyAlice.Name = "secretKeyAlice";
            this.secretKeyAlice.Size = new System.Drawing.Size(426, 22);
            this.secretKeyAlice.TabIndex = 39;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(589, 533);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(102, 17);
            this.label5.TabIndex = 40;
            this.label5.Text = "Bob SecretKey";
            // 
            // secretkeyBob
            // 
            this.secretkeyBob.Location = new System.Drawing.Point(697, 533);
            this.secretkeyBob.Name = "secretkeyBob";
            this.secretkeyBob.Size = new System.Drawing.Size(448, 22);
            this.secretkeyBob.TabIndex = 41;
            // 
            // BBS
            // 
            this.BBS.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.BBS.Location = new System.Drawing.Point(376, 485);
            this.BBS.Name = "BBS";
            this.BBS.Size = new System.Drawing.Size(344, 40);
            this.BBS.TabIndex = 42;
            this.BBS.Text = "Generate Secret Key by BBS";
            this.BBS.UseVisualStyleBackColor = false;
            this.BBS.Click += new System.EventHandler(this.BBS_Click);
            // 
            // btnDecrypt
            // 
            this.btnDecrypt.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.btnDecrypt.Location = new System.Drawing.Point(806, 697);
            this.btnDecrypt.Name = "btnDecrypt";
            this.btnDecrypt.Size = new System.Drawing.Size(80, 38);
            this.btnDecrypt.TabIndex = 43;
            this.btnDecrypt.Text = "Decrypt";
            this.btnDecrypt.UseVisualStyleBackColor = false;
            this.btnDecrypt.Click += new System.EventHandler(this.btnDecrypt_Click);
            // 
            // CyclicGruopNum
            // 
            this.CyclicGruopNum.BackColor = System.Drawing.SystemColors.Info;
            this.CyclicGruopNum.Location = new System.Drawing.Point(256, 50);
            this.CyclicGruopNum.Name = "CyclicGruopNum";
            this.CyclicGruopNum.Size = new System.Drawing.Size(630, 96);
            this.CyclicGruopNum.TabIndex = 44;
            this.CyclicGruopNum.Text = "";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(246, 766);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(80, 17);
            this.label6.TabIndex = 45;
            this.label6.Text = "Enc(INa, K)";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(761, 767);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(170, 17);
            this.label7.TabIndex = 46;
            this.label7.Text = "Dec(Enc(INa, K), K) = INA";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1157, 1055);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.CyclicGruopNum);
            this.Controls.Add(this.btnDecrypt);
            this.Controls.Add(this.BBS);
            this.Controls.Add(this.secretkeyBob);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.secretKeyAlice);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.DiffieHellman);
            this.Controls.Add(this.btn_keys);
            this.Controls.Add(this.qTxt);
            this.Controls.Add(this.Primetxt);
            this.Controls.Add(this.prime);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.richCipherText);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.richTextBox1);
            this.Controls.Add(this.richTextBox3);
            this.Controls.Add(this.txtEncrypted);
            this.Controls.Add(this.Encrypt);
            this.Controls.Add(this.bobShared);
            this.Controls.Add(this.bobPublic);
            this.Controls.Add(this.bobPrivate);
            this.Controls.Add(this.Alicesharedkey);
            this.Controls.Add(this.alicePublic);
            this.Controls.Add(this.alicePrivate);
            this.Controls.Add(this.lableBob);
            this.Controls.Add(this.aliceLable);
            this.Controls.Add(this.publicBob);
            this.Controls.Add(this.privateBob);
            this.Controls.Add(this.publickey);
            this.Controls.Add(this.privateAlice);
            this.Controls.Add(this.txtgenerator);
            this.Controls.Add(this.generator);
            this.Controls.Add(this.cyclic_group);
            this.Controls.Add(this.bob);
            this.Controls.Add(this.alice);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label alice;
        private System.Windows.Forms.Label bob;
        private System.Windows.Forms.Label cyclic_group;
        private System.Windows.Forms.Label generator;
        private System.Windows.Forms.Label privateAlice;
        private System.Windows.Forms.Label publickey;
        private System.Windows.Forms.Label privateBob;
        private System.Windows.Forms.Label publicBob;
        private System.Windows.Forms.Label aliceLable;
        private System.Windows.Forms.Label lableBob;
        private System.Windows.Forms.TextBox alicePrivate;
        private System.Windows.Forms.TextBox alicePublic;
        private System.Windows.Forms.TextBox Alicesharedkey;
        private System.Windows.Forms.TextBox bobPrivate;
        private System.Windows.Forms.TextBox bobPublic;
        private System.Windows.Forms.TextBox bobShared;
        private System.Windows.Forms.TextBox txtgenerator;
        private System.Windows.Forms.Button Encrypt;
        private System.Windows.Forms.TextBox richTextBox3;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RichTextBox richCipherText;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label prime;
        private System.Windows.Forms.TextBox Primetxt;
        private System.Windows.Forms.TextBox qTxt;
        private System.Windows.Forms.Button btn_keys;
        private System.Windows.Forms.Button DiffieHellman;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox secretKeyAlice;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox secretkeyBob;
        private System.Windows.Forms.Button BBS;
        public System.Windows.Forms.TextBox txtEncrypted;
        private System.Windows.Forms.Button btnDecrypt;
        private System.Windows.Forms.RichTextBox CyclicGruopNum;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
    }
}


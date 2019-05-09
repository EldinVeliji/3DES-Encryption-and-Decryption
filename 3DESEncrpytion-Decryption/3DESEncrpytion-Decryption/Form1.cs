using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Security.Cryptography;
namespace _3DESEncrpytion_Decryption
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        byte[] encrypted;
        private void button1_Click(object sender, EventArgs e)
        {
            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            UTF8Encoding utf8 = new UTF8Encoding();
            TripleDESCryptoServiceProvider tDES = new TripleDESCryptoServiceProvider();
            tDES.Key = md5.ComputeHash(utf8.GetBytes(textBox1.Text));
            tDES.Mode = CipherMode.ECB;
            tDES.Padding = PaddingMode.PKCS7;
            ICryptoTransform trans = tDES.CreateEncryptor();
            encrypted = trans.TransformFinalBlock(utf8.GetBytes(textBox2.Text), 0, utf8.GetBytes(textBox2.Text).Length);
            textBox3.Text = BitConverter.ToString(encrypted);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            UTF8Encoding utf8 = new UTF8Encoding();
            TripleDESCryptoServiceProvider tDES = new TripleDESCryptoServiceProvider();
            tDES.Key = md5.ComputeHash(utf8.GetBytes(textBox4.Text));
            tDES.Mode = CipherMode.ECB;
            tDES.Padding = PaddingMode.PKCS7;
            ICryptoTransform trans = tDES.CreateDecryptor();
            textBox5.Text = utf8.GetString(trans.TransformFinalBlock(encrypted, 0, encrypted.Length));
        }
    }
}

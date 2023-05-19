using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            PasswordTextBox.PasswordChar = '*';
            //string filePath = "A:\\Development of decentralized applications\\MD5_HashFunction\\MD5_HashFunction\\user.txt";
            
        }

        string path = "C:\\politex\\DDA\\Lab_1\\keys\\users.txt";


        private void Form1_Load(object sender, EventArgs e)
        {

        }

        

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {

        }

        private void button_Login_Click(object sender, EventArgs e)
        {
       
        }

        private void Login_Click(object sender, EventArgs e)
        {
            try
            {
                if ((PasswordTextBox.Text.Length == 0 && PasswordTextBox.Text.Length == 0) || (PasswordTextBox.Text.Length == 0 || PasswordTextBox.Text.Length == 0))
                {
                    MessageBox.Show("No data", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    if (ValidateUser(path, false))
                    {
                        MessageBox.Show("Load", "Congratulation!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Some error", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Register_Click(object sender, EventArgs e)
        {
            try
            {
                if ((PasswordTextBox.Text.Length == 0 && LoginTextBox.Text.Length == 0) || (LoginTextBox.Text.Length == 0 || PasswordTextBox.Text.Length == 0))
                {
                    MessageBox.Show("Enter valid data", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);

                } else
                {
                    if (!ValidateUser(path, true))
                    {
                        AddUser(LoginTextBox.Text, GetMd5Hash(PasswordTextBox.Text), path);
                        MessageBox.Show("Wow!", "Congratulation!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoginTextBox.Clear();
                        PasswordTextBox.Clear();
                    }
                    else
                    {
                        MessageBox.Show("Some Error!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public bool ValidateUser(string filePath, bool register) //workmode: true if we check only name or false if we check name and password
        {
            FileStream readFileStream = new FileStream(filePath, FileMode.Open);
            StreamReader streamReader = new StreamReader(readFileStream);
            bool validate = false;

            try
            {
                while (!streamReader.EndOfStream)
                {
                    string line = streamReader.ReadLine();

                    if (validate == false)
                    {
                        if (line != null)
                        {
                            var parts = line.Split(';');
                            if (!register)
                            {
                                if (LoginTextBox.Text == parts[0] && GetMd5Hash(PasswordTextBox.Text) == parts[1])
                                    validate = true;
                            }
                            else
                            {
                                if (LoginTextBox.Text == parts[0])
                                    validate = true;
                            }
                        }
                    }
                    else
                    {
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                streamReader.Close();
                readFileStream.Close();
            }
            return validate;
        }

        public string GetMd5Hash(string password)
        {
            using (var md5 = System.Security.Cryptography.MD5.Create())
            {
                var inputBytes = Encoding.ASCII.GetBytes(password);
                var hashBytes = md5.ComputeHash(inputBytes);
                //var hash = Convert.ToHexString(hashBytes);
                //var hash = BitConverter.ToString(hashBytes).Replace("-", "").ToLower();

                // Convert the byte array to hexadecimal string prior to .NET 5
                StringBuilder sb = new System.Text.StringBuilder();
                for (int i = 0; i < hashBytes.Length; i++)
                {
                     sb.Append(hashBytes[i].ToString("X2"));
                 }
                var hash = sb.ToString();
                return hash;
            }
        }

        public void AddUser(string userName, string password, string filePath)
        {
            FileStream fileStream = new FileStream(filePath, FileMode.Append);
            StreamWriter streamWriter = new StreamWriter(fileStream);

            try
            {
                streamWriter.WriteLine($"{LoginTextBox.Text};{GetMd5Hash(PasswordTextBox.Text)}");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                streamWriter.Close();
                fileStream.Close();
            }
        }

                private void PasswordTextBox_TextChanged(object sender, EventArgs e)
        {

        }
    }
}

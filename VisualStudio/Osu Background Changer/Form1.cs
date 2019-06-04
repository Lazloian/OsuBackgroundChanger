using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;

namespace Osu_Background_Changer
{
    public partial class Form1 : Form
    {

        string songs_folder = @"D:\WeebShit\Songs";
        string backup_folder = @"C:\Users\Henry\Desktop\Backup";
        string backgrounds_folder = @"D:\Media\Backgrounds";

        string[] songs;
        string[] backgrounds;

        public Form1()
        {
            InitializeComponent();
        }

        private void TextBox1_TextChanged(object sender, EventArgs e)
        {
            songs_folder = @textBox1.Text;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void TextBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void TextBox4_TextChanged(object sender, EventArgs e)
        {
            backup_folder = @textBox4.Text;
        }

        private void TextBox6_TextChanged(object sender, EventArgs e)
        {
            backgrounds_folder = @textBox6.Text;
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            songs = Directory.GetDirectories(songs_folder, "*");

            foreach (string song in songs)
            {
                Directory.CreateDirectory(backup_folder + @"\" + new DirectoryInfo(song).Name);

                string[] names = Get_BG_Names(song);

                foreach (string name in names)
                {
                    //Console.WriteLine(name);

                    string[] bg = Directory.GetFiles(song, name);

                    foreach (string picture in bg)
                    {
                        File.Copy(picture, backup_folder + @"\" + new DirectoryInfo(song).Name + @"\" + name, true);
                    }
                }
            }
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            songs = Directory.GetDirectories(songs_folder, "*");
            backgrounds = Directory.GetFiles(backgrounds_folder, "*");

            Random rand = new Random();

            foreach (string song in songs)
            {

                string[] names = Get_BG_Names(song);

                foreach (string name in names)
                {
                    //Console.WriteLine(name);

                    string[] bg = Directory.GetFiles(song, name);

                    foreach (string picture in bg)
                    {
                        File.Copy(backgrounds[rand.Next(backgrounds.Length)], song + @"\" + name, true);
                    }
                }
            }
        }

        private string[] Get_BG_Names(string song)
        {
            List<string> names = new List<string>();
            string[] osu_files = Directory.GetFiles(song, "*.osu");

            //Console.WriteLine(song);

            foreach (string file in osu_files)
            {
                string[] lines = File.ReadAllLines(file);

                foreach (string line in lines)
                {
                    if (line.Length > 5 && line.Substring(0,5).Equals("0,0,\""))
                    {
                        int firstq = line.IndexOf("\"");
                        int lastq = line.LastIndexOf("\"");

                        string current_name = line.Substring(firstq + 1, lastq - firstq - 1);

                        bool is_unique = true;

                        if (names.Count != 0)
                        {
                            foreach (string name in names)
                            {
                                if (name.Equals(current_name))
                                {
                                    is_unique = false;
                                }
                            }

                            if (is_unique)
                            {
                                names.Add(current_name);
                            }
                        }
                        else
                        {
                            names.Add(current_name);
                        }
                    }
                }
            }
            return names.ToArray();
        }
    }
}

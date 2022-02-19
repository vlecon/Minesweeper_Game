using Game_Mines.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Game_Mines
{
    public partial class Form1 : Form
    {
        Dictionary<Button, Element> _dictionary = new Dictionary<Button, Element>();

        public Form1()
        {
            InitializeComponent();

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.Width = Options.MapElementWidth * (Options.MapWidthCount + 1);
            this.Height = Options.MapElementHeight * (Options.MapHeightCount + 2);

            var panel = new Panel();
            panel.Dock = DockStyle.Fill;
            for (int i = 0; i < Options.MapHeightCount; i++)
            {
                for (int j = 0; j < Options.MapWidthCount; j++)
                {
                    var btn = new Button() { Width = Options.MapElementWidth, Height = Options.MapElementHeight, Left = i * Options.MapElementWidth, Top = j * Options.MapElementHeight };
                    btn.MouseDown += Btn_Click;
                    var element = new Element() { X = i, Y = j };
                    //if (element.IsBomb) btn.Text = "b";
                    _dictionary[btn] = element;

                    panel.Controls.Add(btn);
                }
            }
            this.Controls.Add(panel);
        }

        private void Btn_Click(object sender, MouseEventArgs e)
        {
            var btn = (Button)sender;
            var element = _dictionary[btn];

            if (e.Button == MouseButtons.Left)
            {
                if (element.IsBomb)
                {
                    MessageBox.Show("Bomb !!!");

                    element.IsShow = true;
                    btn.Image = Properties.Resources.bomb;

                    OpenElement();
                }
                else

                {
                    btn.Image = null;
                    Calculate(element);

                }
            }
            else if (e.Button == MouseButtons.Right && !element.IsShow)
            {
                if (element.IsMarkerAsBomb)
                {
                    btn.Image = null;
                    element.IsMarkerAsBomb = false;

                }
                else
                {
                    btn.Image = Properties.Resources.bomb;
                    element.IsMarkerAsBomb = true;
                }

            }
        }

        private void Calculate(Element element)
        {
            element.IsShow = true;
            var btn = _dictionary.Single(e => e.Value.X == element.X && e.Value.Y == element.Y).Key;
            var count = 0;
            foreach (var currElement in _dictionary.Values)
            {
                if (Math.Abs(currElement.X - element.X) <= 1 && Math.Abs(currElement.Y - element.Y) <= 1 && currElement.IsBomb) count++;

            }

            btn.Text = count.ToString();

            if (count == 0)
            {
                foreach (var currElement in _dictionary.Values)
                {
                    if (Math.Abs(currElement.X - element.X) <= 1 && Math.Abs(currElement.Y - element.Y) <= 1 && !currElement.IsShow)
                    {
                        Calculate(currElement);

                    }
                }
            }
        }

        private void OpenElement()
        {
            foreach (var item in _dictionary)
            {
                if (item.Value.IsBomb)
                {
                    item.Key.Image = Properties.Resources.bomb;
                }
                else
                {
                    Calculate(item.Value);
                }
                item.Value.IsShow = true;
                item.Key.Enabled= false;
            }
        }

        private void OpenElement()
        {
            foreach (var item in _dictionary)
            {
                if (item.Value.IsBomb)
                {
                    item.Key.Image = Properties.Resources.bomb;
                }
                else
                {
                    Calculate(item.Value);
                }
                item.Value.IsShow = true;
            }
        }
    }
}

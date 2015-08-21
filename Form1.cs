using Game28.Controls;
using System.Drawing;
using System.Windows.Forms;

namespace Game28
{
    public partial class Form1 : Form
    {
        const int size = 500;
        public Form1()
        {
            InitializeComponent();
            Clock c = new Clock();
            c.Location = new Point((this.Width - size) / 4, (this.Height - size) / 2);
            c.Size = new Size(size, size);
            c.BackColor = Color.Aqua;

            this.panel1.Controls.Add(c);

            Ring r = new Ring();
            r.Location = new Point(((this.Width - 100) / 2), (this.Height - size) / 2);
            r.Size = new Size(size, size);
            this.panel1.Controls.Add(r);
        }
    }
}

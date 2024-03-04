using System.Runtime.InteropServices;
using System.Text;
using orcToDh.Calculators;

namespace orcToDh
{
    public partial class mainPage : Form
    {
        OfsetFile ofset;


        [DllImport("kernel32.dll", SetLastError = true)]
        static extern bool AllocConsole();
        public const string status = "Status: ";

        public mainPage()
        {
            InitializeComponent();
#if DEBUG
            AllocConsole();
#endif
            statusLable.Text = status + "Indl�s fil for at forts�tte";
            ofset = null;
        }

        private void openFileButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "ofset files (*.off)|*.off";
            openFileDialog.Title = "Select an ofset file";
            if (openFileDialog.ShowDialog() != DialogResult.OK)
            {
                return;
            }
            string ofsetFile = openFileDialog.FileName;

            using (StreamReader file = new(ofsetFile, new ASCIIEncoding()))
            {
                ofset = new OfsetFile(file);
            }
            statusLable.Text = status + "Fil indl�st, klar til beregning";
            bMAXButton.Enabled = true;
        }

        private void bMAXButton_Click(object sender, EventArgs e)
        {
            BMax bMax = new(ofset);
            bMax.ShowDialog();
        }
    }
}

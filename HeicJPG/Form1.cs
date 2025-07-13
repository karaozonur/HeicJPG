using ImageMagick;

namespace HeicJPG
{
    public partial class Form1 : Form
    {
        private string[] selectedFiles;
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog
            {
                Multiselect = true,
                Filter = "HEIC files (*.heic)|*.heic",
                Title = "HEIC Dosyalar�n� Se�"
            };

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                selectedFiles = ofd.FileNames;
                MessageBox.Show($"{selectedFiles.Length} dosya se�ildi.");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (selectedFiles == null || selectedFiles.Length == 0)
            {
                MessageBox.Show("L�tfen �nce dosya se�in.");
                return;
            }

            FolderBrowserDialog fbd = new FolderBrowserDialog();
            if (fbd.ShowDialog() != DialogResult.OK)
                return;

            int count = 0;
            foreach (string file in selectedFiles)
            {
                try
                {
                    using (var image = new MagickImage(file))
                    {
                        image.Format = MagickFormat.Jpeg;
                        string fileName = Path.GetFileNameWithoutExtension(file);
                        string outputPath = Path.Combine(fbd.SelectedPath, fileName + ".jpg");
                        image.Write(outputPath);
                        count++;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Hata olu�tu: {Path.GetFileName(file)}\n{ex.Message}");
                }
            }

            MessageBox.Show($"{count} dosya ba�ar�yla JPG format�na d�n��t�r�ld�.");
        }

    }
    
}

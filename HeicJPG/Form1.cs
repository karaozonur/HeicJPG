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
                Title = "HEIC Dosyalarýný Seç"
            };

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                selectedFiles = ofd.FileNames;
                MessageBox.Show($"{selectedFiles.Length} dosya seçildi.");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (selectedFiles == null || selectedFiles.Length == 0)
            {
                MessageBox.Show("Lütfen önce dosya seçin.");
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
                    MessageBox.Show($"Hata oluþtu: {Path.GetFileName(file)}\n{ex.Message}");
                }
            }

            MessageBox.Show($"{count} dosya baþarýyla JPG formatýna dönüþtürüldü.");
        }

    }
    
}

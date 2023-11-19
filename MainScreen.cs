namespace CG_Lab4
{
    public partial class MainScreen : Form
    {
        Graphics graphics;
        Bitmap bitmap;

        public MainScreen()
        {
            InitializeComponent();

            Rectangle rectangle = pictureBox1.ClientRectangle;
            bitmap = new Bitmap(rectangle.Width, rectangle.Height);
            graphics = Graphics.FromImage(bitmap);
        }
    }
}
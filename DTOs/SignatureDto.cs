namespace WiiTrakClient.DTOs
{
    public class SignatureDto
    {
        public string SignObject { get; set; }
        public int SignzIndex { get; set; }
        public int PenSize { get; set; }
        public string PenColor { get; set; }
        public string PenCursor { get; set; }
        public string StartMessage { get; set; }
        public string ErrorMessage { get; set; }
        public string SuccessMessage { get; set; }
        public string BackGroundImage { get; set; }
        public string RefreshImage { get; set; }

        public int SignWidth { get; set; }
        public int SignHeight { get; set; }
        public int RequiredPoints { get; set; }
        public string BackColor { get; set; }
        public string BorderColor { get; set; }
        public string BorderStyle { get; set; }
        public int BorderWidth { get; set; }
        public bool SmoothSign { get; set; }
        public bool TransparentSign { get; set; }
        public float ImageScaleFactor { get; set; }
        public string Enabled { get; set; }
        public string Visible { get; set; }
        public string Name { get; set; }
    }
}

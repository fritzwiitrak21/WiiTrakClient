/*
* 06.06.2022
* Copyright (c) 2022 WiiTrak, All Rights Reserved.
*/
namespace WiiTrakClient.DTOs
{
    public class SignatureDto
    {
        public string SignObject { get; set; }
        public int SignzIndex { get; set; }
        public int PenSize { get; set; }
        public string PenColor { get; set; } = string.Empty;
        public string PenCursor { get; set; } = string.Empty;
        public string StartMessage { get; set; } = string.Empty;
        public string ErrorMessage { get; set; } = string.Empty;
        public string SuccessMessage { get; set; } = string.Empty;
        public string BackGroundImage { get; set; } = string.Empty;
        public string RefreshImage { get; set; } = string.Empty;
        public int SignWidth { get; set; }
        public int SignHeight { get; set; }
        public int RequiredPoints { get; set; }
        public string BackColor { get; set; } = string.Empty;
        public string BorderColor { get; set; } = string.Empty;
        public string BorderStyle { get; set; } = string.Empty;
        public int BorderWidth { get; set; }
        public bool SmoothSign { get; set; }
        public bool TransparentSign { get; set; }
        public float ImageScaleFactor { get; set; }
        public string Enabled { get; set; } = string.Empty;
        public string Visible { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
    } 
}

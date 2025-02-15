var name = args.FirstOrDefault() ?? string.Empty;
if (string.IsNullOrWhiteSpace(name)) return;

if (System.IO.Directory.Exists(name))
    foreach (var file in System.IO.Directory.GetFiles(name))
        B(file);

if (System.IO.File.Exists(name))
    B(name);

static void B(string name)
{
    var extension = System.IO.Path.GetExtension(name);
    if (!new[] { ".jpg", ".bmp", ".png" }.Contains(extension)) return;

    var nameWithoutExtension = System.IO.Path.GetFileNameWithoutExtension(name);
    var directoryName = System.IO.Path.GetDirectoryName(name);
    var fileName = System.IO.Path.Combine(directoryName, $"_2_{nameWithoutExtension}{extension}");
    if (System.IO.File.Exists(fileName)) return;

    using var img = System.Drawing.Image.FromFile(name);
    using var bmp = new System.Drawing.Bitmap(img.Width + 2, img.Height + 2);
    bmp.SetResolution(bmp.Width, bmp.Height);
    ;
    using var g = System.Drawing.Graphics.FromImage(bmp);
    g.FillRectangle(new System.Drawing.SolidBrush(System.Drawing.Color.FromArgb(255, 127, 127, 127)), 0, 0, bmp.Width, bmp.Height);
    g.DrawImage(img, 1, 1, img.Width, img.Height);

    bmp.Save(fileName);
}